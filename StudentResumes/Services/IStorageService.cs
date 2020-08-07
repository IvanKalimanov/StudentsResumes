using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace StudentResumes.Core.Services
{
    public interface IStorageService
    {
        /// <summary>
        /// Save file from stream to local disk
        /// </summary>
        /// <param name="file"></param>
        /// <param name="fileName"></param>
        /// <param name="storagePrefix"></param>
        /// <returns></returns>
        Task<string> UploadAsync(IFormFile file, string fileName, string storagePrefix);

 
        /// <summary>
        /// Remove file from local disk
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="storagePrefix"></param>
        /// <returns></returns>
        void RemoveFile(string fileName, string storagePrefix);

        /// <summary>
        /// Remove file from local disk
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="storagePrefix"></param>
        /// <returns></returns>
        void RemoveFileByFullPath(string filePath);
    }
}
