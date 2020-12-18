using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using WebAPIAuthJWT.Models;

namespace WebAPIAuthJWT.Helpers
{
    public class Database
    {
        string fileDB = System.IO.Directory.GetCurrentDirectory() + @"\" + ConfigurationManager.AppSetting["AppSettings:DB_SQLite"];
        string strConn;

        // parametri token JWT
        string JWT_secretKey = ConfigurationManager.AppSetting["AppSettings:Secret"];
        int JWT_expirationMinutes = Convert.ToInt32(ConfigurationManager.AppSetting["AppSettings:ExpirationMinute"]);

        public Database()
        {
            // connessione a SQL Lite (vedi www.connectionstrings.com)
            strConn = @"Data Source=" + fileDB + ";Pooling=false;Synchronous=Full;";
        }
        public bool Authenticate(string username, string password)
        {
            bool retVal = false;

            try
            {
                string sql = "";

                // prepara la QUERY
                sql = "";
                sql = sql + "SELECT ";
                sql = sql + "   * ";
                sql = sql + "FROM ";
                sql = sql + "   Utenti ";
                sql = sql + "WHERE 1=1 ";
                sql = sql + "   AND Username = '" + username + "'";

                DataTable dt = DBTable(sql);

                PasswordHasher Hasher = new PasswordHasher();

                if (dt.Rows[0]["Username"].ToString().Length > 0)
                {
                    string EncryptPwd = dt.Rows[0]["Password"].ToString();

                    retVal = Hasher.Check(EncryptPwd, password).Verified;
                }
            }
            catch
            {
            }

            return retVal;
        }

        public string GetToken(string username)
        {
            DataTable dtUtente = this.CheckUser(username);

            //Creazione del Token Jwt
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(JWT_secretKey);

            DataTable dtProfili = DBTable(
                string.Format("SELECT * FROM Utenti_Ruoli WHERE Username ='{0}'", username));
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Name, dtUtente.Rows[0]["Username"].ToString()));

            foreach (DataRow dr in dtProfili.Rows)
            {
                claims.Add(new Claim(ClaimTypes.Role, dr["Ruolo"].ToString()));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                //Validità del Token
                Expires = DateTime.UtcNow.AddMinutes(JWT_expirationMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public DataTable CheckUser(string Username)
        {
            string sql = "";

            // prepara la QUERY
            sql = "";
            sql = sql + "SELECT ";
            sql = sql + "   * ";
            sql = sql + "FROM ";
            sql = sql + "   Utenti ";
            sql = sql + "WHERE 1=1 ";
            sql = sql + "   AND Username = '" + Username + "'";

            DataTable dt = DBTable(sql);
            
            return dt;
        }

        public DataTable DBTable(string query)
        {
            DataTable dt = null;

            try
            {

                SQLiteConnection conn = new SQLiteConnection(strConn);

                conn.Open();

                // crea DataAdapter
                SQLiteDataAdapter da = new SQLiteDataAdapter(query, conn);

                // popola DataTable con DataAdapter 
                dt = new DataTable();
                da.Fill(dt);

                //chiude la connessione
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return dt;
        }

        /// <summary>
        /// Esegui una query SQL (INSERT/UPDATE/DELETE)
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public int DBExecuteSQL(string query)
        {
            int n_rec = 0;

            try
            {

                SQLiteConnection conn = new SQLiteConnection(strConn);

                // crea DataAdapter
                SQLiteCommand cmd = new SQLiteCommand(query, conn);

                cmd.Connection.Open();
                n_rec = cmd.ExecuteNonQuery();

                //chiude la connessione
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return n_rec;
        }

    }
}
