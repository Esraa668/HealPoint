using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Bussiness.Services.AttachmentService
{
    public interface IAttachmentService
    {
        //upload
        public string? Upload(IFormFile file, string folderName);
        //delete
        bool Delete(string filePath);
    }
}
