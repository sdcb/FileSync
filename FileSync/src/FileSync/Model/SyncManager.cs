using FileSync.Functional;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileSync.Model
{
    public class SyncManager
    {
        public Result SyncFile(string sourceFileName, string destFileName)
        {
            var sourceFile = new FileInfo(sourceFileName);
            var descFile = new FileInfo(destFileName);

            if (!sourceFile.Exists)
            {
                return Result.Fail($"File {sourceFile} not exist.");
            }

            if (!descFile.Exists)
            {
                return Result.FromAction(() =>
                    File.Copy(sourceFileName, destFileName));
            }

            if (File.ReadLines(sourceFileName).SequenceEqual(File.ReadLines(destFileName)))
            {
                return Result.Ok();
            }
            else
            {
                return Result.FromAction(() =>
                    File.Copy(sourceFileName, destFileName, true));
            }
        }
    }
}
