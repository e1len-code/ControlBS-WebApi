using ControlBS.BusinessObjects.Response;
using ControlBS.BusinessObjects;
using ControlBS.DataObjects;
using FluentValidation;
using FluentValidation.Results;
using ControlBS.BusinessObjects.Models;
using System.Net;
namespace ControlBS.Facade
{
    public partial class CTFILEFacade
    {
        private CTFILEDao oCTFILEDao;
        private IValidator<CTFILE> _validator;
        private string error = "";
        private bool existError;

        public CTFILEFacade()
        {
            _validator = new CTFILEValidator();
            oCTFILEDao = new CTFILEDao();
        }
        public virtual string GetError() => error;
        public virtual bool ExistError() => existError;

        public virtual async Task<Response<bool>> Save(CTFILE oCTFILE)
        {
            Response<bool> oResponse = new Response<bool>();
            ValidationResult result = await _validator.ValidateAsync(oCTFILE);
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
            string base64Image = oCTFILE.FILEBA64;
            byte[] imageBytes = Convert.FromBase64String(base64Image);
            string directoryPath = Path.Combine(Environment.CurrentDirectory, "Signatures");
            Directory.CreateDirectory(directoryPath); // Create the directory if it doesn't exist
            string filePath = Path.Combine(directoryPath, oCTFILE.FILENAME + ".png");
            File.WriteAllBytes(filePath, imageBytes);

            return new Response<bool> { value = oCTFILEDao.Save(oCTFILE) };
        }
        //     public virtual Response<bool> Delete(int ATTIDEN)
        //     {
        //         if (!oCTFILEDao.Exist(ATTIDEN))
        //         {
        //             return new Response<bool>(HttpStatusCode.NotFound);
        //         }
        //         return new Response<bool> { value = oCTFILEDao.Delete(ATTIDEN) };
        //     }
        //     public virtual Response<CTFILE?> Get(int ATTIDEN)
        //     {
        //         CTFILE? valueGet = oCTFILEDao.Get(ATTIDEN);
        //         if (valueGet == null)
        //         {
        //             return new Response<CTFILE?>(HttpStatusCode.NotFound);
        //         }
        //         return new Response<CTFILE?> { value = valueGet };
        //     }
        //     public virtual Response<bool> Exist(int ATTIDEN)
        //     {
        //         Response<bool> oResponse = new Response<bool>();
        //         oResponse.value = oCTFILEDao.Exist(ATTIDEN);
        //         return oResponse;
        //     }
        //     public virtual Response<List<CTFILE>> List()
        //     {
        //         Response<List<CTFILE>> oResponse = new Response<List<CTFILE>>();
        //         oResponse.value = oCTFILEDao.List();
        //         return oResponse;
        //     }

    }
}