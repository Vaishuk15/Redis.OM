using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Redis.OM.Modeling;

namespace RedisRepository.JsonModel
{
    [Document(IndexName = "Password-idx",StorageType = StorageType.Json)]
    public class Password
    {
        [RedisIdField] // This will be the primary key (ID) in Redis
        [Indexed]      // Allows indexing for faster querying
        public int Id { get; set; }
        [Indexed]
        public string? Category { get; set; }
        public string App { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string EncryptedPassword { get; set; } = null!;
    }
}
