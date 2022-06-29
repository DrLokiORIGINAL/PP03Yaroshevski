using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PP03Yaroshevski.ClassFolder
{
    class CBClass
    {
        SqlConnection sqlConnection =
            new SqlConnection(@"Data Source=desktop-snurd1j\sqlexpress;
            Initial Catalog=PP03Yaroshevski;
            Integrated Security=True");
        SqlDataAdapter sqlData;
        DataSet dataSet;

        public void RoleCBLoad(ComboBox comboBox)
        {
            try
            {
                sqlConnection.Open();
                sqlData = new SqlDataAdapter("Select PPRole, RoleTitle " +
                    "From dbo.[TablitsaRoli] Order by PPRole ASC ", 
                    sqlConnection);
                dataSet = new DataSet();
                sqlData.Fill(dataSet, "[TablitsaRoli]");
                comboBox.ItemsSource = dataSet.Tables["[TablitsaRoli]"].DefaultView;
                comboBox.SelectedValuePath = dataSet.
                    Tables["[TablitsaRoli]"].Columns["PPRole"].ToString();
                comboBox.DisplayMemberPath = dataSet.
                    Tables["[TablitsaRoli]"].Columns["RoleTitle"].ToString();
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
