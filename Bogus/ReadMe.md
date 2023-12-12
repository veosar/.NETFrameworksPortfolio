# Bogus
  

This project showcases Bogus library for generating fake data in .NET.

  

You can use docker compose to run API and Postgres Database.

 BogusApi project showcases the ability to generate fake data for complex models and using complex logic for Faker.

Available endpoints:

  

| Command | Endpoint | Description
|--|--|--|
| GET | http://localhost/fake/customer | Gets one instance of fake Customer |
| GET | http://localhost/fake/customer/{amount} | Gets {amount} number of fake Customer instances |
| GET | http://localhost/fake/item| Gets one instance of fake Item|
| GET | http://localhost/fake/item/{amount} | Gets {amount} number of fake Item instances |
| GET | http://localhost/fake/address| Gets one instance of fake Address|
| GET | http://localhost/fake/address/{amount} | Gets {amount} number of fake Address instances |
| GET | http://localhost/fake/order| Gets one instance of fake Order|
| GET | http://localhost/fake/order/{amount} | Gets {amount} number of fake Order instances |

