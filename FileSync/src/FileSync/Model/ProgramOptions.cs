using CommandLine;
using FileSync.Functional;
using Mono.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileSync.Model
{
    public class ProgramOptions
    {
        [Option('f', "configFile", Required = true, HelpText = "Config file to be processed.")]
        public string ConfigFile { get; set; }

        public Result<ProgramOptions> BusinessValidate()
        {
            Func<string, Result> validateFile = (string fileName) => Result
                .FromFunc(() => new FileInfo(fileName))
                .Ensure(fileInfo => fileInfo.Exists, $"Config file '{fileName}' not exists.");

            return validateFile(ConfigFile)
                .OnSuccess(() => this);    
        }

        public static Result<ProgramOptions> Parse(string[] args)
        {
            return Parser.Default.ParseArguments<ProgramOptions>(args)
                .WithParsed(options => options.BusinessValidate())
                .MapResult(
                    option => option.BusinessValidate(), 
                    errors => Result.Fail<ProgramOptions>(string.Join(";", errors)));
        }
    }
}
