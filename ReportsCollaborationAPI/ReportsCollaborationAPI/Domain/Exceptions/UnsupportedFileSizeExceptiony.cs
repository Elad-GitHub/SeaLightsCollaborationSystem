namespace ReportsCollaborationAPI.Domain
{
    public class UnsupportedFileSizeException : UnsupportedFileException
    {
        public UnsupportedFileSizeException()
            : base("size")
        {

        }
    }
}
