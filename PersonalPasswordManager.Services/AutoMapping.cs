using AutoMapper;
using PersonalPasswordManager.Repository.Models;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PersonalPasswordManager.Services
{
    public class AutoMapping: Profile
    {
        public AutoMapping() {

            CreateMap<PasswordViewModel, Password>().ReverseMap();
        }

    }
}
