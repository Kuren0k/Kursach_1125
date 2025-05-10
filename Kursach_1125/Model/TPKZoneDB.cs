using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Kursach_1125.Model
{
    internal class TPKZoneDB
    {
        DBConnection connection;

        private TPKZoneDB(DBConnection db)
        {
            this.connection = db;
        }

        public bool Insert(TPKZone zone)
        {
            bool result = false;
            if (connection == null)
            {
                return result;
            }

            if (connection.OpenConnection())
            {
                MySqlCommand cmd = connection.CreateCommand("insert into `TPKZone` Values (0, @title, @floor, @square); select LAST_INSERT_ID();");

                cmd.Parameters.Add(new MySqlParameter("title", zone.Title));
                cmd.Parameters.Add(new MySqlParameter("floor", zone.Floor));
                cmd.Parameters.Add(new MySqlParameter("square", zone.Square));

                try
                {
                    int id = (int)(ulong)cmd.ExecuteScalar();
                    if (id > 0)
                    {
                        MessageBox.Show(id.ToString());
                        zone.Id = id;
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

        internal List<TPKZone> SelectAll()
        {
            List<TPKZone> zones = new List<TPKZone>();
            if (connection == null)
            { return zones; }

            if (connection.OpenConnection())
            {
                var command = connection.CreateCommand("select `ID`, `Title`, `Floor`, `Square` from `TPKZone`");
                try
                {
                    MySqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        int id = dr.GetInt32(0);
                        string title = string.Empty;
                        if (!dr.IsDBNull(1))
                            title = dr.GetString("Title");
                        int floor = 0;
                        if (!dr.IsDBNull(2))
                            floor = dr.GetInt32(2);
                        int square = 0;
                        if (!dr.IsDBNull(3))
                            square = dr.GetInt32(3);
                        zones.Add(new TPKZone
                        {
                            Id = id,
                            Title = title,
                            Floor = floor,
                            Square = square
                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return zones;
        }

        internal bool Update(TPKZone zone)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"update `TPKZone` set `Title`=@title, `Floor`=@floor, `Square`=@square where `ID` = {zone.Id}");
                mc.Parameters.Add(new MySqlParameter("title", zone.Title));
                mc.Parameters.Add(new MySqlParameter("floor", zone.Floor));
                mc.Parameters.Add(new MySqlParameter("square", zone.Square));

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

        internal bool Remove(TPKZone remove)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"delete from `TPKZone` where `ID` = {remove.Id}");
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

        static TPKZoneDB db;
        public static TPKZoneDB GetDB()
        {
            if (db == null)
                db = new TPKZoneDB(DBConnection.GetDbConnection());
            return db;
        }
    }
}
