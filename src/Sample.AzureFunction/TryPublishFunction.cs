using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Sample.AzureFunction
{
    public class TryPublishFunction
    {
        readonly IAsyncBusHandle _handle;
        private readonly IPublishEndpoint _publishEndpoint;

        public TryPublishFunction(IAsyncBusHandle handle, IPublishEndpoint publishEndpoint) //IRequestClient<SubmitOrder> client
        {
            _handle = handle;
            _publishEndpoint = publishEndpoint;
        }

        [FunctionName("tryPublish")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest request, ILogger logger)
        {
            await _publishEndpoint.Publish(new
            {
                OrderId = 123
            });

            return new OkObjectResult(new
            {
                Status = "200"
            });
        }
    }
}