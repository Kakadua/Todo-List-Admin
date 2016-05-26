using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
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

namespace ToDoList_Admin_WPF
{

    //TODO, känn av enter knappen för inloggning
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {

       MySqlConnection conn;
        public LoginScreen()
        {
            InitializeComponent();
            string myConnectionString;

            myConnectionString = "server=127.0.0.1;uid=test;" +
                "pwd=password;database=todolist;";

            try
            {
                conn = new MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }
        private void login(object sender, RoutedEventArgs e)
        {
            if(validateUser(usernameTextBox.Text, passwordBox.Password))
            {
                MainWindow mainWindow = new MainWindow(conn);
                mainWindow.Show();
            }
        }

        private Boolean validateUser(string user, string pass)
        {
            string sql = "SELECT * FROM users WHERE username='"+user+"' AND password='"+pass+"'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            int numUsers = Convert.ToInt32(cmd.ExecuteScalar());
            if(numUsers> 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void usernameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
