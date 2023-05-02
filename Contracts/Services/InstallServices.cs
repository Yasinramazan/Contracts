﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Contracts.Services
{
    public static class InstallServices
    {
        public static IServiceCollection AddService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ApiService>();
            serviceCollection.AddScoped<ContractService.ContractService>();
            return serviceCollection;
        }
    }
}
