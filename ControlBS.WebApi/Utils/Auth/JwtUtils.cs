using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using ControlBS.BusinessObjects;
using ControlBS.WebApi.Utils.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ControlBS.WebApi.Utils.Auth{
    public interface IJwtUtils{
        public string GenerateJwtToken(CTPERS oCTPERS);
        public int? ValidateJwtToken(string? token);
    }
    public class JwtUtils : IJwtUtils{
        private readonly AppSettings _appSettings;

        public JwtUtils(IOptions<AppSettings> appSettings){
            _appSettings = appSettings.Value;
            if (string.IsNullOrEmpty(_appSettings.SecretJwt))
                throw new Exception("JWT secret not configured");
        }
        public string GenerateJwtToken(CTPERS oCTPERS){
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.SecretJwt!);
            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new System.Security.Claims.ClaimsIdentity(new[] {new Claim("id",oCTPERS.PERSIDEN.ToString())}),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public int? ValidateJwtToken(string? token){
            if (token == null)
                return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.SecretJwt!);
            try{
                tokenHandler.ValidateToken(token, new TokenValidationParameters{
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    //set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                    
                }, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x=> x.Type == "id").Value);
                //return user id from JWT token if validation succesful
                return userId;   
            }
            catch{
                //return null if validation fails
                return null;
            }
        }
    }
}