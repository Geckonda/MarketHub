using MarketHub.Domain.Abstractions.Repository;
using MarketHub.Domain.Entities;
using MarketHub.Domain.Enums;
using MarketHub.Domain.Helpers;
using MarketHub.Domain.Response;
using MarketHub.Domain.ViewModels;
using MarketHub.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Service.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IBaseRepository<CustomerEntity> _userRepository;
        private readonly ILogger<AccountService> _logger;

        public AccountService(IBaseRepository<CustomerEntity> userRepository,
            ILogger<AccountService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }
        public async Task<UserEntity?> GetUser(int id)
        {
            try
            {
                return await _userRepository.GetOne(id);
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
        {
            try
            {
                var user = _userRepository
                    .GetAll().Result!
                    .Where(x => x.Email == model.Email)
                    .FirstOrDefault();

                if (user == null || user.Password != HashPasswordHelper.HashPassword(model.Password))
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Email или пароль указаны неверно",
                    };
                }
                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    StatusCode = StatusCode.Ok,
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Login]: {ex.Message}]");
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError,
                };
            }
        }

        public async Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model)
        {
            try
            {
                var user = _userRepository
                    .GetAll().Result!
                    .Where(x => x.Email == model.Email)
                    .FirstOrDefault();
                    

                if (user != null)
                {
                    if (user.Email == model.Email)
                    {
                        return new BaseResponse<ClaimsIdentity>()
                        {
                            Description = "Пользователь с такой электронной почтой уже зарегистрирован",
                        };
                    }
                }
                user = new CustomerEntity()
                {
                    RoleId = 2,
                    Name = model.Name,
                    Email = model.Email,
                    Password = HashPasswordHelper.HashPassword(model.Password),
                };
                await _userRepository.Add(user);
                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    Description = "Пользователь зарегистрирован",
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[Register]: {ex.Message}");
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        private ClaimsIdentity Authenticate(UserEntity user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email!),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, "2"),
                new Claim("userId", user.Id.ToString() ),
                new Claim("email", user.Email!.ToString() ),
            };
            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}
