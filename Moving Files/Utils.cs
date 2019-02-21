using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moving_Files
{
    class Utils
    {
        /// <summary>
        /// Returns  a string with the current datetime using a underlines like to separator.
        /// </summary>
        /// <returns>strings</returns>
        public static string NameWithCurrentDatetime()
        {

            string folderName = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            folderName = folderName.Replace("-", "_");
            folderName = folderName.Replace(" ", "_");
            folderName = folderName.Replace(":", "_");

            return folderName;
        }
    }
}
