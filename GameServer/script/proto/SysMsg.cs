using System.Collections;
using System.Collections.Generic;


public class SysMsg
{
    public class MsgPing : MsgBase
    {
        public MsgPing()
        {
            protoName = "MsgPing";
        }
    }
    
    public class MsgPong : MsgBase
    {
        public MsgPong()
        {
            protoName = "MsgPone";
        }
    }
}


