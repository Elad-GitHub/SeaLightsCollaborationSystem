using Microsoft.AspNetCore.Http;
using ReportsCollaborationAPI.Application;
using System;

namespace ReportsCollaborationAPI.Infrastructure
{
    public class FileValidator : IValidator
    {
        private IFormFile fileToValidate;

        public FileValidator(IFormFile file)
        {
            fileToValidate = file;
        }

        public Tuple<bool, string> Validate()
        {
            if(!ValidateFileFormat(fileToValidate))
            {
                return new (false, "Invalid File! The file upload has been blocked because its format can not be .exe / .js / .vbs");
            }

            if (!ValidateFileSize(fileToValidate))
            {
                return new (false, "Invalid File! The file upload has been blocked because its size can not be bigger than 10 mb");
            }

            return new (true, string.Empty);
        }

        //check if file size is less than 10 mb
        public bool ValidateFileSize(IFormFile file)
        {
            return (file.Length < 10000000);
        }

        //check if file type is valid
        public bool ValidateFileFormat(IFormFile file)
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];

            return (!extension.Equals(".exe") && !extension.Equals(".js") && !extension.Equals(".vbs"));
        }
    }
}
