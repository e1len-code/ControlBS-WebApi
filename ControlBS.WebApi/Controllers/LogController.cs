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
                    string message = String.Format("Excepcion: '{0}'\nSource: \nStackTrace: \n'", "El archivo de log no existe.");
                    Log.Error(message);
                    return StatusCode(500, new { error = "El archivo de log no existe." });
                }
                var fileStream = new FileStream(_logFilePath, FileMode.Open, FileAccess.Read);
                return File(fileStream, "text/plain", Path.GetFileName(_logFilePath));
            }
            catch (Exception e)
            {
                string message = String.Format("Excepcion: '{0}'\nSource: {1}\nStackTrace: '{2}\n'", e.Message, e.Source, e.StackTrace);
                Log.Error(message);
                throw;
            }
        }
    }
}