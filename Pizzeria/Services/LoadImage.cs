using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Pizzeria.Services
{
    public static class LoadImage
    {
        public static byte[] GetPictureData(string path)
        {
            FileStream fileStream = new FileStream(path, FileMode.Open);
            byte[] byteData = new byte[fileStream.Length];
            fileStream.Read(byteData, 0, byteData.Length);
            fileStream.Close();
            return byteData;
        }
    }
}
