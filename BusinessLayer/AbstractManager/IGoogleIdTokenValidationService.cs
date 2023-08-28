using Entities.Dtos;
using NuGet.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.AbstractManager
{
    public interface IGoogleIdTokenValidationService
    {
        public Task<String> ValidateIdTokenAsync(ExternalAuthDto model, GoogleUserDto user);
    }
}
