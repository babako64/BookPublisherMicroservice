
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Search.API.Data;
using Search.API.Data.Interfaces;
using Search.API.Repositories;
using Search.API.Repositories.Interfaces;
using Search.API.Settings;

namespace Search.API.Extensions
{
    public static class MongoExtension
    {
        public static void AddMongoDb(this IServiceCollection service, IConfiguration configuration)
        {
            service.Configure<SearchDatabaseSetting>(configuration.GetSection(nameof(SearchDatabaseSetting)));
            service.AddSingleton<ISearchDatabaseSetting>(sp => sp.GetRequiredService<IOptions<SearchDatabaseSetting>>().Value);

            service.AddSingleton<IMongoClient>(sp =>
            {
                var dbSettings = sp.GetService<ISearchDatabaseSetting>();

                return new MongoClient(dbSettings.ConnectionString);
            });

            service.AddSingleton<IMongoDatabase>(sp =>
            {
                var dbSettings = sp.GetService<ISearchDatabaseSetting>();
                var client = sp.GetService<IMongoClient>();

                return client.GetDatabase(dbSettings.DatabaseName);
            });

            service.AddTransient<ISearchContext, SearchContext>();
            service.AddTransient<IBookRepository, BookRepository>();
        }
    }
}
