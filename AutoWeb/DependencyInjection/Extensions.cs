using AutoWeb.Common;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoWeb.DependencyInjection
{
    public static class Extensions
    {

        public static IServiceCollection AddAutoWebService(this IServiceCollection services)
        {
            services.AddTransient<IAutoWeb, PageCollection>();
            return services;
        }

    }
}
