using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop_API.BookShopServices.AppConfig
{
    public class AppConfiguration
    {
        private readonly string _connectionString = string.Empty;
        private readonly string _jwt_key = string.Empty;


        public AppConfiguration()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            var root = configurationBuilder.Build();

            _connectionString = root.GetSection("ConnectionString").GetSection("DefaultConnectionString").Value;
            _jwt_key = root.GetSection("ApplicationSettings").GetSection("JWT_Secret").Value;
        }

        public string ConnectionString
        {
            get => _connectionString;
        }
        public string JwtSecret
        {
            get => _jwt_key;
        }
    }
}
