namespace S3Api.Validators;

public interface IImageFileValidator
{
    public bool IsValidImageFile(IFormFile file);
}

public class ImageFileValidator : IImageFileValidator
{
    public bool IsValidImageFile(IFormFile file)
    {
        if (file.Length <= 0)
        {
            return false;
        }
        
        var headerBytes = new byte[4];
        using var fileStream = file.OpenReadStream();
        fileStream.Read(headerBytes, 0, headerBytes.Length);

        // PNG
        if (headerBytes[0] == 0x89 && headerBytes[1] == 0x50 && headerBytes[2] == 0x4E && headerBytes[3] == 0x47)
            return true;
        // JPG or JPEG
        if (headerBytes[0] == 0xFF && headerBytes[1] == 0xD8)
            return true;
        // GIF
        if (headerBytes[0] == 0x47 && headerBytes[1] == 0x49 && headerBytes[2] == 0x46)
            return true;
        // BMP
        if (headerBytes[0] == 0x42 && headerBytes[1] == 0x4D)
            return true;
        
        return false;
    }
}