using StackExchange.Redis;

namespace MultiShop.Basket.Settings
{
    public class RedisService
    {
        public string _host { get; set; } // Redis sunucusunun adresi
        public int _port { get; set; } // Redis sunucusunun portu
        private ConnectionMultiplexer _connectionMultiplexer;
        public RedisService(string host, int port)
        {
            _host = host;
            _port = port;
        }

        public void Connect() =>_connectionMultiplexer= ConnectionMultiplexer.Connect($"{_host}:{_port}"); // Redis sunucusuna bağlantı kurar
        public IDatabase GetDB(int db=1)=>_connectionMultiplexer.GetDatabase(0); // Belirtilen veritabanını alır, varsayılan olarak 1. veritabanı kullanılır

    }
}
