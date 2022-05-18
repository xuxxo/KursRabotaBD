using System;
using System.Collections.Generic;
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
            
            
            //    // создаем два объекта User
            //    User user1 = new User { Name = "Tom", Age = 33 };
            //    User user2 = new User { Name = "Sam", Age = 26 };

            //    // добавляем их в бд
            //    db.Users.Add(user1);
            //    db.Users.Add(user2);
            //    db.SaveChanges();
                  db.Users.Load();
                  
                  _dtGrid.ItemsSource = db.Users.Local.ToBindingList();
                  
                // получаем объекты из бд и выводим на консоль
                
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
            //var query = "backup database master TO DISK='D:\\backup"+date+".bak'";
            var query = "backup database master TO DISK='D:\\log.bak'";
            db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, query);
        }
        public void SaveJournal()
        {
            var a = "EXEC sp_addumpdevice 'disk', 'dbbackup', 'D:\\log.bak'";
            //db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, a);
            var query = "backup log master to dbbackup WITH NO_TRUNCATE";
            //string curFile = "D:\\log.bak";
            //if(!File.Exists(curFile))
            //{

            //}
            db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, query);
        }
    }
}
