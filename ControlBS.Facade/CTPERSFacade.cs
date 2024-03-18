using System.Net;
using System.Reflection.Metadata.Ecma335;
using ControlBS.BusinessObjects;
using ControlBS.DataObjects;
using FluentValidation;
using FluentValidation.Results;

namespace ControlBS.Facade
{
    public partial class CTPERSFacade
    {
        private readonly CTPERSDao oCTPERSDao;
        private IValidator<CTPERS> _validator;
        private string error = "";
        private bool existError;

        public CTPERSFacade()
        {
            _validator = new CTPERSValidator();
            oCTPERSDao = new CTPERSDao();
        }
        public virtual string GetError() => error;

        public virtual bool ExistError() => existError;

        public virtual async Task<Response<bool>> Save(CTPERS oCTPERS)
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
                return oResponse;
            }
            return new Response<bool> { value = oCTPERSDao.Save(oCTPERS) };
        }
        public virtual Response<bool> Delete(int ATTIDEN)
        {
            Response<bool> oResponse = new Response<bool>();
            if (!oCTPERSDao.Exist(ATTIDEN))
            {
                oResponse.value = false;
                oResponse.errors.Add(new ErrorResponse { message = "No se ha encontrado el elemento para eliminar", source = "Facade - Validaciones", stackTrace = "" });
                return oResponse;
            }
            oResponse.value = oCTPERSDao.Delete(ATTIDEN);
            return oResponse;
        }
        public virtual Response<CTPERS?> Get(int ATTIDEN)
        {
            Response<CTPERS?> oResponse = new Response<CTPERS?>();
            oResponse.value = oCTPERSDao.Get(ATTIDEN);
            return oResponse;
        }
        public virtual Response<bool> Exist(int ATTIDEN)
        {
            Response<bool> oResponse = new Response<bool>();
            oResponse.value = oCTPERSDao.Exist(ATTIDEN);
            return oResponse;
        }
        public virtual Response<List<CTPERS>> List()
        {
            Response<List<CTPERS>> oResponse = new Response<List<CTPERS>>();
            oResponse.value = oCTPERSDao.List();
            return oResponse;
        }
    }
}