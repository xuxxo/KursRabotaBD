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
        public MainWindow()
        {
            InitializeComponent();
            dtGrid.ItemsSource = MainClass.Lol();
           


        }
        
    }
}
