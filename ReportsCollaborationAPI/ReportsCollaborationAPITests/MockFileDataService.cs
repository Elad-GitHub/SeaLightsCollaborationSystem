using ReportsCollaborationAPI.Models;
using ReportsCollaborationAPI.Services;
using System.Collections.Generic;
using System.Linq;

namespace ReportsCollaborationAPITests
{
    public class MockFileDataService : IFileDataService
    {
        private List<File> files = new List<File>()
        {
            new File()
            {
                Id = 1,
                ParentId = 1,
                Link = string.Empty,
                CollaboratorId = 1,
                Privacy = PrivacyType.Public
            },
            new File()
            {
                Id = 2,
                ParentId = 1,
                Link = string.Empty,
                CollaboratorId = 1,
                Privacy = PrivacyType.Private
            },
            new File()
            {
                Id = 3,
                ParentId = 1,
                Link = string.Empty,
                CollaboratorId = 2,
                Privacy = PrivacyType.Public
            },
            new File()
            {
                Id = 4,
                ParentId = 1,
                Link = string.Empty,
                CollaboratorId = 2,
                Privacy = PrivacyType.Private
            }
        };

        public List<File> GetFiles(int reportId, int collaboratorId)
        {
            return files.Where(file => file.ParentId == reportId && (file.Privacy == PrivacyType.Public ||
                                                (file.Privacy == PrivacyType.Private && file.CollaboratorId == collaboratorId))).ToList();
        }

        public void AddFile(File file)
        {
            files.Add(file);
        }
    }
}
