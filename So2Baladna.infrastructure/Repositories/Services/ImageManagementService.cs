using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using So2Baladna.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace So2Baladna.Infrastructure.Repositories.Services
{
    internal class ImageManagementService : IImageManagementService
    {
        private readonly IFileProvider fileProvider;

        public ImageManagementService(IFileProvider fileProvider)
        {
            this.fileProvider = fileProvider;
        }
        public void  DeleteImage(string src)
        {
            var fileInfo = fileProvider.GetFileInfo(src);
            if (fileInfo.Exists)
            {
                File.Delete(fileInfo.PhysicalPath);
            }
         
        }

    
        public async Task<List<string>> UploadImageAsync(IFormFileCollection files, string src)
        {
            var uploadedFilePaths = new List<string>();
            var directory = Path.Combine( "wwwroot","Images", src);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var fileName = file.FileName;
                    var imageSrc = $"/Images/{src}/{fileName}";
                    var root = Path.Combine(directory, fileName);
                    using (var stream = new FileStream(root, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    uploadedFilePaths.Add(imageSrc);

                }

            }
            return uploadedFilePaths;

        }
    }
}
