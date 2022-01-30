using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Search.API.Settings
{
    public class SearchDatabaseSetting : ISearchDatabaseSetting
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}
