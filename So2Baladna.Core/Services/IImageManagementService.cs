using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace So2Baladna.Core.Services
{
    /// this interface defines methods for managing images, including uploading and deleting images.    /// 
    public interface IImageManagementService
    {
        public  Task<List<string>> UploadImageAsync(IFormFileCollection files , string src);
        public  void DeleteImage(string src);
    }
}
