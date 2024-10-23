


public partial class MsgHandler
{
    //注册协议处理
    public static void MsgRegister(ClientState c, MsgBase msgBase)
    {
        MsgRegister msg = (MsgRegister)msgBase;
        //注册
        if (DbManager.Register(msg.id, msg.pw))
        {
            DbManager.CreatePlayer(msg.id);
            msg.result = 0;
        }
        else
        {
            msg.result = 1;
        }
        NetManager.Send(c, msg);

    }

    public static void MsgLogin(ClientState c, MsgBase msgBase)
    {
        MsgLogin msg = (MsgLogin)msgBase;
        //密码校验
        if (!DbManager.CheckPassword(msg.id, msg.pw))
        {
            msg.result = 1;
            NetManager.Send(c, msg);
            return;
        }

        //不允许再次登录
        if (c.player != null)
        {
            msg.result = 1;
            NetManager.Send(c, msg);
            return;
        }

        //如果已经登录，踢下线
        if (PlayerManager.IsOnline(msg.id))
        {
            //发送踢下线协议
            Player other = PlayerManager.GetPlayer(msg.id);
            MsgKick msgKick = new MsgKick();
            msgKick.reason = 0;
            other.Send(msgKick);
            return;
        }

        //获得玩家数据
        PlayerData playerData = DbManager.GetPlayerData(msg.id);
        if (playerData == null)
        {
            msg.result = 1;
            NetManager.Send(c, msg);
            return;
        }

        Player player = new Player(c);
        player.id = msg.id;
        player.data = playerData;
        PlayerManager.AddPlayer(msg.id, player);
        c.player = player;
        //返回协议
        msg.result = 0;
        player.Send(msg);

    }
}

