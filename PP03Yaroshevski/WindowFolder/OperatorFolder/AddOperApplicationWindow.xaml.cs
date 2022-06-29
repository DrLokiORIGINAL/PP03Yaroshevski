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
    /// <summary>
    /// Логика взаимодействия для AddOperApplicationWindow.xaml
    /// </summary>
    public partial class AddOperApplicationWindow : Window
    {
        SqlConnection sqlConnection =
            new SqlConnection(@"Data Source=desktop-snurd1j\sqlexpress;
            Initial Catalog=PP03Yaroshevski;
            Integrated Security=True");
        SqlCommand sqlCommand;
        public AddOperApplicationWindow()
        {
            InitializeComponent();
        }
        private void AddDescriptionBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LastNameSTb.Text))
            {
                MessageBoxClass.ErrorMB("Не введена фамилия сотрудника");
                LastNameSTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(FirstNameSTb.Text))
            {
                MessageBoxClass.ErrorMB("Не введено имя сотрудника");
                FirstNameSTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(MiddleNameSTb.Text))
            {
                MessageBoxClass.ErrorMB("Не введено отчество сотрудника");
                MiddleNameSTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(LastNameOTb.Text))
            {
                MessageBoxClass.ErrorMB("Не введена фамилия оператора");
                LastNameOTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(FirstNameOTb.Text))
            {
                MessageBoxClass.ErrorMB("Не введено имя оператора");
                FirstNameOTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(MiddleNameOTb.Text))
            {
                MessageBoxClass.ErrorMB("Не введено отчество оператора");
                MiddleNameOTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(DescriptionOfTheProblemTb.Text))
            {
                MessageBoxClass.ErrorMB("Не введено описание проблемы");
                DescriptionOfTheProblemTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(OfficeNumberTb.Text))
            {
                MessageBoxClass.ErrorMB("Не введён номер офиса");
                OfficeNumberTb.Focus();
            }
            else if (string.IsNullOrWhiteSpace(EmployeeTelephoneNumberTb.Text))
            {
                MessageBoxClass.ErrorMB("Не введён номер телефона сторудника");
                EmployeeTelephoneNumberTb.Focus();
            }
            else
            {
                try
                {
                    sqlConnection.Open();
                    sqlCommand = new SqlCommand("Insert Into" +
                        " dbo.[TablitsaZayavki] " +
                        "(LastNameS,FirstNameS, " +
                        "MiddleNameS, LastNameO, " +
                        "FirstNameO, MiddleNameO," +
                        " DescriptionOfTheProblem, OfficeNumber, " +
                        "EmployeeTelephoneNumber) Values " +
                        $"('{LastNameSTb.Text}', " +
                        $"'{FirstNameSTb.Text}', " +
                        $"'{MiddleNameSTb.Text}', " +
                        $"'{LastNameOTb.Text}', " +
                        $"'{FirstNameOTb.Text}', " +
                        $"'{MiddleNameOTb.Text}', " +
                        $"'{DescriptionOfTheProblemTb.Text}', " +
                        $"'{OfficeNumberTb.Text}', " +
                        $"'{EmployeeTelephoneNumberTb.Text}')",
                        sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    MessageBoxClass.InfoMB($"Проблема успешно добавлена");
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
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxClass.ExitMB();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            new MenuOperApplicationWindow().ShowDialog();
        }
    }
}
