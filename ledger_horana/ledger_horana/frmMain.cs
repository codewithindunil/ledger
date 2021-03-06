﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ledger_horana
{
    public partial class frmMain : Form
    {
        String user,privilages;
        public frmMain(String user,String privilages)
        {
            this.user=user;
            this.privilages = privilages;
            
            InitializeComponent();
            lblUser.Text = user.ToString();

        }

        public frmMain()
        {
            user = user;
            privilages = privilages;

        }

        private void bunifuTileButton1_Click(object sender, EventArgs e)
        {
            ledgerMain item = new ledgerMain();
            panel2.Controls.Clear();
            //item.Top = 10;
            //item.Left = 10;
            panel2.Controls.Add(item);
        }

        private void bunifuTileButton3_Click(object sender, EventArgs e)
        {

            if (privilages == "1")
            {
                addEditCategories item = new addEditCategories();
                panel2.Controls.Clear();
                //item.Top = 10;
                //item.Left = 10;
                panel2.Controls.Add(item);

            }
            else
            {
                MessageBox.Show("Sorry You Don't Have Admin access");
            }
            

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuTileButton2_Click(object sender, EventArgs e)
        {
            reportMain item = new reportMain();
            panel2.Controls.Clear();
            //item.Top = 10;
            //item.Left = 10;
            panel2.Controls.Add(item);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public String getUser()
        {
            user = lblUser.Text; 
            return user;
        }
        public String getPrivilages()
        {
           
            return privilages;
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            string message = "Do you want logout ?";
            string title = "logout";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                new frmLogin().Show();
                this.Close();
            }
            else
            {
                // Do something  
            }
        }

        private void lblUser_Click(object sender, EventArgs e)
        {
            viewDailyTotal();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            viewDailyTotal();
        }

        private void bunifuTileButton4_Click(object sender, EventArgs e)
        {
            if (privilages == "1")
            {
                new frmAdmin(user).Show();

            }
            else
            {
                MessageBox.Show("Sorry You Don't Have Admin access");
            }
        }
    

        public void viewDailyTotal()
        {
            var d = DateTime.Now;

            CultureInfo cul = CultureInfo.CurrentCulture;
            String serverName = new implementActions().serverName;
            String serveruser = new implementActions().serveruser;
            String serverPassword = new implementActions().serverPassword;
    
            int dateNum = cul.Calendar.GetDayOfYear(d);
            MySqlDataReader rd;
            string sMonth = DateTime.Now.ToString("MM");
            MySqlConnection conn;
            string connetionString = null;
            connetionString = "server='" + serverName + "';database=ledger_horanadb;uid='" + serveruser + "';pwd='" + serverPassword + "';";
            conn = new MySqlConnection(connetionString);
            String query;


            query = "select * from dailyTotal where dateNo='" + dateNum + "'";

            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(query, conn);
                rd = command.ExecuteReader();
                while (rd.Read())
                {
                    lblDebit.Text = rd["debitTotal"].ToString();
                    lblGeweem.Text= rd["creditTotal"].ToString();
                



                }
                conn.Close();



            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
