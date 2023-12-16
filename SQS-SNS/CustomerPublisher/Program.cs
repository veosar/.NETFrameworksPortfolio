using System.Text.Json;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Bogus;
using CustomerPublisher.Models.Options;
using CustomersContracts.Messages;
using Microsoft.Extensions.Configuration;

var snsOptions = new SNSOptions();
var bogusOptions = new BogusOptions();

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

config.GetSection(SNSOptions.Section)
    .Bind(snsOptions);

config.GetSection(BogusOptions.Section)
    .Bind(bogusOptions);

var amazonSns = new AmazonSimpleNotificationServiceClient();

var amountOfMessages = snsOptions.AmountOfMessagesSentPerMessageType;

var customerCreatedFaker = new Faker<CustomerCreated>(bogusOptions.Locale)
    .RuleFor(x => x.Id, _ => Guid.NewGuid())
    .RuleFor(x => x.FirstName, f => f.Name.FirstName())
    .RuleFor(x => x.LastName, f => f.Name.LastName())
    .RuleFor(x => x.Email, (f, c) => f.Internet.Email(c.FirstName, c.LastName))
    .RuleFor(x => x.TotalMoneySpent, _ => 0);

var customerUpdatedFaker = new Faker<CustomerUpdated>(bogusOptions.Locale)
    .RuleFor(x => x.Id, _ => Guid.NewGuid())
    .RuleFor(x => x.FirstName, f => f.Name.FirstName())
    .RuleFor(x => x.LastName, f => f.Name.LastName())
    .RuleFor(x => x.Email, (f, c) => f.Internet.Email(c.FirstName, c.LastName))
    .RuleFor(x => x.OldTotalMoneySpent, f => f.Random.Decimal(0M, 1500M))
    .RuleFor(x => x.NewTotalMoneySpent, f => f.Random.Decimal(800M, 1500M));

var customerDeletedFaker = new Faker<CustomerDeleted>(bogusOptions.Locale)
    .RuleFor(x => x.Id, _ => Guid.NewGuid())
    .RuleFor(x => x.FirstName, f => f.Name.FirstName())
    .RuleFor(x => x.LastName, f => f.Name.LastName())
    .RuleFor(x => x.Email, (f, c) => f.Internet.Email(c.FirstName, c.LastName));

var topicArn = await amazonSns.FindTopicAsync(snsOptions.TopicName);

for (var i = 0; i < amountOfMessages; i++)
{
    var customerCreated = customerCreatedFaker.Generate();
    var publishRequest = new PublishRequest
    {
        TopicArn = topicArn.TopicArn,
        Message = JsonSerializer.Serialize(customerCreated),
        MessageAttributes = new Dictionary<string, MessageAttributeValue>
        {
            {
                snsOptions.MessageTypeAttributeKey, new MessageAttributeValue
                {
                    DataType = "String",
                    StringValue = nameof(CustomerCreated)
                }
            }
        }
    };
    
    await amazonSns.PublishAsync(publishRequest);    
}

for (var i = 0; i < amountOfMessages; i++)
{
    var customerUpdated = customerUpdatedFaker.Generate();
    var publishRequest = new PublishRequest
    {
        TopicArn = topicArn.TopicArn,
        Message = JsonSerializer.Serialize(customerUpdated),
        MessageAttributes = new Dictionary<string, MessageAttributeValue>
        {
            {
                snsOptions.MessageTypeAttributeKey, new MessageAttributeValue
                {
                    DataType = "String",
                    StringValue = nameof(CustomerUpdated)
                }
            }
        }
    };
    
    await amazonSns.PublishAsync(publishRequest);    
}

for (var i = 0; i < amountOfMessages; i++)
{
    var customerDeleted = customerDeletedFaker.Generate();
    var publishRequest = new PublishRequest
    {
        TopicArn = topicArn.TopicArn,
        Message = JsonSerializer.Serialize(customerDeleted),
        MessageAttributes = new Dictionary<string, MessageAttributeValue>
        {
            {
                snsOptions.MessageTypeAttributeKey, new MessageAttributeValue
                {
                    DataType = "String",
                    StringValue = nameof(CustomerDeleted)
                }
            }
        }
    };
    
    await amazonSns.PublishAsync(publishRequest);    
}