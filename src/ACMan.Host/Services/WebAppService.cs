namespace ACMan.Host.Services
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    class WebAppService: IWebAppService
    {
        private readonly ILogger<WebAppService> logger;
        private readonly IConfigurationRoot config;
        private IWebHost webHost;

        public WebAppService(ILogger<WebAppService> logger, IConfigurationRoot config)
        {
            this.logger = logger;
            this.config = config;
        }

        public async Task Start()
        {
            logger.LogInformation("Starting Web App");

            if(this.webHost == null)
            {
                logger.LogInformation("Creating new WebHost");
                this.webHost = BuildWebHost();
            }

            await webHost.StartAsync();

            logger.LogInformation("Web App is running");
        }

        public async Task Stop()
        {
            logger.LogInformation("Stopping Web App");

            if(this.webHost == null)
            {
                logger.LogInformation("No WebHost running");
                return;
            }

            await webHost.StopAsync();

            logger.LogInformation("Web App is stopped");
        }

        private IWebHost BuildWebHost() =>
            WebHost.CreateDefaultBuilder()
            .UseContentRoot(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .UseStartup<ACMan.Web.Startup>()
                .ConfigureLogging(
                    logging => {
                            logging.AddConfiguration(this.config.GetSection("Logging"));
                        }
                    )
                .Build();
    }
}