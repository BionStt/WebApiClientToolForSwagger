using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace WebApiClient.Tool
{
    internal class FileUtil
    {
        public static async Task CreateFile(string filePath,string fileName, string content)
        {
            bool exists = Directory.Exists(filePath);
            if (!exists) {
                Directory.CreateDirectory(filePath);
            }

            using (FileStream stream = File.Create($"{filePath}{fileName}")) 
            {
                await stream.WriteAsync(Encoding.UTF8.GetBytes(content));
            }
        }
    }
}
