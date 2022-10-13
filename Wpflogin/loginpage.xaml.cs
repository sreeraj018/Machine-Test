using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Navigation;


namespace Wpflogin
{
    /// <summary>
    /// Interaction logic for loginpage.xaml
    /// </summary>
    public partial class loginpage : Window
    {
        public loginpage()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {

            SqlConnection con = new SqlConnection(@"Data Source = DESKTOP-U5U50Q0\SQLEXPRESS; Initial Catalog=logindb; Integrated Security=True;");


            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string s = "select count(Userid) from tbluser where Username =@Username and password=@password";
                SqlCommand cmd = new SqlCommand(s, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@Password", txtPassword.Password);
                int cid = Convert.ToInt32(cmd.ExecuteScalar());
                if(cid == 1)
                {
                    SqlConnection con1 = new SqlConnection(@"Data Source=DESKTOP-U5U50Q0\SQLEXPRESS; initial catalog=logindb; Integrated Security=True");
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("select userename from tbluser order by  asc", con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd1);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        MainWindow mw = new MainWindow();
                        mw.Show();
                        this.Close();
                             
                    }

                    con.Close();
                }
                else
                {
                    MessageBox.Show("INVALID USERNAME OR PASSWORD");
                }
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }




        }
    }
}
