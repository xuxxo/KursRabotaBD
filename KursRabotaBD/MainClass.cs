using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace KursRabotaBD
{
    internal class MainClass
    {
        UserContext db = new UserContext();
        DataGrid _dtGrid;
        public void FillTheGrid()
        {
            db.Users.Local.Clear();
            db.Users.Load();
            db.Users.ToList().ForEach(x=>db.Users.Local.Add(x));
            
            _dtGrid.ItemsSource = db.Users.Local.ToBindingList();

        }

        public void DeleteUser(int id)
        {
            db.Users.Remove(db.Users.Find(id));
        }
        public void SaveChanges()
        {
            db.SaveChanges();
        }

        public void SetDataGrid(ref DataGrid dtGrid)
        {
            _dtGrid = dtGrid;
        }

        public void SaveDB(bool isInit = true)
        {
            File.WriteAllText("amountOfLogs.txt","0");

            try
            {

                var query = "BACKUP DATABASE [test] TO  [kurs_backup_db] WITH NOFORMAT, " +
                    (isInit ? "INIT" : "NOINIT") +
                    ", SKIP, NOREWIND, NOUNLOAD";
                db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, query);
                SaveJournal(true, true);
                MessageBox.Show("Операция выполнена успешно");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
}
        public void SaveJournal(bool isInit = false, bool isInnerFunc = false)
        {
            var amount = Convert.ToByte(File.ReadAllText("amountOfLogs.txt"));
            amount++;
            File.WriteAllText("amountOfLogs.txt", amount.ToString());
            try
            {
                var query = "BACKUP LOG [test] TO  [kurs_backup_log] WITH NOFORMAT, " +
                (isInit ? "INIT" : "NOINIT") +
                ", SKIP, NOREWIND, NOUNLOAD";

                db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, query);
                if (!isInnerFunc)
                {
                    MessageBox.Show("Операция выполнена успешно");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void LoadBackup()
        {
            try
            {
                var query = $@"USE [master];
                            ALTER DATABASE [test] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                            RESTORE DATABASE [test] FROM  [kurs_backup_db] WITH  FILE = 1,  RECOVERY,  NOUNLOAD,  REPLACE";
                db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, query);
                MessageBox.Show("Операция выполнена успешно");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        public void LoadJournal()
        {
            try
            {
                string query = $@"USE [master]; 
                ALTER DATABASE [test] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; 
                RESTORE DATABASE [test] FROM  [kurs_backup_db] WITH  FILE = 1,  NORECOVERY,  NOUNLOAD,  REPLACE ";
                //db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, query);
                var amount = Convert.ToByte(File.ReadAllText("amountOfLogs.txt"));
                //query = string.Empty;

                for (int i = 1; i < amount + 1; i++)
                {
                    query +=
                        $"RESTORE LOG [test] FROM  [kurs_backup_log] WITH  FILE = {i},  NOUNLOAD, " + ((i == amount) ? "RECOVERY" : "NORECOVERY") + ";\n";
                }
                db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, query);
                MessageBox.Show("Операция выполнена успешно");

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }


        }

        public void SendCustomQuery(string query)
        {
            db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, query);
            ShowSuccess();

        }

        private void ShowSuccess()
        {
            System.Windows.MessageBox.Show("Операция выполнена успешно");
        }
    }
}
