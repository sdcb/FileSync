using FileSync.Functional;
using FileSync.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileSync.App
{
    public static class Application
    {
        public static int Execute(string[] args)
        {
            var result = ProgramOptions.Parse(args)
                .OnSuccess(option => SyncDefinition.Create(option.ConfigFile))
                .OnSuccess(definition => definition.Sync());

            if (result.IsSuccess)
            {
                Console.WriteLine("Sync OK.");
                return 0;
            }
            else
            {
                Console.WriteLine(result.Error);
                return 1;
            }
        }
    }
}
