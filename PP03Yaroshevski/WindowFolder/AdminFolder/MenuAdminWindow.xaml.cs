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

namespace PP03Yaroshevski.WindowFolder.AdminFolder
{
    public partial class MenuAdminWindow : Window
    {
        DGClass dGClass;
        public MenuAdminWindow()
        {
            InitializeComponent();
            dGClass = new DGClass(ListUserDG);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dGClass.LoadDG("Select * From dbo.[TablitsaPolzovateley]");
        }

        private void AddIm_Click(object sender, RoutedEventArgs e)
        {
            new AddUserWindow().ShowDialog();
            dGClass.LoadDG("Select * From dbo.[TablitsaPolzovateley]");
        }

        private void EditIm_Click(object sender, RoutedEventArgs e)
        {
            new EditUserWindow().ShowDialog();
            dGClass.LoadDG("Select * From dbo.[TablitsaPolzovateley]");
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            dGClass.LoadDG("Select * From" +
                " dbo.[TablitsaPolzovateley]" +
                $"Where IdRole Like '%{SearchTb.Text}'" +
                $"Or Login Like '%{SearchTb.Text}%'");
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
                    new EditUserWindow().ShowDialog();
                    dGClass.LoadDG("Select * From dbo.[TablitsaPolzovateley]");
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
