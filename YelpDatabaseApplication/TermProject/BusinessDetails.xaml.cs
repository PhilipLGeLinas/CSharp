using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Npgsql;

namespace TermProject
{
    /// <summary>
    /// Interaction logic for BusinessDetails.xaml
    /// </summary>
    public partial class BusinessDetails : Window
    {
        private readonly string bid = "";
        private readonly string userid = "";

        public class Tip
        {
            public string content { get; set; }
            public string bid { get; set; }
            public string date { get; set; }
            public string username { get; set; }
            public string likes { get; set; }
        }

        public BusinessDetails(string bid, string userid = "")
        {
            InitializeComponent();
            this.bid = bid;
            this.userid = userid;
            LoadBusinessDetails();
            LoadBusinessNumbers();
            AddTip();
            AddFriendsTips();
            AddColumnsToGrid();
        }

        private void AddColumnsToGrid()
        {
            DataGridTextColumn col0 = new DataGridTextColumn
            {
                Binding = new Binding("date"),
                Header = "Date",
                Width = 160
            };

            DataGridTextColumn col1 = new DataGridTextColumn
            {
                Binding = new Binding("username"),
                Header = "Username",
                Width = 100
            };

            DataGridTextColumn col2 = new DataGridTextColumn
            {
                Binding = new Binding("likes"),
                Header = "Likes",
                Width = 60
            };

            DataGridTextColumn col3 = new DataGridTextColumn
            {
                Binding = new Binding("content"),
                Header = "Content",
                Width = 360
            };

            DataGridTextColumn col4 = new DataGridTextColumn
            {
                Binding = new Binding("bid"),
                Header = "Business ID",
                Width = 0
            };

            tipGrid.Columns.Add(col0);
            tipGrid.Columns.Add(col1);
            tipGrid.Columns.Add(col2);
            tipGrid.Columns.Add(col3);
            tipGrid.Columns.Add(col4);

            DataGridTextColumn col5 = new DataGridTextColumn
            {
                Binding = new Binding("date"),
                Header = "Date",
                Width = 160
            };

            DataGridTextColumn col6 = new DataGridTextColumn
            {
                Binding = new Binding("username"),
                Header = "Username",
                Width = 100
            };

            DataGridTextColumn col7 = new DataGridTextColumn
            {
                Binding = new Binding("likes"),
                Header = "Likes",
                Width = 60
            };

            DataGridTextColumn col8 = new DataGridTextColumn
            {
                Binding = new Binding("content"),
                Header = "Content",
                Width = 360
            };

            DataGridTextColumn col9 = new DataGridTextColumn
            {
                Binding = new Binding("bid"),
                Header = "Business ID",
                Width = 0
            };

            friendsTipsGrid.Columns.Add(col5);
            friendsTipsGrid.Columns.Add(col6);
            friendsTipsGrid.Columns.Add(col7);
            friendsTipsGrid.Columns.Add(col8);
            friendsTipsGrid.Columns.Add(col9);
        }

        private string BuildConnectionString()
        {
            return "Host = localhost; Username = postgres; Database = yelpdb; Password = Shudokan2030";
        }

