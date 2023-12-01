using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGameMovieEngine.Engine.Resources
{
    public class Resource
    {
        public string Name { get; } // Имя файла с расширением
        public string FullName { get; } // Полный путь к файлу
        public string ServerUrl { get; } // Прямая ссылка на ресурс в интернете

        /// <summary>
        /// Проверяет существование ресурса в папке проекта
        /// </summary>
        public bool Exist()
        {
            return File.Exists(FullName);
        }
        public Resource(string fullName, string serverUrl)
        {
            Name = Path.GetFileName(fullName);
            FullName = fullName;
            ServerUrl = serverUrl;
        }
    }
}
