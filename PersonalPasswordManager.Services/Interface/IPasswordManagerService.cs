using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.ViewModels;

namespace PersonalPasswordManager.Services.Interface
{
    public interface IPasswordManagerService
    {
        Task<List<PasswordViewModel>> GetAll();
        Task<PasswordViewModel> GetById(int id);
        Task<PasswordViewModel> Create(PasswordViewModel passwordConfig);
        Task<bool> Update(int id, PasswordViewModel passwordConfig);
        Task Delete(int id);
    }
}
