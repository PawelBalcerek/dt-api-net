using System;
using System.Collections.Generic;
using System.Text;
using Domain.Repositories.BaseRepo.Concrete;
using Domain.Repositories.UserRepo.Abstract;
using Data.Models;
using Domain.DTOToBOConverting;
using System.Linq;

namespace Domain.Repositories.UserRepo.Concrete
{
    public class UserRepository: RepositoryBase<Data.Models.User>, IUserRepository
    {
        private IDTOToBOConverter _converter;
        public UserRepository(RepositoryContext repositoryContext, IDTOToBOConverter converter)
            : base(repositoryContext)
        {
            _converter = converter;
        }
        public Domain.BusinessObject.User GetUserById(int id)
        {
            var userDTO = FindByCondition(user => user.Id == id).FirstOrDefault();
            return _converter.ConvertUser(userDTO);
            
        }

    }
}
