using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;

namespace QueueTrigger
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task Run([QueueTrigger("todos", Connection = "")]string myQueueItem,
             [Blob("todos", Connection = "AzureWebJobsStorage")] CloudBlobContainer container,
             ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
            await container.CreateIfNotExistsAsync();
            var blob = container.GetBlockBlobReference($"{myQueueItem}.txt");
            await blob.UploadTextAsync($"Created a new task: {myQueueItem}");
        }
    }
}