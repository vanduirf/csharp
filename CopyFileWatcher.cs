//----------------------------------------------------------------------------//
//   Aplicativo para monitorar pasta e copiar arquivos criados gerando um log //
//----------------------------------------------------------------------------//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace FileWatcher
{
    class Program
    {
        private static void Watcher()
        {
                FileSystemWatcher watcher = new FileSystemWatcher(@"C:\teste\");  
                watcher.EnableRaisingEvents = true;
                watcher.Created += new FileSystemEventHandler(ArquivoCriado);
                Console.WriteLine("Aperte e para parar o programa.");
                while (Console.Read() != 'e');            
        }
        private static void ArquivoCriado(object sender, FileSystemEventArgs e)
        {
            if (CheckArquivoCopiado(e.FullPath))
            {
                Console.WriteLine($"Copiando: {e.FullPath}");
                File.Copy(e.FullPath, @"C:\teste2\" + e.Name);
                StreamWriter writer = new StreamWriter(@"C:\Logs\log.txt", true);
                writer.WriteLine("Arquivo copiado: " + e.Name + " em " + System.DateTime.Now);
                writer.Flush();
                writer.Close();
            }
        }
        private static bool CheckArquivoCopiado(string FilePath)
        {
            try
            {
                if (File.Exists(FilePath))
                    using (File.OpenRead(FilePath))
                    {
                        return true;
                    }
                else
                    return false;
            }
            catch (Exception)
            {
                Thread.Sleep(100);
                return CheckArquivoCopiado(FilePath);
            }
        }
        static void Main(string[] args)
        {
            Watcher();
        }
    }
}
