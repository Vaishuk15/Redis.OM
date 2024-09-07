using PersonalPasswordManager.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalPasswordManager.Repository.Interface
{
    public interface IPasswordManagerRepository
    {
        Task<List<Password>> GetAll();
        Task<Password> GetById(int id);
        Task<Password> Create(Password password);
        Task<bool> Update(int id, Password password);
        Task Delete(int id);
    }
}
