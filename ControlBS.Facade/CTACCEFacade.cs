using ControlBS.BusinessObjects.Response;
using ControlBS.DataObjects;
using FluentValidation;
using FluentValidation.Results;
using System.Net;
using ControlBS.BusinessObjects.Security;
namespace ControlBS.Facade
{
    public partial class CTACCEFacade
    {
        private CTACCEDao oCTACCEDao;
        private IValidator<CTACCE> _validator;
        private string error = "";
        private bool existError;

        public CTACCEFacade()
        {
            //_validator = new CTACCEValidator();
            oCTACCEDao = new CTACCEDao();
        }
        public virtual string GetError() => error;
        public virtual bool ExistError() => existError;

        // public virtual async Task<Response<bool>> Save(CTACCE oCTACCE)
        // {
        //     Response<bool> oResponse = new Response<bool>();
        //     ValidationResult result = await _validator.ValidateAsync(oCTACCE);
        //     if (!result.IsValid)
        //     {
        //         foreach (ValidationFailure failure in result.Errors)
        //         {
        //             oResponse.errors.Add(new ErrorResponse { message = failure.ErrorMessage, source = "Facade - Validaciones", stackTrace = "" });
        //         }
        //         oResponse.statusCode = HttpStatusCode.BadRequest;
        //         existError = true;
        //         return oResponse;
        //     }
        //     return new Response<bool> { value = oCTACCEDao.Save(oCTACCE) };
        // }
        // public virtual Response<bool> Delete(int ATTIDEN)
        // {
        //     if (!oCTACCEDao.Exist(ATTIDEN))
        //     {
        //         return new Response<bool>(HttpStatusCode.NotFound);
        //     }
        //     return new Response<bool> { value = oCTACCEDao.Delete(ATTIDEN) };
        // }
        // public virtual Response<CTACCE?> Get(int ATTIDEN)
        // {
        //     CTACCE? valueGet = oCTACCEDao.Get(ATTIDEN);
        //     if (valueGet == null)
        //     {
        //         return new Response<CTACCE?>(HttpStatusCode.NotFound);
        //     }
        //     return new Response<CTACCE?> { value = valueGet };
        // }
        // public virtual Response<bool> Exist(int ATTIDEN)
        // {
        //     Response<bool> oResponse = new Response<bool>();
        //     oResponse.value = oCTACCEDao.Exist(ATTIDEN);
        //     return oResponse;
        // }
        // public virtual Response<List<CTACCE>> List()
        // {
        //     Response<List<CTACCE>> oResponse = new Response<List<CTACCE>>();
        //     oResponse.value = oCTACCEDao.List();
        //     return oResponse;
        // }
        public virtual Response<List<CTACCE>> ListAccess(int oPERSIDEN)
        {
            Response<List<CTACCE>> oResponse = new Response<List<CTACCE>>();
            oResponse.value = oCTACCEDao.ListAccess(oPERSIDEN);
            return oResponse;
        }
    }
}