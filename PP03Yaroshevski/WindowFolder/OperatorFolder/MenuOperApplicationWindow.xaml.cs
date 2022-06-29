using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using PP03Yaroshevski.ClassFolder;

namespace PP03Yaroshevski.WindowFolder.OperatorFolder
{
    public partial class MenuOperApplicationWindow : Window
    {
        DGClass dGClass;
        public MenuOperApplicationWindow()
        {
            InitializeComponent();
            dGClass = new DGClass(ListUserDG);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dGClass.LoadDG("Select * From dbo.[TablitsaZayavki]");
        }

        private void AddIm_Click(object sender, RoutedEventArgs e)
        {
            new AddOperApplicationWindow().ShowDialog();
            dGClass.LoadDG("Select * From dbo.[TablitsaZayavki]");
        }

        private void EditIm_Click(object sender, RoutedEventArgs e)
        {
            new EditOperApplicationWindow().ShowDialog();
            dGClass.LoadDG("Select * From dbo.[TablitsaZayavki]");
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            dGClass.LoadDG("Select * From" +
                " dbo.[TablitsaZayavki]" +
                $"Where LastNameS Like '%{SearchTb.Text}%'" +
                $"Or FirstNameS Like '%{SearchTb.Text}%'" +
                $"Or MiddleNameS Like '%{SearchTb.Text}%'");
        }

        private void ListUserDG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ListUserDG.SelectedItem == null)
            {
                MessageBoxClass.ErrorMB("Вы не выбрали строку");
            }
            else
            {
                VariableClass.UserId = dGClass.SelectId();
                try
                {
                    new EditOperApplicationWindow().ShowDialog();
                    dGClass.LoadDG("Select * From dbo.[TablitsaZayavki]");
                }
                catch (Exception ex)
                {
                    MessageBoxClass.ErrorMB(ex);
                }
            }
        }

        private void ExitIm_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxClass.ExitMB();
        }
    }
}
