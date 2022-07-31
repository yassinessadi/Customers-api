using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Movie_api.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Movie_api.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly SignInManager<ApplicationUser> _SignInManager;
        private readonly IConfiguration _Configuration;

        public AccountRepository(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            _UserManager = userManager;
            _SignInManager = signInManager;
            _Configuration = configuration;
        }


        public async Task<string> SingInAsync(SingInModel singIn)
        {
            var result = await _SignInManager.PasswordSignInAsync(singIn.UserName, singIn.Password, true, false);
            if (!result.Succeeded)
            {
                return null;
            }

            var AuthClaim = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,singIn.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            var AuthSignKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_Configuration["JWT:Secret"]));

            var Token = new JwtSecurityToken(
                issuer: _Configuration["JWT:ValidIssuer"],
                audience: _Configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: AuthClaim,
                signingCredentials: new SigningCredentials(AuthSignKey,SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }

        public async Task<IdentityResult> SingUpAsync(RegisterModel register)
        {
            var result = new ApplicationUser()
            {
                FullName = register.FullName,
                Email = register.EmailAddress,
                UserName = register.UserName,
                PhoneNumber = register.Phone
            };
            return await _UserManager.CreateAsync(result, register.Passowrd);
        }
    }
}
