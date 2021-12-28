using System;
using Sys = Cosmos.System;
using Cosmos.System.FileSystem.VFS;

namespace Shard
{
    public class Kernel : Sys.Kernel
    {
        Sys.FileSystem.CosmosVFS fs = new Sys.FileSystem.CosmosVFS();
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
            string input;
            Console.Write("/> ");
            input = Console.ReadLine();

            if (input == "about") 
            {
                Console.WriteLine("\nThis is ShardOS", version, "\nYou are using a dev version\nPlease update as soon as possible\n");
            }
            if (input == "help")
            {
                Console.WriteLine("\nCommands Availible:\nHelp --- Gets you to this message\nAbout --- Tells you about this OS\nGetSize --- Tells you the fs size\nGetType --- Tells you the fs type");
            }
            if (input == "GetSize") {
                var available_space = fs.GetAvailableFreeSpace(@"0:\");
                Console.WriteLine("Available Free Space: " + available_space);
            }
            if (input == "GetType") {
                var fs_type = fs.GetFileSystemType(@"0:\");
                Console.WriteLine("File System Type: " + fs_type);
            }
            if (input == "ls") {
                var directory_list = Directory.GetFiles(@"0:\");
                foreach (var file in directory_list)
                {
                    Console.WriteLine(file);
                }
            }
            if (input == "mk") {
                Console.Write("File name: ");
                string fname = Console.ReadLine();
                
                try
                {
                    var file_stream = File.Create(@"0:\" + fname);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                Console.WriteLine("File successfully created");
            }
            if (input == "wr") {
                Console.WriteLine("Which File would you like to write to? Type the name and extension ex: example.txt");
                string file = Console.ReadLine();
                Console.WriteLine("What would you like to write into this file? Type the contents ex: hello! My name is John");
                string contents = Console.ReadLine();
                try
                {
                    File.WriteAllText(@"0:\" + file, contents);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
           } 
        }
    }
}
