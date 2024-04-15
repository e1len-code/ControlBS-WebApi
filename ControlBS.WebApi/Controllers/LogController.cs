using ControlBS.BusinessObjects.Response;
using Microsoft.AspNetCore.Mvc;
using ControlBS.Facade;
using Serilog;
using FluentValidation;
using ControlBS.WebApi.Utils.Auth;
using ControlBS.BusinessObjects;

namespace ControlBS.WebApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class LogController : ControllerBase
    {
        private IHostEnvironment Environment;
        private Response<ErrorResponse> errorResponse;
        private ErrorResponse? error;


        public LogController(IWebHostEnvironment _environment)
        {
            errorResponse = new Response<ErrorResponse>();
            Environment = _environment;
        }

        [HttpGet("{datetime}")]
        public IActionResult GetLog(string datetime)
        {
            //Format datetime String YYYYMMDD
            try
            {
                string _logFilePath = Path.Combine(this.Environment.ContentRootPath + "/Logs/log" + datetime + ".log");
                // Check if file exists
                if (!System.IO.File.Exists(_logFilePath))
                {
                    error = new ErrorResponse { message = String.Format("No se ha encontrado el archivo de fecha {0}.log", datetime), source = "GetLog - LogController", stackTrace = "" };
                    errorResponse.statusCode = System.Net.HttpStatusCode.NotFound;
                    errorResponse.errors.Add(error);
                    Log.Error(error.ToString());
                    return StatusCode(StatusCodes.Status404NotFound, errorResponse);
                }
                var fileStream = new FileStream(_logFilePath, FileMode.Open, FileAccess.Read,FileShare.Read);
                return File(fileStream, "text/plain", Path.GetFileName(_logFilePath));
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