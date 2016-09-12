using FileSync.Functional;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileSync.Model
{
    public static class ConfigReader
    {
        public static Result<SyncDefinition> CreateSyncDefinitionFromJObject(JObject jObject)
        {
            return Result.Ok((Maybe<string>)jObject.Value<string>("version"))
                .Ensure(version => version.HasValue, $"No version specified in config file.")
                .OnSuccess(version => version.Value)
                .OnSuccess(version => CreateByVersion(version, jObject));
        }

        public static Result<SyncDefinition> CreateByVersion(string version, JObject jObject)
        {
            switch (version)
            {
                case "1.0":
                    return SyncDefinitionV1.Create(jObject).Map(v => (SyncDefinition)v);
                default:
                    return Result.Fail<SyncDefinition>($"Unknown version '{version}'.");
            }
        }

        public static Result<JObject> ReadFileAsJObject(string configFile)
        {
            return Result.Ok(new FileInfo(configFile))
                .Ensure(file => file.Exists, $"File {configFile} not exists.")
                .Map(file => File.ReadAllText(configFile))
                .OnSuccess(json => JObject.Parse(json));
        }
    }
}
