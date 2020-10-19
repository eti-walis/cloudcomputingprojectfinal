using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using BEj;

namespace DALj
{
    public class GoogleDriveApi
    {
        //add scope
        public string[] Scopes = { Google.Apis.Drive.v3.DriveService.Scope.Drive };
        static readonly string ApplicationName = "Drive API .NET Quickstart";


        /// create Drive API service
        public DriveService GetService()
        {
            //get Credentials from client_secret.json file 
            UserCredential credential;
            //Root Folder of project
            var CSPath = System.Web.Hosting.HostingEnvironment.MapPath("~/");
            using (var stream =
                           new FileStream("C:\\Users\\User\\Desktop\\CloudComputingProject1-DB-Shira\\CloudComputingProject1-DB-Shira\\DALj\\credentials.json", FileMode.Open, FileAccess.Read))
            {
                String FolderPath = System.Web.Hosting.HostingEnvironment.MapPath("~/"); ;
                String FilePath = Path.Combine(FolderPath, "DriveServiceCredentials.json");
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(FilePath, true)).Result;
            }
            //create Drive API service.
            DriveService service = new Google.Apis.Drive.v3.DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            return service;
        }

        private List<GoogleDriveFile> GetFiles()
        {
            var service = GetService();
            // Define parameters of request.
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.PageSize = 1000;
            listRequest.Fields = "nextPageToken, files(id, name,webViewLink)";

            // List files.
            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute()
                .Files;
            var filesList = ConvertGoogleFiles(files);
            return filesList;
        }

        private List<GoogleDriveFile> ConvertGoogleFiles(IList<Google.Apis.Drive.v3.Data.File> files)
        {
            List<GoogleDriveFile> NewFiles = new List<GoogleDriveFile>();
            foreach (var file in files)
            {
                NewFiles.Add(new GoogleDriveFile()
                {
                    ID = file.Id,
                    Name = file.Name,
                    WebViewLink = file.WebViewLink
                });
            }
            return NewFiles;
        }

        public List<GoogleDriveFile> GetDriveFolders()
        {
            DriveService service = GetService();
            List<GoogleDriveFile> FolderList = new List<GoogleDriveFile>();
            FilesResource.ListRequest request = service.Files.List();
            request.Q = "mimeType='application/vnd.google-apps.folder'";
            request.Fields = "files(id, name)";
            Google.Apis.Drive.v3.Data.FileList result = request.Execute();
            foreach (var file in result.Files)
            {
                GoogleDriveFile File = new GoogleDriveFile
                {
                    ID = file.Id,
                    Name = file.Name,
                    WebViewLink = file.WebViewLink
                };
                FolderList.Add(File);
            }
            return FolderList;
        }


        public string DownloadGoogleFileByName(string fileName)
        {
            var files = GetFiles();
            string fileId = files.Where(f => f.Name == (fileName + Path.GetExtension(f.Name))).Select(f => f.ID).FirstOrDefault();
            if (fileId != null)
                return DownloadGoogleFile(fileId);
            else
                throw new Exception("icorrect file name");
            
        }

        private string DownloadGoogleFile(string fileId)
        {
            var service = GetService();
            string FolderPath = HttpContext.Current.Server.MapPath("/GoogleDriveFiles/");
            FilesResource.GetRequest request = service.Files.Get(fileId);
            string FileName = request.Execute().Name;
            string FilePathToDownload = Path.Combine(FolderPath, FileName);
            var stream = new MemoryStream();
            request.MediaDownloader.ProgressChanged +=
                (IDownloadProgress progress) =>
                {
                    switch (progress.Status)
                    {
                        case DownloadStatus.Downloading:
                            {
                                break;
                            }
                        case DownloadStatus.Completed:
                            {
                                using (FileStream file = new FileStream(FilePathToDownload, FileMode.Create, FileAccess.ReadWrite))
                                {
                                    stream.WriteTo(file);
                                }
                                break;
                            }
                        case DownloadStatus.Failed:
                            {
                                break;
                            }
                    }
                };
            request.Download(stream);
            return FilePathToDownload;
        }

