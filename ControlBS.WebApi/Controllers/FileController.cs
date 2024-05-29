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
    public class FileController : ControllerBase
    {
        private CTFILEFacade oCTFILEFacade;
        private Response<ErrorResponse> errorResponse;
        public FileController()
        {
            errorResponse = new Response<ErrorResponse>();
            oCTFILEFacade = new CTFILEFacade();
        }

        // [HttpGet]
        // public IActionResult List()
        // {
        //     try
        //     {
        //         Response<List<CTFILE>> oResponse = oCTFILEFacade.List();
        //         return StatusCode((int)oResponse.statusCode, oResponse);
        //     }
        //     catch (Exception e)
        //     {
        //         errorResponse = new Response<ErrorResponse>(e);
        //         Log.Error(errorResponse.errors.First().ToString());
        //         return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
        //         throw;
        //     }
        // }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] CTFILE oCTFILE)
        {
            try
            {
                Response<bool> oResponse = await oCTFILEFacade.Save(oCTFILE);
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

        // [HttpGet("{FILEIDEN}")]
        // public IActionResult Get(int FILEIDEN)
        // {
        //     try
        //     {
        //         Response<CTFILE?> oResponse = oCTFILEFacade.Get(FILEIDEN);
        //         return StatusCode((int)oResponse.statusCode, oResponse);
        //     }
        //     catch (Exception e)
        //     {
        //         errorResponse = new Response<ErrorResponse>(e);
        //         Log.Error(errorResponse.errors.First().ToString());
        //         return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
        //         throw;
        //     }
        // }
        // [HttpGet("getPhoto/{FILEIDEN}")]
        // public IActionResult GetPhoto(int FILEIDEN)
        // {
        //     try
        //     {
        //         Response<String?> oResponse = oCTFILEFacade.GetPhoto(FILEIDEN);
        //         return StatusCode((int)oResponse.statusCode, oResponse);
        //     }
        //     catch (Exception e)
        //     {
        //         errorResponse = new Response<ErrorResponse>(e);
        //         Log.Error(errorResponse.errors.First().ToString());
        //         return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
        //         throw;
        //     }
        // }

        // [HttpDelete("{FILEIDEN}")]
        // public IActionResult Delete(int FILEIDEN)
        // {
        //     try
        //     {
        //         Response<bool> oResponse = oCTFILEFacade.Delete(FILEIDEN);
        //         return StatusCode((int)oResponse.statusCode, oResponse);
        //     }
        //     catch (Exception e)
        //     {
        //         errorResponse = new Response<ErrorResponse>(e);
        //         Log.Error(errorResponse.errors.First().ToString());
        //         return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
        //         throw;
        //     }
        // }
    }
}