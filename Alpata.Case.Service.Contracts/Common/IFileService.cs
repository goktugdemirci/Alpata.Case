using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpata.Case.Service.Contracts.Common
{
    public interface IFileService 
    {
        public string? UploadFilePath { get; }
        public Task<string> CopyFileToDirectory(IFormFile file);
        public long GetSizeOfFile(string fileName);
        public void Delete(string fileName);
        public string GetFilePath(string fileName);
        public Tuple<string, FileStream> GetStreamOfFile(string fileGuidName);
    }
}
