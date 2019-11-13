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
using Microsoft.Extensions.Options;
using System.Diagnostics;
using Domain.Repositories.BaseRepo.Response;
using BCrypt.Net;

namespace Domain.Repositories.UserRepo.Concrete
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly IDTOToBOConverter _converter;
        private readonly TokenManagement _tokenManagement;


        public UserRepository(RepositoryContext repositoryContext, IDTOToBOConverter converter, IOptions<TokenManagement> tokenManagement)
            : base(repositoryContext)
        {
            _converter = converter;
            _tokenManagement = tokenManagement.Value;
        }
        public RepositoryResponse<BusinessObject.User> GetUserById(int id)
        {
            Stopwatch timer = Stopwatch.StartNew();
            User user = FindByCondition(userExpr => userExpr.Id == id).FirstOrDefault();
            timer.Stop();
            return new RepositoryResponse<BusinessObject.User>(_converter.ConvertUser(user), timer.ElapsedMilliseconds);
        }

        public RepositoryResponse<bool> CreateUser(string name, string password, string email)
        {
            Stopwatch timer = Stopwatch.StartNew();
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            RepositoryContext.Users.Add(new User
            {
                Password = hashedPassword,
                Email = email,
                Name = name,
                Cash = 0
            });
            try
            {
                RepositoryContext.SaveChanges();
                timer.Stop();
                return new RepositoryResponse<bool>(true, timer.ElapsedMilliseconds);
            } catch (Exception ex)
            {
                return new RepositoryResponse<bool>(false, timer.ElapsedMilliseconds);
            }
            
        }

        public RepositoryResponse<string> Authenticate(string email, string password)
        {
            var timer = Stopwatch.StartNew();
            var users = from u in RepositoryContext.Users where u.Email == email select u;
            var user = users.FirstOrDefault();
            timer.Stop();
            bool verified = BCrypt.Net.BCrypt.Verify(password, user.Password);
            if (user == null || !verified) return new RepositoryResponse<string>(null, timer.ElapsedMilliseconds);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = System.Text.Encoding.ASCII.GetBytes(_tokenManagement.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim("Id", user.Id.ToString()),
                        new Claim("Name", user.Name)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new RepositoryResponse<string>(tokenHandler.WriteToken(token), timer.ElapsedMilliseconds);
        }
    }
}
