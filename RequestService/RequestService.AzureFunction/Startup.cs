using AutoMapper;
using RequestService.Core.Interfaces.Repositories;
using RequestService.Handlers;
using RequestService.Mappers;
using RequestService.Repo;
using MediatR;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

[assembly: FunctionsStartup(typeof(RequestService.AzureFunction.Startup))]
namespace RequestService.AzureFunction
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddMediatR(typeof(FunctionAHandler).Assembly);
            builder.Services.AddAutoMapper(typeof(AddressDetailsProfile).Assembly);

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                   options.UseInMemoryDatabase(databaseName: "RequestService.AzureFunction"));
            builder.Services.AddTransient<IRepository, Repository>();
        }
    }
}