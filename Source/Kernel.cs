using System;
using Sys = Cosmos.System;


namespace Shard
{
    public class Kernel : Sys.Kernel
    {
        protected override void BeforeRun()
        {
            Console.WriteLine("ShardOS booted successfully. Type 'help' for a list of commands");
            Console.WriteLine("Booting into Shell\n")
        }

        protected override void Run()
        {
            string version = "0.1.0-02";
            string input;
            Console.Write("/> ");
            input = Console.ReadLine();

            if (input == "about") 
            {
                Console.WriteLine("\nThis is ShardOS", version, "\nYou are using a dev version\nPlease update as soon as possible\n");
            }
            if (input == "help")
            {
                Console.WriteLine("\nCommands Availible:\nHelp --- Gets you to this message\nAbout --- Tells you about this OS");
            }
        }
    }
}
