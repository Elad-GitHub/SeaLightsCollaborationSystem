using ReportsCollaborationAPI.Models;
using ReportsCollaborationAPI.Services;
using System.Collections.Generic;
using System.Linq;

namespace ReportsCollaborationAPITests
{
    //A Mock to represnt the file data service with fake data
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
                Privacy = PrivacyLevel.Public
            },
            new File()
            {
                Id = 2,
                ParentId = 1,
                Link = string.Empty,
                CollaboratorId = 1,
                Privacy = PrivacyLevel.Private
            },
            new File()
            {
                Id = 3,
                ParentId = 1,
                Link = string.Empty,
                CollaboratorId = 2,
                Privacy = PrivacyLevel.Public
            },
            new File()
            {
                Id = 4,
                ParentId = 1,
                Link = string.Empty,
                CollaboratorId = 2,
                Privacy = PrivacyLevel.Private
            }
        };

        public List<File> GetFiles(int reportId, int collaboratorId)
        {
            return files.Where(file => file.ParentId == reportId && (file.Privacy == PrivacyLevel.Public ||
                                                (file.Privacy == PrivacyLevel.Private && file.CollaboratorId == collaboratorId))).ToList();
        }

        public void AddFile(File file)
        {
            files.Add(file);
        }
    }
}
