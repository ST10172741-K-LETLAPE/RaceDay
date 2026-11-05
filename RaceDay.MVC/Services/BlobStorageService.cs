using Azure.Storage.Blobs;

namespace RaceDay.MVC.Services
{
    public class BlobStorageService
    {
        private readonly string? _connectionString;
        private readonly string _containerName = "racebanners";
        
        public BlobStorageService(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("AzureBlobStorage");
        }

        public async Task<string> UploadAsync(IFormFile file)
        {
            if (file == null || file.Length == 0) return "";

            if (!string.IsNullOrEmpty(_connectionString) && !_connectionString.Contains("UseDevelopment"))
            {
                try
                {
                    var blobServiceClient = new BlobServiceClient(_connectionString);
                    var containerClient = blobServiceClient.GetBlobContainerClient(_containerName);
                    await containerClient.CreateIfNotExistsAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);
                    var fileName = $"{Guid.NewGuid()}_{file.FileName}";
                    var blobClient = containerClient.GetBlobClient(fileName);
                    using var stream = file.OpenReadStream();
                    await blobClient.UploadAsync(stream, true);
                    return blobClient.Uri.ToString();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Azure Blob upload failed: {ex.Message}, falling back to local");
                }
            }

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            Directory.CreateDirectory(uploadsFolder);
            var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return $"/uploads/{uniqueFileName}";
        }
    }
}
