using ControlBS.Facade;

namespace ControlBS.WebApi.Utils.Auth{
    public class JwtMiddleware{
        private readonly RequestDelegate _next;

        public JwtMiddleware (RequestDelegate next){
            _next = next;
        }

        public async Task Invoke (HttpContext context , IJwtUtils jwtUtils){
            CTPERSFacade oCTPERSFacade = new CTPERSFacade();
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateJwtToken(token);
            if (userId != null){
                //attach user to context on successful jwt validation

                context.Items["User"] = oCTPERSFacade.Get(userId);
            }
            await _next(context);
        }
    }
}