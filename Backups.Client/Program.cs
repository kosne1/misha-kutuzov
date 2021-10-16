using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;

namespace Backups.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            const string firstFilePath = @"C:\Users\misha\Documents\Test2\c.txt";
            const string secondFilePath = @"C:\Users\misha\Documents\Test2\d.txt";
            List<string> filePaths = new List<string>();
            filePaths.Add(firstFilePath);
            filePaths.Add(secondFilePath);
            
            foreach (var f in filePaths)
            {
                try
                {

                    TcpClient tcpClient = new TcpClient("127.0.0.1", 1234);
                    Console.WriteLine("Connected. Sending file.");

                    StreamWriter sWriter = new StreamWriter(tcpClient.GetStream());

                    byte[] bytes = File.ReadAllBytes(f);

                    sWriter.WriteLine(bytes.Length.ToString());
                    sWriter.Flush();

                    sWriter.WriteLine(f);
                    sWriter.Flush();

                    Console.WriteLine("Sending file");
                    tcpClient.Client.SendFile(f);
                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                }

                // Console.Read();
            }
        }
    }
}