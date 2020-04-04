namespace OverPaw.Services
{
    using OverPaw.Data;
    using OverPaw.Services.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using System;
    using OverPaw.RequestModels.Account;
    using OverPaw.Data.Models;

    using static OverPaw.Security.PasswordHasher;
    using OverPaw.Services.Models.Account;
    using System.IdentityModel.Tokens.Jwt;
    using System.Text;
    using Microsoft.Extensions.Options;
    using OverPaw.Commons.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System.Security.Claims;

    public class AccountService : IAccountService
    {
        private readonly OverPawDbContext dbContext;
        private readonly string jwtSettings;

        public AccountService(OverPawDbContext dbContext, IOptions<JwtSettings> jwtSettings)
        {
            this.dbContext = dbContext;
            this.jwtSettings = jwtSettings.Value.Secret;
        }

        public async Task<RegisterResultServiceModel> RegisterUserAsync(RegisterRequestModel requestModel)
        {
            RegisterResultServiceModel registerResult = new RegisterResultServiceModel();

            registerResult.IsEmailTaken = dbContext.Users.Any(x => x.Email == requestModel.Email);
            registerResult.IsUsenameTaken = dbContext.Users.Any(x => x.Username == requestModel.Username);

            if (registerResult.IsEmailTaken || registerResult.IsUsenameTaken)
            {
                return registerResult;
            }

            try
            {
                var user = new User()
                {
                    UserId = Guid.NewGuid().ToString(),
                    HashedPassword = HashPassword(requestModel.Password),
                    Username = requestModel.Username,
                    Email = requestModel.Email,
                    FirstName = requestModel.FirstName,
                    FamilyName = requestModel.FamilyName,
                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false
                };

                await dbContext.Users.AddAsync(user);
                await dbContext.SaveChangesAsync();

                registerResult.Status = 1;
            }
            catch (Exception)
            {
                return registerResult;
            }

            return registerResult;
        }

        public async Task<LoginResultServiceModel> LoginUserAsync(LoginRequestModel requestModel)
        {
            var user = await dbContext.Users
                .FirstOrDefaultAsync(x => x.Email == requestModel.Email && !x.IsDeleted);

            LoginResultServiceModel loginResult = new LoginResultServiceModel();

            if (user == null)
            {
                return loginResult;
            }

            try
            {
                bool isValidPassword = VerifyHashedPassword(user.HashedPassword, requestModel.Password);

                if (!isValidPassword)
                {
                    return loginResult;
                }

                loginResult.Status = 1;
                loginResult.Token = CreateJwt(user.Username);

                return loginResult;
            }
            catch
            {
                return loginResult;
            }
        }

        private string CreateJwt(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.jwtSettings);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddDays(1),
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string jwt = tokenHandler.WriteToken(token);

            return jwt;
        }
    }
}
