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

        public virtual async Task<bool> Save(CTATTN oCTATTN)
        {
            ValidationResult result = await _validator.ValidateAsync(oCTATTN);
            if (!result.IsValid)
            {
                error = string.Concat("El valor ", oCTATTN.ATTNIDEN, " no puede ser menor a 0");
                existError = true;
                return false;
            }
            return oCTATTNDao.Save(oCTATTN);
        }
        public virtual bool Delete(int ATTIDEN)
        {
            if (Exist(ATTIDEN))
                return oCTATTNDao.Delete(ATTIDEN);
            else return false;
        }
        public virtual CTATTN Get(int ATTIDEN)
        {
            return oCTATTNDao.Get(ATTIDEN);
        }
        public virtual bool Exist(int ATTIDEN)
        {
            return oCTATTNDao.Exist(ATTIDEN);
        }
        public virtual List<CTATTN> List()
        {
            return oCTATTNDao.List();
        }

    }
}