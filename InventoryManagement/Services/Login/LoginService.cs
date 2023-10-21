using DataAccess.Interfaces;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Login
{
    public class LoginService: ILoginService
    {
        private IDataAccess<User> _repository;
        public LoginService(IDataAccess<User> repository) 
        {
            _repository = repository;
        }

        public ServiceResult<User> Login(string email, string password)
        {
            var matchedUser = _repository.GetFirstMatch(user => user.Email == email);
            if (matchedUser is null)
            {
                return new ServiceResult<User>("No user found. Make sure you input the correct information.");
            }
            if(matchedUser.Password != password)
            {
                return new ServiceResult<User>("Make sure you input correct email/password.");
            }
            return new ServiceResult<User>(matchedUser);
        }
    }
}
