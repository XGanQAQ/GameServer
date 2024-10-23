
internal class Program
{
    static void Main(string[] args)
    {
        //连接数据库
        if (!DbManager.Connect("localhost", "game", "czg", "1246652674Aa@")) { return; }

        NetManager.StartLoop(8888);
    }
}

