using ConferenceTrackManagement.Interfaces;
using ConferenceTrackManagement.Parser;
using System;

namespace ConferenceTrackManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\t**Welcome To Conference Track Management**\n\n");

            IContentParser contentParser;

            //Input File
            if (args.Length == 1)
            {
                var filePath = args[0];
                contentParser = new FileParser(filePath);
            }
            else
            {
                var data = LoadData();
                contentParser = new StringParser(data);

            }

            var parser = new BaseParser(contentParser);

            var trackManagement = new TrackManagement("Conference 1", parser);
            trackManagement.Schedule();
            trackManagement.PrintSchedule();

            Console.WriteLine("\n\n\t**End To Conference Track Management**\n\n");
            Console.WriteLine("Press Enter To Continue...");
            Console.ReadLine();
        }

        public static string LoadData()
        {
            return @"Enterprise Rails 60min
                    Overdoing it in Python 45min
                    Lua for the Masses 30min
                    Ruby Errors from Mismatched Gem Versions 45min
                    Common Ruby Errors 45min
                    Rails for Python Developers lightning
                    Communicating Over Distance 60min
                    Accounting-Driven Development 45min
                    Woah 30min
                    Sit Down and Write 30min
                    Pair Programming vs Noise 45min
                    Rails Magic 60min
                    Ruby on Rails: Why We Should Move On 60min
                    Clojure Ate Scala (on my project) 45min
                    Programming in the Boondocks of Seattle 30min
                    Ruby vs. Clojure for Back-End Development 30min
                    Ruby on Rails Legacy App Maintenance 60min
                    A World Without HackerNews 30min
                    User Interface CSS in Rails Apps 30min";

            //return @"Enterprise Rails 10min
            //        Overdoing it in Python 45min
            //        Lua for the Masses 30min
            //        Ruby Errors from Mismatched Gem Versions 45min
            //        Common Ruby Errors 45min
            //        ";
        }
    }
}
