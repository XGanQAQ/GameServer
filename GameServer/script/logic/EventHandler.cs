using GameServer.script.net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.script.logic
{
    public partial class EventHandler
    {
        public static void OnDisconnect(ClientState c)
        {
            Console.WriteLine("close");
        }
        public static void OnTimer() {
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
