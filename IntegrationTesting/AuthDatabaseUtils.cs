using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using IIG.DatabaseConnectionUtils;

namespace IIG.CoSFE.DatabaseUtils
{
    public class AuthDatabaseUtils : DatabaseConnection
    {
        public AuthDatabaseUtils(NameValueCollection appSettings) : base(appSettings)
        {
        }

        public AuthDatabaseUtils(string server, string database, bool isTrusted, string login = null,
            string password = null, int connectionTimeOut = 15) : base(server, database, isTrusted, login, password,
            connectionTimeOut)
        {
        }

        private bool Credentials(string operation, string login, string password)
        {
            if (!ConnectToDatabase())
                return false;

            bool res;

            using (var cmd = new SqlCommand($"[dbo].[{operation}Credentials]", Connection))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Login", SqlDbType.NVarChar, 50).Value = login;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar, 64).Value = password;
                    cmd.Parameters.Add("@Result", SqlDbType.Int).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    res = Convert.ToBoolean(cmd.Parameters["@Result"].Value);
                }
                catch
                {
                    res = false;
                }
                finally
                {
                    Connection.Close();
                }
            }

            return res;
        }

        public bool AddCredentials(string login, string password)
        {
            return Credentials("Add", login, password);
        }

        public bool UpdateCredentials(string login, string password, string newLogin, string newPassword)
        {
            if (!ConnectToDatabase())
                return false;

            bool res;

            using (var cmd = new SqlCommand("[dbo].[UpdateCredentials]", Connection))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Login", SqlDbType.NVarChar, 50).Value = login;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar, 64).Value = password;
                    cmd.Parameters.Add("@NewLogin", SqlDbType.NVarChar, 50).Value = newLogin;
                    cmd.Parameters.Add("@NewPassword", SqlDbType.VarChar, 64).Value = newPassword;
                    cmd.Parameters.Add("@Result", SqlDbType.Int).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    res = Convert.ToBoolean(cmd.Parameters["@Result"].Value);
                }
                catch
                {
                    res = false;
                }
                finally
                {
                    Connection.Close();
                }
            }

            return res;
        }

        public bool DeleteCredentials(string login, string password)
        {
            return Credentials("Delete", login, password);
        }

        public bool CheckCredentials(string login, string password)
        {
            return Credentials("Check", login, password);
        }
    }
}