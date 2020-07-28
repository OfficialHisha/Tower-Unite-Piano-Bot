using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using Tower_Unite_Instrument_Player_GUI.Exceptions;

namespace Tower_Unite_Instrument_Player_GUI.Band
{
    static class BandClient
    {
        static readonly TcpClient r_client = new TcpClient();

        static BinaryReader _reader;
        static BinaryWriter _writer;

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

            _reader = new BinaryReader(r_client.GetStream());
            _writer = new BinaryWriter(r_client.GetStream());

            await Receive();
        }

        static async Task Receive()
        {
            while (r_client.Connected)
            {
                string message = await Task.Run(() => _reader.ReadString());
                Console.WriteLine(message);

                if (message == "play")
                {
                    OnServerPlay?.Invoke();
                }
            }
            Console.WriteLine("Disconnected");
        }

        static void Send(string message)
        {
            _writer.Write(message);
        }

    }
}
