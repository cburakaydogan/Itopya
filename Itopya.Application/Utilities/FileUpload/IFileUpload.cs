using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Itopya.Application.Utilities.ImageUpload
{
    public interface IFileUpload
    {
        Task<string> SaveFile(IFormFile file);
        Task<string> UpdateFile(IFormFile file, string filePath);
        void RemoveFile(string filePath);
    }
}