using System.Net.Sockets;
using System.Threading;

namespace Pretpark
{
    class Program
    {
        static async Task Main(string[] args)
        {
            TcpListener server = new TcpListener(new System.Net.IPAddress(new byte[] { 127,0,0,1 }), 5000);
            server.Start();
            int connectionCount = 0;

            while(true)
            {
                Console.WriteLine(" >> [Main] Opening socket");
                Socket connectie = server.AcceptSocket();
                connectionCount++;
                Console.WriteLine(" >> [Main] Calling new Thread for client " + connectionCount + "...");
                new Thread(() => ThreadProc(connectie, connectionCount)).Start();
                //Console.WriteLine(" >> [Main] ");
            }
        }

        private static void ThreadProc(object obj, int connectionCount)
        {
            Console.WriteLine(" >> [Connection Thread " + connectionCount + "] Connection accepted");
            var connectie = (Socket)obj;
            using Stream request = new NetworkStream(connectie);
            using StreamReader requestLezer = new StreamReader(request);
            string[]? regel1 = requestLezer.ReadLine()?.Split(" ");
            if (regel1 == null) return;
            (string methode, string url, string httpversie) = (regel1[0], regel1[1], regel1[2]);
            string? regel = requestLezer.ReadLine();
            int contentLength = 0;
            while (!string.IsNullOrEmpty(regel) && !requestLezer.EndOfStream)
            {
                string[] stukjes = regel.Split(":");
                (string header, string waarde) = (stukjes[0], stukjes[1]);
                if (header.ToLower() == "content-length")
                    contentLength = int.Parse(waarde);
                regel = requestLezer.ReadLine();
            }
            if (contentLength > 0) {
                char[] bytes = new char[(int)contentLength];
                requestLezer.Read(bytes, 0, (int)contentLength);
            }
            Thread.Sleep(5000);
            connectie.Send(System.Text.Encoding.ASCII.GetBytes("HTTP/1.0 200 OK\r\nContent-Type: text/plain\r\nContent-Length: 11\r\n\r\nHello World"));
            Console.WriteLine(" >> [Connection Thread " + connectionCount + "] Response sent");
        }
    }
}