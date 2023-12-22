using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;

namespace PowerCalculator.WebApi.Extensions
{
    /// <summary>
    /// Configuration of compression for response.
    /// </summary>
    public static class CompressionsServiceCollectionExtension
    {
        /// <summary>
        /// Add option to compression Response, reduce size.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> for adding services.</param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddResponseCompressionOptions(this IServiceCollection services)
        {
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
                options.Providers.Add<BrotliCompressionProvider>();
            });

            return services;
        }
    }
}