        public void UplaodFileOnDrive(HttpPostedFileBase file , string drugId)
        {
            if (file != null && file.ContentLength > 0)
            {
                //create service
                DriveService service = GetService();
                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/GoogleDriveFiles"),
                Path.GetFileName(drugId));
                file.SaveAs(path);
                var FileMetaData = new Google.Apis.Drive.v3.Data.File();
                FileMetaData.Name = Path.GetFileName(drugId);
                FileMetaData.MimeType = MimeMapping.GetMimeMapping(path);
                FilesResource.CreateMediaUpload request;
                using (var stream = new System.IO.FileStream(path, System.IO.FileMode.Open))
                {
                    request = service.Files.Create(FileMetaData, stream, FileMetaData.MimeType);
                    request.Fields = "id";
                    request.Upload();
                }

            }
        }

        public void CreateFolder(string FolderName)
        {
            DriveService service = GetService();
            var FileMetaData = new Google.Apis.Drive.v3.Data.File();
            FileMetaData.Name = FolderName;
            //this mimetype specify that we need folder in google drive
            FileMetaData.MimeType = "application/vnd.google-apps.folder";
            FilesResource.CreateRequest request;
            request = service.Files.Create(FileMetaData);
            request.Fields = "id";
            var file = request.Execute();

        }

        private string FolderNameToFolderId(string folderName)
        {
            return (from folder in this.GetDriveFolders()
                    where folder.Name == folderName
                    select folder.ID).FirstOrDefault();

        }

        public void UplaodFileOnDriveInFolder(HttpPostedFileBase file, string folderName)
        {
            if (file != null && file.ContentLength > 0)
            {
                //convert from folder name to it Id
                string folderId = this.FolderNameToFolderId(folderName);
                //if folder not founded- create one and enter the file to the new folder
                if (folderId == null)
                {
                    this.CreateFolder(folderName);
                    folderId = this.FolderNameToFolderId(folderName);
                }
                //create service
                DriveService service = GetService();
                //get file path
                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/GoogleDriveFiles"),
                Path.GetFileName(file.FileName));
                file.SaveAs(path);
                //create file metadata
                var FileMetaData = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = Path.GetFileName(file.FileName),
                    MimeType = MimeMapping.GetMimeMapping(path),
                    //id of parent folder 
                    Parents = new List<string>
                    {
                        folderId
                    }
                };
                FilesResource.CreateMediaUpload request;
                //create stream and upload
                using (var stream = new System.IO.FileStream(path, System.IO.FileMode.Open))
                {
                    request = service.Files.Create(FileMetaData, stream, FileMetaData.MimeType);
                    request.Fields = "id";
                    request.Upload();
                }
                var file1 = request.ResponseBody;
            }
        }


        /// File upload to the Google Drive in existing folder with new file name
        public void UplaodFileOnDriveInFolder(HttpPostedFileBase file, string newFileName, string folderName)
        {
            if (file != null && file.ContentLength > 0)
            {
                //convert from folder name to it Id
                string folderId = this.FolderNameToFolderId(folderName);
                //if folder not founded- create one and enter the file to the new folder
                if (folderId == null)
                {
                    this.CreateFolder(folderName);
                    folderId = this.FolderNameToFolderId(folderName);
                }
                //create service
                DriveService service = GetService();
                //get file path
                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/GoogleDriveFiles"),
                Path.GetFileName(file.FileName));
                file.SaveAs(path);
                //create file metadata
                var FileMetaData = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = newFileName + Path.GetExtension(file.FileName),
                    MimeType = MimeMapping.GetMimeMapping(path),
                    //id of parent folder 
                    Parents = new List<string>
                    {
                        folderId
                    }
                };
                FilesResource.CreateMediaUpload request;
                //create stream and upload
                using (var stream = new System.IO.FileStream(path, System.IO.FileMode.Open))
                {
                    request = service.Files.Create(FileMetaData, stream, FileMetaData.MimeType);
                    request.Fields = "id";
                    request.Upload();
                }
                var file1 = request.ResponseBody;
            }
        }

        public void DeleteFile(string fileId)
        {
            Google.Apis.Drive.v3.DriveService service = GetService();
            try
            {
                // Initial validation.    
                if (service == null)
                    throw new ArgumentNullException("service");

                if (fileId == null)
                    throw new ArgumentNullException(fileId);

                // Make the request.    
                service.Files.Delete(fileId).Execute();
            }
            catch (Exception ex)
            {
                throw new Exception("Request Files.Delete failed.", ex);
            }
        }
    }
}