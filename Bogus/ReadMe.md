
# Bogus
  

This project showcases Bogus library for generating fake data in .NET.

 You can use docker compose to run both APIs and Postgres Database.

Configuration:

You can change "Seed" in order to generate a new set of data from Bogus, or set it to null to get a new set every time.
You can also change "Locale" so the data matches whatever locale you choose.

## BogusApi

BogusApi project showcases the ability to generate fake data for complex models and using complex logic for Faker by providing simple API to retrieve faked data.

Available endpoints:

  

| Command | Endpoint | Description
|--|--|--|
| GET | http://localhost:8080/fake/customer | Gets one instance of fake Customer |
| GET | http://localhost:8080/fake/customer/{amount} | Gets {amount} number of fake Customer instances |
| GET | http://localhost:8080/fake/item| Gets one instance of fake Item|
| GET | http://localhost:8080/fake/item/{amount} | Gets {amount} number of fake Item instances |
| GET | http://localhost:8080/fake/address| Gets one instance of fake Address|
| GET | http://localhost:8080/fake/address/{amount} | Gets {amount} number of fake Address instances |
| GET | http://localhost:8080/fake/order| Gets one instance of fake Order|
| GET | http://localhost:8080/fake/order/{amount} | Gets {amount} number of fake Order instances |

## BogusDBApi

BogusDBApi project showcases how Bogus can be used to populate a test database with fake data. For this purpose, Entity Framework Core is configured to apply InitialMigration to fresh postgres database every time Api is run and then it exposes two endpoints, one for seeding the data with X amount of Orders and another one for retrieving data from database.

Available endpoints:

  

| Command | Endpoint | Description
|--|--|--|
| GET | http://localhost:5678/orders | Gets Orders, along with its linked customers, addresses and items from database |
| POST | http://localhost:5678/seeddatabase/{amount} | Adds {amount} number of Order records in Postgres database, based on generated fake data |
