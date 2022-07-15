using ClientSearchbyPin.Models;
using Oracle.ManagedDataAccess.Client;


namespace ClientSearchbyPin.DB
{
    public class OracleDb
    {
        private string ConnectionString =
                $"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.66.95)(PORT=1521)))" +
                $"(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=ASBANKTST))); " +
                $"User Id = rbpirim; Password =111111; Min Pool Size = 1; Max Pool Size = 10; Pooling = True;" +
                $" Validate Connection = true; Connection Lifetime = 300; Connection Timeout = 300; ";

        private readonly ILogger<OracleDb> LOG;


        public OracleConnection GetConnection()
        {
            OracleConnection con = new OracleConnection(ConnectionString);
            con.Open();
            return con;
        }

        public MushteriMelumat GetClientByPinCode(string pinCode)
        {
            MushteriMelumat mushteriMelumat = new MushteriMelumat();

            var con = GetConnection();

            OracleCommand cmd = con.CreateCommand();
            OracleCommand cmd2 = con.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = $"select m_qeydn, M_AD, XEZ_AD, DOGUM_TAR from sbnk_prl.mushteri where pin_kod = '{pinCode}'";
            cmd2.CommandText = $"SELECT h.hesab, h.hes_ad FROM sbnk_prl.hesablar h left JOIN sbnk_prl.mushteri m ON h.m_qeydn = m.m_qeydn where m.pin_kod = '{pinCode}'";
            var reader = cmd.ExecuteReader();
           
            
            
            
            while (reader.Read())
            {
                mushteriMelumat.M_QEYDN = reader["m_qeydn"].ToString();
                mushteriMelumat.PinCode = pinCode;
                mushteriMelumat.M_AD = reader["M_AD"].ToString();
                mushteriMelumat.XEZ_AD = reader["XEZ_AD"].ToString();
                mushteriMelumat.DOGUM_TAR = Convert.ToDateTime(reader["DOGUM_TAR"].ToString());

            }
            return mushteriMelumat;
        }

        public List<MushteriHesabMelumat> GetAccountsByPinCode(string M_QEYDN)
        {//
            List<MushteriHesabMelumat> mushteriHesabMelumat = new List<MushteriHesabMelumat>();

            var con = GetConnection();

            OracleCommand cmd = con.CreateCommand();
            
            cmd.CommandType = System.Data.CommandType.Text;            
            cmd.CommandText = $"SELECT h.hesab, h.hes_ad FROM sbnk_prl.hesablar h left JOIN sbnk_prl.mushteri m ON h.m_qeydn = m.m_qeydn where m.M_QEYDN = '{M_QEYDN}'";
            var reader = cmd.ExecuteReader();
            //



            while (reader.Read())
            {
                MushteriHesabMelumat model = new MushteriHesabMelumat();

                model.HesabAd = reader["hes_ad"].ToString();
                model.Hesab = reader["hesab"].ToString();

                //test

                mushteriHesabMelumat.Add(model);

            }
            return mushteriHesabMelumat;
        }


    }
}
