
# SQS-SNS

This is a project showcasing using AWS SQS along with SNS in order to publish messages from topic to two different queues. 
You can use docker-compose in order to deploy CustomersApi, CustomersEngagementTrackingService, CustomersNotificationsService, Postgres and MailHog on your local machine.

## CustomersApi

From the top level perspective, we are deploying CustomersApi in order to perform operations on simple Customer object that gets stored in the database. Please note that I understand that storing TotalMoneySpent as a field in Customers table is not the best practice - it is done this way for simplicity sake.

Every time we perform customer creation/update/deletion, message is sent to AWS SNS Topic. This topic then distributes the message based on filters and subscribers configured on AWS platform.

Here are CustomersApi endpoints:

| Command | Endpoint | Description
|--|--|--|
| GET | http://localhost:5000/customers| Gets all Customers|
| GET | http://localhost:5000/customers/{id} | Gets Customer with specific {id} |
| POST | http://localhost:5000/customers| Creates a new Customer|
| PUT | http://localhost:5000/customers/{id} | Updates a Customer with specific {id} |
| DELETE | http://localhost:5000/customers/{id}| Deletes a Customer with specific {id} |

## CustomersNotificationsService and CustomersEngagementTrackingService
These are our two subscribers that read messages from their configured queues. There is filtering logic both in app as well as on topic level to showcase the ability to use in-app filtering. 

These two subscriber services run background service that gets new messages and processes them using MediatR to map particular ISqsMessage to their Handler. 

They then send an email to MailHog instance to verify the results.

These two services can be easily scaled as shown in docker-compose-scaled.yml file.

## CustomersPublisher

This is a sample console application that will use Bogus to generate configurable number of messages for each message type. This allows for local testing as well as checking performance for our services.

You can do performance testing on docker-compose.yml and then try scaling number of subscribers by using docker-compose-scaled.yml. Adding new subscribers significantly reduced time to consume X amount of messages.

## CustomersContracts
This is just a class library to store our message types.
