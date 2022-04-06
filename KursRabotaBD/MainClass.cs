using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace KursRabotaBD
{
    internal class MainClass
    {
        public static List<User> Lol()
        {
            using (UserContext db = new UserContext())
            {
                // создаем два объекта User
                User user1 = new User { Name = "Tom", Age = 33 };
                User user2 = new User { Name = "Sam", Age = 26 };

                // добавляем их в бд
                //db.Users.Add(user1);
               // db.Users.Add(user2);
                //db.SaveChanges();
                Console.WriteLine("Объекты успешно сохранены");
                db.Users.Load();
                // получаем объекты из бд и выводим на консоль
                var users = db.Users.ToList();
                return users;
            }

        }
    }
}
