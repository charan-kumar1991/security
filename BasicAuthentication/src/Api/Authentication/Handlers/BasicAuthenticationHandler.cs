using Core.Entities;
using Core.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Api.Authentication.Handlers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserService _userService;
        private readonly IPasswordService _passwordService;
        private const string _realm = "Default realm";

        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IUserService userService,
            IPasswordService passwordService)
            : base(options,
                  logger,
                  encoder,
                  clock)
        {
            _userService = userService;
            _passwordService = passwordService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // 1. Bypass the methods having AllowAnonymous attribute
            Endpoint endpoint = Context.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != default)
            {
                return AuthenticateResult.NoResult();
            }

            // 2. Authorization header present or not
            if (!Context.Request.Headers.ContainsKey("Authorization"))
            {
                Response.Headers.Add("WWW-Authenticate", string.Format("basic realm=\"{0}\"", _realm));
                return AuthenticateResult.Fail("Authorization header missing");
            }

            AuthenticationHeaderValue header = AuthenticationHeaderValue.Parse(Context.Request.Headers["Authorization"]);
            // After parsing the credentials will be populated into Parameter property
            string encodedCredentials = header.Parameter;

            // 4. Decode base64 string
            byte[] encodedBytes = Convert.FromBase64String(encodedCredentials);
            string plainTextCredentials = Encoding.UTF8.GetString(encodedBytes);

            // 5. Split the string
            string[] credentialsArr = plainTextCredentials.Split(':');
            string username = credentialsArr[0];
            string password = credentialsArr[1];

            // 6. Check in database using IUserService
            User found = await _userService.FetchAsync(username);
            if (found == default)
            {
                Response.Headers.Add("WWW-Authenticate", string.Format("basic realm=\"{0}\"", _realm));
                return AuthenticateResult.Fail("Invalid username / password!");
            }

            // 7. Compare plain text password and hashed password using IPasswordService
            if (!_passwordService.VerifyPassword(found.Password, password))
            {
                Response.Headers.Add("WWW-Authenticate", string.Format("basic realm=\"{0}\"", _realm));
                return AuthenticateResult.Fail("Invalid password!");
            }

            // 8. Build the claims
            Claim[] claims = new Claim[]
            {
                new Claim("id", found.Id.ToString()),
                new Claim(ClaimTypes.GivenName, found.FirstName),
                new Claim(ClaimTypes.Surname, found.LastName),
                new Claim(ClaimTypes.Email, found.Email),
                new Claim(ClaimTypes.DateOfBirth, found.DateOfBirth.ToString("yyyy-MM-dd")),
                new Claim(ClaimTypes.MobilePhone, found.PhoneNo),
                new Claim(ClaimTypes.NameIdentifier, found.Username)
            };

            // 9. Build claimIdentity (1 user = many claim idenities)
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, Scheme.Name);

            // 10. Build ClaimPrincipal (1 user = 1 claim principal -> Collection of many claim identities)
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new[] 
            {
                claimsIdentity
            });

            // 11. Attach into httpcontext using AuthenticationTicket
            AuthenticationTicket ticket = new AuthenticationTicket(claimsPrincipal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
    }
}
