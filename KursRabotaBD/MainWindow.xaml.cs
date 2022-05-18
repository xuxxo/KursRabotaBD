using System;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;

namespace KursRabotaBD
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainClass mainClass;
        public MainWindow()
        {
            InitializeComponent();
            mainClass = new MainClass();
            mainClass.SetDataGrid(ref dtGrid);
            mainClass.FillTheGrid();
            


        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            mainClass.SaveChanges();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (dtGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < dtGrid.SelectedItems.Count; i++)
                {
                    User user = dtGrid.SelectedItems[i] as User;
                    if (user != null)
                    {
                        mainClass.DeleteUser(user.Id);
                    }
                }
                mainClass.SaveChanges();
            }
            
        }

        private void backupButton_Click(object sender, RoutedEventArgs e)
        {
            BackupWindow backupWindow = new BackupWindow();
            
            backupWindow.ShowDialog();

        }
    }
}
