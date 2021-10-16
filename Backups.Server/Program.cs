using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Backups.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            // Listen on port 1234    
            var tcpListener = new TcpListener(IPAddress.Any, 1234);
            tcpListener.Start();

            Console.WriteLine("Server started");
            while (true)
            {
                //Infinite loop to connect to new clients  
                // Accept a TcpClient    
                TcpClient tcpClient = tcpListener.AcceptTcpClient();

                Console.WriteLine("Connected to client");

                var reader = new StreamReader(tcpClient.GetStream());

                // The first message from the client is the file size    
                string cmdFileSize = reader.ReadLine();

                // The first message from the client is the filename    
                string cmdFileName = reader.ReadLine();

                int length = Convert.ToInt32(cmdFileSize);
                byte[] buffer = new byte[length];
                int received = 0;
                int size = 1024;

                // Read bytes from the client using the length sent from the client    
                while (received < length)
                {
                    int remaining = length - received;
                    if (remaining < size)
                    {
                        size = remaining;
                    }

                    int read = tcpClient.GetStream().Read(buffer, received, size);
                    received += read;
                }

                
                // Save the file using the filename sent by the client    
                using (var fStream = new FileStream(Path.GetFileName(cmdFileName), FileMode.Create))
                {
                    fStream.Write(buffer, 0, buffer.Length);
                    fStream.Flush();
                    fStream.Close();
                }

                Console.WriteLine("File received and saved in " + Environment.CurrentDirectory);
            }
        }
    }
}