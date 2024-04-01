using System.Net;

namespace ControlBS.BusinessObjects.Response
{

    public class Response<T>
    {
        public T? value { get; set; }
        public List<ErrorResponse> errors { get; set; } = new List<ErrorResponse>();
        public bool success => errors.Count == 0;
        public HttpStatusCode statusCode { get; set; } = HttpStatusCode.OK; //Se esta poniendo por default 200;

        public Response()
        {

        }
        public Response(Exception e)
        {
            this.errors.Add(new ErrorResponse { message = e.Message, source = e.Source, stackTrace = e.StackTrace });
            statusCode = HttpStatusCode.InternalServerError;
        }
        public Response(HttpStatusCode httpStatusCode){
            switch(httpStatusCode){
                case HttpStatusCode.Unauthorized:{
                    this.errors.Add(new ErrorResponse{message = "La peticion no esta autorizada", source = "Controller - Authorize", stackTrace = ""});
                    this.statusCode = HttpStatusCode.Unauthorized;
                    break;
                }
                case HttpStatusCode.NotFound:{
                    this.errors.Add(new ErrorResponse{message = "No se ha encontrado el elemento", source = "Facade - Validaciones", stackTrace = ""});
                    this.statusCode = HttpStatusCode.NotFound;
                    break;
                }
                default : break;
            }
        }
    }
}