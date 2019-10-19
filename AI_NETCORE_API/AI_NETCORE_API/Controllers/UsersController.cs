using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI_NETCORE_API.Infrastructure.BuisnessObjectToModelsConverting.Abstract;
using AI_NETCORE_API.Models.Objects;
using AI_NETCORE_API.Models.Request;
using Data.Infrastructure.AppsettingsConfiguration.Abstract;
using Data.Infrastructure.EmailAddressValidation.Abstract;
using Data.Infrastructure.Logging.Abstract;
using Data.Infrastructure.PasswordValidation.Abstract;
using Data.Providers.Common.Enum;
using Data.Providers.Users.Abstract;
using Data.Providers.Users.Request.Abstract;
using Data.Providers.Users.Request.Concrete;
using Data.Providers.Users.Response.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        private readonly IBusinessObjectToModelsConverter _businessObjectToModelsConverter;

        public UsersController(ILogger logger, IPasswordValidator passwordValidator, IEmailValidator emailValidator, IUserProvider userProvider, IBusinessObjectToModelsConverter businessObjectToModelsConverter)
        {
            _logger = logger;
            _passwordValidator = passwordValidator;
            _emailValidator = emailValidator;
            _userProvider = userProvider;
            _businessObjectToModelsConverter = businessObjectToModelsConverter;
        }
        /// <param name="item"> UserId</param>
        /// <response code="200">Returns found user</response>
        /// <response code="404"> If the User not found, empty response</response>  
        /// <response code="500"> Exception, empty response</response>  
        [HttpGet("user/{id:int}")]
        [ProducesResponseType(200, Type = typeof(UserModel))]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
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
        [ProducesResponseType(200,Type =typeof(UserModel))]
        [ProducesResponseType(500)]
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                return StatusCode(200);
            }
            catch(Exception ex)
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
        [ProducesResponseType(200,Type = typeof(UserModel))]
        public async Task<ActionResult<UserModel>> Register([FromBody] RegisterRequest registerRequest)
        {
            try
            {
                if (!registerRequest.IsValid(_passwordValidator,_emailValidator))
                {
                    return StatusCode(400);
                }

                //TODO Add new user in database and return their details

                return Ok(new UserModel
                {
                    Email = registerRequest.Email,
                    Name = registerRequest.Name
                });
            }
            catch(Exception ex)
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