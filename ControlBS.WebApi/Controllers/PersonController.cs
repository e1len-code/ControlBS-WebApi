using ControlBS.BusinessObjects.Response;
using ControlBS.BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using ControlBS.Facade;
using Serilog;
using ControlBS.WebApi.Utils.Auth;
using ControlBS.BusinessObjects.Models;

namespace ControlBS.WebApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private CTPERSFacade oCTPERSFacade;
        private Response<ErrorResponse> errorResponse;
        public PersonController()
        {
            errorResponse = new Response<ErrorResponse>();
            oCTPERSFacade = new CTPERSFacade();
        }

        [HttpGet]
        public IActionResult List()
        {
            try
            {
                Response<List<CTPERS>> oResponse = oCTPERSFacade.List();
                return StatusCode((int)oResponse.statusCode, oResponse);
            }
            catch (Exception e)
            {
                errorResponse = new Response<ErrorResponse>(e);
                Log.Error(errorResponse.errors.First().ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] CTPERSSaveRequest oCTPERS)
        {
            try
            {
                Response<bool> oResponse = await oCTPERSFacade.Save(oCTPERS);
                return StatusCode((int)oResponse.statusCode, oResponse);
            }
            catch (Exception e)
            {
                errorResponse = new Response<ErrorResponse>(e);
                Log.Error(errorResponse.errors.First().ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
                throw;
            }
        }

        [HttpGet("{PERSIDEN}")]
        public IActionResult Get(int PERSIDEN)
        {
            try
            {
                Response<CTPERS?> oResponse = oCTPERSFacade.Get(PERSIDEN);
                return StatusCode((int)oResponse.statusCode, oResponse);
            }
            catch (Exception e)
            {
                errorResponse = new Response<ErrorResponse>(e);
                Log.Error(errorResponse.errors.First().ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
                throw;
            }
        }
        [HttpDelete("{PERSIDEN}")]
        public IActionResult Delete(int PERSIDEN)
        {
            try
            {
                Response<bool> oResponse = oCTPERSFacade.Delete(PERSIDEN);
                return StatusCode((int)oResponse.statusCode, oResponse);
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