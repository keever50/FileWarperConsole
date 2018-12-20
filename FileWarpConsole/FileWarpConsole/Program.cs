using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace FileWarpConsole
{

    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Selected files:");
            foreach(string name in args)
            {
                Console.WriteLine("     " + name );
            }
            Console.Write("Enter warp path>");
            string[] warp_entries = args;
            string warp_exit = Console.ReadLine();

            //Moves all files to warp path.
            Console.WriteLine("Setting up warpholes...");
            foreach(string entry in warp_entries)
            {
                string entryname = Path.GetFileName(entry);
                string newentry = Path.Combine(warp_exit, entryname);
                
                Console.WriteLine( "    " + newentry);

                File.Move(entry, newentry);
            }

            //setting up symlinks in cmd
            Console.WriteLine("Opening warpholes...");
            foreach (string entry in warp_entries)
            {
                string entryname = Path.GetFileName(entry);
                string newentry = Path.Combine(warp_exit, entryname);

                Console.WriteLine("/c " + "mklink " + entry + " " + newentry);


                Process cmd = new Process();
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.CreateNoWindow = false;
                cmd.StartInfo.UseShellExecute = false;
                cmd.Start();
                cmd.StandardInput.WriteLine(@"mklink " + entry + " " + newentry);
                cmd.StandardInput.Flush();
                cmd.StandardInput.Close();
                cmd.WaitForExit();
                Console.WriteLine(cmd.StandardOutput.ReadToEnd());
            }

            Console.WriteLine("Warpholes opened.");
            Console.ReadLine();
        }
    }
}
