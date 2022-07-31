using Microsoft.AspNetCore.Identity;
using Movie_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_api.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SingUpAsync(RegisterModel register);
        Task<string> SingInAsync(SingInModel singIn);
    }
}
