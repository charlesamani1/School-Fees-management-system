using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

namespace SFMS
{
    public class connection
        {
            public MySqlConnection con;
            private string server;
            private string database;
            private string uid;
            private string Passsword;
            public connection()
            {
                ConnectToDatabase();
            }
            private void ConnectToDatabase()//private so that it can only be accced within the class
            {
                server = "localhost";
                database = "sfms";
                uid = "root";
                Passsword = "root";
                string ConnectionString;
                ConnectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + Passsword + ";";
                con = new MySqlConnection(ConnectionString);

            }
            //open connection
            public bool OpenConnection()
            {
                try
                {
                    con.Open();
                    return true;
                }
                catch (MySqlException Ex)
                {
                    switch (Ex.Number)
                    {
                        case 0:
                            MessageBox.Show("Cannot connect to sever.contact administrator");
                            break;
                        case 1045:
                            MessageBox.Show("Invalid username/password, please try again");
                            break;

                    }
                    return false;
                }

            }
            // close database
            public bool CloseConnection()
            {
                try
                {
                    con.Close();
                    return true;
                }
                catch (MySqlException Ex)
                {
                    MessageBox.Show(Ex.Message);
                    return false;

                }
            }
        }
    }
