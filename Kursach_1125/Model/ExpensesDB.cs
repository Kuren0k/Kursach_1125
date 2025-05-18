using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using System.Windows;

namespace Kursach_1125.Model
{
    internal class ExpensesDB
    {
        DBConnection connection;

        private ExpensesDB(DBConnection db)
        {
            this.connection = db;
        }

        public bool Insert(Expenses expenses)
        {
            bool result = false;
            if (connection == null)
            {
                return result;
            }

            if (connection.OpenConnection())
            {
                MySqlCommand cmd = connection.CreateCommand("insert into `Expenses` Values (0, @title, @cost); select LAST_INSERT_ID();");

                cmd.Parameters.Add(new MySqlParameter("title", expenses.Title));
                cmd.Parameters.Add(new MySqlParameter("cost", expenses.Cost));

                try
                {
                    int id = (int)(ulong)cmd.ExecuteScalar();
                    if (id > 0)
                    {
                        MessageBox.Show(id.ToString());
                        expenses.Id = id;
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

        internal List<Expenses> SelectAll()
        {
            List<Expenses> expenses = new List<Expenses>();
            if (connection == null)
            { return expenses; }

            if (connection.OpenConnection())
            {
                var command = connection.CreateCommand("select `ID`, `Title`, `Cost` from `Expenses`");
                try
                {
                    MySqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        int id = dr.GetInt32(0);
                        string title = string.Empty;
                        if (!dr.IsDBNull(1))
                            title = dr.GetString("Title");
                        int cost = 0;
                        if (!dr.IsDBNull(2))
                            cost = dr.GetInt32(2);
                        expenses.Add(new Expenses
                        {
                            Id = id,
                            Title = title,
                            Cost = cost
                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return expenses;
        }

        internal bool Update(Expenses expenses)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"update `Expenses` set `Title`=@title, `Cost`=@cost where `ID` = {expenses.Id}");
                mc.Parameters.Add(new MySqlParameter("title", expenses.Title));
                mc.Parameters.Add(new MySqlParameter("cost", expenses.Cost));

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

        internal bool Remove(Expenses remove)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"delete from `Expenses` where `ID` = {remove.Id}");
                try
                {
                    mc.ExecuteNonQuery();
                    result = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Удаление невозможно, элемент связан с другой таблицей" );
                }
            }
            connection.CloseConnection();
            return result;
        }

        static ExpensesDB db;
        public static ExpensesDB GetDB()
        {
            if (db == null)
                db = new ExpensesDB(DBConnection.GetDbConnection());
            return db;
        }
    }
}
