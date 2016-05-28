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
       User user;  
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
                MainWindow mainWindow = new MainWindow(conn, this.user);
                mainWindow.Show();
                this.Close();
            }
        }

        private Boolean validateUser(string user, string pass)
        {
            string sql = "SELECT * FROM users WHERE username='"+user+"' AND password='"+pass+"'";
            //string sql = "SELECT * FROM users";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader sqlReader = cmd.ExecuteReader();
            

            if (sqlReader.HasRows)
            {
                sqlReader.Read();
                this.user = new User((int)sqlReader.GetInt64(0));
                sqlReader.Close();
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

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                login(null, null);
            }
        }

    }
}
