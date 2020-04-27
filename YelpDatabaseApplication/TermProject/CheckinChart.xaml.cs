using Npgsql;
using System;
using System.Collections.Generic;
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

namespace TermProject
{
    /// <summary>
    /// Interaction logic for Chart.xaml
    /// </summary>
    public partial class CheckinChart : Window
    {
        private string bid;

        public CheckinChart(string bid)
        {
            this.bid = bid;
            InitializeComponent();
            LoadChart();
        }

        /// <summary>
        /// Generate a check-in chart that shows the total number of check-ins per month.
        /// </summary>
        private void LoadChart()
        {
            List<KeyValuePair<string, int>> checkinData = new List<KeyValuePair<string, int>>();
            string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            using (var connection = new NpgsqlConnection(BuildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT EXTRACT(MONTH FROM \"time\"), COUNT(*) FROM business NATURAL JOIN checkin WHERE bid = '" + bid + "' GROUP BY EXTRACT(MONTH FROM \"time\");";
                    try
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                            checkinData.Add(new KeyValuePair<string, int>(months[(int)reader.GetDouble(0) - 1], (int)reader.GetInt64(1)));
                    }
                    catch (NpgsqlException ex)
                    {
                        System.Windows.MessageBox.Show("SQL Error - " + ex.Message.ToString());
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            myChart.DataContext = checkinData;
        }

        private string BuildConnectionString()
        {
            return "Host = localhost; Username = postgres; Database = yelpdb; Password = Shudokan2030";
        }
    }
}
