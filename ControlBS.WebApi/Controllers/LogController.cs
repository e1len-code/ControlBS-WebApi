using ControlBS.BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using ControlBS.Facade;
using Serilog;
using FluentValidation;

namespace ControlBS.WebApi.Controllers
{
    [Route("[controller]")]
    public class LogController : Controller
    {
        private IHostEnvironment Environment;
        private ErrorResponse? error;


        public LogController(IWebHostEnvironment _environment)
        {
            Environment = _environment;
        }

        [HttpGet]
        public IActionResult GetLog(string datetime)
        {
            //Format datetime String YYYYMMDD
            try
            {
                string _logFilePath = Path.Combine(this.Environment.ContentRootPath + "/Logs/log" + datetime + ".log");
                // Check if file exists
                if (!System.IO.File.Exists(_logFilePath))
                {
                    error = new ErrorResponse { message = String.Format("No se ha encontrado el archivo de la fecha {0}  .log", datetime), source = "GetLog - LogController", stackTrace = "" };
                    Log.Error(error.ToString());
                    return StatusCode(StatusCodes.Status404NotFound, error.ToString());
                }
                var fileStream = new FileStream(_logFilePath, FileMode.Open, FileAccess.Read);
                return File(fileStream, "text/plain", Path.GetFileName(_logFilePath));
            }
            catch (Exception e)
            {
                error = new ErrorResponse { message = e.Message, source = e.Source, stackTrace = e.StackTrace };
                Log.Error(error.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new { error.message });
                throw;
            }
        }
    }
}