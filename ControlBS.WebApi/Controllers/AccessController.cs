using ControlBS.BusinessObjects.Response;
using ControlBS.BusinessObjects.Security;
using ControlBS.Facade;
using ControlBS.WebApi.Utils.Auth;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace ControlBS.WebApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class AccessController : ControllerBase
    {
        private Response<ErrorResponse> errorResponse;
        private CTACCEFacade oCTACCEFacade;
        public AccessController()
        {
            oCTACCEFacade = new CTACCEFacade();
            errorResponse = new Response<ErrorResponse>();
        }
        [HttpGet("filterList/{PERSIDEN}")]
        public IActionResult FilterList(int PERSIDEN)
        {
            try
            {
                Response<List<CTACCE>> oResponse = oCTACCEFacade.ListAccess(PERSIDEN);
                return StatusCode(StatusCodes.Status200OK, oResponse);
            }
            catch (Exception e)
            {
                errorResponse = new Response<ErrorResponse>(e);
                Log.Error(errorResponse.errors.First().ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
                throw;
            }
        }
    }
}