using System;
using Sys = Cosmos.System;
using Cosmos.System.FileSystem.VFS;
using System.IO;

namespace Shard
{
    public class Kernel : Sys.Kernel
    {
        Sys.FileSystem.CosmosVFS fs = new Sys.FileSystem.CosmosVFS();
        string current_directory = "0:\\"
        protected override void BeforeRun()
        {
            Console.WriteLine("ShardOS booted successfully. Type 'help' for a list of commands");
            Console.WriteLine("Booting into Shell\n");
            Sys.FileSystem.CosmosVFS fs = new Sys.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            fs.Format("0" /*drive id*/, "FAT32" /*fat type*/, true /*use quick format*/);
        }

        protected override void Run()
        {
            string version = "0.2.1";
            bool isDev = true // put false when stable
            string input;
            Console.Write("/> ");
            input = Console.ReadLine();
            
            if (input == "about")
            {
                if (isDev == true)
                    {
                        Console.WriteLine("\nThis is ShardOS ", version, "\nYou are using a dev version\nPlease update as soon as possible\n");
                    }
                else
                {
                    Console.WriteLine("\nThis is ShardOS ", version)
                }
            }
            else if (input == "help")
            {
                Console.WriteLine("\nCommands Availible:\nHelp --- Gets you to this message\nAbout --- Tells you about this OS\nGetSize --- Tells you the fs size\nGetType --- Tells you the fs type\nls --- lists all the files in the 0 dir\nmk [file name] --- creates a file\nwr [file name] --- writes into a file");
            }
            else if (input == "GetSize")
            {
                var available_space = fs.GetAvailableFreeSpace(@"0:");
                Console.WriteLine("Available Free Space: " + available_space);
            }
            else if (input == "GetType")
            {
                var fs_type = fs.GetFileSystemType(@"0:");
                Console.WriteLine("File System Type: " + fs_type);
            }
            else if (input == "ls")
            {
                var directory_list = Directory.GetFiles(current_directory);
                foreach (var file in directory_list)
                {
                    Console.WriteLine(file);
                }
            }
            else if (input.StartsWith("mk "))
            {
                string fname = input.Remove(0, 3);

                try
                {
                    var file_stream = File.Create(current_directory + fname);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                Console.WriteLine("File successfully created");
            }
            else if (input.StartsWith("mkdir "))
            {
                string dname = input.Remove(0, 6);

                try
                {
                    var file_stream = Directory.CreateDirectory(current_directory + dname);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                Console.WriteLine("Directory succefully created");
            }
            else if (input.StartsWith("wr "))
            {
                string file = input.Remove(0, 3);
                Console.WriteLine("What would you like to write into this file? Type the contents ex: hello! My name is John");
                string contents = Console.ReadLine();
                try
                {
                    File.WriteAllText(current_directory + file, contents);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            else if (input.StartsWith("cd "))
            {
                bool isDirExist = Directory.Exists(current_directory + input.Remove(0, 3));
                try
                {
                    if (isDirExist == true)
                    {
                        current_directory = current_directory + input.Remove(0, 3);
                    }
                    else if (input.Remove(0,3) == "0:\\")
                    {
                        current_directory = "0:\\";
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
    }
}
