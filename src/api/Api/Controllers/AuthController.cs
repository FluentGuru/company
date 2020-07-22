using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Company.Domain.Exceptions;
using Company.Domain.Options;
using Company.Domain.Types;
using Company.Messages;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Company.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly JwtOptions _jwt;

        public AuthController(IMediator mediator, IOptions<JwtOptions> jwt)
        {
            _mediator = mediator;
            _jwt = jwt.Value;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Login([FromBody]Credentials credentials)
        {
            try
            {
                await _mediator.Send(new AuthenticateCommand(credentials));
                return GenerateToken(credentials);
            }
            catch(UserNotFoundException)
            {
                return NotFound();
            }
            catch(AuthenticationFailedException)
            {
                return Unauthorized();
            }
        }

        private string GenerateToken(Credentials credentials)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var cred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] { new Claim(JwtRegisteredClaimNames.Sub, credentials.UserName) };

            var token = new JwtSecurityToken(_jwt.Issuer,
              _jwt.Issuer,
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: cred);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
