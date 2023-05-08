using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using System.Text;

namespace BookShop.Filters
{
    public class TimeElapsed : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Stopwatch timer = Stopwatch.StartNew();
            await next();
            timer.Stop();

            string result = "<p>ElapsedTime: " +
                $"{timer.Elapsed.TotalMilliseconds} ms</p>" ;
            byte[] bytes = Encoding.ASCII.GetBytes(result);

            await context.HttpContext.Response.Body.WriteAsync(bytes, 0, bytes.Length);
        }


    }
}
