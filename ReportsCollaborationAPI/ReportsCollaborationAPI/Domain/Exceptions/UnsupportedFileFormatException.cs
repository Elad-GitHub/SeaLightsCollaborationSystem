namespace ReportsCollaborationAPI.Domain
{
    public class UnsupportedFileFormatException : UnsupportedFileException
    {
        public UnsupportedFileFormatException()
            : base("format")
        {

        }
    }
}
