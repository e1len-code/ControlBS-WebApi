using ControlBS.BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using ControlBS.Facade;
using Serilog;
using FluentValidation;
using ExcepcionCustom = ControlBS.BusinessObjects.Excepcion;
using ControlBS.DataObjects;

namespace ControlBS.WebApi.Controllers
{
    [Route("[controller]")]
    public class PersonController : Controller
    {
        private ExcepcionCustom error;
        private CTPERSFacade oCTPERSFacade;

        public PersonController()
        {
            oCTPERSFacade = new CTPERSFacade();
            error = new ExcepcionCustom();
        }

        [HttpGet]
        public IActionResult List()
        {
            try
            {
                return Ok(oCTPERSFacade.List());
            }
            catch (Exception e)
            {
                error = new ExcepcionCustom { message = e.Message, source = e.Source, stackTrace = e.StackTrace };
                Log.Error(error.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new { error.message });
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] CTPERS oCTPERS)
        {
            try
            {
                bool _saved = await oCTPERSFacade.Save(oCTPERS);
                if (_saved)
                {
                    return Ok(_saved);
                }
                else
                {
                    error = new Excepcion { message = oCTPERSFacade.GetError(), source = "", stackTrace = "" };
                    Log.Error(error.ToString());
                    return StatusCode(StatusCodes.Status400BadRequest, new { error.message });
                }
            }
            catch (Exception e)
            {
                error = new Excepcion { message = e.Message, source = e.Source, stackTrace = e.StackTrace };
                Log.Error(error.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new { error.message });
                throw;
            }
        }
        [HttpGet("{PERSIDEN}")]
        public IActionResult Get(int PERSIDEN)
        {
            try
            {
                return Ok(oCTPERSFacade.Get(PERSIDEN));
            }
            catch (Exception e)
            {
                error = new Excepcion { message = e.Message, source = e.Source, stackTrace = e.StackTrace };
                Log.Error(error.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new { error.message });
                throw;
            }
        }
        [HttpDelete("{PERSIDEN}")]
        public IActionResult Delete(int PERSIDEN)
        {
            try
            {
                return Ok(oCTPERSFacade.Delete(PERSIDEN));
            }
            catch (Exception e)
            {
                error = new Excepcion { message = e.Message, source = e.Source, stackTrace = e.StackTrace };
                Log.Error(error.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new { error.message });
                throw;
            }
        }
    }
}