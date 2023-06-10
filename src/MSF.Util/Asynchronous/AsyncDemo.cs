using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MSF.Util.Asynchronous
{
    public class AsyncDemo
    {
        // Using 'async await' put tasks in a separated thread, allowing the main thread to remain responsive and handle user interactions.

        /* ASYNC - Used to mark a method as asynchronous. Indicated that the method can perform a non-blocking operation and return a Task or Task<TResult> object
         *      Cannot be used with contructors and properties
         *      Must contain at least one 'await' expression
         *      Can have multiple 'await' expressions
         */

        /* AWAIT - Used with and 'async' method to temporarily suspend its executions and yield contral back to the calling method until the awaited task is completed.
         */

        // A palavra-chave AWAIT passa o controle de execução para o método chamador (anterior)!
        // Para evitar que o método chamador contínue a execução do código subsequente, é necessário torná-lo assíncrono também!

        public async Task<string> FetchDataAsync()
        {
            // Create a new instance of HttpClient
            using (var httpClient = new HttpClient())
            {
                // Use the await keyword to perform a non-blocking GET request
                string result = await httpClient.GetStringAsync("https://archive.org");

                // Return the result when the request is completed
                return result;
            }
        }

        public async Task<string> ReadFileAsync(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                string content = await reader.ReadToEndAsync().ConfigureAwait(false);
                return content;
            }
        }

        public async Task WriteToFileAsync(string filePath, string content)
        {
            using (var writer = new StreamWriter(filePath))
            {
                await writer.WriteAsync(content);
            }
        }

        public async Task CopyFileAsync(string sourcePath, string destinationPath)
        {
            using (var sourceStream = File.OpenRead(sourcePath))
            using (var destinationStream = File.Create(destinationPath))
            {
                await sourceStream.CopyToAsync(destinationStream);
            }
        }

        public async Task<IEnumerable<string>> FetchMultipleDataAsync(IEnumerable<string> urls)
        {
            using (var httpClient = new HttpClient())
            {
                var tasks = urls.Select(url => httpClient.GetStringAsync(url));

                string[] results = await Task.WhenAll(tasks);

                return results;
            }
        }

        public async Task<string> Teste()
        {
            Debug.WriteLine("Começando a ler...");

            var texto = await ReadFileAsync(@"C:\Users\Falt_\Documentos\Teste.txt");

            Debug.WriteLine($"{texto}");
            Debug.WriteLine("Terminei de ler...");

            return texto;
        }
    }
}
