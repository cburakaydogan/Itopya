using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Itopya.Application.Utilities.ImageUpload
{
    public class ImageUpload : IFileUpload
    {
        
        public async Task<string> SaveFile(IFormFile file)
        {
            List<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };

            var ext = file.FileName.Substring(file.FileName.LastIndexOf('.'));
            var extension = ext.ToLower();

            if (!AllowedFileExtensions.Contains(extension)) {

                throw new HttpException(415, "Unsupported Media type, Allowed types : .jpg .gif .png" );
            }

            string imagePath = "wwwroot/images/";
            string imageLink = "/images/";
            string imageName = Guid.NewGuid().ToString() + ".jpg";

            imagePath = Path.Combine(imagePath, imageName);
            imageLink = Path.Combine(imageLink, imageName);

            try
            {
                using var stream = File.Create(imagePath);
                await file.CopyToAsync(stream);

            }
            catch (Exception)
            {
                
                    throw new HttpException(503, "Error Occured." );
            }

            return imageLink;

        }

        public async Task<string> UpdateFile(IFormFile file,string filePath)
        {
            string imageLink = await SaveFile(file);

            RemoveFile(filePath);

            return imageLink;
        }

        public void RemoveFile(string filePath)
        {
            filePath = "wwwroot"+filePath;
            if (File.Exists(filePath))
            {
               File.Delete(filePath);
            }
        }
    }
}