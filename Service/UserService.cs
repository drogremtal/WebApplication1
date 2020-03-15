using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using WebApplication1.Models;

namespace WebApplication1.Service
{
    public class UserService
    {

        public bool CreateTableUsers() {

            //название файла базы данных
            var dBname = "userDb.sqlite";

            //Проверяем на существование файла
            if (System.IO.File.Exists(dBname))
            { System.IO.File.Delete(dBname); }

            SQLiteConnection.CreateFile(dBname);

            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=userDb.sqlite;Version=3;");
            m_dbConnection.Open();

            try
            {
                var sql = "CREATE TABLE \"Users\" (" +
                    "\"id\" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, " +
                    "\"login\" TEXT, " +
                    "\"password\"  TEXT, " +
                    "\"status\" TEXT DEFAULT 0, " +
                    "\"ban\" INTEGER DEFAULT 0) ";
                //"CREATE TABLE sqlite_sequence(name,seq)

                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();

                var sqlInsertUsers =
             "INSERT INTO \"Users\"(\"id\", \"login\", \"password\", \"status\", \"ban\") VALUES ('1', 'babushka', 'aklshdfalsk', 'admin', '0');    "
            + "INSERT INTO \"Users\"(\"id\", \"login\", \"password\", \"status\", \"ban\") VALUES('2', 'askdjhalk', 'safshlk', 'sklafdj', '0');     "
            + "INSERT INTO \"Users\"(\"id\", \"login\", \"password\", \"status\", \"ban\") VALUES('3', 'laskfhl', 'aslfkh', 'dsfk', '0');           "
            + "INSERT INTO \"Users\"(\"id\", \"login\", \"password\", \"status\", \"ban\") VALUES('4', 'skjafnalk', 'sdkdslk', 'klsdnvlk', '0');    "
            + "INSERT INTO \"Users\"(\"id\", \"login\", \"password\", \"status\", \"ban\") VALUES('5', 'skfln', 'asdas', 'dsfds', '0');             "
            + "INSERT INTO \"Users\"(\"id\", \"login\", \"password\", \"status\", \"ban\") VALUES('6', 'sdasd', 'sada', 'ghjg', '0');               "
            + "INSERT INTO \"Users\"(\"id\", \"login\", \"password\", \"status\", \"ban\") VALUES('7', 'sada', 'asdas', 'asda', '0');               "
            + "INSERT INTO \"Users\"(\"id\", \"login\", \"password\", \"status\", \"ban\") VALUES('8', 'dsdf', 'adfas', 'asd', '0');                "
            + "INSERT INTO \"Users\"(\"id\", \"login\", \"password\", \"status\", \"ban\") VALUES('9', 'assfa', 'dsasfs', 'asda', '0');             "
            + "INSERT INTO \"Users\"(\"id\", \"login\", \"password\", \"status\", \"ban\") VALUES('10', 'safa', 'asdfas', 'sad', '0');              "
            + "INSERT INTO \"Users\"(\"id\", \"login\", \"password\", \"status\", \"ban\") VALUES('11', 'safad', 'sada', 'sad', '0');               "
            + "INSERT INTO \"Users\"(\"id\", \"login\", \"password\", \"status\", \"ban\") VALUES('12', 'dsfds', 'asd', 'sda', '0');                "
            + "INSERT INTO \"Users\"(\"id\", \"login\", \"password\", \"status\", \"ban\") VALUES('13', 'dsfds', 'sada', 'sad', '0');               ";

                command = new SQLiteCommand(sqlInsertUsers, m_dbConnection);
                command.ExecuteNonQuery();
                m_dbConnection.Close();
            }
            catch { return false; }

            return true;
        }

        public List<UsersModel> GetListUsers() {

            var QuerySelect = string.Format(@"SELECT * from  Users");
            SQLiteConnection connection = new SQLiteConnection("Data Source=userDb.sqlite;Version=3;");
            connection.Open();
            SQLiteCommand comm = new SQLiteCommand(QuerySelect, connection);

            var dt = comm.ExecuteReader();

            var UsersList = new List<UsersModel> { };

            if (dt.HasRows)
            {
                while (dt.Read())
                {
                   var users = new UsersModel() { };
                    users.Id = Convert.ToInt32(dt.GetValue(0));
                    users.Login = dt.GetValue(1).ToString();
                    users.Password = dt.GetValue(2).ToString();
                    users.Status = dt.GetValue(3).ToString();
                    users.Ban = Convert.ToInt64(dt.GetValue(4));

                    UsersList.Add(users);

                }
            }
            return UsersList;
        }

        public bool SetBanStatusUsers(int id, int ban)
        {

            var sql = String.Format("UPDATE  \"Users\" set \"ban\"={0}  where \"id\" ={1}", ban, id);

            try
            {
                SQLiteConnection connection = new SQLiteConnection("Data Source=userDb.sqlite;Version=3;");
                connection.Open();
                SQLiteCommand comm = new SQLiteCommand(sql, connection);
                comm.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
