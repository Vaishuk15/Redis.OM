using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using Redis.OM;
using Redis.OM.Contracts;
using Redis.OM.Searching;
using RedisRepository.Interface;
using RedisRepository.JsonModel;
using StackExchange.Redis;

namespace RedisRepository.Implementation
{
    public class RedisCache<T> : IRedisCache<T> where T : class
    {
        private readonly RedisConnectionProvider _provider;
        private readonly IDatabase _cacheDb;
        private readonly IRedisCollection<Password> _passwordRedisCollection;
        private IMapper _mapper;
        public RedisCache(RedisConnectionProvider provider, IConnectionMultiplexer connection, IMapper mapper)
        {
            _provider = provider;
            _cacheDb = connection.GetDatabase();
            _passwordRedisCollection = (RedisCollection<Password>)provider.RedisCollection<Password>();
            _mapper=mapper;
        }

        public async Task<T> GetAsync(string key)
        {
            //if (key==null) throw new ArgumentNullException(nameof(key));
            //var password=  await _passwordRedisCollection.Where(x => x.Id == key).FirstAsync();
            //return password as T;
            return await _provider.Connection.JsonGetAsync<T>(key);
        }

        public async Task SetAsync(string key, T value, TimeSpan? expiry = null)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));
            if (value == null) throw new ArgumentNullException(nameof(value));

            try
            {
                // Serialize the object to JSON string with custom settings (e.g., handling DateTime)
                string jsonData = JsonConvert.SerializeObject(value, new JsonSerializerSettings
                {
                    // Handle special cases like dates, circular references, etc.
                    DateFormatString = "yyyy-MM-ddTHH:mm:ssZ", // Customize DateTime format if needed
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore // Ignore circular references
                });

                // Make sure the serialized JSON string is properly quoted for Redis
                string formattedJson = $"\'{jsonData}\'"; // Ensures proper quoting for Redis

                // Store the JSON data in Redis
                //await _provider.Connection.JsonSetAsync(key, "$", formattedJson);

                var data= _mapper.Map<Password>(value);
                await _passwordRedisCollection.InsertAsync(data);
                // Optionally set an expiry
                if (expiry.HasValue)
                {
                    await _cacheDb.KeyExpireAsync(key, expiry);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error serializing object: {ex.Message}");
                throw; // Rethrow the exception for further handling
            }
        }


        public async Task RemoveAsync(string key)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));
            await _provider.Connection.UnlinkAsync(key);
        }

        public async Task<bool> ExistsAsync(string key)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));
            return await _cacheDb.KeyExistsAsync(key);
        }

        public async Task<bool> CreateIndexAsync()
        {
            try
            {
                await _provider.Connection.CreateIndexAsync(typeof(T));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating index: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> IndexExistsAsync(string indexName)
        {
            var result = await _provider.Connection.ExecuteAsync("FT._LIST");
            var indexes = (result.ToArray()).Select(r => r.ToString()).ToList();
            return indexes.Contains(indexName);
        }
    }
}
