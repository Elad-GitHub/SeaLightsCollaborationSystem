using ReportsCollaborationAPI.Models;
using System.Collections.Generic;

namespace ReportsCollaborationAPI.Services
{
    public interface IFileDataService
    {
        List<File> GetFiles(int parentId, int collaboratorId);

        void AddFile(File file);
    }
}
