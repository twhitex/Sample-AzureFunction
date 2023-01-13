using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace Sample.AzureFunction
{
    public class SeeOptionsFunction
    {
        private readonly MassTransitOptions _mtOptions;

        public SeeOptionsFunction(MassTransitOptions mtOptions) //IRequestClient<SubmitOrder> client
        {
            _mtOptions = mtOptions;
        }

        [FunctionName("option")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest request)
        {
            return new OkObjectResult(_mtOptions);
        }
    }
}