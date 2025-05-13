using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using System.Windows;

namespace Kursach_1125.Model
{
    internal class EmployeeDB
    {
        DBConnection connection;

        private EmployeeDB(DBConnection db)
        {
            this.connection = db;
        }

        public bool Insert(Employee employee)
        {
            bool result = false;
            if (connection == null)
            {
                return result;
            }

            if (connection.OpenConnection())
            {
                MySqlCommand cmd = connection.CreateCommand("insert into `Employee` Values (0, @jobtitleID, @tPKZoneID, @fIO, @contactPhone); select LAST_INSERT_ID();");

                cmd.Parameters.Add(new MySqlParameter("jobtitleID", employee.JobTitleID));
                cmd.Parameters.Add(new MySqlParameter("tPKZoneID", employee.TPKZoneID));
                cmd.Parameters.Add(new MySqlParameter("fIO", employee.FIO));
                cmd.Parameters.Add(new MySqlParameter("contactPhone", employee.PhoneNumber));

                try
                {
                    int id = (int)(ulong)cmd.ExecuteScalar();
                    if (id > 0)
                    {
                        MessageBox.Show(id.ToString());
                        employee.Id = id;
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

        internal List<Employee> SelectAll()
        {
            List<Employee> employees = new List<Employee>();
            if (connection == null)
            { return employees; }

            if (connection.OpenConnection())
            {
                var command = connection.CreateCommand("select e.`ID`, `JobTitleID`, `TPKZoneID`, `FIO`, `ContactPhoneNumber`, j.`Title`, j.`Task`, j.`ID`, " +
                    "t.`Title`, t.`Floor`, t.`ID`," +
                    "from `Employee` e JOIN `JobTitle` j ON `JobTitleID` = j.ID JOIN `TPKZone` t ON `TPKZoneID` = t.`ID`");
                try
                {
                    MySqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        int id = dr.GetInt32(0);
                        int jobid = dr.GetInt32(1);
                        int tPKid = dr.GetInt32(2);
                        string fio = string.Empty;
                        if (!dr.IsDBNull(3))
                            fio = dr.GetString("FIO");
                        string contact = string.Empty;
                        if (!dr.IsDBNull(4))
                            contact = dr.GetString("ContactPhoneNumber");
                        string tTitle = string.Empty;
                        if (!dr.IsDBNull(5))
                            tTitle = dr.GetString("tTitle");
                        string task = string.Empty;
                        if (!dr.IsDBNull(6))
                            task = dr.GetString("jTask");

                        employees.Add(new Employee
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
            return employees;
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
