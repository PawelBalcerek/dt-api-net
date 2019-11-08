using System;
using System.Collections.Generic;
using System.Text;
using Domain.Repositories.BaseRepo.Abstract;
using Domain.Repositories.BaseRepo.Concrete;
using Data.Models;
using Domain.Repositories.BaseRepo.Response;

namespace Domain.Repositories.UserRepo.Abstract
{
    public interface IUserRepository : IRepositoryBase<Data.Models.User>
    {
        RepositoryResponse<BusinessObject.User> GetUserById(int id);
        RepositoryResponse<bool> CreateUser(string name, string password, string email);
        RepositoryResponse<string> Authenticate(string login, string password);
    }
}
