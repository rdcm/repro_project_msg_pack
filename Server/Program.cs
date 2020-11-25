using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using MessagePack;

namespace Server
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var ip = new IPEndPoint(IPAddress.Loopback, 7777);
            var server = new UdpClient(ip);

            while (true)
            {
                var result = await server.ReceiveAsync();
                
                var s = string.Empty;
                foreach (var b in result.Buffer)
                {
                    s += " ";
                    s += b;
                }
                
                Console.WriteLine($"Bytes received {s}");

                try
                {
                    var joinEvent = MessagePackSerializer.Deserialize<JoinEvent>(result.Buffer);
                    Console.WriteLine($"Player -{joinEvent.Nickname} joined to room - {joinEvent.RoomId}");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}
//received  130 166 82 111 111 109 73 100 123 168 78 105 99 107 110 97 109 101 163 102 111 111
//sended    130 166 82 111 111 109 73 100 123 168 78 105 99 107 110 97 109 101 163 102 111 111
