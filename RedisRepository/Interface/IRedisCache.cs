using System;
using System.Threading.Tasks;

namespace RedisRepository.Interface
{
    public interface IRedisCache<T> where T : class
    {
        /// <summary>
        /// Gets an object from the Redis cache by its key.
        /// </summary>
        /// <param name="key">The key of the object to retrieve.</param>
        /// <returns>The object if found; otherwise, null.</returns>
        Task<T> GetAsync(string key);

        /// <summary>
        /// Sets an object in the Redis cache with an optional expiration time.
        /// </summary>
        /// <param name="key">The key to store the object under.</param>
        /// <param name="value">The object to store.</param>
        /// <param name="expiry">The optional expiration time for the key.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task SetAsync(string key, T value, TimeSpan? expiry = null);

        /// <summary>
        /// Removes an object from the Redis cache by its key.
        /// </summary>
        /// <param name="key">The key of the object to remove.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task RemoveAsync(string key);

        /// <summary>
        /// Checks whether a key exists in the Redis cache.
        /// </summary>
        /// <param name="key">The key to check for existence.</param>
        /// <returns>True if the key exists; otherwise, false.</returns>
        Task<bool> ExistsAsync(string key);

        /// <summary>
        /// Creates an index for the given type in Redis.
        /// </summary>
        /// <returns>True if the index is created successfully; otherwise, false.</returns>
        Task<bool> CreateIndexAsync();

        /// <summary>
        /// Checks whether a specific index exists in Redis.
        /// </summary>
        /// <param name="indexName">The name of the index to check.</param>
        /// <returns>True if the index exists; otherwise, false.</returns>
        Task<bool> IndexExistsAsync(string indexName);
    }
}
