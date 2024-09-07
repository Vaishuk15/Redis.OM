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

namespace PersonalPasswordManager.Services.Implementation
{
    public class PasswordManagerService: IPasswordManagerService
    {
        private readonly IPasswordManagerRepository _passwordManagerRepository;
        private readonly IMapper _mapper;
        public PasswordManagerService(IPasswordManagerRepository passwordManagerRepository, IMapper mapper) {
            _passwordManagerRepository= passwordManagerRepository;
            _mapper= mapper;
        }
        public async Task<List<PasswordViewModel>> GetAll()
        {
            var passwords = await _passwordManagerRepository.GetAll();
            var passwordViewModels = _mapper.Map<List<PasswordViewModel>>(passwords);
            return passwordViewModels;
        }
        public async Task<PasswordViewModel> GetById(int id)
        {
            var password = await _passwordManagerRepository.GetById(id);
            if (password == null)
            {
                return null;
            }
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
            var password = _mapper.Map<Password>(passwordConfig);
            password.EncryptedPassword= encryptedPassword;
            var result = await _passwordManagerRepository.Create(password);
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
            var password = _mapper.Map<Password>(passwordConfig);
            await _passwordManagerRepository.Update(id, password);
            return true;
        }
        public async Task Delete(int id)
        {
            await _passwordManagerRepository.Delete(id);
        }

    }
}
