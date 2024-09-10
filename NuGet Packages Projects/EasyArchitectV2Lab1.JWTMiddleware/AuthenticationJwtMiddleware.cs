using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyArchitectV2Lab1.JWTMiddleware
{
    public static class AuthenticationJwtMiddleware
    {
        public static IApplicationBuilder UseCustomJwtAuthentication(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtService>();
        }
    }
}
