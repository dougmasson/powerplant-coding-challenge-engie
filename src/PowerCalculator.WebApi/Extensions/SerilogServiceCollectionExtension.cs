using Serilog;

namespace PowerCalculator.WebApi.Extensions
{
    /// <summary>
    /// Configuratin of Serilog.
    /// </summary>
    public static class SerilogServiceCollectionExtension
    {
        /// <summary>
        /// Use Serilog.
        /// </summary>
        /// <param name="hostBuilder">The host builder.</param>
        public static void AddSerilog(this IHostBuilder hostBuilder)
        {
            hostBuilder.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
        }

        /// <summary>
        /// Use logs on requests.
        /// </summary>
        /// <param name="app">The application builder.</param>
        public static void UseSerilogRequest(this IApplicationBuilder app)
        {
            //Add support to logging request with SERILOG
            app.UseSerilogRequestLogging();
        }
    }
}
