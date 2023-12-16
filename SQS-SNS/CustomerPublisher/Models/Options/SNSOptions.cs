namespace CustomerPublisher.Models.Options;

public class SNSOptions
{
    public static string Section = "SNS";
    public string TopicName { get; set; }
    public string MessageTypeAttributeKey { get; set; }
    public int AmountOfMessagesSentPerMessageType { get; set; }
}