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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Npgsql;

namespace TermProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class Business
        {
            public string bid { get; set; }
            public string name { get; set; }
            public string state { get; set; }
            public string city { get; set; }
            public string address { get; set; }
            public string stars { get; set; }
            public string distance { get; set; }
            public string numTips { get; set; }
            public string totalCheckins { get; set; }
        }

        public class Friend
        {
            public string fullname { get; set; }
            public string totallikes { get; set; }
            public string avgstars { get; set; }
            public string yelpingsince { get; set; }
        }

        public class FriendTip
        {
            public string username { get; set; }
            public string business { get; set; }
            public string city { get; set; }
            public string text { get; set; }
            public string date { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();
            AddState();
            AddColumnsToGrid();
        }

        private string BuildConnectionString()
        {
            return "Host = localhost; Username = postgres; Database = yelpdb; Password = Shudokan2030";
        }

        private void AddState()
        {
            using (var connection = new NpgsqlConnection(BuildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT distinct state FROM business ORDER BY state";
                    try
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                            stateList.Items.Add(reader.GetString(0));
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
                        while (reader.Read())
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

        private void AddColumnsToGrid()
        {
            DataGridTextColumn col1 = new DataGridTextColumn();
            col1.Binding = new Binding("name");
            col1.Header = "Business Name";
            col1.Width = 100;
            businessGrid.Columns.Add(col1);

            DataGridTextColumn col2 = new DataGridTextColumn();
            col2.Binding = new Binding("state");
            col2.Header = "State";
            col2.Width = 30;
            businessGrid.Columns.Add(col2);

            DataGridTextColumn col3 = new DataGridTextColumn();
            col3.Binding = new Binding("city");
            col3.Header = "City";
            col3.Width = 80;
            businessGrid.Columns.Add(col3);

            DataGridTextColumn col4 = new DataGridTextColumn();
            col4.Binding = new Binding("bid");
            col4.Header = "Business ID";
            col4.Width = 50;
            businessGrid.Columns.Add(col4);

            DataGridTextColumn col5 = new DataGridTextColumn();
            col5.Binding = new Binding("address");
            col5.Header = "Address";
            col5.Width = 50;
            businessGrid.Columns.Add(col5);

            DataGridTextColumn col6 = new DataGridTextColumn();
            col6.Binding = new Binding("stars");
            col6.Header = "Stars";
            col6.Width = 30;
            businessGrid.Columns.Add(col6);

            DataGridTextColumn col7 = new DataGridTextColumn();
            col7.Binding = new Binding("distance");
            col7.Header = "Distance (miles)";
            col7.Width = 30;
            businessGrid.Columns.Add(col7);

            DataGridTextColumn col8 = new DataGridTextColumn();
            col8.Binding = new Binding("numTips");
            col8.Header = "# Tips";
            col8.Width = 30;
            businessGrid.Columns.Add(col8);

            DataGridTextColumn col9 = new DataGridTextColumn();
            col9.Binding = new Binding("totalCheckins");
            col9.Header = "Check-ins";
            col9.Width = 30;
            businessGrid.Columns.Add(col9);

            DataGridTextColumn nameCol = new DataGridTextColumn();
            nameCol.Binding = new Binding("fullname");
            nameCol.Header = "Name";
            nameCol.Width = 91;
            friendsDataGrid.Columns.Add(nameCol);

            DataGridTextColumn totalLikesCol = new DataGridTextColumn();
            totalLikesCol.Binding = new Binding("totallikes");
            totalLikesCol.Header = "Total Likes";
            totalLikesCol.Width = 50;
            friendsDataGrid.Columns.Add(totalLikesCol);

            DataGridTextColumn avgStarsCol = new DataGridTextColumn();
            avgStarsCol.Binding = new Binding("avgstars");
            avgStarsCol.Header = "Average Stars";
            avgStarsCol.Width = 50;
            friendsDataGrid.Columns.Add(avgStarsCol);

            DataGridTextColumn yelpingSinceCol = new DataGridTextColumn();
            yelpingSinceCol.Binding = new Binding("yelpingsince");
            yelpingSinceCol.Header = "Yelping Since";
            yelpingSinceCol.Width = 173;
            friendsDataGrid.Columns.Add(yelpingSinceCol);

            DataGridTextColumn usernameCol = new DataGridTextColumn();
            usernameCol.Binding = new Binding("username");
            usernameCol.Header = "Username";
            usernameCol.Width = 81;
            friendsTipsDataGrid.Columns.Add(usernameCol);

            DataGridTextColumn businessCol = new DataGridTextColumn();
            businessCol.Binding = new Binding("business");
            businessCol.Header = "Business";
            businessCol.Width = 81;
            friendsTipsDataGrid.Columns.Add(businessCol);

            DataGridTextColumn cityCol = new DataGridTextColumn();
            cityCol.Binding = new Binding("city");
            cityCol.Header = "City";
            cityCol.Width = 81;
            friendsTipsDataGrid.Columns.Add(cityCol);

            DataGridTextColumn textCol = new DataGridTextColumn();
            textCol.Binding = new Binding("text");
            textCol.Header = "Text";
            textCol.Width = 81;
            friendsTipsDataGrid.Columns.Add(textCol);

            DataGridTextColumn dateCol = new DataGridTextColumn();
            dateCol.Binding = new Binding("date");
            dateCol.Header = "Date";
            dateCol.Width = 81;
            friendsTipsDataGrid.Columns.Add(dateCol);

            sortByList.Items.Add("Business Name");
            sortByList.Items.Add("Highest Rating");
            sortByList.Items.Add("Number of Tips");
            sortByList.Items.Add("Most Checkins");
            sortByList.Items.Add("Nearest");
        }

        private void AddCity(NpgsqlDataReader R)
        {
            cityList.Items.Add(R.GetString(0));
        }

        private void AddZip(NpgsqlDataReader R)
        {
            zipList.Items.Add(R.GetInt32(0));
        }

        private void AddCategory(NpgsqlDataReader R)
        {
            categoryList.Items.Add(R.GetString(0));
        }

        private void AddSubAttribute(NpgsqlDataReader R)
        {
            CheckBox cb = new CheckBox() { Content = R.GetString(0) };
            cb.Click += new RoutedEventHandler(Checkbox_Click);
            attributeList.Items.Add(cb);
        }

        public void Checkbox_Click(object sender, RoutedEventArgs e)
        {
            businessGrid.Items.Clear();
            UpdateGridRows();
        }

        private void AddGridRow(NpgsqlDataReader R)
        {
            businessGrid.Items.Add(new Business() { name = R.GetString(0), state = R.GetString(1), city = R.GetString(2), bid = R.GetString(3), address = R.GetString(4), stars = R.GetDouble(5).ToString(), distance = Math.Round((R.GetDouble(6) / 1.609), 2).ToString(), numTips = R.GetInt32(7).ToString(), totalCheckins = R.GetInt32(8).ToString() });
        }

        private void AddFriendRow(NpgsqlDataReader R)
        {
            friendsDataGrid.Items.Add(new Friend() { fullname = R.GetString(0), totallikes = R.GetInt32(1).ToString(), avgstars = R.GetDouble(2).ToString(), yelpingsince = R.GetDateTime(3).ToString() });
        }

        private void AddFriendTipRow(NpgsqlDataReader R)
        {
            friendsTipsDataGrid.Items.Add(new FriendTip() { username = R.GetString(0), business = R.GetString(1), city = R.GetString(2), text = R.GetString(3), date = R.GetDateTime(4).ToString() });
        }

        private void AddUserID(NpgsqlDataReader R)
        {
            userIDListBox.Items.Add(R.GetString(0));
        }

        private void AddFullName(NpgsqlDataReader R)
        {
            string fullname = R.GetString(0);
            nameTextBox.Text = char.ToUpper(fullname[0]) + fullname.Substring(1);
        }

        private void AddStars(NpgsqlDataReader R)
        {
            starsTextBox.Text = R.GetDouble(0).ToString();
        }

        private void AddFans(NpgsqlDataReader R)
        {
            fansTextBox.Text = R.GetInt32(0).ToString();
        }
        
        private void AddYelpingSince(NpgsqlDataReader R)
        {
            yelpingSinceTextBox.Text = R.GetDateTime(0).ToString();
        }

        private void AddFunny(NpgsqlDataReader R)
        {
            funnyTextBox.Text = R.GetInt32(0).ToString();
        }

        private void AddCool(NpgsqlDataReader R)
        {
            coolTextBox.Text = R.GetInt32(0).ToString();
        }

        private void AddUseful(NpgsqlDataReader R)
        {
            usefulTextBox.Text = R.GetInt32(0).ToString();
        }

        private void AddTips(NpgsqlDataReader R)
        {
            tipsTextBox.Text = R.GetInt32(0).ToString();
        }

        private void AddTipLikes(NpgsqlDataReader R)
        {
            tipLikesTextBox.Text = R.GetInt32(0).ToString();
        }

        private void AddLatitude(NpgsqlDataReader R)
        {
            if (!R.IsDBNull(0))
                latTextBox.Text = R.GetDouble(0).ToString();
        }

        private void AddLongitude(NpgsqlDataReader R)
        {
            if (!R.IsDBNull(0))
                longTextBox.Text = R.GetDouble(0).ToString();
        }

        private void StateList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cityList.Items.Clear();
            if (stateList.SelectedIndex > -1)
            {
                string sqlStr = "SELECT distinct city FROM business WHERE state = '" + stateList.SelectedItem.ToString() + "' ORDER BY city";
                ExecuteQuery(sqlStr, AddCity);
            }
        }

        private void CityList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            zipList.Items.Clear();
            if (cityList.SelectedIndex > -1)
            {
                string sqlStr = "SELECT distinct zip FROM business WHERE state = '" + stateList.SelectedItem.ToString() + "' AND city = '" + cityList.SelectedItem.ToString() + "' ORDER BY zip";
                ExecuteQuery(sqlStr, AddZip);
            }
        }

        private List<string> GetSuperAttributes()
        {
            List<string> superattributes = new List<string>();
            string sqlStr = "SELECT DISTINCT \"name\" FROM superattribute ORDER BY \"name\";";
            using (var connection = new NpgsqlConnection(BuildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = sqlStr;
                    try
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                            superattributes.Add(reader.GetString(0));
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

            return superattributes;
        }

        private void ZipList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            categoryList.Items.Clear();
            attributeList.Items.Clear();
            if (zipList.SelectedIndex > -1)
            {
                string sqlStr = "SELECT DISTINCT name FROM category WHERE bid IN (SELECT DISTINCT bid from business WHERE zip = '" + zipList.SelectedItem.ToString() + "') ORDER BY name;";
                ExecuteQuery(sqlStr, AddCategory);

                sqlStr = "SELECT DISTINCT \"name\" FROM \"attribute\" WHERE \"name\" NOT IN (SELECT DISTINCT \"name\" FROM superattribute) AND \"name\" NOT IN (SELECT DISTINCT \"attributename\" FROM superattribute) AND \"value\" IN ('True', 'False') ORDER BY \"name\";";
                ExecuteQuery(sqlStr, AddSubAttribute);

                List<string> superattributes = GetSuperAttributes();
                foreach (string superattribute in superattributes)
                {
                    attributeList.Items.Add(superattribute.ToUpper());
                    sqlStr = "SELECT DISTINCT \"attributename\" FROM superattribute WHERE \"name\" = '" + superattribute + "' AND \"attributename\" IN (SELECT \"name\" FROM \"attribute\" WHERE \"value\" IN ('True', 'False')) ORDER BY \"attributename\";";
                    ExecuteQuery(sqlStr, AddSubAttribute);
                }
            }

            UpdateGridRows();
        }

        private void CategoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            businessGrid.Items.Clear();
            UpdateGridRows();
        }

        private void UpdateFriendTipRows()
        {
            string sqlStr = "SELECT DISTINCT friendid FROM friends WHERE userid = '" + userIDListBox.SelectedItem.ToString() + "';";
            List<string> friendList = new List<string>();
            using (var connection = new NpgsqlConnection(BuildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = sqlStr;
                    try
                    {
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                            friendList.Add(reader.GetString(0));
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

            foreach (string friendid in friendList)
            {
                sqlStr = "SELECT fullname, \"name\", city, \"content\", \"time\" FROM \"user\", tip, business WHERE \"user\".userid = tip.userid AND tip.bid = business.bid AND \"user\".userid = '" + friendid + "' ORDER BY \"time\" LIMIT 1";
                ExecuteQuery(sqlStr, AddFriendTipRow);
            }
        }

        private void UpdateGridRows()
        {
            try
            {
                string userid;
                if (userIDListBox.SelectedItem != null)
                    userid = userIDListBox.SelectedItem.ToString();
                else
                    userid = "Lz0U-zqdPGV73wru05-W5Q";

                businessGrid.Items.Clear();
                string sqlStr;
                if (latTextBox.Text == "" || longTextBox.Text == "")
                    sqlStr = "SELECT name, state, city, bid, street, rating, public.geodistance(latitude, longitude, latitude, longitude) AS distance, numtips, numcheckins FROM business WHERE state = '" + stateList.SelectedItem.ToString() + "' AND city = '" + cityList.SelectedItem.ToString() + "' ";
                else
                    sqlStr = "SELECT name, state, city, bid, street, rating, public.geodistance((SELECT latitude FROM \"user\" WHERE userid = '" + userid + "'), (SELECT longitude FROM \"user\" WHERE userid = '" + userid + "'), latitude, longitude) AS distance, numtips, numcheckins FROM business WHERE state = '" + stateList.SelectedItem.ToString() + "' AND city = '" + cityList.SelectedItem.ToString() + "' ";

                foreach (object s in categoryList.SelectedItems)
                    sqlStr += "AND bid IN (SELECT bid FROM category WHERE name = '" + s.ToString() + "') ";

                foreach (object o in attributeList.Items)
                    if (o.GetType().Name == "CheckBox")
                        if ((bool)((CheckBox)o).IsChecked)
                            sqlStr += "AND bid IN (SELECT bid FROM \"attribute\" WHERE \"name\" = '" + ((CheckBox)o).Content.ToString() + "' AND \"value\" = 'True') ";

                if ((bool)price1.IsChecked)
                    sqlStr += "AND bid IN (SELECT bid FROM \"attribute\" WHERE \"name\" = 'RestaurantsPriceRange2' AND \"value\" = '1') ";

                if ((bool)price2.IsChecked)
                    sqlStr += "AND bid IN (SELECT bid FROM \"attribute\" WHERE \"name\" = 'RestaurantsPriceRange2' AND \"value\" = '2') ";

                if ((bool)price3.IsChecked)
                    sqlStr += "AND bid IN (SELECT bid FROM \"attribute\" WHERE \"name\" = 'RestaurantsPriceRange2' AND \"value\" = '3') ";

                if ((bool)price4.IsChecked)
                    sqlStr += "AND bid IN (SELECT bid FROM \"attribute\" WHERE \"name\" = 'RestaurantsPriceRange2' AND \"value\" = '4') ";

                if (sortByList.SelectedIndex > 0)
                {
                    if (sortByList.SelectedIndex == 1)
                    {
                        sqlStr += "ORDER BY rating DESC";
                    }
                    else if (sortByList.SelectedIndex == 2)
                    {
                        sqlStr += "ORDER BY numtips DESC";
                    }
                    else if (sortByList.SelectedIndex == 3)
                    {
                        sqlStr += "ORDER BY numcheckins DESC";
                    }
                    else
                    {
                        sqlStr += "ORDER BY public.geodistance((SELECT latitude FROM \"user\" WHERE userid = '" + userid + "'), (SELECT longitude FROM \"user\" WHERE userid = '" + userid + "'), latitude, longitude)";
                    }
                }
                else
                {
                    sqlStr += "ORDER BY \"name\"";
                }

                ExecuteQuery(sqlStr, AddGridRow);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("Cannot update grid rows.");
            }
        }

        private void BusinessGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (businessGrid.SelectedIndex > -1)
            {
                Business B = businessGrid.Items[businessGrid.SelectedIndex] as Business;
                if ((B.bid != null) && (B.bid.ToString().CompareTo("") != 0))
                {
                    BusinessDetails businessWindow;
                    if (userIDListBox.SelectedIndex > -1)
                    {
                        businessWindow = new BusinessDetails(B.bid.ToString(), userIDListBox.SelectedItem.ToString());
                    }
                    else
                    {
                        businessWindow = new BusinessDetails(B.bid.ToString());
                    }

                    businessWindow.Show();
                }
            }
        }

        private void UserTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (userIDListBox != null)
                userIDListBox.Items.Clear();
            string user = userTextBox.Text;
            string sqlStr = "SELECT distinct userid FROM \"user\" WHERE fullname LIKE '" + userTextBox.Text + "%' ORDER BY userid";
            ExecuteQuery(sqlStr, AddUserID);
        }

        private void ClearUserInfo()
        {
            nameTextBox.Clear();
            fansTextBox.Clear();
            starsTextBox.Clear();
            yelpingSinceTextBox.Clear();
            funnyTextBox.Clear();
            coolTextBox.Clear();
            usefulTextBox.Clear();
            latTextBox.Clear();
            longTextBox.Clear();
            friendsDataGrid.Items.Clear();
            friendsTipsDataGrid.Items.Clear();
        }

        private void UserIDListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearUserInfo();
            if (userIDListBox.SelectedItem != null)
            {
                string userid = userIDListBox.SelectedItem.ToString();

                string sqlStr = "SELECT distinct fullname FROM \"user\" WHERE userid = '" + userid + "';";
                ExecuteQuery(sqlStr, AddFullName);

                sqlStr = "SELECT distinct averagestars FROM \"user\" WHERE userid = '" + userid + "';";
                ExecuteQuery(sqlStr, AddStars);

                sqlStr = "SELECT distinct fancount FROM \"user\" WHERE userid = '" + userid + "';";
                ExecuteQuery(sqlStr, AddFans);

                sqlStr = "SELECT distinct joindate FROM \"user\" WHERE userid = '" + userid + "';";
                ExecuteQuery(sqlStr, AddYelpingSince);

                sqlStr = "SELECT distinct funny FROM \"user\" WHERE userid = '" + userid + "';";
                ExecuteQuery(sqlStr, AddFunny);

                sqlStr = "SELECT distinct cool FROM \"user\" WHERE userid = '" + userid + "';";
                ExecuteQuery(sqlStr, AddCool);

                sqlStr = "SELECT distinct useful FROM \"user\" WHERE userid = '" + userid + "';";
                ExecuteQuery(sqlStr, AddUseful);

                sqlStr = "SELECT distinct tipcount FROM \"user\" WHERE userid = '" + userid + "';";
                ExecuteQuery(sqlStr, AddTips);

                sqlStr = "SELECT distinct totallikes FROM \"user\" WHERE userid = '" + userid + "';";
                ExecuteQuery(sqlStr, AddTipLikes);

                sqlStr = "SELECT distinct latitude FROM \"user\" WHERE userid = '" + userid + "';";
                ExecuteQuery(sqlStr, AddLatitude);

                sqlStr = "SELECT distinct longitude FROM \"user\" WHERE userid = '" + userid + "';";
                ExecuteQuery(sqlStr, AddLongitude);

                sqlStr = "SELECT DISTINCT fullname, totallikes, averagestars, joindate FROM \"user\" WHERE userid IN (SELECT DISTINCT friendid FROM friends WHERE userid = '" + userIDListBox.SelectedItem.ToString() + "') ORDER BY fullname;";
                ExecuteQuery(sqlStr, AddFriendRow);

                UpdateFriendTipRows();
            }
        }

        private void UpdateLocation()
        {
            using (var connection = new NpgsqlConnection(BuildConnectionString()))
            {
                connection.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = "UPDATE \"user\" SET latitude = '" + latTextBox.Text + "', longitude = '" + longTextBox.Text + "' WHERE userid = '" + userIDListBox.SelectedItem.ToString() + "';";
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

        private void UpdateLocationButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateLocation();
            latTextBox.IsEnabled = false;
            longTextBox.IsEnabled = false;
        }

        private void EditLocationButton_Click(object sender, RoutedEventArgs e)
        {
            latTextBox.IsEnabled = true;
            longTextBox.IsEnabled = true;
        }

        private void Price1_Checked(object sender, RoutedEventArgs e)
        {
            price2.IsChecked = false;
            price3.IsChecked = false;
            price4.IsChecked = false;
            UpdateBusinessGrid();
        }

        private void Price2_Checked(object sender, RoutedEventArgs e)
        {
            price1.IsChecked = false;
            price3.IsChecked = false;
            price4.IsChecked = false;
            UpdateBusinessGrid();
        }

        private void Price3_Checked(object sender, RoutedEventArgs e)
        {
            price1.IsChecked = false;
            price2.IsChecked = false;
            price4.IsChecked = false;
            UpdateBusinessGrid();
        }

        private void Price4_Checked(object sender, RoutedEventArgs e)
        {
            price1.IsChecked = false;
            price2.IsChecked = false;
            price3.IsChecked = false;
            UpdateBusinessGrid();
        }

        private void Price1_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateBusinessGrid();
        }

        private void Price2_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateBusinessGrid();
        }

        private void Price3_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateBusinessGrid();
        }

        private void Price4_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateBusinessGrid();
        }

        private void UpdateBusinessGrid()
        {
            businessGrid.Items.Clear();
            UpdateGridRows();
        }

        private void SortByList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateBusinessGrid();
        }
    }
}
