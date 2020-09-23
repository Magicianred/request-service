using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using AzureFunctions.Extensions.Swashbuckle;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Hosting;
using RequestService.AzureFunction;

[assembly: WebJobsStartup(typeof(SwashBuckleStartup))]
namespace RequestService.AzureFunction
{
    internal class SwashBuckleStartup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.AddSwashBuckle(Assembly.GetExecutingAssembly());
        }
    }
}
