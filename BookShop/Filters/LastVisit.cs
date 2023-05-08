using Microsoft.AspNetCore.Mvc.Filters;

namespace BookShop.Filters
{
    public class LastVisit : Attribute, IResourceFilter
    {
        //  Этот метод выплняется перед вызовом метода
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            context.HttpContext.Response.Cookies.Append("LastVisit", DateTime.Now.ToString("dd/mm/yyyy hh-mm"));
        }

        // Этот метод выполяется после вызова метода
        public void OnResourceExecuted(ResourceExecutedContext context)
        {

        }
    }
}
