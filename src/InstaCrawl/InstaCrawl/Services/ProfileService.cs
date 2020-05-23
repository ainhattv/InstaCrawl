using InstaCrawl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using InstaCrawl.Settings;
using InstaCrawl.Helpers;

namespace InstaCrawl.Services
{
    public class ProfileService
    {
        private readonly IMongoCollection<Profile> _profiles;

        public ProfileService(IOptions<MongoDBSetting> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);

            _profiles = database.GetCollection<Profile>(settings.Value.ProfileCollection);
        }

        public async Task<IEnumerable<Profile>> GetAsync() =>
            await _profiles.Find(p => true).ToListAsync();

        public async Task<Profile> GetAsync(string id) =>
           await _profiles.Find<Profile>(p => p.Id == id).FirstOrDefaultAsync();

        public async Task<bool> IsExistProfile(string userName)
        {
            var total = await _profiles.CountDocumentsAsync<Profile>(p => p.UserName.ToUpper() == userName.ToUpper());

            if (total == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<Profile> CreateAsync(Profile profile)
        {
            profile.ProfileStatus = ProfileStatus.NEW;
            profile.CreatedDay = DateTime.UtcNow;
            profile.UpdatedDay = DateTime.UtcNow;

            await _profiles.InsertOneAsync(profile);
            return profile;
        }

        public async Task UpdateAsync(string id, Profile profileIn)
        {
            profileIn.UpdatedDay = DateTime.UtcNow;
            await _profiles.ReplaceOneAsync(p => p.Id == id, profileIn);
        }

        public async Task CrawlAllProfilesAsync()
        {
            var profiles = await _profiles.Find(p => p.ProfileStatus != ProfileStatus.INACTIVATED).ToListAsync();

            foreach (var profile in profiles)
            {
                await CrawlProfileImagesAsync(profile);
            }
        }

        public async Task CrawlProfileImagesAsync(Profile profile)
        {
            if (profile == null)
                throw new ArgumentNullException(nameof(profile));

            CrawlHelper.CrawlProfile(profile.UserName);

            profile.ProfileStatus = ProfileStatus.CRAWLED;

            await _profiles.ReplaceOneAsync(p => p.Id == profile.Id, profile);
        }

        public async Task CrawlProfileImagesAsync(string id)
        {
            var profile = await _profiles.Find<Profile>(p => p.Id == id).FirstOrDefaultAsync();

            if (profile == null)
                throw new Exception($"Cannot find profile with Id: {id}");

            CrawlHelper.CrawlProfile(profile.UserName);

            profile.ProfileStatus = ProfileStatus.CRAWLED;

            await _profiles.ReplaceOneAsync(p => p.Id == id, profile);
        }

        public async Task RemoveAsync(Profile profileIn) =>
            await _profiles.DeleteOneAsync(p => p.Id == profileIn.Id);

        public async Task RemoveAsync(string id) =>
            await _profiles.DeleteOneAsync(p => p.Id == id);
    }
}
