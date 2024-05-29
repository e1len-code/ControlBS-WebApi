using ControlBS.BusinessObjects.Response;
using ControlBS.BusinessObjects;
using ControlBS.DataObjects;
using FluentValidation;
using FluentValidation.Results;
using ControlBS.BusinessObjects.Models;
using System.Net;
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
                oResponse.statusCode = HttpStatusCode.BadRequest;
                existError = true;
                return oResponse;
            }
            return new Response<bool> { value = oCTATTNDao.Save(oCTATTN) };
        }
        public virtual Response<bool> Delete(int ATTIDEN)
        {
            if (!oCTATTNDao.Exist(ATTIDEN))
            {
                return new Response<bool>(HttpStatusCode.NotFound);
            }
            return new Response<bool> { value = oCTATTNDao.Delete(ATTIDEN) };
        }
        public virtual Response<CTATTN?> Get(int ATTIDEN)
        {
            CTATTN? valueGet = oCTATTNDao.Get(ATTIDEN);
            if (valueGet == null)
            {
                return new Response<CTATTN?>(HttpStatusCode.NotFound);
            }
            return new Response<CTATTN?> { value = valueGet };
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
        public virtual Response<List<CTATTNFilterResponse>> FilterList(CTATTNFilterRequest oCTATTNFilterRequest)
        {
            Response<List<CTATTNFilterResponse>> oResponse = new Response<List<CTATTNFilterResponse>>();
            oResponse.value = oCTATTNDao.FilterList(oCTATTNFilterRequest);
            return oResponse;
        }
        public virtual Response<List<CTATTNResponseReport>> Report(CTATTNFilterRequest oCTATTNFilterRequest)
        {
            List<CTATTNResponseReport> listReport = new List<CTATTNResponseReport>();
            List<CTATTNFilterResponse> listFilter = oCTATTNDao.FilterList(oCTATTNFilterRequest);
            foreach (CTATTNFilterResponse oFilter in listFilter)
            {
                CTATTNResponseReport? itemReport = listReport.Find(x => DateOnly.FromDateTime(oFilter.ATTNDATE) == x.FECHA);
                if (itemReport == null)
                {
                    itemReport = new CTATTNResponseReport()
                    {
                        NOMBRES_Y_APELLIDOS = oFilter.PERSNAME,
                        OBSERVACION = oFilter.ATTNOBSE,
                        FECHA = DateOnly.FromDateTime(oFilter.ATTNDATE)
                    };
                }
                else
                {
                    listReport.Remove(itemReport);
                }
                switch (oFilter.ATTNLINE)
                {
                    case 1:
                        itemReport.HORA_INGRESO = TimeOnly.FromDateTime(oFilter.ATTNDATE);
                        break;
                    case 2:
                        itemReport.HORA_ALMUERZO = TimeOnly.FromDateTime(oFilter.ATTNDATE);
                        break;
                    case 3:
                        itemReport.HORA_ALMUERZO_FINAL = TimeOnly.FromDateTime(oFilter.ATTNDATE);
                        break;
                    case 4:
                        itemReport.HORA_SALIDA = TimeOnly.FromDateTime(oFilter.ATTNDATE);
                        break;
                }
                listReport.Add(itemReport);
            }

            return new Response<List<CTATTNResponseReport>> { value = listReport };
        }

    }
}