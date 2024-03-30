using ControlBS.BusinessObjects.Response;
using ControlBS.BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using ControlBS.Facade;
using Serilog;
using ControlBS.BusinessObjects.Models;

namespace ControlBS.WebApi.Controllers
{
    [Route("[controller]")]
    public class AttendanceController : Controller
    {
        private Response<ErrorResponse> errorResponse;
        private CTATTNFacade oCTATTNFacade;
        public AttendanceController()
        {
            oCTATTNFacade = new CTATTNFacade();
            errorResponse = new Response<ErrorResponse>();
        }
        [HttpGet]
        public IActionResult List()
        {
            try
            {
                Response<List<CTATTN>> oResponse = oCTATTNFacade.List();
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

        [HttpPost("filterList")]
        public IActionResult FilterList([FromBody] CTATTNFilterRequest oCTATTNFilterRequest)
        {
            try
            {
                Response<List<CTATTNFilterResponse>> oResponse = oCTATTNFacade.FilterList(oCTATTNFilterRequest);
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

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] CTATTN oCTATTN)
        {
            try
            {
                Response<bool> oResponse = await oCTATTNFacade.Save(oCTATTN);
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
        [HttpGet("{ATTNIDEN}")]
        public IActionResult Get(int ATTNIDEN)
        {
            try
            {
                Response<CTATTN?> oResponse = oCTATTNFacade.Get(ATTNIDEN);
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
        [HttpDelete("{ATTNIDEN}")]
        public IActionResult Delete(int ATTNIDEN)
        {
            try
            {
                Response<bool> oResponse = oCTATTNFacade.Delete(ATTNIDEN);
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