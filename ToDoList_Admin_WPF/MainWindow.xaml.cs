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
using MySql.Data.MySqlClient;

namespace ToDoList_Admin_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private User user;
        private MySqlConnection conn;
        public MainWindow(MySqlConnection conn, User user)
        {
            InitializeComponent();
            this.conn = conn;
            this.user = user;

            assigneeComboBox.Items.Add(user);
        }
        public static long ToUnixTimestamp(DateTime target)
        {
            var date = new DateTime(1970, 1, 1, 0, 0, 0, target.Kind);
            var unixTimestamp = System.Convert.ToInt64((target - date).TotalSeconds);
            return unixTimestamp;
        }
        
        public int pickerToTimestamp(DatePicker picker)
        {
            //Requires ToUnixTimestamp(DateTime target)
            if (picker.SelectedDate.HasValue)
            {
                return (int)ToUnixTimestamp((DateTime)picker.SelectedDate);
            }
            else {
                return (int)ToUnixTimestamp(DateTime.Now) + (60*60*24); //Now plus one day
            }

        }
        public static DateTime ToDateTime(DateTime target, long timestamp)
        {
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, target.Kind);
            return dateTime.AddSeconds(timestamp);
        }

        string GetString(RichTextBox rtb)
        {
            var textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            return textRange.Text;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            string sql = "INSERT INTO `list` (`date`, `displayName`, `status`, `assignee`, `description`, `addedBy`, `dueDate`) " +
                "VALUES ("+ToUnixTimestamp(DateTime.Now)+", '" + displaynameTextBox.Text+"', 0, "+((User)assigneeComboBox.SelectedItem).getUserId()+", '" + GetString(descriptionRichTextBox).Replace("\r", "").Replace("\n", "") + "', " + user.getUserId() + ", "+pickerToTimestamp(dueDatePicker)+")";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Added: " + displaynameTextBox.Text, "Added");

        }
    }
}
