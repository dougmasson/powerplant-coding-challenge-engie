using Serilog;

namespace PowerCalculator.WebApi.Extensions
{
    /// <summary>
    /// Configuratin of Serilog.
    /// </summary>
    public static class SerilogStartupExtension
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
        /// <returns>WebApplication configured.</returns>
        public static IApplicationBuilder UseSerilogRequest(this IApplicationBuilder app)
        {
            app.UseSerilogRequestLogging(options =>
            {
                options.MessageTemplate = "{RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";

                options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                {
                    diagnosticContext.Set("CorellationId", httpContext.Items["x-correlation-id"]);
                };
            });

            return app;
        }
    }
}