        private void AddFriendsTips()
        {
            friendsTipsGrid.Items.Clear();
            using (var connection = new NpgsqlConnection(BuildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT DISTINCT \"time\", fullname, likecount, content, bid FROM tip NATURAL JOIN \"user\" WHERE bid = '" + this.bid + "' AND userid IN (SELECT friendid FROM friends WHERE userid = '" + this.userid + "');";
                    try
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                            friendsTipsGrid.Items.Add(new Tip() { date = reader.GetDateTime(0).ToString(), username = reader.GetString(1), likes = reader.GetInt32(2).ToString(), content = reader.GetString(3), bid = reader.GetString(4) });
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
        }

        private void AddTip()
        {
            tipGrid.Items.Clear();
            using (var connection = new NpgsqlConnection(BuildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT DISTINCT \"time\", fullname, likecount, content, bid FROM tip NATURAL JOIN \"user\" WHERE bid = '" + this.bid + "';";
                    try
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                            tipGrid.Items.Add(new Tip() { date = reader.GetDateTime(0).ToString(), username = reader.GetString(1), likes = reader.GetInt32(2).ToString(), content = reader.GetString(3), bid = reader.GetString(4) });
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
        }

        private void NewTip()
        {
            using (var connection = new NpgsqlConnection(BuildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "INSERT INTO tip (time, content, likecount, bid, userid) VALUES ('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + newTip.Text + "', '" + 0 + "', '" + this.bid + "', '" + this.userid + "');";
                    try
                    {
                        cmd.ExecuteReader();
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
        }

        private void NewLike(string bid, string content, string date, string newLikeCount)
        {
            using (var connection = new NpgsqlConnection(BuildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "UPDATE tip SET likecount = '" + newLikeCount + "' WHERE bid = '" + bid + "' AND content = '" + content + "' AND \"time\" = '" + date + "';";
                    try
                    {
                        cmd.ExecuteReader();
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
        }

        private void ExecuteQuery(string sqlstr, Action<NpgsqlDataReader> myf)
        {
            using (var connection = new NpgsqlConnection(BuildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = sqlstr;
                    try
                    {
                        var reader = cmd.ExecuteReader();
                        reader.Read();
                        myf(reader);
                    }
                    catch (NpgsqlException ex)
                    {
                        Console.WriteLine(ex.Message.ToString());
                        System.Windows.MessageBox.Show("SQL Error - " + ex.Message.ToString());
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        private void SetBusinessDetails(NpgsqlDataReader R)
        {
            bname.Text = R.GetString(0);
            state.Text = R.GetString(1);
            city.Text = R.GetString(2);
        }

        void SetNumInState(NpgsqlDataReader R)
        {
            numInState.Content = R.GetInt16(0).ToString();
        }

        void SetNumInCity(NpgsqlDataReader R)
        {
            numInCity.Content = R.GetInt16(0).ToString();
        }

        void SetNumTips(NpgsqlDataReader R)
        {
            numTips.Content = R.GetInt32(0).ToString();
        }

        void SetNumCheckins(NpgsqlDataReader R)
        {
            numCheckins.Content = R.GetInt32(0).ToString();
        }

        void SetOpenTime(NpgsqlDataReader R)
        {
            try
            {
                openTime.Content = R.GetValue(0).ToString();
            }
            catch
            {
                openTime.Content = "Unknown";
            }
        }

        void SetCloseTime(NpgsqlDataReader R)
        {
            try
            {
                closeTime.Content = R.GetValue(0).ToString();
            }
            catch
            {
                closeTime.Content = "Unknown";
            }
        }

        private void AddGridRow(NpgsqlDataReader R)
        {
            tipGrid.Items.Add(new Tip() { content = R.GetString(0) });
        }

        private void AddCategory(NpgsqlDataReader R)
        {
            categoryList.Items.Add(R.GetString(0));
        }

        private void AddAttribute(NpgsqlDataReader R)
        {
            attributeList.Items.Add(R.GetString(0));
        }

        private void LoadBusinessNumbers()
        {
            UpdateNumInState();
            UpdateNumInCity();
            UpdateNumTips();
            UpdateNumCheckins();
            UpdateHoursOpen();
            UpdateCategoryList();
            UpdateAttributeList();
        }

        private void UpdateNumInState()
        {
            string sqlStr = "SELECT count(*) FROM business WHERE state = (SELECT state FROM business WHERE bid = '" + this.bid + "');";
            ExecuteQuery(sqlStr, SetNumInState);
        }

        private void UpdateNumInCity()
        {
            string sqlStr = "SELECT count(*) FROM business WHERE city = (SELECT city FROM business WHERE bid = '" + this.bid + "');";
            ExecuteQuery(sqlStr, SetNumInCity);
        }

        private void UpdateNumTips()
        {
            string sqlStr = "SELECT numtips FROM business WHERE bid = '" + this.bid + "';";
            ExecuteQuery(sqlStr, SetNumTips);
        }

        private void UpdateNumCheckins()
        {
            string sqlStr = "SELECT numcheckins FROM business WHERE bid = '" + this.bid + "';";
            ExecuteQuery(sqlStr, SetNumCheckins);
        }

        private void UpdateHoursOpen()
        {
            string today = DateTime.Now.DayOfWeek.ToString();
            dayOfWeek.Content = today + ":";
            string sqlStr = "SELECT opentime FROM business NATURAL JOIN hours WHERE bid = '" + this.bid + "' AND day = '" + today + "';";
            ExecuteQuery(sqlStr, SetOpenTime);
            sqlStr = "SELECT closetime FROM business NATURAL JOIN hours WHERE bid = '" + this.bid + "' AND day = '" + today + "';";
            ExecuteQuery(sqlStr, SetCloseTime);
        }

        private void UpdateCategoryList()
        {
            categoryList.Items.Clear();
            using (var connection = new NpgsqlConnection(BuildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT DISTINCT \"name\" FROM category WHERE bid = '" + this.bid + "';";
                    try
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                            categoryList.Items.Add(reader.GetString(0));
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
        }

        private void UpdateAttributeList()
        {
            attributeList.Items.Clear();
            using (var connection = new NpgsqlConnection(BuildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT DISTINCT \"name\" FROM \"attribute\" WHERE bid = '" + this.bid + "';";
                    try
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                            attributeList.Items.Add(reader.GetString(0));
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
        }

        private void LoadBusinessDetails()
        {
            string sqlStr = "SELECT name, state, city FROM business WHERE bid = '" + this.bid + "';";
            ExecuteQuery(sqlStr, SetBusinessDetails);
        }

        private void NewTipButton_Click(object sender, RoutedEventArgs e)
        {
            NewTip();
            AddTip();
            AddFriendsTips();
            LoadBusinessNumbers();
        }

        private void LikeTipButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (Tip tip in tipGrid.SelectedItems)
                NewLike(tip.bid, tip.content, tip.date, (int.Parse(tip.likes) + 1).ToString());

            AddTip();
            AddFriendsTips();
        }

        private void CheckInButton_Click(object sender, RoutedEventArgs e)
        {
            using (var connection = new NpgsqlConnection(BuildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "INSERT INTO checkin (time, bid) VALUES ('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + this.bid + "');";
                    try
                    {
                        cmd.ExecuteReader();
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

            UpdateNumCheckins();
        }

        private void ShowChartButton_Click(object sender, RoutedEventArgs e)
        {
            CheckinChart chart = new CheckinChart(bid);
            chart.Show();
        }

        private void ImagesButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.google.com/search?q=" + bname.Text + "+" + city.Text + "+" + state.Text + "&tbm=isch");
        }
    }
}
