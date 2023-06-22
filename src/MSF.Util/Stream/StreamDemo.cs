using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace MSF.Util.StreamDemo
{
    public static class FileStreamDemo
    {
        // [Obsolete(message: "Obsoleto")]

        public static void WriteFile()
        {
            using (var stream = new FileStream(@"C:\Users\Falt_\Documentos\Teste.txt", FileMode.Create, FileAccess.Write))
            {
                if (stream.CanWrite)
                {
                    var data = Encoding.UTF8.GetBytes("Olá, Stream!");
                    stream.Write(data, 0, data.Length);
                }
            }
        }

        public static void ReadFile()
        {
            using (var stream = new FileStream(@"C:\Users\Falt_\Documentos\Teste.txt", FileMode.Open, FileAccess.Read))
            {
                if (stream.CanRead)
                {
                    byte[] buffer = new byte[stream.Length];
                    var bytesRead = stream.Read(buffer, 0, buffer.Length);

                    Debug.WriteLine(Encoding.UTF8.GetString(buffer, 0, bytesRead));
                }
            }
        }
    }

    public static class MemoryStreamDemo
    {

    }

    public static class StreamDemo
    {

    }
}
