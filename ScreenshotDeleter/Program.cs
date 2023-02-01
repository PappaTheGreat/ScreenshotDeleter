using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using System.Collections.Specialized;
using System.Net;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Data;
using ScreenshotDeleter;

namespace ConsoleApp2
{
    class Program
    {
        static DALModal dALModal = new DALModal();
        public static DataTable dt_log = null;
        static void Main(string[] args)
        {
            
            DataTable dt_Mail = new DataTable("Logs");
           
            string rootPath = @"C:\";
            var dirs = Directory.GetDirectories(rootPath, "*.*", SearchOption.TopDirectoryOnly);

            string n1;
            n1 = ConfigurationManager.AppSettings.Get("Key0");
            int n = int.Parse(n1);
            string n2;
            n2 = ConfigurationManager.AppSettings.Get("ScreenshotName");

            int totalCountDirectoryDelete = 0;
            int totalCountFileDelete = 0;
            int noFilesFound = 0;
            int noFoldersFound = 0;
            int noExcelFound = 0;
            int totalExcelFilesDelete = 0;
            int totalExcelsFilesDelete = 0;
            int noExcelsFound = 0;
            int noDownLoadsFound = 0; ;
            int totalDownLoadsFileDelete = 0;
            int totalCountEmptyDelete = 0;
            string dirPath = "";
            string dirPath2 = "";
            string dirPath3 = "";
            string dirPath4 = "";
            string dirPath5 = "";
            string dirPath6 = "";
            string dirPath7 = "";
            string dirPath10 = "";
            string myfile = string.Empty;
            string emp = string.Empty;
            List<int> totalCounter = new List<int>();
            List<int> totalCounter2 = new List<int>();
            List<int> DeletedCount = new List<int>();
            List<string> totalDirectories = new List<string>();
            IEnumerable<int> howManyDeleted = null;

            foreach (string dir in dirs)
            {

                dirPath = Path.GetFullPath(dir);
                if (dir.Contains("Prod_"))
                {
                    //Console.WriteLine(dirPath);
                    totalDirectories.Add(dirPath);
                    var countFiles = System.IO.Directory.GetFiles(dirPath, "*", SearchOption.AllDirectories).Count();
                    var countFolders = System.IO.Directory.GetDirectories(dirPath, "*", SearchOption.AllDirectories).Count();
                    //Console.WriteLine(countFiles);
                    //Console.WriteLine(countFolders);
                    //int[] total = new int[n];

                    int total = countFolders + countFiles;
                    totalCounter.Add(total);
                    

                    var dirs2 = Directory.GetDirectories(dirPath, "*.*", SearchOption.TopDirectoryOnly);
                    foreach (string dirIn in dirs2)
                    {
                        dirPath2 = Path.GetFullPath(dirIn);
                        //Console.WriteLine(dirPath2);
                        if (dirPath2.Contains("ScriptSchedular"))
                        {
                            /*DirectoryInfo currentDirectoryInfo = new DirectoryInfo(dirPath2);
                            String grandParentPath = currentDirectoryInfo.Parent.Name;
                            Console.WriteLine(grandParentPath);*/

                            var dirs3 = Directory.GetDirectories(dirPath2, "*.*", SearchOption.TopDirectoryOnly);
                            foreach (string dirIn2 in dirs3)
                            {
                                dirPath3 = Path.GetFullPath(dirIn2);
                                //Console.WriteLine(dirPath3);
                                if (dirPath3 != null)
                                {

                                    var dirs4 = Directory.GetDirectories(dirPath3, "*.*", SearchOption.TopDirectoryOnly);
                                    //Console.WriteLine(dirs4);
                                    foreach (string dirIn3 in dirs4)
                                    {
                                        dirPath4 = Path.GetFullPath(dirIn3);
                                        //Console.WriteLine(dirPath4);
                                        string parent = System.IO.Directory.GetParent(dirPath4).FullName;
                                        /*System.IO.DirectoryInfo dirrrrr = new System.IO.DirectoryInfo(dirPath2);
                                        int count = dirrrrr.GetFiles().Count();
                                        count++;
                                        Console.WriteLine(count);*/
                                        if (dirPath4.Contains("Excels"))
                                        {
                                            var excelFiles = Directory.GetFiles(dirPath4, "*.*", SearchOption.TopDirectoryOnly);
                                            foreach (string excelsToDelete in excelFiles)
                                            {
                                                noExcelFound++;
                                                dirPath7 = Path.GetFullPath(excelsToDelete);
                                                FileInfo fi1 = new FileInfo(dirPath7);

                                                if (fi1.LastWriteTime < DateTime.Now.AddDays(-n))
                                                {
                                                    fi1.Delete();
                                                    totalExcelFilesDelete++;
                                                    //Console.WriteLine("Excels Deleted : " + dirPath7);
                                                }
                                            }
                                        }
                                        if (dirPath4.Contains("Excel"))
                                        {
                                            var excelsFiles = Directory.GetFiles(dirPath4, "*.*", SearchOption.TopDirectoryOnly);
                                            foreach (string excelToDelete in excelsFiles)
                                            {
                                                noExcelsFound++;
                                                string dirPath8 = Path.GetFullPath(excelToDelete);
                                                FileInfo fi4 = new FileInfo(dirPath8);

                                                if (fi4.LastWriteTime < DateTime.Now.AddDays(-n))
                                                {
                                                    fi4.Delete();
                                                    totalExcelsFilesDelete++;
                                                    //Console.WriteLine("Excels Deleted : " + dirPath8);
                                                }
                                            }
                                        }
                                        if (dirPath4.Contains("Download"))
                                        {
                                            var downLoadsFiles = Directory.GetFiles(dirPath4, "*.*", SearchOption.TopDirectoryOnly);
                                            foreach (string downloadsToDelete in downLoadsFiles)
                                            {
                                                noDownLoadsFound++;
                                                string dirPath8 = Path.GetFullPath(downloadsToDelete);
                                                FileInfo fi2 = new FileInfo(dirPath8);

                                                if (fi2.LastWriteTime < DateTime.Now.AddDays(-n))
                                                {
                                                    fi2.Delete();
                                                    totalDownLoadsFileDelete++;
                                                    //Console.WriteLine("Downloads Deleted : " + dirPath8);
                                                }
                                            }
                                        }


                                        if (dirPath4.Contains("Screenshots"))
                                        {

                                            var Directories = Directory.GetDirectories(dirPath4, "*.*", SearchOption.AllDirectories);
                                            var Files = Directory.GetFiles(dirPath4, "*.*", SearchOption.TopDirectoryOnly);

                                            foreach (string directoriesToDelete in Directories)
                                            {
                                                noFoldersFound++;
                                                dirPath5 = Path.GetFullPath(directoriesToDelete);
                                                //Console.WriteLine(dirPath5);

                                                if (dirPath5 != null)
                                                {

                                                    string[] folders = Directory.GetFiles(dirPath5, "*", SearchOption.AllDirectories);
                                                    //Console.WriteLine(folders);
                                                    foreach (string ssInsideDirectory in folders)
                                                    {

                                                        string dirPath9 = Path.GetFullPath(ssInsideDirectory);
                                                        //Console.WriteLine(dirPath9);

                                                        FileInfo fi3 = new FileInfo(dirPath9);
                                                        noFoldersFound++;
                                                        if (fi3.LastWriteTime < DateTime.Now.AddDays(-n))
                                                        {

                                                            fi3.Delete();
                                                            
                                                            //Console.WriteLine(myfile = ("Screenshots inside Folder Deleted : " + dirPath9));
                                                            //emp+= Environment.NewLine + myfile + Environment.NewLine ;
                                                            //Console.WriteLine(n2);
                                                        }
                                                        totalCountDirectoryDelete++;

                                                    }
                                                }
                                            }

                                            foreach (string filesToDelete in Files)
                                            {
                                                
                                                noFilesFound++;
                                                dirPath6 = Path.GetFullPath(filesToDelete);
                                                //Console.WriteLine(dirPath6);
                                                FileInfo fi = new FileInfo(dirPath6);
                                                if (fi.LastWriteTime < DateTime.Now.AddDays(-n))
                                                {
                                                    fi.Delete();
                                                    
                                                    //Console.WriteLine("File Deleted: " + dirPath6);
                                                }
                                                totalCountFileDelete++;
                                               
                                            }

                                            foreach (string emptyFoldersToDelete in Directories)
                                            {
                                                dirPath10 = Path.GetFullPath(emptyFoldersToDelete);
                                                //Console.WriteLine(dirPath10);
                                                DirectoryInfo di3 = new DirectoryInfo(dirPath10);
                                                if (di3.LastWriteTime < DateTime.Now.AddDays(-n))
                                                {
                                                    Directory.Delete(dirPath10);

                                                    
                                                }
                                                totalCountEmptyDelete++;

                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }
                    var countFilesEnd = System.IO.Directory.GetFiles(dirPath, "*", SearchOption.AllDirectories).Count();
                    var countFoldersEnd = System.IO.Directory.GetDirectories(dirPath, "*", SearchOption.AllDirectories).Count();
                    int total2 = countFoldersEnd + countFilesEnd;
                    totalCounter2.Add(total2);
                    howManyDeleted = totalCounter2.Except(totalCounter2);
                    //Console.WriteLine(total2);
                }

            }
            
            for (int i = 0; i < totalCounter.Count; i++)
            {
                Console.WriteLine(totalCounter[i]);
            }
            for (int i = 0; i < totalCounter2.Count; i++)
            {
                Console.WriteLine(totalCounter2[i]);
            }
            for (int i = 0; i < totalDirectories.Count; i++)
            {
                Console.WriteLine(totalDirectories[i]);
            }
            for (int i = 0; i <= totalCounter.Count - 1; i++)
            {
                DeletedCount.Add(totalCounter[i] - totalCounter2[i]);
            }
            for (int i = 0; i <= DeletedCount.Count - 1; i++)
            {
                Console.WriteLine(DeletedCount[i]);
            }

            /*foreach (var val in howManyDeleted)
            {
                DeletedCount.Add(val);
            }
            for (int i = 0; i < DeletedCount.Count; i++)
            {
                Console.WriteLine(DeletedCount[i]);
            }*/

            //Console.WriteLine(howManyDeleted.GetType());
            //DeletedCount = totalCounter.Except(totalCounter2);


            //string[] tempDirs = Directory.GetDirectories(rootPath, "*", SearchOption.TopDirectoryOnly);
            var tmpPath = Path.GetTempPath();
            var tempFiles = Directory.GetFiles(tmpPath, "*.*", SearchOption.AllDirectories);
            var tempFolders = Directory.GetDirectories(tmpPath, "*.*", SearchOption.AllDirectories);
            foreach (string allTempFiles in tempFiles)
            {
                string tempPath = Path.GetFullPath(allTempFiles);
                //Console.WriteLine(tempPath);
                FileInfo fi4 = new FileInfo(tempPath);
                try
                {
                    if (fi4.LastAccessTime < DateTime.Now)
                    {
                        fi4.Delete();
                    }
                }
                catch (System.IO.IOException)
                {
                    continue;
                }
                catch (UnauthorizedAccessException)
                {
                    continue;
                }
            }
            foreach (string allTempFolders in tempFolders)
            {
                string tempPath2 = Path.GetFullPath(allTempFolders);
                //Console.WriteLine(tempPath2);
                DirectoryInfo di4 = new DirectoryInfo(tempPath2);
                try
                {
                    if (di4.LastAccessTime < DateTime.Now.AddDays(-n))
                    {
                        Directory.Delete(tempPath2, true);
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    continue;
                }
                catch (System.IO.IOException)
                {
                    continue;
                }
            }
            Console.WriteLine(Environment.NewLine + "No. of Screenshots found : " + (noFilesFound + noFoldersFound));
            Console.WriteLine("Total Number of Screenshot deleted : " + (totalCountFileDelete + totalCountDirectoryDelete + totalCountEmptyDelete) + Environment.NewLine);

            Console.WriteLine("Total Number of Excel files found : " + (noExcelFound));
            Console.WriteLine("Total Number of Excel files deleted : " + ((totalExcelFilesDelete + totalExcelsFilesDelete) + Environment.NewLine));

            Console.WriteLine("Total Number of Download files found : " + (noDownLoadsFound));
            Console.WriteLine("Total Number of Download files deleted : " + (totalDownLoadsFileDelete + Environment.NewLine));

            

            //Console.WriteLine(n2);
            Console.ReadLine();

            dt_log = new DataTable();
            dt_log.Columns.AddRange(new DataColumn[3]
                    {
                        new DataColumn("Sr No.", typeof(string)),
                        new DataColumn("Folder Name", typeof(string)),
                        new DataColumn("Screenshot Path", typeof(string)),

                    });
            DALModal.insertLog(dt_log);

        }
      
        


    }
}
        
        
    
    
    
