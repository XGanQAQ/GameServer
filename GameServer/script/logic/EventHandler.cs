using GameServer.script.db;
using GameServer.script.net;
using System;

namespace GameServer.script.logic
{
    public partial class EventHandler
    {
        public static void OnDisconnect(ClientState c)
        {
            Console.WriteLine("close");
            //Player下线
            if (c.player != null)
            {
                //保存数据
                DbManager.UpdatePlayerData(c.player.id, c.player.data);
                //移除
                PlayerManager.RemovePlayer(c.player.id);
            }
        }
        public static void OnTimer()
        {
            CheckPing();
        }

        public static void CheckPing()
        {
            long timeNow = NetManager.GetTimeStamp();

            foreach (ClientState s in NetManager.clients.Values)
            {
                Console.WriteLine("ping Close" + s.socket.RemoteEndPoint.ToString());
                NetManager.Close(s);
                return;
            }
        }
    }
}
