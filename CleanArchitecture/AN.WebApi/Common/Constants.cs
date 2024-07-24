namespace AN.WebApi.Common;

public static class Constants
{
    public const string FixedRateLimiterPolicy = "fixed";
    public const string SlidingRateLimiterPolicy = "sliding";
    public const string TokenRateLimiterPolicy = "token";
    public const string ConcurrencyRateLimiterPolicy = "concurrency";
}