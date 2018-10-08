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


            var readStream = new ReadStream(filePath);
            do
            {
                Read(readStream, out var result);
                Console.WriteLine(result);
            } while (!readStream.EndOfStream());



            Console.ReadLine();
        }

        public static void Read(ReadStream readStream, out string val)
        {

            val = readStream.ReadLine();
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

        public bool EndOfStream()
        {
            return streamReaderLazy.Value.EndOfStream;
        }
    }
}
