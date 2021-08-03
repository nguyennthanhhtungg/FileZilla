
using FluentFTP;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FileZilla.Services
{
    public class FileZillaService : IFileZillaService
    {
        public async Task UploadFile(string fileData, string fileName)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(fileData);

            NameValueCollection fileZillaSettings = ConfigurationManager.GetSection("FileZillaSettings") as NameValueCollection; 

            string host = fileZillaSettings["host"];
            int port = Int32.Parse(fileZillaSettings["port"]);
            string user = fileZillaSettings["user"];
            string pass = fileZillaSettings["pass"];

            using (FtpClient ftpClient = new FtpClient(host, port, user, pass))
            {
                using (MemoryStream memoryStream = new MemoryStream(bytes))
                {
                    await ftpClient.UploadAsync(memoryStream, fileName);
                }
            }
        }
    }
}
