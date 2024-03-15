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

        public virtual async Task<bool> Save(CTPERS oCTPERS)
        {
            ValidationResult result = await _validator.ValidateAsync(oCTPERS);
            if (!result.IsValid)
            {
                foreach (var failure in result.Errors)
                {
                    error += "Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage + "\n";
                }
                existError = true;
                return false;
            }
            return oCTPERSDao.Save(oCTPERS);
        }
        public virtual bool Delete(int ATTIDEN)
        {
            if (Exist(ATTIDEN))
                return oCTPERSDao.Delete(ATTIDEN);
            else return false;
        }
        public virtual CTPERS Get(int ATTIDEN)
        {
            return oCTPERSDao.Get(ATTIDEN);
        }
        public virtual bool Exist(int ATTIDEN)
        {
            return oCTPERSDao.Exist(ATTIDEN);
        }
        public virtual List<CTPERS> List()
        {
            return oCTPERSDao.List();
        }
    }
}