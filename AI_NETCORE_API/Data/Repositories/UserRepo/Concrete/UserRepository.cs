using System.Collections.Generic;
using Domain.Repositories.BaseRepo.Concrete;
using Domain.Repositories.UserRepo.Abstract;
using Data.Models;
using Domain.DTOToBOConverting;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System;
using Domain.Infrastructure;

namespace Domain.Repositories.UserRepo.Concrete
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly IDTOToBOConverter _converter;
        private readonly TokenManagement _tokenManagement;


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

        public string Authenticate(string login, string password)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = System.Text.Encoding.ASCII.GetBytes(_tokenManagement.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim("id", "0"),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
