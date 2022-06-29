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

namespace PP03Yaroshevski.WindowFolder
{
    public partial class AuthorizationWindow : Window
    {
        SqlConnection sqlConnection =
            new SqlConnection(@"Data Source=desktop-snurd1j\sqlexpress;
            Initial Catalog=PP03Yaroshevski;
            Integrated Security=True");
        SqlCommand sqlCommand;
        SqlDataReader dataReader;
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void RegistrationBtn_Click(object sender, RoutedEventArgs e)
        {
            new RegistrationWindow().ShowDialog();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxClass.ExitMB();
        }

        private void LogInBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoginTb.Text))
            {
                MessageBoxClass.ErrorMB("Вы не ввели логин");
                LoginTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(PasswordPsb.Password))
            {
                MessageBoxClass.ErrorMB("Вы не ввели пароль");
                PasswordPsb.Focus();
            }
            else
            {
                try
                {
                    sqlConnection.Open();
                    sqlCommand=new SqlCommand("Select Login, Password, IdRole " +
                        "From dbo.[TablitsaPolzovateley] " +
                        $"Where Login='{LoginTb.Text}'",
                        sqlConnection);
                    dataReader = sqlCommand.ExecuteReader();
                    dataReader.Read();

                    if (dataReader[1].ToString()!=
                        PasswordPsb.Password)
                    {
                        MessageBoxClass.ErrorMB("Вы ввели неверный пароль");
                        PasswordPsb.Focus();
                    }
                    else
                    {
                        switch (dataReader[2].ToString())
                        {
                            case "1":
                                new AdminFolder.
                                    MenuAdminWindow().ShowDialog();
                                this.Close();
                                break;
                            case "2":
                                new OperatorFolder.
                                    MenuOperApplicationWindow().ShowDialog();
                                break;
                        }
                    }
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
        }
    }
}
