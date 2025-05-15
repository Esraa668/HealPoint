using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Bussiness.Services.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {
        List<string> allowedExtensions = [".jpg", ".png", ".jpeg"];
        const int maxSize = 2_097_152; //2MB
        public string? Upload(IFormFile file, string folderName)
        {
            //1. Check Extension.
            var extension = Path.GetExtension(file.FileName);
            if (!allowedExtensions.Contains(extension)) return null;

            //2. Check Size.
            if (file.Length == 0 || file.Length > maxSize) return null;

            //3. Get located folder path.
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName);

            //4. Make attachment name Unique, --Guid.
            var fileName = $"{Guid.NewGuid}_{file.FileName}";

            //5. Get File path.
            var filePath = Path.Combine(folderPath, fileName);

            //6. Create file stream to copy the file.
            using FileStream fs = new FileStream(filePath, FileMode.Create);

            //7. Use FileStream to copy the file.
            file.CopyTo(fs);
            //File.Create(filePath);//Opens the stream and create the file in one step.

            //8. Return file name to restore in DB.
            return fileName;
        }
        public bool Delete(string filePath)
        {
            if(!File.Exists(filePath)) return false;
            else
            {
                File.Delete(filePath);
                return true;
            }
        }
    }
}
