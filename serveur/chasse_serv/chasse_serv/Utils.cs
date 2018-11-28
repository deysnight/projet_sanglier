using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
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

        public static bool user_logged(HttpRequest request)
        {
            if (request.Cookies["logged_in"] == "true")
                return true;
            return false;
        }

        public static string Encrypt(string inputString)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha256.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }




    }
}
