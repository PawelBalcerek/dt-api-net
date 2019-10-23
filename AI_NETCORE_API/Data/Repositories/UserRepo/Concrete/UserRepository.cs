using System.Collections.Generic;
using Domain.Repositories.BaseRepo.Concrete;
using Domain.Repositories.UserRepo.Abstract;
using Data.Models;
using Domain.DTOToBOConverting;
using System.Linq;

namespace Domain.Repositories.UserRepo.Concrete
{
    public class UserRepository: RepositoryBase<User>, IUserRepository
    {
        private readonly IDTOToBOConverter _converter;
        public UserRepository(RepositoryContext repositoryContext, IDTOToBOConverter converter)
            : base(repositoryContext)
        {
            _converter = converter;
        }
        public BusinessObject.User GetUserById(int id)
        {
            User user = FindByCondition(userExpr => userExpr.Id == id).FirstOrDefault();
            return _converter.ConvertUser(user);
        }

        public void CreateUser(int id, string name, string password, string email)
        {
            RepositoryContext.Users.Add(new User
            {
                Password = password,
                Email = email,
                Name = name,
                Cash = 0,
                Id = id,
                Resources = new List<Resource>()
            });
            RepositoryContext.SaveChanges(true);
        }
    }
}
