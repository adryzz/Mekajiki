using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace Mekajiki.Server.Security;


public class MekajikiAuthHandler
        : AuthenticationHandler<MekajikiAuthSchemeOptions>
    {
        public MekajikiAuthHandler(
            IOptionsMonitor<MekajikiAuthSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // validation comes in here
            if (!Request.Headers.ContainsKey(HeaderNames.Authorization))
            {
                return Task.FromResult(AuthenticateResult.Fail("Header Not Found."));
            }

            var token = Request.Headers[HeaderNames.Authorization].ToString();

            User? user = SecurityManager.GetUser(token);

            if (user != null)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.Name)
                };
                
                var claimsIdentity = new ClaimsIdentity(claims,
                    nameof(MekajikiAuthHandler));
                
                var ticket = new AuthenticationTicket(
                    new ClaimsPrincipal(claimsIdentity), Scheme.Name);
                
                return Task.FromResult(AuthenticateResult.Success(ticket));
            }
            
            return Task.FromResult(AuthenticateResult.Fail("Model is Empty"));
        }
    }