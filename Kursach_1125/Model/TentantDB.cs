using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using System.Windows;

namespace Kursach_1125.Model
{
    internal class TentantDB
    {
        DBConnection connection;

        private TentantDB(DBConnection db)
        {
            this.connection = db;
        }

        public bool Insert(Tentant tentant)
        {
            bool result = false;
            if (connection == null)
            {
                return result;
            }

            if (connection.OpenConnection())
            {
                MySqlCommand cmd = connection.CreateCommand("insert into `Tentant` Values (0, @contactPerson, @phoneNumber, @email, @rentalStartDate); select LAST_INSERT_ID();");

                cmd.Parameters.Add(new MySqlParameter("title", tentant.Title));
                cmd.Parameters.Add(new MySqlParameter("contactPerson", tentant.ContactPerson));
                cmd.Parameters.Add(new MySqlParameter("phoneNumber", tentant.PhoneNumber));
                cmd.Parameters.Add(new MySqlParameter("email", tentant.Email));
                cmd.Parameters.Add(new MySqlParameter("rentalStartDate", tentant.RentalStartDate));

                try
                {
                    int id = (int)(ulong)cmd.ExecuteScalar();
                    if (id > 0)
                    {
                        MessageBox.Show(id.ToString());
                        tentant.Id = id;
                        result = true;
                    }
                    else
                    {
                        MessageBox.Show("Запись не добавлена");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return result;
        }

        internal List<Tentant> SelectAll()
        {
            List<Tentant> tentants = new List<Tentant>();
            if (connection == null)
            { return tentants; }

            if (connection.OpenConnection())
            {
                var command = connection.CreateCommand("select `ID`, `Title`, `ContactPerson`, `PhoneNumber`, `Email`, `RentalStartDate` from `Tentant`");
                try
                {
                    MySqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        int id = dr.GetInt32(0);
                        string title = string.Empty;
                        if (!dr.IsDBNull(1))
                            title = dr.GetString("Title");
                        string contact = string.Empty;
                        if (!dr.IsDBNull(2))
                            contact = dr.GetString("ContactPerson");
                        string phone = string.Empty;
                        if (!dr.IsDBNull(3))
                            phone = dr.GetString("PhoneNumber");
                        string email = string.Empty;
                        if (!dr.IsDBNull(4))
                            email = dr.GetString("Email");
                        DateTime rentalStart = DateTime.Now;
                        if (!dr.IsDBNull(5))
                            rentalStart = dr.GetDateTime("RentalStartDate");
                        tentants.Add(new Tentant
                        {
                            Id = id,
                            Title = title,
                            ContactPerson = contact,
                            PhoneNumber = phone,
                            Email = email,
                            RentalStartDate = rentalStart
                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return tentants;
        }

        internal bool Update(Tentant tentant)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"update `Tentant` set `Title`=@title, `ContactPerson`=@contactPerson, `PhoneNumber`=@phoneNumber, `Email`=@email, `RentalStartDate`=@rentalStartDate where `ID` = {tentant.Id}");
                mc.Parameters.Add(new MySqlParameter("title", tentant.Title));
                mc.Parameters.Add(new MySqlParameter("contactPerson", tentant.ContactPerson));
                mc.Parameters.Add(new MySqlParameter("phoneNumber", tentant.PhoneNumber));
                mc.Parameters.Add(new MySqlParameter("email", tentant.Email));
                mc.Parameters.Add(new MySqlParameter("rentalStartDate", tentant.RentalStartDate));

                try
                {
                    mc.ExecuteNonQuery();
                    result = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return result;
        }

        internal bool Remove(Tentant remove)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"delete from `Tentant` where `ID` = {remove.Id}");
                try
                {
                    mc.ExecuteNonQuery();
                    result = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return result;
        }

        static TentantDB db;
        public static TentantDB GetDB()
        {
            if (db == null)
                db = new TentantDB(DBConnection.GetDbConnection());
            return db;
        }
    }
}
