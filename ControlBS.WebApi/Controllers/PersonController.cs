using ControlBS.BusinessObjects.Response;
using ControlBS.BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using ControlBS.Facade;
using Serilog;

namespace ControlBS.WebApi.Controllers
{
    [Route("[controller]")]
    public class PersonController : Controller
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
                return StatusCode(oResponse.statusCode, oResponse);
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
        public async Task<IActionResult> Save([FromBody] CTPERS oCTPERS)
        {
            try
            {
                Response<bool> oResponse = await oCTPERSFacade.Save(oCTPERS);
                oResponse.statusCode = oResponse.success ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest;
                return StatusCode(oResponse.statusCode, oResponse);
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
                oResponse.statusCode = oResponse.value != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound;
                return StatusCode(oResponse.statusCode, oResponse);
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
                oResponse.statusCode = oResponse.success ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest;
                return StatusCode(oResponse.statusCode, oResponse);
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