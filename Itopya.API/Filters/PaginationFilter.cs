using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Itopya.Application.Models.Product;
using Itopya.Domain.Entities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Itopya.API.Filters
{
    public class PaginationFilter<T> : IAsyncActionFilter where T : class
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            if (resultContext.Result is OkObjectResult)
            {
                var result = resultContext.Result as OkObjectResult;
                var resultModel = result.Value as PagedList<T>;

             resultContext.HttpContext.Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(resultModel.MetaData));
               
            }
            
        }
    }
}