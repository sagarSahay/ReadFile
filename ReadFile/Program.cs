using System;

namespace ReadFile
{
    using System.IO;
    using Microsoft.Extensions.Configuration;

    public class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configuration = builder.Build();

            var filePath = configuration.GetSection("FilePath").Value;

            while (Read(filePath, out var result))
            {
                Console.WriteLine(result);
            }
        }

        public static bool Read(string filePath, out string val)
        {
            var readStream = new ReadStream(filePath);
            val = readStream.ReadLine();
            return string.IsNullOrEmpty(val);
        }
    }

    public interface IReadStream : IDisposable
    {
        string ReadLine();
    }

    public class ReadStream : IReadStream
    {
        private string fileName;
        private Lazy<StreamReader> streamReaderLazy;

        public ReadStream(string fileName)
        {
            this.fileName = fileName;
            this.streamReaderLazy = new Lazy<StreamReader>(() => new StreamReader(fileName));
        }
        public void Dispose()
        {
            streamReaderLazy?.Value?.Dispose();
        }

        public string ReadLine()
        {
            return streamReaderLazy.Value.ReadLine();
        }
    }
}
