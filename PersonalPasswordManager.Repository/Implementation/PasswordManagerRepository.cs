using Microsoft.EntityFrameworkCore;
using PersonalPasswordManager.Repository.Interface;
using PersonalPasswordManager.Repository.Models;
using PersonalPasswordManager.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PersonalPasswordManager.Repository.Implementation
{
    public class PasswordManagerRepository: IPasswordManagerRepository
    {
        private readonly PasswordManagerDbContext _dbContext;

        public PasswordManagerRepository(PasswordManagerDbContext dbContext) {
            _dbContext=dbContext;
        }
        public async Task<List<Password>> GetAll()
        {
            return await _dbContext.Passwords.ToListAsync();
        }
        public async Task<Password> GetById(int id)
        {
            return await _dbContext.Passwords.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Password> Create(Password password)
        {
            await _dbContext.Passwords.AddAsync(password);

            // Save the changes to the database
            await _dbContext.SaveChangesAsync();
            return password;
        }
        public async Task<bool> Update(int id, Password password)
        {
            // Retrieve the existing config by its ID
            var existingPassword = await _dbContext.Passwords.FirstOrDefaultAsync(x => x.Id == id);

            if (existingPassword == null)
            {
                throw new Exception($"Password with ID {id} not found");
            }
            existingPassword.App = password.App;
            existingPassword.Category = password.Category;
            existingPassword.UserName = password.UserName;
            existingPassword.EncryptedPassword = password.EncryptedPassword;
            await _dbContext.SaveChangesAsync();
            return true;

        }
        public async Task Delete(int id)
        {
            // Find the existing config by its ID
            var existingPassword = await _dbContext.Passwords.FirstOrDefaultAsync(x => x.Id == id);

            if (existingPassword == null)
            {
                throw new Exception($"Password with ID {id} not found");
            }
            _dbContext.Passwords.Remove(existingPassword);
            await _dbContext.SaveChangesAsync();
        }

    }
}
