using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Tower_Unite_Instrument_Player_GUI.Band
{
    class BandMember
    {
        public TcpClient Client { get; }
        public string Identifier { get; }
        public EndPoint EndPoint { get; }
        public BinaryReader Reader { get; }
        public BinaryWriter Writer { get; }
        public int Latency { get; private set; }
        public DateTime LastPing { get; private set; }
        public DateTime LastMessage { get; set; }

        public BandMember(TcpClient client, string identifier)
        {
            Client = client;
            Identifier = identifier;
            EndPoint = client.Client.RemoteEndPoint;
            Reader = new BinaryReader(client.GetStream());
            Writer = new BinaryWriter(client.GetStream());
            Ping();
        }

        public void Ping()
        {
            LastPing = DateTime.Now;
            Writer.Write("ping");
        }

        public void Pong()
        {
            LastMessage = DateTime.Now;
            Latency = LastMessage.Millisecond - LastPing.Millisecond;
        }

        public void SendMessage(string message)
        {
            Writer.Write(message);
        }
    }
}
