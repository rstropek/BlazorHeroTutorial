using HeroesCore;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace HeroesWasm.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IMessageService, MessageService>();
            services.AddSingleton<IHeroService, HeroService>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<HeroesRazorLib.App>("app");
        }
    }
}
