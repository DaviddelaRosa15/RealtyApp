﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtyApp.Core.Application.Helpers
{
    public static class ImageUpload
    {
        //I hope this will be usuful for you guys. Code with 🎯

        public static async Task<string> FileUpload(IFormFile fileForm, int ItemId, string ContainerName = "Item")
        {//work with the optionals parametyers

            //Get current directory
            string basePath = $"/{ContainerName}/{ItemId}";

            string servePath = Directory.GetCurrentDirectory();

            string ServerAndBasePath = Path.Combine(servePath, $"wwwroot{basePath}");

            if (!Directory.Exists(ServerAndBasePath))
            {
                Directory.CreateDirectory(ServerAndBasePath);
            }

            FileInfo fileInfo = new FileInfo(fileForm.FileName);
            Guid guid = Guid.NewGuid();

            string uniqueFileName = guid + fileInfo.Extension;

            string uniqueFileWithBaseServePath = Path.Combine(ServerAndBasePath, uniqueFileName);

            using (FileStream stream = new FileStream(uniqueFileWithBaseServePath, FileMode.Create))
            {
                await fileForm.CopyToAsync(stream);
            }

            return Path.Combine(basePath, uniqueFileName);
        }


        public static async Task<string> FileUpload(IFormFile fileForm, string currentsImgUrl, int ItemId, string ContainerName = "Item", bool IsUpdateMode = false)
        {

            //Get current directory
            string basePath = $"/{ContainerName}/{ItemId}";

            string servePath = Directory.GetCurrentDirectory();

            string ServerAndBasePath = Path.Combine(servePath, $"wwwroot{basePath}");

            if (!Directory.Exists(ServerAndBasePath))
            {
                Directory.CreateDirectory(ServerAndBasePath);
            }

            FileInfo fileInfo = new FileInfo(fileForm.FileName);
            Guid guid = Guid.NewGuid();

            string uniqueFileName = guid + fileInfo.Extension;

            string uniqueFileWithBaseServePath = Path.Combine(ServerAndBasePath, uniqueFileName);

            using (FileStream stream = new FileStream(uniqueFileWithBaseServePath, FileMode.Create))
            {
                await fileForm.CopyToAsync(stream);
            }

            //DELETE THE OLD IMAGE
            if (IsUpdateMode)
            {
                string[] oldImageParts = currentsImgUrl.Split("/");
                string oldImageFileName = oldImageParts[^1];
                string completeOldImagePath = Path.Combine(ServerAndBasePath, oldImageFileName);

                if (File.Exists(completeOldImagePath))
                {
                    File.Delete(completeOldImagePath);
                }

            }

            return $"{basePath}/{uniqueFileName}"; //The new image url :)!
        }


        public static async Task<List<string>> FileUpload(IFormFile[] fileForms, int ItemId, string ContainerName = "Item")
        {
            List<string> imgUrl = new List<string>();

            //Get current directory
            string basePath = $"/{ContainerName}/{ItemId}";

            string servePath = Directory.GetCurrentDirectory();

            string ServerAndBasePath = Path.Combine(servePath, $"wwwroot{basePath}");

            if (!Directory.Exists(ServerAndBasePath))
            {
                Directory.CreateDirectory(ServerAndBasePath);
            }

            foreach (var fileForm in fileForms)
            {
                if (fileForm != null)
                {
                    FileInfo fileInfo = new FileInfo(fileForm.FileName);
                    Guid guid = Guid.NewGuid();

                    string uniqueFileName = guid + fileInfo.Extension;

                    string uniqueFileWithBaseServePath = Path.Combine(ServerAndBasePath, uniqueFileName);

                    using (FileStream stream = new FileStream(uniqueFileWithBaseServePath, FileMode.Create))
                    {
                        await fileForm.CopyToAsync(stream);
                    }

                    imgUrl.Add($"{basePath}/{uniqueFileName}");
                }
                else
                {
                    imgUrl.Add("");
                }

            }

            return imgUrl;

        }


        public static async Task<List<string>> FileUpload(IFormFile[] fileForms, int ItemId, bool IsUpdateMode = false, string ContainerName = "Item", List<string> currentsImgUrl = null)
        {

            List<string> imgUrl = new List<string>() { }; //Tal vez innecesario.

            //Get current directory
            string basePath = $"/{ContainerName}/{ItemId}";

            string servePath = Directory.GetCurrentDirectory();

            string ServerAndBasePath = Path.Combine(servePath, $"wwwroot{basePath}");

            if (!Directory.Exists(ServerAndBasePath))
            {
                Directory.CreateDirectory(ServerAndBasePath);
            }

            //An index to be able to iterate inside the currentsImgUrl List at the same time of fileForms Array.
            int index = 0;

            foreach (var fileForm in fileForms)
            {


                if (IsUpdateMode)
                {

                    if (fileForm != null)
                    {
                        FileInfo fileInfo = new FileInfo(fileForm.FileName);
                        Guid guid = Guid.NewGuid();

                        string uniqueFileName = guid + fileInfo.Extension;

                        string uniqueFileWithBaseServePath = Path.Combine(ServerAndBasePath, uniqueFileName);

                        using (FileStream stream = new FileStream(uniqueFileWithBaseServePath, FileMode.Create))
                        {
                            await fileForm.CopyToAsync(stream);
                        }

                        //DELETE THE OLD IMAGE
                        if (IsUpdateMode)
                        {
                            string[] oldImagePart = currentsImgUrl[index].Split("/");
                            string oldImageFileName = oldImagePart[^1];
                            string completeOldImagePath = Path.Combine(ServerAndBasePath, oldImageFileName);

                            if (File.Exists(completeOldImagePath))
                            {
                                File.Delete(completeOldImagePath);
                            }

                        }

                        imgUrl.Insert(index, $"{basePath}/{uniqueFileName}");

                    }
                    else
                    {
                        imgUrl.Insert(index, currentsImgUrl[index]);
                    }


                }

                index++;
            }

            return imgUrl;

        }


        //Helpful when we wan to delete the entire entity e.g: User and his/her image profile.
        public static void DeleteFile(int ItemId, string ContainerName = "Item")
        {

            //Get current directory
            string basePath = $"/{ContainerName}/{ItemId}";

            string servePath = Directory.GetCurrentDirectory();

            string ServerAndBasePath = Path.Combine(servePath, $"wwwroot{basePath}");

            if (Directory.Exists(ServerAndBasePath))
            {

                DirectoryInfo directoryInfo = new DirectoryInfo(ServerAndBasePath);

                foreach (FileInfo files in directoryInfo.GetFiles())
                {
                    files.Delete();
                }

                foreach (DirectoryInfo directories in directoryInfo.GetDirectories())
                {
                    directories.Delete(true);
                }

                Directory.Delete(ServerAndBasePath, true);
            }


        }


    }
}
