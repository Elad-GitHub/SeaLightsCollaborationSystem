using Microsoft.AspNetCore.Http;
using ReportsCollaborationAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ReportsCollaborationAPI.Services
{
    public class FileDataService : IFileDataService
    {
        private CollaborationSystemContext _Context;

        public FileDataService(CollaborationSystemContext context)
        {
            _Context = context;
        }

        public List<Models.File> GetFiles(int parentId, int collaboratorId)
        {
            return _Context.Files.Where(note => note.ParentId == parentId && (note.Privacy == PrivacyLevel.Public ||
                                                (note.Privacy == PrivacyLevel.Private && note.CollaboratorId == collaboratorId))).ToList();
        }

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

            return new (bytes, existingFile.Link);
        }

        public async Task<bool> SaveFile(IFormFile file, int parentId, int collaboratorId)
        {
            bool isSaveSuccess = false;

            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];

                //Create a new Name for the file due to security reasons.
                string fileName = $"{DateTime.Now.Ticks}_{parentId}_{collaboratorId}{extension}";

                var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), $"FileRepository\\files\\{parentId}\\{collaboratorId}");

                if (!Directory.Exists(pathBuilt))
                {
                    Directory.CreateDirectory(pathBuilt);
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(), $"FileRepository\\files\\{parentId}\\{collaboratorId}", fileName);

                var fileToSave = new Models.File()
                {
                    Link = path,
                    ParentId = parentId,
                    CollaboratorId = collaboratorId
                };

                AddFile(fileToSave);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                isSaveSuccess = true;
            }
            catch (Exception e)
            {
                //log error
            }

            return isSaveSuccess;
        }

        private void AddFile(Models.File file)
        {
            _Context.Files.Add(file);

            _Context.SaveChanges();
        }
    }
}
