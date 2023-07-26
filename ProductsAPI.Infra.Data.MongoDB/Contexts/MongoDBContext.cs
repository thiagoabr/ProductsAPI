using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProductsAPI.Application.Models.Queries;
using ProductsAPI.Infra.Data.MongoDB.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAPI.Infra.Data.MongoDB.Contexts
{
    public class MongoDBContext
    {
        private readonly MongoDBSettings? _mongoDBSettings;
        private IMongoDatabase? _mongoDatabase;

        public MongoDBContext(IOptions<MongoDBSettings>? mongoDBSettings)
        {
            _mongoDBSettings = mongoDBSettings.Value;

            #region Conexão com o banco de dados

            var client = MongoClientSettings.FromUrl(new MongoUrl(_mongoDBSettings.Host));
            if (_mongoDBSettings.IsSSL)
                client.SslSettings = new SslSettings
                {
                    EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12
                };

            _mongoDatabase = new MongoClient(client).GetDatabase(_mongoDBSettings.Name);

            #endregion
        }

        public IMongoCollection<ProductsDTO> Products => _mongoDatabase?.GetCollection<ProductsDTO>("Products");
    }
}
