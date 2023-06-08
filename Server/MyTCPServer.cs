using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TcpServerC3c.Command;

namespace TcpServerC3c
{
    public class MyTCPServer
    {

        public TcpListener myServer { get; }
        private bool isRunning;
        private Dictionary<string, StreamWriter> clients;
        private Dictionary<string, ICommand> commands = new Dictionary<string, ICommand>();

        public MyTCPServer(int port)
        {
            clients = new Dictionary<string, StreamWriter>();
            myServer = new TcpListener(System.Net.IPAddress.Any, port) ;
            myServer.Start();
            isRunning = true;
            Inicializace();
            GenerateMap(50);
            ServerLoop();
        }

        private void Inicializace()
        {
            commands.Add("help", new HelpCommand());
            commands.Add("time", new TimeCommand());
            commands.Add("ipconfig", new IpConfigCommand());
        }

        private void ServerLoop()
        {
            Console.WriteLine("Server byl spusten");
            while (isRunning)
            {
                TcpClient client = myServer.AcceptTcpClient();
                Thread thread = new Thread(new ParameterizedThreadStart(ClientLoop));
                thread.Start(client);
            }
        }

        private void ClientLoop(object myClient)
        {
            TcpClient client = (TcpClient)myClient;
            StreamReader reader = new StreamReader(client.GetStream(), Encoding.UTF8);
            StreamWriter writer = new StreamWriter(client.GetStream(), Encoding.UTF8);
            
            writer.WriteLine("print Byl jsi pripojen");
            writer.Flush();
            bool loginValid = false;
            string username = "";
            while (!loginValid)
            {
                writer.WriteLine("print zadej uzivatelske jmeno");
                writer.WriteLine("input");
                writer.Flush ();
                username =reader.ReadLine();
                if (clients.ContainsKey(username))
                {
                    writer.WriteLine("print "+ username + " exists");
                    writer.Flush();
                }
                else
                {
                    clients.Add(username, writer);
                    writer.WriteLine("print ok");
                    writer.Flush();
                    Console.WriteLine(username + " pripojen");
                    loginValid = true;

                }

            }
            bool clientConnect = true;
            string? data = null;
            string? dataRecive = null;

            while (clientConnect)
            {
                writer.WriteLine("input"); 
                writer.Flush();
                data = reader.ReadLine();
                data = data.ToLower();
                Console.WriteLine(data);
                string[] word = data.Split(' ', 2);
                switch(word[0])
                {
                    case "send":
                        foreach (StreamWriter w in clients.Values)
                        {
                            if (w != writer)
                            {
                                w.WriteLine("print "+word[1]);
                                w.WriteLine("input");
                                w.Flush();
                            }
                        }
                        break;
                    case "end":
                        clientConnect = false;
                        break;
                    default:
                        writer.WriteLine("print unknown command " + word[0]);
                        writer.Flush();
                        Console.WriteLine("unknown command " + word[0]);
                        break;

                }
   
            }
            writer.WriteLine("print Byl jsi odpojen");
            writer.Flush();
            clients.Remove(username);
            client.Close();
        }
    }
}
