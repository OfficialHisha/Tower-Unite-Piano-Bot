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

        static readonly System.Timers.Timer r_pingTimer = new System.Timers.Timer(1000);

        public static Dictionary<EndPoint, BandMember> Clients { get; } = new Dictionary<EndPoint, BandMember>();

        public static async Task Start(int port)
        {
            if (_enabled) return;

            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            TcpListener listener = new TcpListener(localAddr, port);

            _enabled = true;
            await Task.Run(() => listener.Start());

            Console.WriteLine($"Server started on {localAddr}:{port}");

            r_pingTimer.AutoReset = true;
            r_pingTimer.Elapsed += (timer, args) =>
            {
                foreach (BandMember client in Clients.Values)
                {
                    if ((client.LastMessage - DateTime.Now).TotalSeconds > 30)
                    {
                        // It's more than 30 seconds since the client sent a message or responded to pings
                        // Client is assumed to be disconnected
                        Clients.Remove(client.EndPoint);
                        client.Client.Close();
                        Console.WriteLine($"{client.EndPoint} lost connection");
                        continue;
                    }

                    client.Ping();
                }
            };
            r_pingTimer.Start();

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

            BandMember member = new BandMember(client, client.Client.RemoteEndPoint.ToString());

            Clients.Add(client.Client.RemoteEndPoint, member);
            await Receive(client.Client.RemoteEndPoint, member.Reader);
        }

        static async Task Receive(EndPoint clientEndPoint, BinaryReader clientReader)
        {
            while (_enabled)
            {
                string message = await Task.Run(() => clientReader.ReadString());
                Console.WriteLine($"Received message from {clientEndPoint}: {message}");
                switch (message)
                {
                    case "pong":
                        Clients[clientEndPoint].Pong();
                        Console.WriteLine($"Received ping response from {clientEndPoint} ({Clients[clientEndPoint].Latency} ms)");
                        break;
                    default:
                        break;
                }
            }
        }

        public static async Task Broadcast(string message)
        {
            if (!_enabled) return;

            await Task.Run(() =>
            {
                foreach (BandMember client in Clients.Values)
                {
                    client.SendMessage(message);
                }
            });
        }
    }
}
