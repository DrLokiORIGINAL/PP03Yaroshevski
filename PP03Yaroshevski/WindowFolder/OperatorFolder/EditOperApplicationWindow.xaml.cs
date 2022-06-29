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
    public partial class EditOperApplicationWindow : Window
    {
        SqlConnection sqlConnection =
            new SqlConnection(@"Data Source=desktop-snurd1j\sqlexpress;
            Initial Catalog=PP03Yaroshevski;
            Integrated Security=True");
        SqlCommand sqlCommand;
        SqlDataReader dataReader;
        public EditOperApplicationWindow()
        {
            InitializeComponent();
        }

        private void EditDescriptionBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlConnection.Open();
                sqlCommand =
                    new SqlCommand("Update " +
                    "dbo.[TablitsaZayavki] " +
                    $"Set LastNameS ='{LastNameSTb.Text}'," +
                    $"FirstNameS='{FirstNameSTb.Text}'," +
                    $"MiddleNameS='{MiddleNameSTb.Text}'," +
                    $"LastNameO='{LastNameOTb.Text}'," +
                    $"FirstNameO='{FirstNameOTb.Text}'," +
                    $"MiddleNameO='{MiddleNameOTb.Text}'," +
                    $"DescriptionOfTheProblem='{DescriptionOfTheProblemTb.Text}'," +
                    $"OfficeNumber='{OfficeNumberTb.Text}'," +
                    $"EmployeeTelephoneNumber='{EmployeeTelephoneNumberTb.Text}' " +
                    $"Where PP='{VariableClass.UserId}'",
                    sqlConnection);
                sqlCommand.ExecuteNonQuery();
                MessageBoxClass.InfoMB($"Данные проблемы успешно отредактированы");
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
            new MenuOperApplicationWindow().ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("Select * from dbo.[TablitsaZayavki] " +
                $"Where PP='{VariableClass.UserId}'",
                sqlConnection);
                dataReader = sqlCommand.ExecuteReader();
                dataReader.Read();
                FirstNameSTb.Text = dataReader[1].ToString();
                LastNameSTb.Text = dataReader[2].ToString();
                MiddleNameSTb.Text = dataReader[3].ToString();
                FirstNameOTb.Text = dataReader[4].ToString();
                LastNameOTb.Text = dataReader[5].ToString();
                MiddleNameOTb.Text = dataReader[6].ToString();
                DescriptionOfTheProblemTb.Text = dataReader[7].ToString();
                OfficeNumberTb.Text = dataReader[8].ToString();
                EmployeeTelephoneNumberTb.Text = dataReader[9].ToString();
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
