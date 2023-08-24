using IceProducts.Shared.InputModels;
using Microsoft.AspNetCore.Components.Forms;

namespace IceProducts.Server.Helpers
{
    public class ImageHandler
    {
        public static async Task<byte[]> AdjustAndGetImageAsBytes(IFormFile image)
        {
            var format = image.ContentType;
            int maxWidth = 640;
            int maxheight = 480;

            // Process the image here
            using (var stream = image.OpenReadStream())
            {
                // Save the stream to the database or file system
                // You can convert the stream to a byte array, for example
                byte[] imageBytes;
                using (var memoryStream = new MemoryStream())
                {
                    await stream.CopyToAsync(memoryStream);
                    imageBytes = memoryStream.ToArray();
                }

                return imageBytes;
            }

            //var resizedBrowserFile = await image.RequestImageFileAsync(format, maxWidth, maxheight);
            //var imageAsBytes = new byte[resizedBrowserFile.Size];
            //await resizedBrowserFile.OpenReadStream().ReadAsync(imageAsBytes);

            //return imageAsBytes;
        }
    }
}
