using Artist_api1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Artist_api1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        public class AdminAuthMiddleware
        {
            private readonly RequestDelegate _sql;
            private const string AdminToken = "admin1234"; // или вынеси в конфиг

            public AdminAuthMiddleware(RequestDelegate sql)
            {
                _sql = sql;
            }



           



            public async Task Invoke(HttpContext context)
            {
                // Только для защищённых маршрутов
               // if (context.Request.Path.StartsWithSegments("/api/home") &&
              //      (context.Request.Method == "DELETE" || context.Request.Method == "PUT" || context.Request.Path.Value.Contains("add", StringComparison.OrdinalIgnoreCase)))
              //  {
              //      if (!context.Request.Headers.TryGetValue("Authorization", out var token) || token != AdminToken)
              //      {
               //         context.Response.StatusCode = 401; // Unauthorized
               //         await context.Response.WriteAsync("Unauthorized");
                //        return;
                 //   }
               // }

                await _sql(context);
            }
        }

    }
}
