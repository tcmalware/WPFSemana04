using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFSemana04
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-3I1FGFE\SQLEXPRESS;Initial Catalog=School;Integrated Security=True");
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            List<Person> people = new List<Person>();

            connection.Open();
            SqlCommand command = new SqlCommand("BuscarPersonaNombre", connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter paramater1 = new SqlParameter();
            paramater1.SqlDbType = SqlDbType.VarChar;
            paramater1.Size = 50;

            paramater1.Value = "ar";
            paramater1.ParameterName = "@FirstName";

            command.Parameters.Add(paramater1);

            SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                people.Add(new Person
                {
                    PersonID = dataReader["PersonID"].ToString(),
                    LastName = dataReader["LastName"].ToString(),
                    FirstName = dataReader["FirstName"].ToString(),

                    FullName = String.Concat(dataReader["FirstName"].ToString(), " ",
                    dataReader["LastName"].ToString())
                });
            }
            connection.Close();
            dgvPeople.ItemsSource = people;
        }
    }
}