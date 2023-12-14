namespace S3Api.Models.Options;

public class AWSOptions
{
    public static string SectionName => "AWS";
    public string BucketName { get; set; }
    public string ImagesKeyPrefix { get; set; }
    public string ImagesBlurredKeyPrefix { get; set; }
    public string MetadataPrefix { get; set; }
    public string OriginalNameMetadataKey { get; set; }
    public string ExtensionMetadataKey { get; set; }
}