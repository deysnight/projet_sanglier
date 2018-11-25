using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace chasse_serv
{
    public class Utils
    {
        public static ContentResult Retrive_file(string file, string type, string folder)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), folder);
            filePath += "\\" + file;
            string line = File.ReadAllText(filePath);
            return new ContentResult
            {
                ContentType = type,
                StatusCode = (int)HttpStatusCode.OK,
                Content = line
            };
        }


    }
}
