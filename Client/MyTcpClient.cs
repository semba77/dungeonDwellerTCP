using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace zaverecny_projekt
{
    public class MyTcpClient
    {
        private TcpClient myClient;
        private bool isRunning;
        private StreamReader reader;
        StreamReader Reader{ get; set; }

        public StreamWriter writer { get; set; }
        //StreamWriter Writer{ get; set; }

        public MyTcpClient(string ipAddress, int port)
        {
            myClient = new TcpClient(ipAddress, port);
            Thread thread = new Thread(new ParameterizedThreadStart(ClientLoop));
            thread.Start(myClient);
            isRunning = true;


        }
        /// <summary>
        /// celé řízení hry
        /// </summary>
        /// <param name="tcpClient"></param>
        private async void ClientLoop(object tcpClient)
        {
            Console.WriteLine("Klient byl spusten"); 
            try
            {
                ArrayList inventory = new ArrayList();
                TcpClient myClient = (TcpClient)tcpClient;
                reader = new StreamReader(myClient.GetStream(), Encoding.UTF8);
                writer = new StreamWriter(myClient.GetStream(), Encoding.UTF8);
                string? data = null;
                string? dataRecive = null;
                bool loginValid = false;
                string? username = null;
                ArrayList messeges = new ArrayList();         
//              kontroluje zadání uzivatelského jména
                while (!loginValid)
                {
                    data = reader.ReadLine();
                    string[] word = data.Split(' ', 2);
                    if (word[0] == "input")
                    {
                        dataRecive = "";
                        do
                        {
                            try
                            {
                                Console.Write("\r>> ");
                                dataRecive = MyReader.ReadLine(10000);
                                username = dataRecive;
                                writer.WriteLine(dataRecive);
                                writer.Flush();
                            }
                            catch (TimeoutException)
                            {
                                bool avail = myClient.GetStream().DataAvailable;
                                if (avail)
                                {
                                    Console.WriteLine("");
                                    break;
                                }

                            }
                        } while (dataRecive == "");
                    }
                    else
                    {
                        Console.WriteLine(word[1]);
                        if(word[1] == "ok")
                        {
                            loginValid = true;
                        }
                    }
                }
                Console.WriteLine("vyber si postavu \n1.warior \n2.mage \n3.priest \n -------------------");
                bool choosen = false;
                Player? w = null;
                Mage? m = null;
                Priest? p = null;
                while (!choosen)
                {
                    string pClass = Console.ReadLine();
                    if (pClass == "1")
                    {

                        Equipment equipedHelm = new Equipment("spatna helma", TypVyz.Helm, 1);
                        Equipment equipedArmor = new Equipment("spatna tunika", TypVyz.Armor, 1);
                        Equipment equipedWeapon = new Equipment("rezava mec", TypVyz.Weapon, 1);
                        w = new Player(20, 20, 10, 1, 20, 0, 1, equipedHelm, equipedArmor, equipedWeapon);                        
                        choosen = true;
                    }
                    else if (pClass == "2")
                    {
                        
                        Equipment equipedHelm = new Equipment("spatna helma", TypVyz.Helm, 1);
                        Equipment equipedArmor = new Equipment("spatna tunika", TypVyz.Armor, 1);
                        Equipment equipedWeapon = new Equipment("plesniva hul", TypVyz.Weapon, 1);
                        m = new Mage(20, 20, 10, 1, 20, 0, 1, equipedHelm, equipedArmor, equipedWeapon, 20, 20);
                        choosen = true;
                    }
                    else if (pClass == "3")
                    {
                        Equipment equipedHelm = new Equipment("spatna helma", TypVyz.Helm, 1);
                        Equipment equipedArmor = new Equipment("spatna tunika", TypVyz.Armor, 1);
                        Equipment equipedWeapon = new Equipment("plesniva hul", TypVyz.Weapon, 1);
                        p = new Priest(20, 20, 10, 1, 20, 0, 1, equipedHelm, equipedArmor, equipedWeapon, 20, 20);
                        choosen = true;
                    }
                    
                }
                Console.WriteLine($"probudil jsi se v neznámé místnosti, pamatujes si pouze ze se jmenujes {dataRecive}");
                while (isRunning)
                {
                    Console.WriteLine("muzes pouzit:\n status\n presun\n chat \n -------------------");
                    bool avail = myClient.GetStream().DataAvailable;
                    if (avail)
                    {
                        data = reader.ReadLine();
                        string[] word = data.Split(' ', 2);
                        if (word[0] == "print")
                        {
                            messeges.Add(word[1]);
                        }
                    }
                    string st;
                    st = Console.ReadLine();
                    switch (st)
                    {
                        case "status":
                            if(p != null)
                            {
                                int damage = p.baseDamadge + p.weapon.stat;
                                int defence = p.armor.stat + p.baseDefence;
                            Console.WriteLine($"username:{username}  \n level: {p.level} \n xp: {p.curentXp}|{p.maxXp} \n hp: {p.curentHealth}|{p.maxHealth} \n mana: {p.curentMana}|{p.maxMana} \n streght: {damage} \n defence: {defence} \n -------------------");
                            }
                            if (w != null)
                            {
                                int damage = w.baseDamadge + w.weapon.stat;
                                int defence = w.armor.stat + w.baseDefence;
                                Console.WriteLine($"username:{username}  \n level: {w.level} \n xp: {w.curentXp}|{w.maxXp} \n hp: {w.curentHealth}|{w.maxHealth} \n streght: {damage} \n defence: {defence} \n -------------------");
                            }
                            if (m != null)
                            {
                                int damage = m.baseDamadge + m.weapon.stat;
                                int defence = m.armor.stat + m.baseDefence;
                                Console.WriteLine($"username:{username}  \n level: {m.level} \n xp: {m.curentXp}|{m.maxXp} \n hp: {m.curentHealth}|{m.maxHealth} \n mana: {m.curentMana}|{m.maxMana} \n streght: {damage} \n defence: {defence} \n -------------------");
                            }
                            break;
                        case "inventar":
                            bool leave = false;
                            while (!leave)
                            {
                                Console.WriteLine("1.prohlednout vybavu \n 2.nahlednout do batohu \n 3.opustit inventar");
                                string typinve = Console.ReadLine();
                                if (typinve == "1")
                                {
                                    bool leavev = false;
                                    if (p != null)
                                    {
                                        while (!leavev)
                                        {
                                            Console.WriteLine($" helma:{p.helm.nazev} \n zbroj:{p.armor.nazev} \n zbran:{p.weapon.nazev}");
                                            Console.WriteLine("vyber akci:\n 1.info helma \n 2.info zbroj \n 3.info zbran \n 4.sundat helmu \n 5.sundat zbroj \n 6.sundat zbran \n 7.vratit zpet");
                                            string typakce = Console.ReadLine();
                                            switch (typakce)
                                            {
                                                case "1":
                                                    Console.WriteLine($"nazev: {p.helm.nazev}\n stat: {p.helm.stat}");
                                                    break;
                                                case "2":
                                                    Console.WriteLine($"nazev: {p.armor.nazev}\n stat: {p.armor.stat}");
                                                    break;
                                                case "3":
                                                    Console.WriteLine($"nazev: {p.weapon.nazev}\n stat: {p.weapon.stat}");
                                                    break;
                                                case "4":

                                                    break;
                                                case "5":

                                                    break;
                                                case "6":

                                                    break;
                                                case "7":
                                                    leavev = true;
                                                    break;
                                            }
                                        }
                                    }
                                    if (w != null)
                                    {
                                        while (!leavev)
                                        {
                                            Console.WriteLine($" helma:{w.helm.nazev} \n zbroj:{w.armor.nazev} \n zbran:{w.weapon.nazev}");
                                            Console.WriteLine("vyber akci:\n 1.info helma \n 2.info zbroj \n 3.info zbran \n 4.sundat helmu \n 5.sundat zbroj \n 6.sundat zbran \n 7.vratit zpet");
                                            string typakce = Console.ReadLine();
                                            switch (typakce)
                                            {
                                                case "1":
                                                    Console.WriteLine($"nazev: {w.helm.nazev}\n stat: {w.helm.stat}");
                                                    break;
                                                case "2":
                                                    Console.WriteLine($"nazev: {w.armor.nazev}\n stat: {w.armor.stat}");
                                                    break;
                                                case "3":
                                                    Console.WriteLine($"nazev: {w.weapon.nazev}\n stat: {w.weapon.stat}");
                                                    break;
                                                case "4":

                                                    break;
                                                case "5":

                                                    break;
                                                case "6":

                                                    break;
                                                case "7":
                                                    leavev = true;
                                                    break;
                                            }
                                        }
                                    }
                                    if (m != null)
                                    {
                                        while (!leavev)
                                        {
                                            Console.WriteLine($" helma:{m.helm.nazev} \n zbroj:{m.armor.nazev} \n zbran:{m.weapon.nazev}");
                                            Console.WriteLine("vyber akci:\n 1.info helma \n 2.info zbroj \n 3.info zbran \n 4.sundat helmu \n 5.sundat zbroj \n 6.sundat zbran \n 7.vratit zpet");
                                            string typakce = Console.ReadLine();
                                            switch (typakce)
                                            {
                                                case "1":
                                                    Console.WriteLine($"nazev: {m.helm.nazev}\n stat: {m.helm.stat}");
                                                    break;
                                                case "2":
                                                    Console.WriteLine($"nazev: {m.armor.nazev}\n stat: {m.armor.stat}");
                                                    break;
                                                case "3":
                                                    Console.WriteLine($"nazev: {m.weapon.nazev}\n stat: {m.weapon.stat}");
                                                    break;
                                                case "4":

                                                    break;
                                                case "5":

                                                    break;
                                                case "6":

                                                    break;
                                                case "7":
                                                    leavev = true;
                                                    break;
                                            }
                                        }
                                    }
                                }
                                else if (typinve == "2")
                                {
                                    
                                }
                                else if (typinve == "3")
                                {
                                    leave = true;
                                }
                            }
                            break;
                        case "presun":
                            writer.WriteLine("presun request");
                            writer.Flush();
                            data = reader.ReadLine();
                            Console.WriteLine(data);
                            data = "presun "; 
                            data += Console.ReadLine();
                            writer.WriteLine(data);
                            break;
                        case "souboj":

                            break;
                        case "chat":
 //                           data = reader.ReadLine();
                            string[] word = data.Split(' ', 2);
                            bool chatRunning = true;
                            foreach (string elements in messeges)
                            {
                                Console.WriteLine(elements);
                            }
                            Console.WriteLine("před zprávu napiste 'send'");
                            word[0] = "input";
                            bool start = false;
                            while (chatRunning)
                            {
                                if (start)
                                {
                                    data = reader.ReadLine();
                                    word = data.Split(' ', 2);
                                }
                                start = true;
                                switch (word[0])
                                {
                                    case "print":
                                        Console.WriteLine(word[1]);
                                        break;
                                    case "input":
                                        dataRecive = "";
                                        do
                                        {
                                            try
                                            {
                                                Console.Write("\r>> ");
                                                dataRecive = MyReader.ReadLine(10000);
                                                writer.WriteLine(dataRecive);
                                                writer.Flush();
                                            }
                                            catch (TimeoutException)
                                            {
                                                avail = myClient.GetStream().DataAvailable;
                                                if (avail)
                                                {
                                                    Console.WriteLine("");
                                                    break;
                                                }

                                            }
                                        } while (dataRecive == "");
                                        break;
                                    case "end":
                                        chatRunning = false;
                                        break;
                                    default:
                                        Console.WriteLine("unknown command " + word[0]);
                                        break;
                                }
                            }
                            break;
                        case "end":
                            isRunning = false;
                            break;
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
