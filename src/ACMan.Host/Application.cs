namespace ACMan.Host
{
    using System;
    using System.Threading.Tasks;
    using ACMan.Host.Services;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    public class Application
    {
        private readonly IWebAppService webAppService;
        private readonly ILogger<Application> logger;
        private readonly IConfigurationRoot config;

        public Application(
                IWebAppService webAppService,
                IConfigurationRoot config,
                ILogger<Application> logger
            )
        {
            this.webAppService = webAppService;
            this.logger = logger;
            this.config = config;
        }

        public async Task Run()
        {
            this.logger.LogInformation(this.config["Host:Address"]);

            var input = "";

            do
            {
                Console.Write("{0}> ", "ACMan");
                ConsoleColor originalColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.White;

                input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    continue;
                }

                switch (input.ToLower())
                {
                    case "start":
                        await this.webAppService.Start();
                     break;
                    case "stop":
                        await this.webAppService.Stop();
                     break;
                }
                Console.ForegroundColor = originalColor;

            } while (input != "x");

        }
    }
}