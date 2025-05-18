using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Kursach_1125.Model
{
    internal class JobTitleDB
    {
        DBConnection connection;

        private JobTitleDB(DBConnection db)
        {
            this.connection = db;
        }

        internal List<JobTitle> SelectAll()
        {
            List<JobTitle> jobTitles = new List<JobTitle>();
            if (connection == null)
            { return jobTitles; }

            if (connection.OpenConnection())
            {
                var command = connection.CreateCommand("select `ID`, `Title`, `Task`, `Wages` from `JobTitle`");
                try
                {
                    MySqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        int id = dr.GetInt32(0);
                        string title = string.Empty;
                        if (!dr.IsDBNull(1))
                            title = dr.GetString("Title");
                        string task = string.Empty;
                        if (!dr.IsDBNull(2))
                            task = dr.GetString("Task");
                        int wages = 0;
                        if (!dr.IsDBNull(3))
                            wages = dr.GetInt32(3);

                        jobTitles.Add(new JobTitle
                        {
                            Id = id,
                            Title = title,
                            Task = task,
                            Wages = wages
                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Удаление невозможно, элемент связан с другой таблицей");
                }
            }
            connection.CloseConnection();
            return jobTitles;
        }
        static JobTitleDB db;
        public static JobTitleDB GetDB()
        {
            if (db == null)
                db = new JobTitleDB(DBConnection.GetDbConnection());
            return db;
        }
    }
}
