using GameServer.script.logic;
using GameServer.script.net;
using System;

public partial class MsgHandler
{
    //获得记事本内容
    public static void MsgGetText(ClientState c, MsgBase msgBase)
    {
        MsgGetText msg = (MsgGetText)msgBase;
        Player player = c.player;
        if (player == null) { return; }
        //获取text
        msg.text = player.data.text;
        player.Send(msg);
    }

    //保存记事本内容
    public static void MsgSaveText(ClientState c, MsgBase msgBase)
    {
        MsgSaveText msg = (MsgSaveText)msgBase;
        Player player = c.player;
        if (player == null) return;
        //获取 text
        player.data.text = msg.text;
        player.Send(msg);
    }
}
