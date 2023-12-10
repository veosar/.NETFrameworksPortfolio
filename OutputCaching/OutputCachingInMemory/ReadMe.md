# OutputCachingInMemory

This project showcases in memory API output caching.

You can use docker compose to run API and Postgres Database, and run Benchmark in Release mode to test the results.

These are sample results of simple GetCustomers request, along with simulation of longer operation:

| Method                                 | Mean      | Error    | StdDev    |
|--------------------------------------- |----------:|---------:|----------:|
| GetCustomersWithCache                  |  10.00 ms | 0.479 ms |  1.405 ms |
| GetCustomersWithoutCache               |  27.48 ms | 0.547 ms |  1.535 ms |
| GetCustomersWithCacheFollowedByPost    |  90.44 ms | 4.268 ms | 12.585 ms |
| GetCustomersWithoutCacheFollowedByPost | 187.49 ms | 3.737 ms | 10.961 ms |

We can observe that in this particular example, caching gave us 2.7 times faster result with simple GET operations, and 2.07 times faster result with more complex simulation, when compared to not using cache.

Please note that the usefulness of output caching scales massively with time that is required to perform the operation. Here are the same benchmark's results when simulating a large delay on endpoint by awaiting Task.Delay(1000):

| Method                                 | Mean           | Error        | StdDev       |
|--------------------------------------- |---------------:|-------------:|-------------:|
| GetCustomersWithCache                  |       615.1 us |     12.95 us |     37.57 us |
| GetCustomersWithoutCache               | 1,011,811.9 us | 13,431.43 us | 12,563.76 us |
| GetCustomersWithCacheFollowedByPost    | 1,021,605.4 us | 17,022.94 us | 15,923.27 us |
| GetCustomersWithoutCacheFollowedByPost | 6,072,676.1 us | 26,756.46 us | 23,718.91 us |

Here can observe that when operation to fetch data from DB takes longer (as simulated by Task.Delay), caching gave us 16449 times faster result with simple GET operations, and 5.94 times faster result with more complex simulation, when compared to not using cache.
This is a much more observable difference.