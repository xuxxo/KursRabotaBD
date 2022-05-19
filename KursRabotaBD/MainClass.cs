using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
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

        public void SaveDB()
        {
            var date = DateTime.Now.ToString();
            date = date.Replace(".", "_");
            date = date.Replace(" ", "_");

            date = date.Replace(":", "_");

            date = date.Replace("/", "_");
            var query = "backup database test TO DISK='D:\\backup_" + date + ".bak'";
            //var query = "backup database test TO DISK='D:\\log.bak'";
            db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, query);
            ShowSuccess();

        }
        public void SaveJournal()
        {
            var a = "ALTER DATABASE test SET RECOVERY FULL";
            //db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, a);
            var query = "backup log test to dbbackup";

            db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, query);
            ShowSuccess();

        }
        public void LoadBackup()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            
            openFileDialog.InitialDirectory = "d:\\";
            openFileDialog.Filter = "bak files (*.bak)|*.bak";
            string filePath;
            if ((bool)openFileDialog.ShowDialog())
            {
                filePath = openFileDialog.FileName;
                var query = $"USE master;RESTORE DATABASE test FROM DISK='{filePath}' WITH REPLACE";
                db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, query);
                ShowSuccess();

            }


        }

        public void LoadJournal()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "d:\\";
            openFileDialog.Filter = "bak files (*.bak)|*.bak";
            string filePath;
            if ((bool)openFileDialog.ShowDialog())
            {
                filePath = openFileDialog.FileName;
                //var query = $"USE master;RESTORE LOG test FROM DISK='{filePath}'";
                var query = $@"use master;RESTORE LOG test FROM DISK = '{filePath}'";
                try
                {
                    db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, query);
                    ShowSuccess();
                }
                catch (Exception ex)
                {

                    System.Windows.MessageBox.Show(ex.Message);
                }
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
