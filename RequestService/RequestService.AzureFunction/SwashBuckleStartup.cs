using AzureFunctions.Extensions.Swashbuckle;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using RequestService.AzureFunction;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

[assembly: WebJobsStartup(typeof(SwashBuckleStartup))]
namespace RequestService.AzureFunction
{
    internal class SwashBuckleStartup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            //Register the extension
            builder.AddSwashBuckle(Assembly.GetExecutingAssembly());

        }
    }
}
