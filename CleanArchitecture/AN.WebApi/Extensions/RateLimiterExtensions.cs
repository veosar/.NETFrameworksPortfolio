using System.Threading.RateLimiting;
using AN.WebApi.Common;
using Microsoft.AspNetCore.RateLimiting;

namespace AN.WebApi.Extensions;

public static class RateLimiterExtensions
{
    public static IServiceCollection AddRateLimiters(this IServiceCollection services)
    {
        services.AddRateLimiter(rateLimiterOptions =>
        {
            rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            rateLimiterOptions.AddFixedWindowLimiter(Constants.FixedRateLimiterPolicy, options =>
            {
                options.Window = TimeSpan.FromSeconds(10);
                options.PermitLimit = 4;
                options.QueueLimit = 4;
                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
            });
            rateLimiterOptions.AddSlidingWindowLimiter(Constants.SlidingRateLimiterPolicy, options =>
            {
                options.Window = TimeSpan.FromSeconds(15);
                options.SegmentsPerWindow = 3;
                options.PermitLimit = 15;
            });
            rateLimiterOptions.AddTokenBucketLimiter(Constants.TokenRateLimiterPolicy, options =>
            {
                options.TokenLimit = 100;
                options.ReplenishmentPeriod = TimeSpan.FromSeconds(5);
                options.TokensPerPeriod = 10;
            });

            rateLimiterOptions.AddConcurrencyLimiter(Constants.ConcurrencyRateLimiterPolicy, options =>
            {
                options.PermitLimit = 5;
            });
        });
        return services;
    }
}