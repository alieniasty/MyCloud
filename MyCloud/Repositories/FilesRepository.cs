﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyCloud.Models;
using MyCloud.Services;

namespace MyCloud.Repositories
{
    public class FilesRepository : IFilesRepository
    {
        private CloudContext _context;
        private IRaspberryFiles _raspberry;

        public FilesRepository(CloudContext context, IRaspberryFiles raspberry)
        {
            _context = context;
            _raspberry = raspberry;
        }

        public async Task<bool> AddNewFileAsync(string base64File, string identityName, string fileName, string folder)
        {
            var user = _context.CloudUsers
                .Include(u => u.Folders)
                .ThenInclude(u => u.FileDatas)
                .SingleOrDefault(u => u.UserName == identityName);

            var userFolder = user.Folders
                .SingleOrDefault(f => f.Name == folder);

            var newFile = new FileData
            {
                Base64Code = base64File,
                Name = $"{DateTime.Now:yyyy-MM-dd}_{fileName}",
                Folder = folder
            };

            await _raspberry.SaveFileToUsb(newFile.Base64Code, newFile.Name);

            userFolder.FileDatas
                .Add(newFile);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<string>> GetBase64Files(string folder, string identityName)
        {
            var userFiles = await _context.CloudUsers
                .Where(u => u.UserName == identityName)
                .Include(t => t.Folders)
                .ThenInclude(f => f.FileDatas)

                .SelectMany(f => f.Folders)
                .Where(f => f.Name == folder)
                .SelectMany(f => f.FileDatas)
                .ToListAsync();


            var codes = userFiles
                .Select(uf => uf.Base64Code)
                .ToList();

            return codes;
        }

        public async Task<bool> DeleteFileAsync(string base64Code, string folder, string identityName)
        {
            var user = _context.CloudUsers
                .Include(u => u.Folders)
                .ThenInclude(u => u.FileDatas)
                .SingleOrDefault(u => u.UserName == identityName);

            var userFolder = user.Folders
                .SingleOrDefault(f => f.Name == folder);

            var fileToBeRemoved = userFolder.FileDatas.FirstOrDefault(fd => fd.Base64Code == base64Code);

            await _raspberry.RemoveFileFromUsb(fileToBeRemoved.Name);

            userFolder.FileDatas.Remove(fileToBeRemoved);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> MoveFilesAsync(List<string> codesOfFilesToMove, string currentFolder, string newFolder, string identityName)
        {
            var user = _context.CloudUsers
                .Include(u => u.Folders)
                .ThenInclude(u => u.FileDatas)
                .SingleOrDefault(u => u.UserName == identityName);

            var oldFolder = user.Folders
                .SingleOrDefault(f => f.Name == currentFolder);

            var futureFolder = user.Folders
                .SingleOrDefault(f => f.Name == newFolder);

            List<FileData> filesToMove = new List<FileData>();

            codesOfFilesToMove.ForEach(code =>
            {
                filesToMove.Add(oldFolder.FileDatas.FirstOrDefault(fd => fd.Base64Code == code));
            });

            filesToMove.ForEach(file =>
            {
                futureFolder.FileDatas.Add(file);
            });
            
            filesToMove.ForEach(file =>
            {
                oldFolder.FileDatas.Remove(file);
            });

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
