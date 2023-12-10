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


