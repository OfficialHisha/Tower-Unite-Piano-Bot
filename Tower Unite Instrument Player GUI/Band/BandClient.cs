using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Timers;
using Tower_Unite_Instrument_Player_GUI.Exceptions;

namespace Tower_Unite_Instrument_Player_GUI.Band
{
    static class BandClient
    {
        static readonly TcpClient r_client = new TcpClient();

        static BinaryReader _reader;
        static BinaryWriter _writer;

        static readonly Timer r_timeoutTimer = new Timer(30000);

        public static event Action OnServerPlay;
        public static async Task Join(string ipAddress, int port)
        {
            Console.WriteLine($"Connecting to {ipAddress}:{port}");

            try
            {
                await r_client.ConnectAsync(ipAddress, port);
            }
            catch (SocketException e)
            {
                throw new BandConnectionException(e.Message);
            }

            Console.WriteLine($"Connected to {ipAddress}:{port}");

            r_timeoutTimer.Elapsed += (timer, args) =>
            {
                // It's more than 30 seconds since the server has sent a message or ping
                // Server is assumed to be unresponsive
                r_client.Close();
                Console.WriteLine("Connection to server was lost");
            };
            r_timeoutTimer.Start();

            _reader = new BinaryReader(r_client.GetStream());
            _writer = new BinaryWriter(r_client.GetStream());

            await Receive();
        }

        static async Task Receive()
        {
            while (r_client.Connected)
            {
                string message = await Task.Run(() => _reader.ReadString());
                Console.WriteLine($"Received message from server: {message}");

                switch (message)
                {
                    case "ping":
                        _writer.Write("pong");
                        r_timeoutTimer.Interval = 30000;
                        break;
                    case "play":
                        OnServerPlay?.Invoke();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
