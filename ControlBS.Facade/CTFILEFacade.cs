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
            string base64Image = oCTFILE.FILEBA64!;
            byte[] imageBytes = Convert.FromBase64String(base64Image);
            string filePath = Path.Combine(Environment.CurrentDirectory, oCTFILE.FILEPATH!);
            File.WriteAllBytes(filePath, imageBytes);

            return new Response<bool> { value = oCTFILEDao.Save(oCTFILE) };
        }
        public virtual Response<string?> Get(string filepath)
        {
            Response<String?> oResponse = new Response<String?>();
            CTFILE? oCTFILE = oCTFILEDao.GetFile(filepath);
            if (oCTFILE == null)
            {
                oResponse.statusCode = HttpStatusCode.NotFound;
                return oResponse;
            }
            byte[] imageBytes = File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, oCTFILE.FILEPATH!));
            string base64Image = Convert.ToBase64String(imageBytes);
            oCTFILE.FILEBA64 = base64Image;
            oResponse.value = oCTFILE.FILEBA64;
            return oResponse;
        }
    }
}