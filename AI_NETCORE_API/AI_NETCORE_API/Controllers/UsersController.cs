using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AI_NETCORE_API.Infrastructure.BuisnessObjectToModelsConverting.Abstract;
using AI_NETCORE_API.Models;
using AI_NETCORE_API.Models.Objects;
using AI_NETCORE_API.Models.Request;
using AI_NETCORE_API.Models.Response.ExecutingTimes;
using AI_NETCORE_API.Models.Response.Users;
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
using Domain.Repositories.UserRepo.Const;
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
        /// <response code="200">Returns logged in user</response>
        /// <response code="500">Exception, empty response</response>  
        [HttpGet("users")]
        [ProducesResponseType(200, Type = typeof(GetUserResponse))]
        [ProducesResponseType(500)]
        [Authorize]
        public ActionResult<GetUserResponse> GetUser()
        {
            try
            {
                Stopwatch timer = Stopwatch.StartNew();
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var id = int.Parse(identity.Claims.Where(c => c.Type == "Id").FirstOrDefault().Value);
              
                IGetUserByIdRequest getUserByIdRequest = new GetUserByIdRequest(id);
                IGetUserByIdResponse getUserByIdResponse = _userProvider.GetUserById(getUserByIdRequest);
                return PrepareResponseAfterGetUserById(getUserByIdResponse, timer);
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
        /// <returns>LoginResponse</returns>
        [ProducesResponseType(200, Type = typeof(LoginResponse))]
        [ProducesResponseType(500)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                Stopwatch timer = Stopwatch.StartNew();

                if (!TryValidateModel(loginRequest))
                {
                    return StatusCode(400);
                }

                ILoginUserRequest loginRequestData = new LoginUserRequest(loginRequest.Email, loginRequest.Password);
                var loginResponse = _userProvider.LoginUser(loginRequestData);

                if(loginResponse.Token == null)
                {
                    return StatusCode(401);
                }

                timer.Stop();
                var response = new LoginResponse
                {
                    Token = loginResponse.Token,
                    ExecDetails = new ExecutionDetails
                    {
                        DbTime = loginResponse.DbTime,
                        ExecTime = timer.ElapsedMilliseconds
                    }
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return StatusCode(500);
            }
        }

        /*
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
        */
        /// <summary>
        /// Method to create new user 
        /// </summary>
        /// <param name="registerRequest"> Request object required to create new user </param>
        /// <returns>RegisterResponse</returns>
        [HttpPost("users")]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(200, Type = typeof(RegisterResponse))]
        public async Task<ActionResult<RegisterResponse>> Register([FromBody] RegisterRequest registerRequest)
        {
            try
            {
                Stopwatch timer = Stopwatch.StartNew();
                if (!registerRequest.IsValid(_passwordValidator, _emailValidator))
                {
                    return StatusCode(400);
                }

                IUserCreateResponse result = _userCreator.CreateUser(new UserCreateRequest(registerRequest.Name, registerRequest.Password,
                    registerRequest.Email));

                timer.Stop();

                switch (result.ResponseEnum)
                {
                    case CreateUserResponseEnum.Success:
                    {
                        var response = new RegisterResponse
                        {
                            ExecDetails = new ExecutionDetails
                            {
                                DbTime = result.DbTime,
                                ExecTime = timer.ElapsedMilliseconds
                            }
                        };
                        return Ok(response);
                    }
                    case CreateUserResponseEnum.EmailAlreadyExists:
                        return StatusCode(409);
                    case CreateUserResponseEnum.Exception:
                        return StatusCode(500);
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return StatusCode(500);
            }
        }

        private ActionResult<GetUserResponse> PrepareResponseAfterGetUserById(IGetUserByIdResponse getUserByIdResponse, Stopwatch timer)
        {
            switch (getUserByIdResponse.ProvideResult)
            {
                case ProvideEnumResult.Exception:
                    return StatusCode(500);
                case ProvideEnumResult.Success:
                    return Ok(PrepareSuccessResponseAfterGetUserById(getUserByIdResponse, timer));
                case ProvideEnumResult.NotFound:
                    return StatusCode(404);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private GetUserResponse PrepareSuccessResponseAfterGetUserById(IGetUserByIdResponse getUserByIdResponse, Stopwatch timer)
        {
            var user = _businessObjectToModelsConverter.ConvertUser(getUserByIdResponse.User);
            timer.Stop();
            return new GetUserResponse
            {
                user = user,
                ExecDetails = new ExecutionDetails
                {
                    DbTime = getUserByIdResponse.DbTime,
                    ExecTime = timer.ElapsedMilliseconds
                }
            };
        }
    }
}