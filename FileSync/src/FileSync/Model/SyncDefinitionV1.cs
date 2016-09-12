using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileSync.Functional;
using Newtonsoft.Json.Linq;

namespace FileSync.Model
{
    public class SyncDefinitionV1 : SyncDefinition
    {
        public override string Version => "1.0";

        public List<FileSyncDefinitionV1Item> Items { get; set; }

        public static Result<SyncDefinitionV1> Create(JObject jObject)
        {
            return Result.FromFunc(() => jObject.ToObject<SyncDefinitionV1>());
        }

        public override Result Sync()
        {
            return Result.Combine(Items
                .Select(x => SyncManager.SyncFile(x.SourceFileName, x.DestFileName)));
        }
    }

    public class FileSyncDefinitionV1Item
    {
        public string SourceFileName { get; set; }

        public string DestFileName { get; set; }
    }
}
