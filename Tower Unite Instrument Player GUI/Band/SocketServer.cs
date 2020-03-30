using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tower_Unite_Instrument_Player_GUI.Exceptions;

namespace Tower_Unite_Instrument_Player_GUI.Band
{
    static class BandServer
    {
        // Only accept clients when the server is enabled
        static bool _enabled = false;

        // Threat signal used for accepting connections
        static ManualResetEvent _allDone = new ManualResetEvent(false);

        public static void Start(string ipAddress, int port)
        {
            if (IPAddress.TryParse(ipAddress, out IPAddress address))
                throw new BandServerException("The provided IP Address is invalid");

            TcpListener listener = new TcpListener(address, port);

            _enabled = true;
            listener.Start();

            while (_enabled)
            {
                // Set the event to nonsignaled state.  
                _allDone.Reset();

                // Start an asynchronous socket to listen for connections.
                listener.BeginAcceptTcpClient(new AsyncCallback(AcceptCallback), listener);

                // Wait until a connection is made before continuing.  
                _allDone.WaitOne();
            }
        }

        static async void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.  
            _allDone.Set();

            TcpListener server = (TcpListener)ar.AsyncState;
            TcpClient client = server.EndAcceptTcpClient(ar);
            
            await Receive(new BinaryReader(client.GetStream()));
        }

        static Task Receive(BinaryReader clientReader)
        {
            while (true)
            {
                var result = clientReader.ReadString();
                Console.WriteLine(result);
            }
        }

        
    }
}
