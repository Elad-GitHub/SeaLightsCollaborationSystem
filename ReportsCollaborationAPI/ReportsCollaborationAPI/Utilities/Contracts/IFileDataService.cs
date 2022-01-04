﻿using Microsoft.AspNetCore.Http;
using ReportsCollaborationAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReportsCollaborationAPI.Services
{
    public interface IFileDataService
    {
        List<File> GetFiles(int parentId, int collaboratorId);

        Task<Tuple<byte[], string>> GetFile(int Id, int parentId, int collaboratorId);

        Task<bool> SaveFile(IFormFile file, int parentId, int collaboratorId);
    }
}
