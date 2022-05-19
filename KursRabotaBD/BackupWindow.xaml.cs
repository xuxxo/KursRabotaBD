using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KursRabotaBD
{
    /// <summary>
    /// Логика взаимодействия для BackupWindow.xaml
    /// </summary>
    public partial class BackupWindow : Window
    {
        readonly MainClass mainClass = new MainClass();
        public BackupWindow()
        {
            InitializeComponent();
        }
        private void recBase_Click(object sender, RoutedEventArgs e)
        {
            
            mainClass.SaveDB();
        }

        private void recJour_Click(object sender, RoutedEventArgs e)
        {
            
            mainClass.SaveJournal();
        }

        private void saveBase_Click(object sender, RoutedEventArgs e)
        {
            mainClass.LoadBackup();
        }

        private void saveJour_Click(object sender, RoutedEventArgs e)
        {
            mainClass.LoadJournal();
        }
    }
}
