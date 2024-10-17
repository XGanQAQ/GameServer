using GameServer.script.db;
using GameServer.script.logic;
using GameServer.script.net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //连接数据库
            if (!DbManager.Connect("localhost", "game", "czg", "1246652674Aa@")) { return; }

            NetManager.StartLoop(8888);
        }
    }
}
