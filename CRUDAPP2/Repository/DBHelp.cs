using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDAPP2.Repository
{
    public class DBHelp
    {
        static MySqlCommand cmd = null;
        static MySqlDataAdapter _adp;
        static MySqlConnection _con = null;
        static DataTable _table = null;
        static string _conStr = "Server=localhost;Port=3306;Database=testdev;Uid=root;Pwd=zsxd2356";
        public static MySqlDataAdapter getInformation(string query, ref string lsmg)
        {
            try
            {

                _con = new MySqlConnection(_conStr);
                _table = new DataTable();
                _adp = new MySqlDataAdapter(query, _con);
                return _adp;
            }
            catch (Exception err)
            {
                lsmg = err.Message;
            }
            return _adp;
        }


        public static void postInformation(string query, ref string lsmg)
        {
            try
            {
                using (cmd = new MySqlCommand(query, _con))
                {
                    if (_con.State == ConnectionState.Closed)
                        _con.Open();
                    cmd.ExecuteReader();
                    _con.Close();
                }
            }
            catch (Exception err)
            {
                lsmg = err.Message;
            }
        }

        public static bool getReturnInfor(string query, ref string lsmg)
        {
            bool verificar = false;
            try
            {
                using (cmd = new MySqlCommand(query, _con))
                {
                    if (_con.State == ConnectionState.Closed)
                        _con.Open();
                      verificar  = cmd.ExecuteReader().HasRows;
                    _con.Close();
                }
                return verificar;
            }
            catch (Exception err)
            {
                lsmg = err.Message;
            }
            return verificar;
        }
    }
}
