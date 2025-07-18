using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Project.Code.Runtime.Architecture.FileSystem
{
    public sealed class FileProvider : IFileProvider
    {
        private CancellationTokenSource cancellationToken;

        public FileProvider() =>
            this.cancellationToken = new();

        public async Task<string> ReadFileAsync(string filePath)
        {
            this.cancellationToken = new();
            if (!File.Exists(filePath))
                return string.Empty; // Don't create an empty file, just return empty

#if UNITY_EDITOR
            Debug.Log($"Reading file by path : {filePath}");
#endif

            using FileStream sourceStream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true);
            using StreamReader reader = new(sourceStream);
            string content = await reader.ReadToEndAsync();
            this.cancellationToken.Cancel();
            this.cancellationToken = default;
            return content;
        }

        public async Task WriteFileAsync(string filePath, string text)
        {
#if UNITY_EDITOR
            Debug.Log($"Writing file by path : {filePath}");
#endif

            using FileStream destinationStream = new(filePath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true);
            using StreamWriter writer = new(destinationStream);
            await writer.WriteAsync(text);
        }

        public void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
                File.Delete(filePath);
            else
            {
#if UNITY_EDITOR
                Debug.Log($"Can't delete file by path : {filePath}");
#endif
            }
        }

        public void CreateSavesFolder(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
        }

        public void Cancel()
        {
            if (this.cancellationToken is null) return;
            this.cancellationToken.Cancel();
            this.cancellationToken.Dispose();
        }
    }
}