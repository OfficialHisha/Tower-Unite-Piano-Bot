using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Tower_Unite_Instrument_Player_GUI.Band
{
    static class BandServer
    {
        // Only accept clients when the server is enabled
        static bool _enabled = false;

        // Thread signal used for accepting connections
        static readonly ManualResetEvent r_allDone = new ManualResetEvent(false);

        public static Dictionary<TcpClient, BinaryWriter> Clients { get; } = new Dictionary<TcpClient, BinaryWriter>();

        public static async Task Start(int port)
        {
            if (_enabled) return;

            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            TcpListener listener = new TcpListener(localAddr, port);

            _enabled = true;
            await Task.Run(() => listener.Start());

            Console.WriteLine($"Server started on {localAddr}:{port}");

            await Task.Run(() =>
            {
                while (_enabled)
                {
                    // Set the event to nonsignaled state.  
                    r_allDone.Reset();

                    // Start an asynchronous socket to listen for connections.
                    listener.BeginAcceptTcpClient(new AsyncCallback(AcceptCallback), listener);

                    // Wait until a connection is made before continuing.  
                    r_allDone.WaitOne();
                }
            });
        }

        static async void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.  
            r_allDone.Set();

            TcpListener server = (TcpListener)ar.AsyncState;
            TcpClient client = server.EndAcceptTcpClient(ar);

            Console.WriteLine($"Accepted connection from {client.Client.RemoteEndPoint}");

            Clients.Add(client, new BinaryWriter(client.GetStream()));
            await Receive(client.Client.RemoteEndPoint, new BinaryReader(client.GetStream()));
        }

        static async Task Receive(EndPoint clientEndPoint, BinaryReader clientReader)
        {
            while (_enabled)
            {
                string message = await Task.Run(() => clientReader.ReadString());
                Console.WriteLine($"Received message from {clientEndPoint}: {message}");
            }
        }

        static void Send(TcpClient client, string message)
        {
            Clients[client].Write(message);
        }

        public static async Task Broadcast(string message)
        {
            if (!_enabled) return;

            await Task.Run(() =>
            {
                foreach (BinaryWriter writer in Clients.Values)
                {
                    writer.Write(message);
                }
            });
        }
    }
}
