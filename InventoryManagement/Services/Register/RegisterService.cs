
using DataAccess.Interfaces;
using Entities;
using System.ComponentModel.DataAnnotations;

namespace Services.Register;

public class RegisterService: IRegisterService
{
    private readonly IDataAccess<User> _repository;
    public RegisterService(IDataAccess<User> repository) 
    {
        _repository = repository;
    }

    public ServiceResult<User> Register(User user)
    {
        
        if(user is null)
        {
            return new ServiceResult<User>("User information cannot be null.");
        }

        if(string.IsNullOrEmpty(user.Email) 
            || string.IsNullOrEmpty(user.UserName) 
            || string.IsNullOrEmpty(user.Password))
        {
            return new ServiceResult<User>("All fields must be filled.");
        }

        var isValidEmailFormat = new EmailAddressAttribute().IsValid(user.Email);

        if(!isValidEmailFormat)
        {
            return new ServiceResult<User>("Not correct email format. Make sure you input a valid email format.");
        }

        var users = _repository.GetAll();

        var doesUserExist = users.Any(u => u.Email == user.Email);

        if (doesUserExist)
        {
            return new ServiceResult<User>("This email had been registed already.");
        }

        _repository.Add(user);
        return new ServiceResult<User>(user);
    }
}
