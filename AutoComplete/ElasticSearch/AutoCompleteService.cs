using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace Helper.ElasticSearch
{
    public static class AutoCompleteService
    {

//        readonly ElasticClient _elasticClient;

//        public AutoCompleteService(ConnectionSettings connectionSettings)
//        {
//            _elasticClient = new ElasticClient(connectionSettings);
//        }

//        public static void AddElasticsearch(
//            this IServiceCollection services, IConfiguration configuration)
//        {
//            var url = configuration["ELKConfiguration:url"];
//            var defaultIndex = configuration["ELKConfiguration:index"];

//            var settings = new ConnectionSettings(new Uri(url)).BasicAuthentication(userName, pass)
//                .PrettyJson()
//                .DefaultIndex(defaultIndex);

//            AddDefaultMappings(settings);

//            var client = new ElasticClient(settings);

//            services.AddSingleton<IElasticClient>(client);

//            CreateIndex(client, defaultIndex);
//        }

//        private static void AddDefaultMappings(ConnectionSettings settings)
//        {
//            settings
//                .DefaultMappingFor<Tags>(m => m
//                    .Ignore(p => p.Price)
//                    .Ignore(p => p.Measurement)
//                );
//        }

//        private static void CreateIndex(IElasticClient client, string indexName)
//        {
//            var createIndexResponse = client.Indices.Create(indexName,
//                index => index.Map<Tags>(x => x.AutoMap())
//            );
//        }
//    }

}
}
