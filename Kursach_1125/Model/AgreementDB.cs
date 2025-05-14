using System;
using MySqlConnector;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Kursach_1125.Model
{
    internal class AgreementDB
    {
        DBConnection connection;

        private AgreementDB(DBConnection db)
        {
            this.connection = db;
        }

        public bool Insert(Agreement agreement)
        {
            bool result = false;
            if (connection == null)
            {
                return result;
            }

            if (connection.OpenConnection())
            {
                MySqlCommand cmd = connection.CreateCommand("insert into `Agreement` Values (0, @tentant, @tpkZ, @dateOfS, @endDate, @rentalR, @status); select LAST_INSERT_ID();");

                cmd.Parameters.Add(new MySqlParameter("tentant", agreement.TentantID));
                cmd.Parameters.Add(new MySqlParameter("tpkZ", agreement.TPKZoneID));
                cmd.Parameters.Add(new MySqlParameter("dateOfS", agreement.DateOfString));
                cmd.Parameters.Add(new MySqlParameter("endDate", agreement.EndDate));
                cmd.Parameters.Add(new MySqlParameter("rentalR", agreement.RentalRate));
                cmd.Parameters.Add(new MySqlParameter("status", agreement.Status));

                try
                {
                    int id = (int)(ulong)cmd.ExecuteScalar();
                    if (id > 0)
                    {
                        MessageBox.Show(id.ToString());
                        agreement.Id = id;
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

        internal List<Agreement> SelectAll()
        {
            List<Agreement> agreements = new List<Agreement>();
            if (connection == null)
                { return agreements; }

            if (connection.OpenConnection())
            {
                var command = connection.CreateCommand("select a.`ID`, `TentantID`, `TPKZoneID`, `DateOfSigning`, `EndDate`, `RentalRate`, `Status`, " +
                    "t.`Title` as tTitle, t.`ContactPerson`, t.`Email`, t.`ID`," +
                    "p.`Title` as pTitle, p.`Floor`, p.`ID` from `Agreement` a JOIN `Tentant` t ON `TentantID` = t.ID JOIN `TPKZone` p ON `TPKZoneID` = p.`ID`");
                try
                {
                    MySqlDataReader dr = command.ExecuteReader();
                    
                    while (dr.Read())
                    {
                        int id = dr.GetInt32(0);
                        int tentantID = dr.GetInt32(1);
                        int tpkZoneID = dr.GetInt32(2);
                        DateTime dateOfS = DateTime.Now;
                        if (!dr.IsDBNull(3))
                            dateOfS = dr.GetDateTime("DateOfSigning");
                        DateTime endDate = DateTime.Now;
                        if (!dr.IsDBNull(4))
                            endDate = dr.GetDateTime("EndDate");
                        int rentalRate = 0;
                        if (!dr.IsDBNull(5))
                            rentalRate = dr.GetInt32(5);
                        bool status= false;
                        if (!dr.IsDBNull(6))
                            status = dr.GetBoolean(6);
                        string Ttitle = string.Empty;
                        if (!dr.IsDBNull(7))
                            Ttitle = dr.GetString("tTitle");
                        string contactPerson = string.Empty;
                        if (!dr.IsDBNull(8))
                            contactPerson = dr.GetString("ContactPerson");
                        string Email = string.Empty;
                        if (!dr.IsDBNull(9))
                            Email = dr.GetString("Email");
                        int idTentant = dr.GetInt32(10);
                        string Ptitle = string.Empty;
                        if (!dr.IsDBNull(11))
                            Ptitle = dr.GetString("pTitle");
                        int floor = 0;
                        if (!dr.IsDBNull(12))
                            floor = dr.GetInt32(12);
                        int idTPKZone = dr.GetInt32(13);

                        Tentant tentant = new Tentant();

                        tentant.Id = idTentant;
                        tentant.Title = Ttitle;
                        tentant.ContactPerson = contactPerson;
                        tentant.Email = Email;

                        TPKZone tPKZone = new TPKZone();

                        tPKZone.Id = idTPKZone;
                        tPKZone.Title = Ptitle;
                        tPKZone.Floor = floor;
                        agreements.Add(new Agreement
                        {
                            Id = id,
                            TentantID = tentantID,
                            TPKZoneID = tpkZoneID,
                            DateOfString = dateOfS,
                            EndDate = endDate,
                            RentalRate = rentalRate,
                            Status = status,
                            Tentants = tentant,
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
            return agreements;
        }

        internal bool Update(Agreement edit)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"update `Agreement` set `TentantID`=@tentantID, `TPKZoneID`=@tpkZoneID, `DateOfSigning`=@dateOfS, " +
                    $"`EndDate`=@endDate, `RentalRate`=@rentalRate, `Status`=@status where `ID` = {edit.Id}");
                mc.Parameters.Add(new MySqlParameter("tentantID", edit.TentantID));
                mc.Parameters.Add(new MySqlParameter("tpkZoneID", edit.TPKZoneID));
                mc.Parameters.Add(new MySqlParameter("dateOfS", edit.DateOfString));
                mc.Parameters.Add(new MySqlParameter("endDate", edit.EndDate));
                mc.Parameters.Add(new MySqlParameter("rentalRate", edit.RentalRate));
                mc.Parameters.Add(new MySqlParameter("status", edit.Status));

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

        internal bool Remove (Agreement remove)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"delete from `Agreement` where `ID` = {remove.Id}");
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

        static AgreementDB db;
        public static AgreementDB GetDB()
        {
            if (db == null)
                db = new AgreementDB(DBConnection.GetDbConnection());
            return db;
        }
    }
}
