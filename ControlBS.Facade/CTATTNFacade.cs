using System.Data.Common;
using ControlBS.BusinessObjects;
using ControlBS.DataObjects;
using FluentValidation;
using FluentValidation.Results;
namespace ControlBS.Facade
{
    public partial class CTATTNFacade
    {
        private CTATTNDao oCTATTNDao;
        private IValidator<CTATTN> _validator;
        private string error = "";
        private bool existError;

        public CTATTNFacade()
        {
            _validator = new CTATTNValidator();
            oCTATTNDao = new CTATTNDao();
        }
        public virtual string GetError() => error;

        public virtual bool ExistError() => existError;

        public virtual async Task<Response<bool>> Save(CTATTN oCTATTN)
        {
            Response<bool> oResponse = new Response<bool>();
            ValidationResult result = await _validator.ValidateAsync(oCTATTN);
            if (!result.IsValid)
            {
                foreach (ValidationFailure failure in result.Errors)
                {
                    oResponse.errors.Add(new ErrorResponse { message = failure.ErrorMessage, source = "Facade - Validaciones", stackTrace = "" });
                }

                existError = true;
                return oResponse;
            }
            return new Response<bool> { value = oCTATTNDao.Save(oCTATTN) };
        }
        public virtual Response<bool> Delete(int ATTIDEN)
        {
            Response<bool> oResponse = new Response<bool>();
            if (!oCTATTNDao.Exist(ATTIDEN))
            {
                oResponse.value = false;
                oResponse.errors.Add(new ErrorResponse { message = "No se ha encontrado el elemento para eliminar", source = "Facade - Validaciones", stackTrace = "" });
                return oResponse;
            }
            oResponse.value = oCTATTNDao.Delete(ATTIDEN);
            return oResponse;
        }
        public virtual Response<CTATTN?> Get(int ATTIDEN)
        {
            Response<CTATTN?> oResponse = new Response<CTATTN?>();
            oResponse.value = oCTATTNDao.Get(ATTIDEN);
            return oResponse;
        }
        public virtual Response<bool> Exist(int ATTIDEN)
        {
            Response<bool> oResponse = new Response<bool>();
            oResponse.value = oCTATTNDao.Exist(ATTIDEN);
            return oResponse;
        }
        public virtual Response<List<CTATTN>> List()
        {
            Response<List<CTATTN>> oResponse = new Response<List<CTATTN>>();
            oResponse.value = oCTATTNDao.List();
            return oResponse;
        }

    }
}