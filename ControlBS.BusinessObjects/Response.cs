using System.Net;

namespace ControlBS.BusinessObjects
{

    public class Response<T>
    {
        public T? value { get; set; }
        public List<ErrorResponse> errors { get; set; } = new List<ErrorResponse>();
        public bool success => errors.Count == 0;
        public int statusCode { get; set; } = 200; //Se esta poniendo por default 200 ya que algunos métodos son por default;

        public Response()
        {

        }
        public Response(Exception e)
        {
            this.errors.Add(new ErrorResponse { message = e.Message, source = e.Source, stackTrace = e.StackTrace });
            statusCode = 500;
        }
    }
}