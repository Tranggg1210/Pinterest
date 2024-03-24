using Nest;
using PixelPalette.Entities;

namespace PixelPalette.Extensions
{
    public static class ElasticSearchExtensions
    {
        public static void AddElasticSearch(
            this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration["ELKConfiguration:Uri"];
            var defaultIndex = configuration["ELKConfiguration:index"];

            var settings = new ConnectionSettings(new Uri(url))
                .PrettyJson()
                .DefaultIndex(defaultIndex);
            AddDefaultMappings(settings);

            var client = new ElasticClient(settings);
            services.AddSingleton<IElasticClient>(client);
            CreateIndex(client, defaultIndex);
        }
        public static void AddDefaultMappings(ConnectionSettings settings)
        {
            settings.DefaultMappingFor<User>(u =>
                u.Ignore(x => x.Id)
            );
        }
        private static void CreateIndex(IElasticClient client, string indexName)
        {
            client.Indices.Create(indexName, i => i.Map<User>(x => x.AutoMap()));
        }
    }
}
