using ReportsCollaborationAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace ReportsCollaborationAPI.Services
{
    public class FileDataService : IFileDataService
    {
        private CollaborationSystemContext _Context;

        public FileDataService(CollaborationSystemContext context)
        {
            _Context = context;
        }

        public List<File> GetFiles(int parentId, int collaboratorId)
        {
            return _Context.Files.Where(note => note.ParentId == parentId && (note.Privacy == PrivacyType.Public ||
                                                (note.Privacy == PrivacyType.Private && note.CollaboratorId == collaboratorId))).ToList();
        }

        public void AddFile(File file)
        {
            _Context.Files.Add(file);

            _Context.SaveChanges();
        }
    }
}
