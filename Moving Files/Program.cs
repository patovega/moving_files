using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Moving_Files
{
    class Program
    {
        static void Main(string[] args)
        {
            Run();
        }

        public static void Run()
        {


            try
            {
                var currentVersionDirectory = MovingFiles.Default.official_folder;
                var newVersionDirectory = MovingFiles.Default.new_version_folder;


                var newVersionAvailable = CheckFolderForNewVersion(newVersionDirectory);

                if (!newVersionAvailable)
                {
                    return;
                }

                Console.WriteLine("preparing files for new version");
                //moving files 

                //1 - saving data in a new folder for backup
                var flagBackUpVersion = CreateBackup();

                if (!flagBackUpVersion)
                {
                    return;
                }


                Console.WriteLine("files copied to the folder successfully!");
                Console.WriteLine("Press any key for close the program...");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Exceptions.Show(ex);
            }


        }

        /// <summary>
        /// Create a folder for the current version backup.
        /// </summary>
        /// <returns>bools</returns>
        private static bool CreateBackup()
        {
            try
            {
                string folderName = Utils.NameWithCurrentDatetime();
                string directory = MovingFiles.Default.root_folder + "\\" + folderName;

                if (!System.IO.Directory.Exists(directory))
                {    //if directory dont exist then create the folder
                    System.IO.Directory.CreateDirectory(directory);
                }

                Console.WriteLine("creating backup files for last version");
                Copy(MovingFiles.Default.official_folder, directory);
                Thread.Sleep(2000);
                Console.WriteLine("backup ready");
                Thread.Sleep(2000);
                Console.WriteLine("Installing new version");
                DeleteFilesFromFolder(MovingFiles.Default.official_folder);
                Thread.Sleep(2000);
                Copy(MovingFiles.Default.new_version_folder, MovingFiles.Default.official_folder);
                Console.WriteLine("Update its complete");
                return true;
            }
            catch (Exception ex)
            {
                Exceptions.Show(ex);
            }

            return false;
        }

        /// <summary>
        /// Move files from one directory to another. This copy can overwrite files with the same name (and extension)
        /// </summary>
        /// <param name="sourceDir">string, folder of current version</param>
        /// <param name="targetDir">string, folder for backup current version backup</param>
        public static void Copy(string sourceDir, string targetDir)
        {
            Directory.CreateDirectory(targetDir);

            foreach (var file in Directory.GetFiles(sourceDir))
                File.Copy(file, Path.Combine(targetDir, Path.GetFileName(file)));

            //recursive copy :P
            foreach (var directory in Directory.GetDirectories(sourceDir))
                Copy(directory, Path.Combine(targetDir, Path.GetFileName(directory)));
        }

        /// <summary>
        /// Deletes files and directories of one folder
        /// </summary>
        /// <param name="path">string</param>
        public static void DeleteFilesFromFolder(string path)
        {

            System.IO.DirectoryInfo di = new DirectoryInfo(path);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
        }
        /// <summary>
        /// Check if folder exist and if it has files.
        /// </summary>
        /// <param name="newVersionDirectory">string</param>
        /// <returns>bool</returns>
        private static bool CheckFolderForNewVersion(string newVersionDirectory)
        {
            try
            {
                if (!Directory.Exists(newVersionDirectory))
                {
                    Exceptions.Show("Folder for new version dont exist");

                    return false;
                }

                int files_count = Directory.GetFiles(newVersionDirectory, "*", SearchOption.TopDirectoryOnly).Length;

                if (files_count < 1)
                {
                    Exceptions.Show("Folder for new version exist but dont have any file");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Exceptions.Show(ex);
            }



            return true;
        }
    }
}
