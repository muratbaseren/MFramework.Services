using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using System;
using System.IO;
using System.Linq;

namespace MFramework.Services.WebCommon
{
    public interface IFileFolderManager
    {

        IHostingEnvironment Environment { get; }

        void DirectoryIfNotExistsCreate(string path);
        string GenerateFileNameByGuid(string extension);
        string GenerateFileNameByObjectId(string extension);
        string GenerateNameFromFileName(string filename, string prefix);
        bool CheckFormFile(IFormFile file);
        void CopyFormFile(IFormFile file, string path, FileMode mode = FileMode.Create);
    }

    public class FileFolderManager : IFileFolderManager
    {
        protected readonly string _wwwRootFolderName = "wwwroot";
        public IHostingEnvironment Environment { get; protected set; }

        public FileFolderManager(IHostingEnvironment environment)
        {
            Environment = environment;
            Environment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), _wwwRootFolderName);
        }

        public void DirectoryIfNotExistsCreate(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public string GenerateNameFromFileName(string filename, string prefix)
        {
            return prefix + "." + (filename?.Split('.')?.Last() ?? string.Empty);
        }

        public bool CheckFormFile(IFormFile file)
        {
            return file.Length > 0;
        }

        public void CopyFormFile(IFormFile file, string path, FileMode mode = FileMode.Create)
        {
            using (var stream = new FileStream(path, mode))
            {
                file.CopyTo(stream);
            }
        }

        public string GenerateFileNameByObjectId(string extension)
        {
            if (string.IsNullOrEmpty(extension) || string.IsNullOrWhiteSpace(extension))
                return ObjectId.GenerateNewId().ToString();
            else
                return ObjectId.GenerateNewId().ToString() + "." + extension;
        }

        public string GenerateFileNameByGuid(string extension)
        {
            if (string.IsNullOrEmpty(extension) || string.IsNullOrWhiteSpace(extension))
                return Guid.NewGuid().ToString();
            else
                return Guid.NewGuid().ToString() + "." + extension;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath">example: MailTemplate/index.html or full path</param>
        /// <returns></returns>
        public string ReadFileContent(string filepath)
        {
            if (!File.Exists(filepath))
                throw new FileNotFoundException("File is not found", filepath);

            return File.ReadAllText(filepath);
        }
    }
}
