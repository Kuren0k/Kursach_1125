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
                var command = connection.CreateCommand("select e.`ID`, `JobTitleID`, `TPKZoneID`, `FIO`, `ContactPhoneNumber`, j.`Title` as jTitle, j.`Task` as jTask, j.`Wages`, j.`ID`, " +
                    "t.`Title` as tTitle, t.`Floor`, t.`ID`" +
                    "from `Employee` e JOIN `JobTitle` j ON `JobTitleID` = j.`ID` JOIN `TPKZone` t ON `TPKZoneID` = t.`ID`");
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
                        string jTitle = string.Empty;
                        if (!dr.IsDBNull(5))
                            jTitle = dr.GetString("jTitle");
                        string task = string.Empty;
                        if (!dr.IsDBNull(6))
                            task = dr.GetString("jTask");
                        int wages = 0;
                        if (!dr.IsDBNull(7))
                            wages = dr.GetInt32(7);
                        int idjob = dr.GetInt32(8);
                        string tTitle = string.Empty;
                        if (!dr.IsDBNull(9))
                            tTitle = dr.GetString("tTitle");
                        int floor = 0;
                        if (!dr.IsDBNull(10))
                            floor = dr.GetInt32(10);
                        int idTPK = dr.GetInt32(11);

                        TPKZone tPKZone = new TPKZone();

                        tPKZone.Id = idTPK;
                        tPKZone.Title = tTitle;
                        tPKZone.Floor = floor;

                        JobTitle jobTitle = new JobTitle();

                        jobTitle.Id = idjob;
                        jobTitle.Title = jTitle;
                        jobTitle.Task = task;
                        jobTitle.Wages = wages;

                        employees.Add(new Employee
                        {
                            Id = id,
                            JobTitleID = jobid,
                            TPKZoneID = tPKid,
                            FIO = fio,
                            PhoneNumber = contact,
                            JobTitles = jobTitle,
                            TPKZones = tPKZone
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

        internal bool Update(Employee employee)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"update `Employee` set `JobTitleID`=@jobId, `TPKZoneID`=@tpkId, `FIO`=@fio, `ContactPhoneNumber`=@phone where `ID` = {employee.Id}");
                mc.Parameters.Add(new MySqlParameter("jobId", employee.JobTitleID));
                mc.Parameters.Add(new MySqlParameter("tpkId", employee.TPKZoneID));
                mc.Parameters.Add(new MySqlParameter("fio", employee.FIO));
                mc.Parameters.Add(new MySqlParameter("phone", employee.PhoneNumber));

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

        internal bool Remove(Employee remove)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"delete from `Employee` where `ID` = {remove.Id}");
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

        static EmployeeDB db;
        public static EmployeeDB GetDB()
        {
            if (db == null)
                db = new EmployeeDB(DBConnection.GetDbConnection());
            return db;
        }
    }
}
