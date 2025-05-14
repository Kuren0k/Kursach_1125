using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using System.Windows;
using Spire.Doc;

namespace Kursach_1125.Model
{
    internal class AgreementDocumentDB
    {
        DBConnection connection;

        private AgreementDocumentDB(DBConnection db)
        {
            this.connection = db;
        }
        internal List<AgreementDocument> SelectAll()
        {
            List<AgreementDocument> documents = new List<AgreementDocument>();
            if (connection == null)
            { return documents; }

            if (connection.OpenConnection())
            {
                var command = connection.CreateCommand("select d.`ID`, `AgreementID`, `FilePath`, `CreateDate`, a.`ID`, `TentantID`, `TPKZoneID`, `DateOfSigning`, `EndDate`, `RentalRate`, `Status`, " +
                    "t.`Title` as tTitle, t.`ContactPerson`, t.`Email`, t.`ID`," +
                    "p.`Title` as pTitle, p.`Floor`, p.`ID`, t.`PhoneNumber`  " +
                    "from `AgreementDocument` d JOIN `Agreement` a ON `AgreementID` = a.ID JOIN `Tentant` t ON `TentantID` = t.ID JOIN `TPKZone` p ON `TPKZoneID` = p.`ID`");
                try
                {
                    MySqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        int id = dr.GetInt32(0);
                        int agreementId = dr.GetInt32(1);
                        string file = string.Empty;
                        if (!dr.IsDBNull(2))
                            file = dr.GetString("FilePath");
                        DateTime createDate = DateTime.Now;
                        if (!dr.IsDBNull(3))
                            createDate = dr.GetDateTime("CreateDate");
                        int idAgreement = dr.GetInt32(4);
                        int tentantID = dr.GetInt32(5);
                        int tpkZoneID = dr.GetInt32(6);
                        DateTime dateOfS = DateTime.Now;
                        if (!dr.IsDBNull(7))
                            dateOfS = dr.GetDateTime("DateOfSigning");
                        DateTime endDate = DateTime.Now;
                        if (!dr.IsDBNull(8))
                            endDate = dr.GetDateTime("EndDate");
                        int rentalRate = 0;
                        if (!dr.IsDBNull(9))
                            rentalRate = dr.GetInt32(9);
                        bool status = false;
                        if (!dr.IsDBNull(10))
                            status = dr.GetBoolean(10);
                        string Ttitle = string.Empty;
                        if (!dr.IsDBNull(11))
                            Ttitle = dr.GetString("tTitle");
                        string contactPerson = string.Empty;
                        if (!dr.IsDBNull(12))
                            contactPerson = dr.GetString("ContactPerson");
                        string Email = string.Empty;
                        if (!dr.IsDBNull(13))
                            Email = dr.GetString("Email");
                        int idTentant = dr.GetInt32(14);
                        string Ptitle = string.Empty;
                        if (!dr.IsDBNull(15))
                            Ptitle = dr.GetString("pTitle");
                        int floor = 0;
                        if (!dr.IsDBNull(16))
                            floor = dr.GetInt32(16);
                        int idTPKZone = dr.GetInt32(17);
                        string phone = string.Empty;
                        if (!dr.IsDBNull(18))
                            phone = dr.GetString("PhoneNumber");

                        Tentant tentant = new Tentant();

                        tentant.Id = idTentant;
                        tentant.Title = Ttitle;
                        tentant.ContactPerson = contactPerson;
                        tentant.Email = Email;
                        tentant.PhoneNumber = phone;

                        TPKZone tPKZone = new TPKZone();

                        tPKZone.Id = idTPKZone;
                        tPKZone.Title = Ptitle;
                        tPKZone.Floor = floor;

                        Agreement agreement = new Agreement();

                        agreement.Id = idAgreement;
                        agreement.TentantID = tentantID;
                        agreement.TPKZoneID = tpkZoneID;
                        agreement.DateOfString = dateOfS;
                        agreement.EndDate = endDate;
                        agreement.RentalRate = rentalRate;
                        agreement.Status = status;
                        agreement.Tentants = tentant;
                        agreement.TPKZones = tPKZone;

                        documents.Add(new AgreementDocument
                        {
                            Id = id,
                            AgreementId = agreementId,
                            FilePath = file,
                            CreateDate = createDate,
                            Agreements = agreement
                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return documents;
        }

        internal bool Remove(AgreementDocument remove)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"delete from `AgreementDocument` where `ID` = {remove.Id}");
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

        public void OpenDocument(AgreementDocument document)
        {
            if (document == null)
            {
                MessageBox.Show("Документ не выбран.");
                return;
            }

            string filePath = document.FilePath;

            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Путь к файлу не указан.");
                return;
            }

            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(filePath) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось открыть файл: {ex.Message}");
            }
        }

        static AgreementDocumentDB db;
        public static AgreementDocumentDB GetDB()
        {
            if (db == null)
                db = new AgreementDocumentDB(DBConnection.GetDbConnection());
            return db;
        }
    }
}
