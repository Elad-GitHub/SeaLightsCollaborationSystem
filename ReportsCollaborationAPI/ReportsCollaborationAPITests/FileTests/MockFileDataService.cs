using Microsoft.AspNetCore.Http;
using ReportsCollaborationAPI.Application;
using ReportsCollaborationAPI.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ReportsCollaborationAPITests
{
    //A Mock to represnt the file data service with fake data
    public class MockFileDataService : IFileDataService
    {
        private List<ReportsCollaborationAPI.Domain.File> files = new List<ReportsCollaborationAPI.Domain.File>()
        { 
            new ReportsCollaborationAPI.Domain.File()
            {
                Id = 1,
                ParentId = 1,
                Link = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName ,"FileTests\\TestFileRepository\\files\\1\\1\\637769406318521861_1_1.txt"),
                CollaboratorId = 1,
                Privacy = PrivacyLevel.Public
            },
            new ReportsCollaborationAPI.Domain.File()
            {
                Id = 2,
                ParentId = 1,
                Link = string.Empty,
                CollaboratorId = 1,
                Privacy = PrivacyLevel.Private
            },
            new ReportsCollaborationAPI.Domain.File()
            {
                Id = 3,
                ParentId = 1,
                Link = string.Empty,
                CollaboratorId = 2,
                Privacy = PrivacyLevel.Public
            },
            new ReportsCollaborationAPI.Domain.File()
            {
                Id = 4,
                ParentId = 1,
                Link = string.Empty,
                CollaboratorId = 2,
                Privacy = PrivacyLevel.Private
            }
        };

        public async Task<Tuple<byte[], string>> GetFile(int Id, int parentId, int collaboratorId)
        {
            //get all report user files
            var files = GetFiles(parentId, collaboratorId);

            //filter only wnated file to download
            var existingFile = files.FirstOrDefault(file => file.Id == Id);

            if (existingFile == null)
            {
                return null;
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(existingFile.Link);

            return new(bytes, existingFile.Link);
        }

        public List<ReportsCollaborationAPI.Domain.File> GetFiles(int reportId, int collaboratorId)
        {
            return files.Where(file => file.ParentId == reportId && (file.Privacy == PrivacyLevel.Public ||
                                                (file.Privacy == PrivacyLevel.Private && file.CollaboratorId == collaboratorId))).ToList();
        }

        public Task<bool> SaveFile(IFormFile file, int parentId, int collaboratorId)
        {
            var path = "testPath";

            var fileToSave = new ReportsCollaborationAPI.Domain.File()
            {
                Link = path,
                ParentId = parentId,
                CollaboratorId = collaboratorId
            };

            AddFile(fileToSave);

            return Task.Run(async () => { await Task.Delay(3000); return true; });
        }

        public void AddFile(ReportsCollaborationAPI.Domain.File file)
        {
            files.Add(file);
        }
    }
}
