using ControlBS.BusinessObjects.Response;
using ControlBS.BusinessObjects;
using ControlBS.DataObjects;
using FluentValidation;
using FluentValidation.Results;
using ControlBS.BusinessObjects.Auth;
using System.Net;
using ControlBS.BusinessObjects.Models;
using ControlBS.BusinessObjects.Security;

namespace ControlBS.Facade
{
    public partial class CTPERSFacade
    {
        private readonly CTPERSDao oCTPERSDao;
        private readonly CTFILEFacade oCTFILEFacade;
        private IValidator<CTPERSSaveRequest> _validator;
        private string error = "";
        private bool existError;

        public CTPERSFacade()
        {
            _validator = new CTPERSValidator();
            oCTFILEFacade = new CTFILEFacade();
            oCTPERSDao = new CTPERSDao();
        }
        public virtual string GetError() => error;

        public virtual bool ExistError() => existError;

        public virtual async Task<Response<bool>> Save(CTPERSSaveRequest oCTPERS)
        {
            Response<bool> oResponse = new Response<bool>();
            ValidationResult result = await _validator.ValidateAsync(oCTPERS);
            if (!result.IsValid)
            {
                foreach (ValidationFailure failure in result.Errors)
                {
                    oResponse.errors.Add(new ErrorResponse { message = failure.ErrorMessage, source = "Facade - Validaciones", stackTrace = "" });
                }
                existError = true;
                oResponse.statusCode = HttpStatusCode.BadRequest;
                return oResponse;
            }
            return new Response<bool> { value = oCTPERSDao.Save(oCTPERS) };
        }
        public virtual Response<bool> Delete(int PERSIDEN)
        {
            if (!oCTPERSDao.Exist(PERSIDEN))
            {
                return new Response<bool>(HttpStatusCode.NotFound);
            }
            return new Response<bool> { value = oCTPERSDao.Delete(PERSIDEN) };
        }
        public virtual Response<CTPERS?> Get(int? PERSIDEN)
        {
            if (PERSIDEN == null)
            {
                return new Response<CTPERS?>(HttpStatusCode.NotFound);
            }
            CTPERS? oCTPERS = oCTPERSDao.Get((int)PERSIDEN);
            if (oCTPERS == null)
            {
                return new Response<CTPERS?>(HttpStatusCode.NotFound);
            }
            return new Response<CTPERS?> { value = oCTPERS };
        }
        public virtual Response<bool> Exist(int PERSIDEN)
        {
            Response<bool> oResponse = new Response<bool>();
            oResponse.value = oCTPERSDao.Exist(PERSIDEN);
            return oResponse;
        }
        public virtual Response<List<CTPERS>> List()
        {
            Response<List<CTPERS>> oResponse = new Response<List<CTPERS>>();
            oResponse.value = oCTPERSDao.List();
            return oResponse;
        }
        public virtual Response<CTPERS?> AuthLogin(AuthRequest oAuthRequest)
        {
            Response<CTPERS?> oResponse = new Response<CTPERS?>();
            if (oAuthRequest.userName == null || oAuthRequest.userName.Trim() == "")
            {
                oResponse.errors.Add(new ErrorResponse { message = "El usuario no puede ser vacío o nulo", source = "Auth - Facade", stackTrace = "" });
                oResponse.statusCode = HttpStatusCode.BadRequest;
                return oResponse;
            }
            if (oAuthRequest.password == null || oAuthRequest.password.Trim() == "")
            {
                oResponse.errors.Add(new ErrorResponse { message = "La contraseña no puede ser vacío o nulo", source = " Auth - Facade", stackTrace = "" });
                oResponse.statusCode = HttpStatusCode.BadRequest;
                return oResponse;
            }

            CTPERS? oCTPERS = oCTPERSDao.Auth(oAuthRequest);
            if (oCTPERS != null)
            {
                oResponse.value = oCTPERS;
            }
            else
            {
                oResponse.errors.Add(new ErrorResponse { message = "El usuario y/o contraseña son incorrectos", source = " Auth - Facade", stackTrace = "" });
                oResponse.statusCode = HttpStatusCode.NotFound;
            }

            return oResponse;
        }
        public virtual Response<bool> UpdatePassword(CTPEUP o)
        {
            Response<bool> oResponse = new Response<bool>();
            if (o.PERSIDEN == 0)
            {
                oResponse.errors.Add(new ErrorResponse { message = "El identificador del usuario no puede ser 0", source = "UpdatePassword - Facade", stackTrace = "" });
                oResponse.statusCode = HttpStatusCode.BadRequest;
                return oResponse;
            }
            if (o.PERSPASS == null || o.PERSPASS.Trim() == "")
            {
                oResponse.errors.Add(new ErrorResponse { message = "La contraseña no puede ser vacía o nula", source = "UpdatePassword - Facade", stackTrace = "" });
                oResponse.statusCode = HttpStatusCode.BadRequest;
                return oResponse;
            }
            oResponse.value = oCTPERSDao.UpdatePassword(o);
            return oResponse;
        }
    }

}