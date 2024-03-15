using ControlBS.BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using ControlBS.Facade;
using ExceptionCustom = ControlBS.BusinessObjects.Excepcion;
using Serilog;

namespace ControlBS.WebApi.Controllers
{
    [Route("[controller]")]
    public class AttendanceController : Controller
    {
        private ExceptionCustom error;
        private CTATTNFacade oCTATTNFacade;
        public AttendanceController()
        {
            oCTATTNFacade = new CTATTNFacade();
            error = new ExceptionCustom();
        }
        [HttpGet]
        public IActionResult List()
        {
            try
            {
                CTATTNFacade oCTATTNFacade = new CTATTNFacade();
                return Ok(oCTATTNFacade.List());
            }
            catch (Exception e)
            {
                error = new ExceptionCustom { message = e.Message, source = e.Source, stackTrace = e.StackTrace };
                Log.Error(error.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new { error.message });
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] CTATTN oCTATTN)
        {
            try
            {
                bool _saved = await oCTATTNFacade.Save(oCTATTN);
                if (_saved)
                {
                    return Ok(_saved);
                }
                else
                {
                    error = new ExceptionCustom { message = oCTATTNFacade.GetError(), source = "", stackTrace = "" };
                    Log.Error(error.ToString());
                    return StatusCode(StatusCodes.Status400BadRequest, new { error.message })
                    ;
                }
            }
            catch (Exception e)
            {
                error = new ExceptionCustom { message = e.Message, source = e.Source, stackTrace = e.StackTrace };
                Log.Error(error.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new { error.message });
                throw;
            }
        }
        [HttpGet("{ATTNIDEN}")]
        public IActionResult Get(int ATTIDEN)
        {
            try
            {
                return Ok(oCTATTNFacade.Get(ATTIDEN));
            }
            catch (Exception e)
            {
                error = new ExceptionCustom { message = e.Message, source = e.Source, stackTrace = e.StackTrace };
                Log.Error(error.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new { error.message });
                throw;
            }
        }
        [HttpDelete("{ATTNIDEN}")]
        public IActionResult Delete(int ATTIDEN)
        {
            try
            {
                CTATTNFacade oCTATTNFacade = new CTATTNFacade();
                return Ok(oCTATTNFacade.Delete(ATTIDEN));
            }
            catch (Exception e)
            {
                error = new ExceptionCustom { message = e.Message, source = e.Source, stackTrace = e.StackTrace };
                Log.Error(error.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, new { error.message });
                throw;
            }
        }
    }
}