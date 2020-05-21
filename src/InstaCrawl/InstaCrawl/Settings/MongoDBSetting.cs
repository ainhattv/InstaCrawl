using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaCrawl.Settings
{
    public class MongoDBSetting
    {
        public string ProfileCollection { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
