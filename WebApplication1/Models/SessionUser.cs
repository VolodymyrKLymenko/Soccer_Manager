using DAL.Model_Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Infrastructure;
namespace WebApplication1.Models
{
    public static class Organaizer
    {

        public static int GetOrganaizerUser(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;
            int userId = session?.GetJson<int>("Organaizer")
                ?? -1;

            return userId;
        }

        public static void SetOrganaizerUser(IServiceProvider services, Tournament tournament)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

            session.SetJson("Organaizer", tournament);
        }

    }
}
