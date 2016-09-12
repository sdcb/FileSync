using FileSync.Functional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileSync.Model
{
    public abstract class SyncDefinition
    {
        public virtual string Version { get; }

        public SyncManager SyncManager { get; set; } = new SyncManager();

        public static Result<SyncDefinition> Create(string configFile)
        {
            return Result.Ok(configFile)
                .OnSuccess(file => ConfigReader.ReadFileAsJObject(file))
                .OnSuccess(jObject => ConfigReader.CreateSyncDefinitionFromJObject(jObject));
        }

        public abstract Result Sync();
    }
}
