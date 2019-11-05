using System;
using System.Collections.Generic;
using System.Text;
using Domain.Repositories.BaseRepo.Abstract;
using Domain.Repositories.BaseRepo.Concrete;
using Data.Models;

namespace Domain.Repositories.UserRepo.Abstract
{
    public interface IUserRepository : IRepositoryBase<Data.Models.User>
    {
        BusinessObject.User GetUserById(int id);
        void CreateUser(int id, string name, string password, string email);
        string Authenticate(string login, string password);
    }
}
