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
    public partial class AddUserWindow : Window
    {
        SqlConnection sqlConnection =
            new SqlConnection(@"Data Source=desktop-snurd1j\sqlexpress;
            Initial Catalog=PP03Yaroshevski;
            Integrated Security=True");
        SqlCommand sqlCommand;
        CBClass cBClass;
        public AddUserWindow()
        {
            InitializeComponent();
            cBClass = new CBClass();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cBClass.RoleCBLoad(RoleCb);
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            new MenuAdminWindow().ShowDialog();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxClass.ExitMB();
        }

        private void AddUserBtn_Click(object sender, RoutedEventArgs e)
        {
            string pass = PasswordPsb.Password;
            string zagl = "QWERTYUIOPASDFGHJKLZXCVBNM";
            string mal = "qwertyuiopasdfghjklzxcvbnm";
            string cif = "1234567890";
            string znak = "!@№#;$%:^?&*()-_+=" +
                "[{]};:'|<,>./";
            if (string.IsNullOrWhiteSpace(LoginTb.Text))
            {
                MessageBoxClass.ErrorMB("Некоректный логин");
                LoginTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(PasswordPsb.Password))
            {
                MessageBoxClass.ErrorMB("Некоректный пароль");
                PasswordPsb.Focus();
            }
            else if (zagl.IndexOfAny(pass.ToCharArray()) == -1)
            {
                MessageBoxClass.ErrorMB("Пароль должен содержать заглавную букву");
                PasswordPsb.Focus();
            }
            else if (mal.IndexOfAny(pass.ToCharArray()) == -1)
            {
                MessageBoxClass.ErrorMB("Пароль должен содержать маленькую букву");
                PasswordPsb.Focus();
            }
            else if (cif.IndexOfAny(pass.ToCharArray()) == -1)
            {
                MessageBoxClass.ErrorMB("Пароль должен содержать цифру");
                PasswordPsb.Focus();
            }
            else if (znak.IndexOfAny(pass.ToCharArray()) == -1)
            {
                MessageBoxClass.ErrorMB("Пароль должен содержать " +
                    "один из этих знаков " + znak);
                PasswordPsb.Focus();
            }
            else if (RoleCb.SelectedIndex == -1)
            {
                MessageBoxClass.ErrorMB("Не выбрана роль");
                RoleCb.Focus();
            }
            else
            {

                try
                {
                    sqlConnection.Open();
                    sqlCommand = new SqlCommand("Insert Into dbo.[TablitsaPolzovateley] " +
                        "(Login, Password, IdRole) " +
                        "Values " +
                        $"('{LoginTb.Text}', '{PasswordPsb.Password}', " +
                        $"'{RoleCb.SelectedValue.ToString()}')",
                        sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    MessageBoxClass.InfoMB($"Пользователь {PPUserTb.Text} " +
                        $"{LoginTb.Text} Успешно добавлен");
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
