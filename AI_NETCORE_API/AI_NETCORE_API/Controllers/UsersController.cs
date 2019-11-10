using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AI_NETCORE_API.Infrastructure.BuisnessObjectToModelsConverting.Abstract;
using AI_NETCORE_API.Models;
using AI_NETCORE_API.Models.Objects;
using AI_NETCORE_API.Models.Request;
using AI_NETCORE_API.Models.Response;
using AI_NETCORE_API.Models.Response.SellOffers;
using Domain.Creators.Users.Abstract;
using Domain.Creators.Users.Request.Concrete;
using Domain.Creators.Users.Response.Abstract;
using Domain.Infrastructure.AppsettingsConfiguration.Abstract;
using Domain.Infrastructure.EmailAddressValidation.Abstract;
using Domain.Infrastructure.Logging.Abstract;
using Domain.Infrastructure.PasswordValidation.Abstract;
using Domain.Providers.Common.Enum;
using Domain.Providers.SellOffers.Abstract;
using Domain.Providers.SellOffers.Request.Abstract;
using Domain.Providers.SellOffers.Request.Concrete;
using Domain.Providers.SellOffers.Response.Abstract;
using Domain.Providers.Users.Abstract;
using Domain.Providers.Users.Request.Abstract;
using Domain.Providers.Users.Request.Concrete;
using Domain.Providers.Users.Response.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AI_NETCORE_API.Controllers
{

    [Route("api/")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IPasswordValidator _passwordValidator;
        private readonly IEmailValidator _emailValidator;
        private readonly IUserProvider _userProvider;
        private readonly IUserCreator _userCreator;
        private readonly IBusinessObjectToModelsConverter _businessObjectToModelsConverter;

        public UsersController(ILogger logger,
            IPasswordValidator passwordValidator,
            IEmailValidator emailValidator,
            IUserProvider userProvider,
            IBusinessObjectToModelsConverter businessObjectToModelsConverter,
            IUserCreator userCreator)
        {
            _logger = logger;
            _passwordValidator = passwordValidator;
            _emailValidator = emailValidator;
            _userProvider = userProvider;
            _businessObjectToModelsConverter = businessObjectToModelsConverter;
            _userCreator = userCreator;
        }
        /// <param name="item"> UserId</param>
        /// <response code="200">Returns found user</response>
        /// <response code="404"> If the User not found, empty response</response>  
        /// <response code="500"> Exception, empty response</response>  
        [HttpGet("user/{id:int}")]
        [ProducesResponseType(200, Type = typeof(UserModel))]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        [Authorize]
        public ActionResult<UserModel> GetUserById(int id)
        {
            try
            {


                IGetUserByIdRequest getUserByIdRequest = new GetUserByIdRequest(id);
                IGetUserByIdResponse getUserByIdResponse = _userProvider.GetUserById(getUserByIdRequest);
                return PrepareResponseAfterGetUserById(getUserByIdResponse);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Method to login user to API
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns>UserModel</returns>
        [ProducesResponseType(200, Type = typeof(LoginResponse))]
        [ProducesResponseType(500)]
        [HttpPost("login")]
        public async Task<ActionResult<ILoginUserResponse>> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                ILoginUserRequest loginRequestData = new LoginUserRequest(loginRequest.Login, loginRequest.Password);
                var loginResponse = _userProvider.LoginUser(loginRequestData);

                if(loginResponse.Token == null)
                {
                    return StatusCode(401);
                }

                return Ok(loginResponse);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return StatusCode(500);
            }
        }
        /// <summary>
        /// Method to logout user from API
        /// </summary>
        /// <returns>Only httpCode</returns>
        [HttpGet("logout")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> Logout()
        {
            try
            {
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Method to create new user 
        /// </summary>
        /// <param name="registerRequest"> Request object required to create new user </param>
        /// <returns>UserModel</returns>
        [HttpPost("register")]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(200, Type = typeof(UserModel))]
        public async Task<ActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            try
            {
                if (!registerRequest.IsValid(_passwordValidator, _emailValidator))
                {
                    return StatusCode(400);
                }

                IUserCreateResponse result = _userCreator.CreateUser(new UserCreateRequest(registerRequest.Name, registerRequest.Password,
                    registerRequest.Email));
                return result.Success ? Ok() : StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return StatusCode(500);
            }
        }
        private ActionResult<UserModel> PrepareResponseAfterGetUserById(IGetUserByIdResponse getUserByIdResponse)
        {
            switch (getUserByIdResponse.ProvideResult)
            {
                case ProvideEnumResult.Exception:
                    return StatusCode(500);
                case ProvideEnumResult.Success:
                    return Ok(_businessObjectToModelsConverter.ConvertUser(getUserByIdResponse.User));
                case ProvideEnumResult.NotFound:
                    return StatusCode(404);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}