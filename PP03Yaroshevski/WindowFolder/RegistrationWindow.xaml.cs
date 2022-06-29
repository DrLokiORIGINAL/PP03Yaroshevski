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
    public partial class RegistrationWindow : Window
    {
        SqlConnection sqlConnection =
            new SqlConnection(@"Data Source=desktop-snurd1j\sqlexpress;
            Initial Catalog=PP03Yaroshevski;
            Integrated Security=True");
        SqlCommand sqlCommand;
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void ExitBtn_Click(object sender,RoutedEventArgs e)
        {
            MessageBoxClass.ExitMB();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationWindow window = new AuthorizationWindow();
            window.ShowDialog();
            this.Close();
        }

        private void RegistrationBtn_Click(object sender, RoutedEventArgs e)
        {
            string pass = PasswordPsb.Password;
            string zagl = "QWERTYUIOPASDFGHJKLZXCVBNM";
            string mal = "qwertyuiopasdfghjklzxcvbnm";
            string cif = "1234567890";
            string znak = "!@№#;$%:^?&*()-_+= " +
                "[{]};:'|<,>./";
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
            else if (zagl.IndexOfAny(pass.ToCharArray()) == -1)
            {
                MessageBoxClass.ErrorMB
                    ("пароль должен содержать" +
                    " заглавную букву");
                PasswordPsb.Focus();
            }
            else if (mal.IndexOfAny(pass.ToCharArray()) == -1)
            {
                MessageBoxClass.ErrorMB
                    ("пароль должен содержать" +
                    " прописную букву");
                PasswordPsb.Focus();
            }
            else if (cif.IndexOfAny(pass.ToCharArray()) == -1)
            {
                MessageBoxClass.ErrorMB
                    ("пароль должен содержать цифру");
                PasswordPsb.Focus();
            }
            else if (znak.IndexOfAny(pass.ToCharArray()) == -1)
            {
                MessageBoxClass.ErrorMB
                    ("Пароль должен содержать" +
                    " один из следующих знаков" + znak);
                PasswordPsb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(RepeatPasswordPsb.Password))
            {
                MessageBoxClass.ErrorMB("Вы не ввели повторно пароль");
                RepeatPasswordPsb.Focus();
            }
            else if (RepeatPasswordPsb.Password != PasswordPsb.Password)
            {
                MessageBoxClass.ErrorMB("Пароли не совпадают");
                RepeatPasswordPsb.Focus();
                PasswordPsb.Focus();
            }
            else
            {
                try
                {
                    sqlConnection.Open();
                    sqlCommand = new SqlCommand("Insert Into dbo.[TablitsaPolzovateley] " +
                        "(Login, Password, IdRole) Values " +
                        $"('{LoginTb.Text}','{PasswordPsb.Password}',2)",
                        sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    MessageBoxClass.InfoMB
                        ("Вы успешно зарегестрировались");
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
