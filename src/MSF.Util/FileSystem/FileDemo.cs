using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MSF.Util.FileSystem
{
    public class FileDemo
    {
        public Dictionary<string, string> ReadFile()
        {
            var caminho = "C:\\Users\\Falt_\\Documentos\\github\\Log\\SaveGeneric.txt";
            var retornos = new Dictionary<string, string>();

            if (!System.IO.File.Exists(caminho))
            {
                Console.WriteLine($"O arquivo não foi encontrado: {caminho}");
            }

            byte[] array;

            using (FileStream fs = System.IO.File.OpenRead(caminho))
            {
                array = new byte[fs.Length];

                using (BinaryReader binaryReader = new BinaryReader(fs))
                {
                    array = binaryReader.ReadBytes((int)fs.Length);
                }
                
                var arrayToBase64 = Convert.ToBase64String(array);
                var arrayToString = Encoding.UTF8.GetString(array);
                retornos.Add("Retorno 1", arrayToString);
            }

            using (FileStream fs = new FileStream(caminho, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader leitor = new StreamReader(fs, Encoding.UTF8))
                {
                    var conteudo = leitor.ReadToEnd();
                    retornos.Add("Retorno 2", conteudo);
                }
            }

            return retornos;
        }

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
