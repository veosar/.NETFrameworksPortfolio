using System.Net;
using System.Net.Mail;
using Amazon.SQS;
using CustomersEngagementTrackingService.HostedServices;
using CustomersEngagementTrackingService.Models.Options;
using CustomersEngagementTrackingService.Services;

var builder = WebApplication.CreateBuilder(args);

var port = Environment.GetEnvironmentVariable("APP_PORT");
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(int.Parse(port));
});

builder.Services.AddSingleton<IAmazonSQS, AmazonSQSClient>();
builder.Services.Configure<QueueOptions>(builder.Configuration.GetSection(QueueOptions.Section));
builder.Services.Configure<EmailOptions>(builder.Configuration.GetSection(EmailOptions.Section));
builder.Services.Configure<EngagementOptions>(builder.Configuration.GetSection(EngagementOptions.Section));
var emailOptions = builder.Configuration.GetSection(EmailOptions.Section)
    .Get<EmailOptions>();
builder.Services.AddHostedService<QueueConsumerService>();
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddFluentEmail(emailOptions.Sender)
    .AddRazorRenderer()
    .AddSmtpSender(() => new SmtpClient(emailOptions.Server, emailOptions.Port)
    {
        EnableSsl = false, // This is obviously not the best practice, done for docker-showcase reasons
        Credentials = new NetworkCredential
        {
            UserName = emailOptions.Username,
            Password = emailOptions.Password
        }
    });

builder.Services.AddScoped<ICustomerEmailService, CustomerEmailService>();
var app = builder.Build();
app.Run();