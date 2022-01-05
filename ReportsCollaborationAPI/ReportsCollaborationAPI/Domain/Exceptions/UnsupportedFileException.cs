using System;

namespace ReportsCollaborationAPI.Domain
{
    public class UnsupportedFileException : Exception
    {
        public UnsupportedFileException(string unsupportedFileSettings)
            : base($"File {unsupportedFileSettings} is unsupported")
        {

        }
    }
}
