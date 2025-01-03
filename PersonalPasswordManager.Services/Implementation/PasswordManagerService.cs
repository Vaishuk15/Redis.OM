using AutoMapper;
using PersonalPasswordManager.Repository.Interface;
using PersonalPasswordManager.Services.Interface;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalPasswordManager.Utilities;
using PersonalPasswordManager.Repository.Models;
using RedisRepository.Interface;
using RedisRepository.JsonModel;
using RedisRepository.Implementation;

namespace PersonalPasswordManager.Services.Implementation
{
    public class PasswordManagerService: IPasswordManagerService
    {
        private readonly IPasswordManagerRepository _passwordManagerRepository;
        private readonly IMapper _mapper;
        private readonly IRedisCache<RedisRepository.JsonModel.Password> _redisCache;
        public PasswordManagerService(IPasswordManagerRepository passwordManagerRepository, IMapper mapper, IRedisCache<RedisRepository.JsonModel.Password> redisCache) {
            _passwordManagerRepository= passwordManagerRepository;
            _mapper= mapper;
            _redisCache= redisCache;
        }
        public async Task<List<PasswordViewModel>> GetAll()
        {
            var passwords = await _passwordManagerRepository.GetAll();
            var passwordViewModels = _mapper.Map<List<PasswordViewModel>>(passwords);
            return passwordViewModels;
        }
        public async Task<PasswordViewModel> GetById(int id)
        {
            var isIndexExists = await _redisCache.IndexExistsAsync("Password-idx");
            if (!isIndexExists)
            {
                await _redisCache.CreateIndexAsync();
            }
            string redisKey = $"RedisRepository.JsonModel.Password:{id}";
            var keyExists= await _redisCache.ExistsAsync(redisKey);
            if( keyExists)
            {
                var passwordRedisEntity = await _redisCache.GetAsync(redisKey);
                var dto = _mapper.Map<PasswordViewModel>(passwordRedisEntity);
                return dto;
            }

            var password = await _passwordManagerRepository.GetById(id);
            if (password == null)
            {
                return null;
            }
            //set redis cache
            await _redisCache.SetAsync(id.ToString(), _mapper.Map<RedisRepository.JsonModel.Password>(password));

            var decryptedPassword = Base64Helper.DecodeFromBase64(password.EncryptedPassword);
            var passwordViewModel = _mapper.Map<PasswordViewModel>(password);
            passwordViewModel.DecryptedPassword= decryptedPassword;
            return passwordViewModel;
        }
        public async Task<PasswordViewModel> Create(PasswordViewModel passwordConfig)
        {
            if (passwordConfig == null)
            {
                return null;
            }
            var encryptedPassword = Base64Helper.EncodeToBase64(passwordConfig.DecryptedPassword);
            var password = _mapper.Map<Repository.Models.Password>(passwordConfig);
            password.EncryptedPassword= encryptedPassword;
            var result = await _passwordManagerRepository.Create(password);
            //redis
            await _redisCache.SetAsync(result.Id.ToString(), _mapper.Map<RedisRepository.JsonModel.Password>(password));

            var resultViewModel = _mapper.Map<PasswordViewModel>(result);
            return resultViewModel;

        }
        public async Task<bool> Update(int id, PasswordViewModel passwordConfig)
        {
            if (passwordConfig == null)
            {
                return false;
            }
            if (!string.IsNullOrEmpty(passwordConfig.DecryptedPassword))
            {
                passwordConfig.EncryptedPassword = Base64Helper.EncodeToBase64(passwordConfig.DecryptedPassword);
            }
            var password = _mapper.Map<Repository.Models.Password>(passwordConfig);
            var result=await _passwordManagerRepository.Update(id, password);
            //redis
            await _redisCache.SetAsync(id.ToString(), _mapper.Map<RedisRepository.JsonModel.Password>(password));
            return result;
        }
        public async Task Delete(int id)
        {
            await _passwordManagerRepository.Delete(id);
            //redis
            await _redisCache.RemoveAsync(id.ToString());
        }

    }
}
