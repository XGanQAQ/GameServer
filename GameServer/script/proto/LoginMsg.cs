
//注册
public class MsgRegister:MsgBase
{
    public MsgRegister() 
    { protoName = "MsgRegister"; }
    //客户端发送
    public string id = "";
    public string pw = "";
    //服务端回复(0-成功，1-失败)
    public int result = 0;

}

//登录
public class MsgLogin : MsgBase
{
    public MsgLogin()
    {
        protoName = "MsgLogin";
    }

    public string id = "";
    public string pw = "";
    //服务端回复(0-成功，1-失败)
    public int result = 0;
}

//踢下线（服务器推送）
public class MsgKick : MsgBase
{
    public MsgKick() { protoName = "MsgKick"; }
    //原因(0-其他人登入同一账户)
    public int reason = 0;
}
