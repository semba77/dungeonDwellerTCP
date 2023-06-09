namespace zaverecny_projekt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyTCPServer server = new MyTCPServer(65525);
        }
    }
}