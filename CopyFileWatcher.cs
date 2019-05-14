using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FileWatcher
{
    class Program
    {
        private static void Watcher()
        {
            using (FileSystemWatcher watcher = new FileSystemWatcher())
            {
                watcher.Path = @"E:\EsalesXml\pedido";
                watcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.FileName | NotifyFilters.DirectoryName;
                watcher.Created += OnChanged;                
                watcher.EnableRaisingEvents = true;
                Console.WriteLine("Aperte e para parar o programa.");
                while (Console.Read() != 'e') ;
            }
        }
        private static void OnChanged(object source, FileSystemEventArgs e)
        {            
            Console.WriteLine($"Copiando: {e.FullPath}");
            File.Copy(e.FullPath, @"\\kundenbd\KUNDEN\XML_PEDIDOS\TESTE\" + e.Name);
            StreamWriter writer = new StreamWriter(@"\\kundenbd\KUNDEN\XML_PEDIDOS\log_copia_xml.txt", true);
            writer.WriteLine("Arquivo copiado: " + e.Name + " em " + System.DateTime.Now);
            writer.Flush();
            writer.Close();
        }
        static void Main(string[] args)
        {
            Watcher();
        }
    }
}
