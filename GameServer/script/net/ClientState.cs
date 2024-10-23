using System.Net.Sockets;


public class ClientState
{
    public Socket socket;
    public ByteArray readBuff = new ByteArray();
    //Ping
    public long lastPingTime = NetManager.GetTimeStamp(); //怎加了一个初始时间戳
    //玩家
    public Player player;
}

