using Alpata.Case.Nuget;
using Alpata.Case.Service.Contracts.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Alpata.Case.Services.Common
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public string? UploadFilePath => $"{_webHostEnvironment.WebRootPath}/Uploads";

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> CopyFileToDirectory(IFormFile file)
        {
            string guidName = Guid.NewGuid().ToString();
            string extension = new FileInfo(file.FileName).Extension;
            string fileName = $"{guidName}{extension}";
            string uploadPath = $"{this.UploadFilePath}/{fileName}";
            using (FileStream stream = new FileStream(uploadPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return fileName;
        }

        public long GetSizeOfFile(string fileName)
        {
            string filePath = $"{this.UploadFilePath}/{fileName}";
            var fileInfo = new FileInfo(filePath);
            var size = fileInfo.Length;
            return size;
        }

        public void Delete(string fileName)
        {
            string uploadPath = $"{this.UploadFilePath}/{fileName}";
            File.Delete(uploadPath);
        }

        public string GetFilePath(string fileName)
        {
            string filePath = $"{this.UploadFilePath}/{fileName}";
            return filePath;
        }

        public Tuple<string, FileStream> GetStreamOfFile(string fileGuidName)
        {
            string path = $"{this.UploadFilePath}/{fileGuidName}";
            var stream = new FileStream(path, FileMode.Open);
            string mimeType = MimeTypes.GetMimeType(path);
            return Tuple.Create(mimeType, stream);
        }
    }
}
