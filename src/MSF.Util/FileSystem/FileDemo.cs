using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MSF.Util.FileSystem
{
    internal class FileDemo
    {
        public static void File()
        {
            string rootPath = @"C:\Users\Falt_\Documentos\I AM TIM COREY - Youtube Course\10. FileSystemDemo\Files";

            #region :: Diretórios ::
            //string[] directories = Directory.GetDirectories(rootPath, "*", SearchOption.AllDirectories);

            //foreach (var dir in directories)
            //{
            //    Console.WriteLine(dir);
            //} 
            #endregion

            #region :: Arquivos ::
            //var files = Directory.GetFiles(rootPath, "*.*", SearchOption.TopDirectoryOnly);

            //foreach (string file in files)
            //{
            //    //Console.WriteLine(file);
            //    //Console.WriteLine(Path.GetFullPath(file));
            //    //Console.WriteLine(Path.GetFileName(file));
            //    //Console.WriteLine(Path.GetDirectoryName(file));
            //    //Console.WriteLine(Path.GetFileNameWithoutExtension(file));

            //    var info = new FileInfo(file);
            //    Console.WriteLine($"{Path.GetFileName(file)} = {info.Length} bytes - Último acesso: {info.LastAccessTime}");

            //}
            #endregion

            #region :: Existe | Não Existe | Criar Folder ::
            //string newPath = @"C:\Users\Falt_\Documentos\10. FileSystemDemo\Files\mfalcirolli";
            //bool dirExists = Directory.Exists(newPath);

            //if (dirExists)
            //{
            //    Console.WriteLine("Este diretório existe!");
            //}
            //else
            //{
            //    Console.WriteLine("Este diretório não existe!");
            //    Directory.CreateDirectory(newPath);
            //} 
            #endregion

            #region :: Copiar Arquivos ::
            //string[] files = Directory.GetFiles(rootPath);
            //string destinationFolder = @"C:\Users\Falt_\Documentos\I AM TIM COREY - Youtube Course\10. FileSystemDemo\Files\mfalcirolli\";

            //foreach (string file in files)
            //{
            //    File.Copy(file, $"{destinationFolder}{Path.GetFileName(file)}", true);
            //}

            //for (int i = 0; i < files.Length; i++)
            //{
            //    File.Copy(files[i], $"{destinationFolder}{i}.txt", true);
            //} 
            #endregion

            #region :: Mover Arquivos ::
            //string[] files = Directory.GetFiles(rootPath);
            //string destinationFolder = @"C:\Users\Falt_\Documentos\I AM TIM COREY - Youtube Course\10. FileSystemDemo\Files\mfalcirolli\";

            //foreach (string file in files)
            //{
            //    File.Move(file, $"{destinationFolder}{Path.GetFileName(file)}");
            //} 
            #endregion

            var files = Directory.GetFiles(rootPath, "*folder*.*", SearchOption.TopDirectoryOnly);

            foreach (string file in files)
            {
                Console.WriteLine(file);
                //Console.WriteLine(Path.GetFullPath(file));
                //Console.WriteLine(Path.GetFileName(file));
                //Console.WriteLine(Path.GetDirectoryName(file));
                //Console.WriteLine(Path.GetFileNameWithoutExtension(file));

                //var info = new FileInfo(file);
                //Console.WriteLine($"{Path.GetFileName(file)} = {info.Length} bytes - Último acesso: {info.LastAccessTime}");

            }

            Console.ReadLine();
        }
    }
}
