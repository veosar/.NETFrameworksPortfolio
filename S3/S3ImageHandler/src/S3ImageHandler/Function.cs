using System.Net.Mime;
using Amazon.Lambda.Core;
using Amazon.Lambda.S3Events;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.Processing;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace S3ImageHandler;

public class Function
{
    IAmazonS3 S3Client { get; set; } = new AmazonS3Client();
    
    public async Task FunctionHandler(S3Event evnt, ILambdaContext context)
    {
        var eventRecords = evnt.Records ?? new List<S3Event.S3EventNotificationRecord>();
        foreach (var record in eventRecords)
        {
            var s3Event = record.S3;
            if (s3Event == null)
            {
                continue;
            }
 
            try
            {
                if (record.EventName.Equals("ObjectRemoved:Delete", StringComparison.InvariantCultureIgnoreCase))
                {
                    await HandleImageDeletionAsync(s3Event);
                    continue;
                }
                
                var response = await S3Client.GetObjectMetadataAsync(s3Event.Bucket.Name, s3Event.Object.Key);
                
                if (response.Metadata["x-amz-meta-blurred"] == true.ToString())
                {
                    context.Logger.LogInformation($"Item with key {s3Event.Object.Key} has already been blurred");
                    continue;
                }
                
                await using var itemStream = await S3Client.GetObjectStreamAsync(s3Event.Bucket.Name, s3Event.Object.Key, new Dictionary<string, object>());
                
                using var outStream = new MemoryStream();
                using var image = await Image.LoadAsync(itemStream);
                image.Mutate(x => x.GaussianBlur(3));
                
                var originalName = response.Metadata["x-amz-meta-originalname"];
                await image.SaveAsync(outStream, image.DetectEncoder(originalName));
                
                var id = s3Event.Object.Key.Substring(s3Event.Object.Key.LastIndexOf("/") + 1);
                
                var putObjectRequest = new PutObjectRequest
                {
                    BucketName = s3Event.Bucket.Name,
                    Key = $"images-blurred/{id}",
                    Metadata =
                    {
                        ["x-amz-meta-originalname"] = originalName,
                        ["x-amz-meta-extension"] = response.Metadata["x-amz-meta-extension"],
                        ["x-amz-meta-blurred"] = true.ToString(),
                    },
                    ContentType = response.Headers.ContentType,
                    InputStream = outStream
                };

                await S3Client.PutObjectAsync(putObjectRequest);
            }
            catch (Exception e)
            {
                context.Logger.LogError(
                    $"Error getting object {s3Event.Object.Key} from bucket {s3Event.Bucket.Name}. Make sure they exist and your bucket is in the same region as this function.");
                context.Logger.LogError(e.Message);
                context.Logger.LogError(e.StackTrace);
                throw;
            }
        }
    }

    private async Task HandleImageDeletionAsync(S3Event.S3Entity s3Event)
    {
        var id = s3Event.Object.Key.Substring(s3Event.Object.Key.LastIndexOf("/") + 1);
        var deleteObjectRequest = new DeleteObjectRequest
        {
            BucketName = s3Event.Bucket.Name,
            Key = $"images-blurred/{id}"
        };

        await S3Client.DeleteObjectAsync(deleteObjectRequest);
    }
}