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
    public partial class EditUserWindow : Window
    {
        CBClass cBClass;
        SqlConnection sqlConnection =
            new SqlConnection(@"Data Source=desktop-snurd1j\sqlexpress;
            Initial Catalog=PP03Yaroshevski;
            Integrated Security=True");
        SqlCommand sqlCommand;
        SqlDataReader dataReader;
        public EditUserWindow()
        {
            InitializeComponent();
            cBClass = new CBClass();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cBClass.RoleCBLoad(RoleCb);
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("Select * from dbo.[TablitsaPolzovateley] " +
                $"Where PPUser='{VariableClass.UserId}'",
                sqlConnection);
                dataReader = sqlCommand.ExecuteReader();
                dataReader.Read();
                LoginTb.Text = dataReader[1].ToString();
                PasswordTb.Text = dataReader[2].ToString();
                RoleCb.SelectedValue = dataReader[3].ToString();
            }
            catch (Exception ex)
            {
                MessageBoxClass.ErrorMB(ex);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void EditUserBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlConnection.Open();
                sqlCommand =
                new SqlCommand("Update " +
                "dbo.[TablitsaPolzovateley] " +
                $"Set [Login] ='{LoginTb.Text}'," +
                $"[Password]='{PasswordTb.Text}'," +
                $"IdRole = '{RoleCb.SelectedValue.ToString()}' " +
                $"Where PPUser='{VariableClass.UserId}'",
                sqlConnection);
                sqlCommand.ExecuteNonQuery();
                MessageBoxClass.InfoMB($"Данные пользователя" +
                $" успешно отредактированы");
            }
            catch (Exception ex)
            {
                MessageBoxClass.ErrorMB(ex);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxClass.ExitMB();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            new MenuAdminWindow().ShowDialog();
        }
    }
}
