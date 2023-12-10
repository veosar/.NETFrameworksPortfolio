
# OutputCaching

This project showcases API output caching.

You can use docker compose to run API, Postgres Database and Redis, and run Benchmark in Release mode to test the results.

You can also switch caching method from InMemory to Redis by changing appsettings.CacheSettings.OutputCacheType to "Redis" (Please note that setting this in appsettings isn't a standard practice, it's for showcase example).

**Results:**

InMemory Cache:

Direct DB call:

| Method                                 | Mean      | Error    | StdDev    |
|--------------------------------------- |----------:|---------:|----------:|
| GetCustomersWithCache                  |  18.83 ms | 0.696 ms |  2.042 ms |
| GetCustomersWithoutCache               |  31.94 ms | 0.638 ms |  1.304 ms |
| GetCustomersWithCacheFollowedByPost    | 101.87 ms | 4.018 ms | 11.784 ms |
| GetCustomersWithoutCacheFollowedByPost | 207.95 ms | 3.736 ms |  6.444 ms |

**1.69** times faster for simple GET operations, **2.04** times faster for longer operation simulation.

Delayed DB call (5 seconds simulated overhead):

| Method                                 | Mean         | Error     | StdDev    |
|--------------------------------------- |-------------:|----------:|----------:|
| GetCustomersWithCache                  |     18.86 ms |  0.817 ms |  2.382 ms |
| GetCustomersWithoutCache               |  5,030.64 ms |  7.268 ms |  6.798 ms |
| GetCustomersWithCacheFollowedByPost    |  5,129.31 ms | 18.809 ms | 16.674 ms |
| GetCustomersWithoutCacheFollowedByPost | 30,218.94 ms | 27.318 ms | 22.812 ms |

**266.73** times faster for simple GET operations, **5.89** times faster for longer operation simulation.


Redis:

Direct DB call:

| Method                                 | Mean      | Error    | StdDev    |
|--------------------------------------- |----------:|---------:|----------:|
| GetCustomersWithCache                  |  15.56 ms | 0.762 ms |  2.248 ms |
| GetCustomersWithoutCache               |  35.08 ms | 0.698 ms |  0.908 ms |
| GetCustomersWithCacheFollowedByPost    | 130.44 ms | 5.233 ms | 15.430 ms |
| GetCustomersWithoutCacheFollowedByPost | 214.85 ms | 3.375 ms |  2.992 ms |

**2.25** times faster for simple GET operations, **1.64** times faster for longer operation simulation.

Delayed DB call (5 seconds simulated overhead):

| Method                                 | Mean         | Error     | StdDev    |
|--------------------------------------- |-------------:|----------:|----------:|
| GetCustomersWithCache                  |     13.17 ms |  0.596 ms |  1.749 ms |
| GetCustomersWithoutCache               |  5,028.87 ms |  4.650 ms |  4.122 ms |
| GetCustomersWithCacheFollowedByPost    |  5,114.44 ms | 23.890 ms | 21.178 ms |
| GetCustomersWithoutCacheFollowedByPost | 30,198.90 ms | 17.732 ms | 15.719 ms |

**381.84** times faster for simple GET operations, **5.90** times faster for longer operation simulation.

Conclusion: In this particular example it looks like using Redis for Output Caching is preferable, because it's both faster than  caching and can be distributed along multiple application nodes. It also won't consume as much memory as  options.
