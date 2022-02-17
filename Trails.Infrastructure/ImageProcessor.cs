using System.Globalization;
using Microsoft.AspNetCore.Http;
using Trails.Data.DomainModels;

namespace Trails.Infrastructure
{
    public static class ImageProcessor
    {
        public static string ProcessImageFromDb(Event @event)
        {
            var imageBaseData = Convert.ToBase64String(@event.Image.DataBytes);
            return $"data:image/jpg;base64,{imageBaseData}";
        }

        public static async Task<Image> ProcessImageToDb(IFormFile imgFile, string currentUserId)
        {
            await using var memoryStream = new MemoryStream();
            await imgFile.CopyToAsync(memoryStream);

            var img = new Image
            {
                Title = $"{Guid.NewGuid().ToString()}-{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}",
                CreatedOn = DateTime.UtcNow,
                CreatorId = currentUserId,
                DataBytes = memoryStream.ToArray()
            };

            return img;
        }
    }
}
