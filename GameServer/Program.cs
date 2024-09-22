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
            MsgMove msgMove = new MsgMove();
            msgMove.x = 100;
            msgMove.y = -20;

            byte[] bytes = MsgBase.Encode(msgMove);

            string s = Encoding.UTF8.GetString(bytes);

            Console.WriteLine(s);

            byte[] bytess = Encoding.UTF8.GetBytes(s);

            MsgMove m = (MsgMove)MsgBase.Decode("MsgMove", bytess,0, bytess.Length);
            Console.WriteLine(m.x);
            Console.WriteLine(m.y);

            Console.ReadLine();
        }
    }
}
