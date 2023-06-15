using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
//using TcpServerC3c.Command;

namespace zaverecny_projekt
{
    public class MyTCPServer
    {

        public TcpListener myServer { get; }
        private bool isRunning;
        private Dictionary<string, StreamWriter> clients;
        //private Dictionary<string, ICommand> commands = new Dictionary<string, ICommand>();
        Node curentRoom = new Node(Typ.Spawn);
        public MyTCPServer(int port)
        {
            clients = new Dictionary<string, StreamWriter>();
            myServer = new TcpListener(System.Net.IPAddress.Any, port) ;
            myServer.Start();
            isRunning = true;
            Inicializace();
            MapArchitexture m = new MapArchitexture();

            m.GenerateMap(50, curentRoom);
            ServerLoop();
        }

        private void Inicializace()
        {
           // commands.Add("help", new HelpCommand());
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
        /// <summary>
        /// komunikace s hráci
        /// </summary>
        /// <param name="myClient"></param>
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
                data = reader.ReadLine();
                data = data.ToLower();
                Console.WriteLine(data);
                string[] word = data.Split(' ', 2);
                switch(word[0])
                {
                    case "presun":
                        if(word[1] == "request")
                        {
                            data = "napiste cislo smeru";
                            if (curentRoom.front != null)
                            {
                                data += "\n1: dopredu";
                            }
                            if (curentRoom.left != null)
                            {
                                data += "\n2: doleva";
                            }
                            if (curentRoom.right != null)
                            {
                                data += "\n3: doprava";
                            }
                            if (curentRoom.back != null)
                            {
                                data += "\n4: dozadu";
                            }
                            writer.WriteLine(data);
                            writer.Flush();
                            break;
                        }

                        if (word[1] == "1") 
                        {
                            if(curentRoom.front != null)
                            {
                                curentRoom = curentRoom.front;
                                break;
                            }
                        }
                        if (word[1] == "2")
                        {
                            if (curentRoom.left != null)
                            {
                                curentRoom = curentRoom.left;
                                break;
                            }
                        }
                        if (word[1] == "3")
                        {
                            if (curentRoom.right != null)
                            {
                                curentRoom = curentRoom.right;
                                break;
                            }
                        }
                        if (word[1] == "4")
                        {
                            if (curentRoom.back != null)
                            {
                                curentRoom = curentRoom.back;
                                break;
                            }
                        }
                        break;
                    case "send":
                        foreach (StreamWriter w in clients.Values)
                        {
                            if (w != writer)
                            {
                                w.WriteLine("print "+word[1]);
//                                w.WriteLine("input");
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
