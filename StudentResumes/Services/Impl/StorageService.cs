using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using StudentResumes.Data.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace StudentResumes.Core.Services.Impl
{
    public class StorageService : IStorageService
    {
        private readonly EnvironmentConfig _environmentConfig;

        public StorageService(IOptions<EnvironmentConfig> environmentConfig)
        {
            _environmentConfig = environmentConfig.Value;
        }

        #region Public Methods

        public async Task<string> UploadAsync(IFormFile file, string fileName, string storagePrefix)
        {
            var fullPath = CreateFilePath(storagePrefix, fileName);

            await SaveFileAsync(fullPath, file);

            return fullPath;
        }


        public void RemoveFileByFullPath(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public void RemoveFile(string storagePrefix, string fileName)
        {
            var fullPath = CreateFilePath(storagePrefix, fileName);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        #endregion

        #region Private Methods

        private string CreateFilePath(string storagePrefix, string fileName)
        {
            return _environmentConfig.STORAGE_PATH + storagePrefix + fileName;
        }

        private static async Task SaveFileAsync(string fullPath, IFormFile file)
        {
            await using var outputFileStream = File.Create(fullPath);
            await file.CopyToAsync(outputFileStream);
        }


        #endregion

    }
}
