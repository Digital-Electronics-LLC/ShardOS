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
            string version = "0.2.0";
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
        }
    }
}
