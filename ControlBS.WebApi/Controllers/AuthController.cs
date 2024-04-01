using ControlBS.BusinessObjects;
using ControlBS.BusinessObjects.Auth;
using ControlBS.BusinessObjects.Response;
using ControlBS.Facade;
using ControlBS.WebApi.Utils.Auth;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace ControlBS.WebApi.Controllers {
    
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private Response<ErrorResponse> errorResponse;
        private CTPERSFacade oCTPERSFacade;
        private readonly IJwtUtils _jwtUtils;
        public AuthController(IJwtUtils jwtUtils){
            oCTPERSFacade = new CTPERSFacade();
            errorResponse = new Response<ErrorResponse>();
            _jwtUtils = jwtUtils;
        }
        [HttpPost("/auth")]
        public IActionResult Login([FromBody] AuthRequest oAuthRequest){
            try{
                Response<CTPERS?> oResponse = oCTPERSFacade.AuthLogin(oAuthRequest);
                Response<AuthResponse> oResponseAuth = new Response<AuthResponse>();
                if (oResponse.value == null){
                    oResponse.statusCode = StatusCodes.Status404NotFound;
                    return StatusCode(StatusCodes.Status404NotFound, oResponse);
                } 
                var token = _jwtUtils.GenerateJwtToken(oResponse.value!);
                oResponseAuth.value = new AuthResponse(oResponse.value, token);
                return StatusCode(StatusCodes.Status200OK,oResponseAuth);
                
            }catch(Exception e){
                errorResponse = new Response<ErrorResponse>(e);
                Log.Error(errorResponse.errors.First().ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
                throw;
            }
        }
    }
} 