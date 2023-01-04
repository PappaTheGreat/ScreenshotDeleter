using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            string rootPath = @"C:\";
            var dirs = Directory.GetDirectories(rootPath, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string dir in dirs)
            {
                string dirPath = Path.GetFullPath(dir);
                if (dir.Contains("Prod_"))
                {
                    //Console.WriteLine(dir);
                    var dirs2 = Directory.GetDirectories(dirPath, "*.*", SearchOption.TopDirectoryOnly);
                    foreach (string dirIn in dirs2)
                    {
                        string dirPath2 = Path.GetFullPath(dirIn);
                        //Console.WriteLine(dirPath2);
                        if (dirPath2.Contains("ScriptSchedular"))
                        {
                            var dirs3 = Directory.GetDirectories(dirPath2, "*.*", SearchOption.TopDirectoryOnly);
                            foreach (string dirIn2 in dirs3)
                            {
                                string dirPath3 = Path.GetFullPath(dirIn2);
                                //Console.WriteLine(dirPath3);
                                if (dirPath3 != null)
                                {

                                    var dirs4 = Directory.GetDirectories(dirPath3, "*.*", SearchOption.TopDirectoryOnly);
                                    foreach (string dirIn3 in dirs4)
                                    {
                                        string dirPath4 = Path.GetFullPath(dirIn3);
                                        //Console.WriteLine(dirPath4);
                                        if (dirPath4.Contains("Screenshots"))
                                        {
                                            var Directories = Directory.GetDirectories(dirPath4, "*.*", SearchOption.TopDirectoryOnly);
                                            var Files = Directory.GetFiles(dirPath4, "*.*", SearchOption.TopDirectoryOnly);
                                            foreach (string directoriesToDelete in Directories)
                                            {
                                                string dirPath5 = Path.GetFullPath(directoriesToDelete);
                                                Console.WriteLine("Folder Deleted: " + dirPath5);
                                                DirectoryInfo di = new DirectoryInfo(dirPath5);
                                                if (di.LastWriteTime < DateTime.Now.AddMonths(-1))
                                                {
                                                    Directory.Delete(dirPath5);
                                                }
                                            }
                                            foreach (string filesToDelete in Files)
                                            {
                                                string dirPath6 = Path.GetFullPath(filesToDelete);
                                                //Console.WriteLine("File Deleted: " + dirPath6);
                                                FileInfo fi = new FileInfo(dirPath6);
                                                if (fi.LastWriteTime < DateTime.Now.AddMonths(-1))
                                                {
                                                    //fi.Delete();
                                                }
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }
                }

            }
            Console.ReadLine();
        }
    }
}