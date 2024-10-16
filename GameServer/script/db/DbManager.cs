using GameServer.script.logic;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;


namespace GameServer.script.db
{
    public class DbManager
    {
        public static SqlConnection SqlConnection;
        private static JavaScriptSerializer Js = new JavaScriptSerializer();

        //连接数据库
        public static bool Connect(string db, string ip, string user, string pw)
        {
            SqlConnection = new SqlConnection(); //Server=localhost;Database=game;User Id=czg;Password=1246652674Aa;
            //连接参数
            string s = string.Format("Server={0};Database={1};User Id={2};Password={3}"
                , db, ip, user, pw);
            SqlConnection.ConnectionString = s;
            //连接
            try
            {
                SqlConnection.Open();
                Console.WriteLine("[数据库]connect succ");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("[数据库]connect fail, " + e.Message);
                return false;
            }
        }

        //检测账户是否存在
        public static bool IsAccountExist(string id)
        {
            if (!IsSafeString(id)) { return false; }

            string s = $"select * from account where id='{id}';";

            try
            {
                SqlCommand cmd = new SqlCommand(s, SqlConnection);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                bool IsExist = true;
                if (ds.Tables[0].Rows.Count <= 0) IsExist = false;

                return IsExist;
            }
            catch (Exception e)
            {
                Console.WriteLine("[数据库] IsSafeString err, " + e.Message);
                return true;
            }
        }

        //注册
        public static bool Register(string id, string pw)
        {
            if (!IsSafeString(id))
            {
                Console.WriteLine("[数据库] Register fail,id not safe");
                return false;
            }
            if (!IsSafeString(pw))
            {
                Console.WriteLine("[数据库] Register fail,pw not safe");
                return false;
            }
            //能否注册
            if (IsAccountExist(id))
            {
                Console.WriteLine("[数据库] Register fail,id exist");
                return false;
            }
            //写入数据库
            string sql = $"insert into account(id,pw) values('{id}','{pw}');";
            try
            {
                SqlCommand cmd = new SqlCommand(sql, SqlConnection);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("[数据库] Register fail " + e.Message);
                return false;
            }
        }

        //创建角色
        public static bool CreatePlayer(string id)
        {
            
            if (!IsSafeString(id))
            {
                Console.WriteLine("[数据库] CreatePlayer fail, id not safe");
                return false;
            }

            PlayerData playerData = new PlayerData();
            string data =Js.Serialize(playerData);

            string sql = $"insert into player(id,data) values('{id}','{data}');";
            try
            {
                SqlCommand cmd = new SqlCommand(sql, SqlConnection);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("[数据库] CreatePlayer err " + e.Message);
                return false;
            }
  
        }

        //检测用户名密码
        public static bool CheckPassword(string id,string pw)
        {
            if (!IsSafeString(id))
            {
                Console.WriteLine("[数据库] CheckPassword fail,id not safe");
                return false;
            }
            if (!IsSafeString(pw))
            {
                Console.WriteLine("[数据库] CheckPassword fail,pw not safe");
                return false;
            }

            string sql = $"select * from account where id='{id}' and pw='{pw}';";

            try
            {
                SqlCommand cmd = new SqlCommand(sql, SqlConnection);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                bool res = false;
                if (ds.Tables[0].Rows.Count > 0) res = true;

                return res;
            }
            catch (Exception e)
            {
                Console.WriteLine("[数据库] IsSafeString err, " + e.Message);
                return false;
            }
        }

        //获得玩家数据
        public static PlayerData GetPlayerData(string id) 
        {
            if (!IsSafeString(id))
            {
                Console.WriteLine("[数据库] GetPlayerData fail,id not safe");
                return null;
            }

            string sql = $"select * from player where id='{id}';";
            try
            {
                SqlCommand cmd = new SqlCommand(sql, SqlConnection);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables[0].Rows.Count <= 0) return null;

                string data = ds.Tables[0].Rows[0]["data"].ToString();

                PlayerData playerData = Js.Deserialize<PlayerData>(data);

                return playerData;
            }
            catch(Exception e)
            {
                Console.WriteLine("[数据库] GetPlayerData fail, " + e.Message);
                return null;
            }

        }

        //保存角色数据
        public static bool UpdatePlayerData(string id,PlayerData playerData)
        {
            string data = Js.Serialize(playerData);

            string sql = $"update player set data='{playerData}' where id='{id}';";

            try
            {
                SqlCommand cmd = new SqlCommand(sql, SqlConnection);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("[数据库] UpdatPlayerData err, " + e.Message);
                return false;
            }
        }

        //防止sql注入
        private static bool IsSafeString(string str)
        {
            string pattern = @"[\"";\-\-]";
            return !Regex.IsMatch(str, pattern);
        }

        
    }
}
