using Newtonsoft.Json;
using Razorvine.Pyro;
using System;
using System.Collections.Generic;

namespace pyro4
{
    class Program
    {
        private PyroProxy proxy;

        public Program(String host, int port, String server)
        {
            proxy = new PyroProxy(host, port, server);

        }

        private String GetSelection()
        {
            Console.WriteLine("Option: ");
            return Console.ReadLine();
        }

        public void MainMenu()
        {
            PrintMenu();
            String option = GetSelection();

            GetWhichMessagesToPrint(option);

            MainMenu();
        }

        private void PrintFiles(FileEntity[] files)
        {
            List<FileEntity> fileList = new List<FileEntity>(files);
            foreach (FileEntity file in fileList)
            {
                Console.WriteLine("NUME: " + file.Nume + " PATH: " + file.Path + " HASH: " + file.Hash);
            }
            Console.WriteLine();
        }

        private void FindAllFiles()
        {
            String result = (String)proxy.call("findAllFiles");
            FileEntity[] files = JsonConvert.DeserializeObject<FileEntity[]>(result);
            PrintFiles(files);

        }

        private void GetWhichMessagesToPrint(String option)
        {
            switch (option)
            {
                case "1":
                    FindAllFiles();
                    break;
                case "2":
                    break;
                case "3":
                    break;
            };
        }

        private void PrintMenu()
        {
            Console.WriteLine("1: Find all files");
            Console.WriteLine("2: Find files containing substring");
            Console.WriteLine("3: Find files by parts of content (text)");
            Console.WriteLine("4: Find files by parts of content (binary)");
            Console.WriteLine("5: Find files with duplicate content");
        }

        static void Main(string[] args)
        {
            Program client;
            if (args.Length > 0)
                client = new Program(args[0], Int32.Parse(args[1]), args[2]);
            else
                client = new Program("localhost", 7543, "exec");
            client.MainMenu();
            Console.Read();
        }
    }
}
