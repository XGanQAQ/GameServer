using GameServer.script.logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.script.net
{
    public class ClientState
    {
        public Socket socket;
        public ByteArray readBuff = new ByteArray();
        //Ping
        public long lastPingTime = 0;
        //玩家
        public Player player;
    }
}
