using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace zaverecny_projekt
{
    public enum Typ
    {
        Spawn,
        Normal,
        Chest,
        Shop,
        Stairs
    }

    public class Node
    {
        public Node front;
        public Node left;
        public Node right;
        public Node back;
        public Typ typ;

        public Node(Typ typ)
        {
            this.front = null;
            this.left = null;
            this.right = null;
            this.back = null;
            this.typ = typ;
        }
    }
    public class MapArchitexture
    {
        Node room = new Node(Typ.Normal);

        public static void GenerateMap(int pocet)
        {
            Node room = new Node(Typ.Spawn);
            Node spawn = room;
            bool repeat1 = true;
            bool repeat2 = true;
            Random rand = new Random();
            while (repeat1)
            {

                int random1 = rand.Next(4);
                switch (random1)
                {
                    case 0:
                        if (room.front != null && room.back != null && room.left != null && room.right != null)
                        {
                            room = room.front;
                            break;
                        }
                        else if (room.front != null)
                        {
                            break;
                        }
                        else
                        {
                            room.front = new Node(Typ.Normal);
                            repeat1 = false;
                            break;
                        }
                    case 1:
                        if (room.front != null && room.back != null && room.left != null && room.right != null)
                        {
                            room = room.front;
                            break;
                        }
                        else if (room.back != null)
                        {
                            break;
                        }
                        else
                        {
                            room.back = new Node(Typ.Normal);
                            repeat1 = false;
                            break;
                        }
                    case 2:
                        if (room.front != null && room.back != null && room.left != null && room.right != null)
                        {
                            room = room.front;
                            break;
                        }
                        else if (room.left != null)
                        {
                            break;
                        }
                        else
                        {
                            room.left = new Node(Typ.Normal);
                            repeat1 = false;
                            break;
                        }
                    case 3:
                        if (room.front != null && room.back != null && room.left != null && room.right != null)
                        {
                            room = room.front;
                            break;
                        }
                        else if (room.right != null)
                        {
                            break;
                        }
                        else
                        {
                            room.right = new Node(Typ.Normal);
                            repeat1 = false;
                            break;
                        }
                        while (repeat2)
                        {
                            int random2 = rand.Next(4);
                            switch (random2)
                            {
                                case 0:
                                    if (room.front == null)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        room = room.front;
                                        repeat2 = false;
                                        break;
                                    }
                                case 1:
                                    if (room.back == null)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        room = room.back;
                                        repeat2 = false;
                                        break;
                                    }
                                case 2:
                                    if (room.left == null)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        room = room.left;
                                        repeat2 = false;
                                        break;
                                    }
                                case 3:
                                    if (room.right == null)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        room = room.right;
                                        repeat2 = false;
                                        break;
                                    }
                            }
                        }
                        if (pocet > 0)
                            GenerateMap(pocet - 1);
                }
            }

        }
    }
}
