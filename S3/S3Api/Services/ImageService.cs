using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;
using S3Api.Models.Options;

namespace S3Api.Services;

public interface IImageService
{
    Task<PutObjectResponse> UploadImageAsync(Guid id, IFormFile formFile);
    Task<DeleteObjectResponse> DeleteImageAsync(Guid id);
    Task<GetObjectResponse?> GetImageAsync(Guid id);
    Task<GetObjectResponse?> GetImageBlurredAsync(Guid id);
}

public class ImageService : IImageService
{
    private readonly IAmazonS3 _amazonS3;
    private readonly IOptions<AWSOptions> _awsOptions;

    public ImageService(IAmazonS3 amazonS3, IOptions<AWSOptions> awsOptions)
    {
        _amazonS3 = amazonS3;
        _awsOptions = awsOptions;
    }

    public async Task<PutObjectResponse> UploadImageAsync(Guid id, IFormFile formFile)
    {
        var putObjectRequest = new PutObjectRequest
        {
            BucketName = _awsOptions.Value.BucketName,
            Key = $"{_awsOptions.Value.ImagesKeyPrefix}/{id}",
            ContentType = formFile.ContentType,
            InputStream = formFile.OpenReadStream(),
            Metadata =
            {
                [$"{_awsOptions.Value.MetadataPrefix}-{_awsOptions.Value.OriginalNameMetadataKey}"] = formFile.FileName,
                [$"{_awsOptions.Value.MetadataPrefix}-{_awsOptions.Value.ExtensionMetadataKey}"] = Path.GetExtension(formFile.FileName)
            }
        };

        var response = await _amazonS3.PutObjectAsync(putObjectRequest);
        return response;
    }

    public async Task<DeleteObjectResponse> DeleteImageAsync(Guid id)
    {
        var deleteObjectRequest = new DeleteObjectRequest
        {
            BucketName = _awsOptions.Value.BucketName,
            Key = $"{_awsOptions.Value.ImagesKeyPrefix}/{id}"
        };

        var response = await _amazonS3.DeleteObjectAsync(deleteObjectRequest);

        return response;
    }

    public async Task<GetObjectResponse?> GetImageAsync(Guid id)
    {
        return await GetObjectByKeyPrefix(_awsOptions.Value.ImagesKeyPrefix, id);
    }
    
    public async Task<GetObjectResponse?> GetImageBlurredAsync(Guid id)
    {
        return await GetObjectByKeyPrefix(_awsOptions.Value.ImagesBlurredKeyPrefix, id);
    }

    private async Task<GetObjectResponse?> GetObjectByKeyPrefix(string keyPrefix, Guid id)
    {
        try
        {
            var getObjectRequest = new GetObjectRequest
            {
                BucketName = _awsOptions.Value.BucketName,
                Key = $"{keyPrefix}/{id}"
            };

            var response = await _amazonS3.GetObjectAsync(getObjectRequest);
            return response;
        }
        catch (AmazonS3Exception ex) when (ex.Message is "The specified key does not exist.")
        {
            return null;
        }
    }
}