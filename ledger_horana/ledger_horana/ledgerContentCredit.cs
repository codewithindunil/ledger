﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Globalization;

namespace ledger_horana
{
    public partial class ledgerContentCredit : UserControl
    {
        public ledgerContentCredit()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
           
        }

        private void txtSubmit_Click(object sender, EventArgs e)
        {
            if(txtBmn.Text =="" && txtInvoiceNo.Text==""&& txtInvoiceValue.Text=="" && txtPaidAmount.Text=="" )
            {
                MessageBox.Show("Please fill all fields");
            }
            else
            {

            String category, subCategory, invoiceNo, cOrCheque, user;
            double invoiceValue, paidValue, igesheOld,igesheNew;
            int bnn;
            String invoiceDate;

            category = comboBox1.SelectedItem.ToString();
            subCategory = comboBox2.SelectedItem.ToString();
          
            invoiceNo = txtInvoiceNo.Text.ToString();
            cOrCheque = bunifuDropdown3.selectedValue.ToString();
            user = "test";

            invoiceValue = double.Parse(txtInvoiceValue.Text);
            paidValue = double.Parse(txtPaidAmount.Text);
            invoiceDate = dateTimePicker1.Value.ToString();
            var d = dateTimePicker1.Value.Date;

            CultureInfo cul = CultureInfo.CurrentCulture;
            int weekNum = cul.Calendar.GetWeekOfYear(
                 d,
                CalendarWeekRule.FirstDay,
                DayOfWeek.Monday);

            int dateNum = cul.Calendar.GetDayOfYear(d);
            bnn = int.Parse(txtBmn.Text);
             String ret= new implementActions().getCreditFinal();
            igesheOld = double.Parse(ret);
            //Console.WriteLine(ret);


            
                igesheNew = igesheOld + invoiceValue;
                bool status = new implementActions().addCredit(category, subCategory, invoiceNo, invoiceDate, invoiceValue, paidValue, cOrCheque, bnn, user, igesheNew,weekNum.ToString(),dateNum.ToString());
                if (status == true)
                {
                    MessageBox.Show("success");
                }
                else
                {
                    MessageBox.Show("failes");
                }



            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ledgerContentCredit_Load(object sender, EventArgs e)
        {
            loadCategories();
        }

        

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            loadSubCategories(comboBox1.SelectedItem.ToString());
        }






        public void loadSubCategories(String categ)
        {
            MySqlDataReader rd;
            String serverPassqord = new implementActions().serverPassword;
            String serverName = new implementActions().serverName;
            String serveruser = new implementActions().serveruser;
            MySqlConnection conn;
            string connetionString = null;
            connetionString = "server='" + serverName + "';database=ledger_horanadb;uid='" + serveruser + "';pwd='" + serverPassqord+"';";
            conn = new MySqlConnection(connetionString);
            String query;


            query = "SELECT * FROM ledger_horanadb.subcategories where cname = '" + categ+"'";
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(query, conn);
                rd = command.ExecuteReader();
                comboBox2.Items.Clear();
                while (rd.Read())
                {
                    comboBox2.Items.Add(rd.GetString("sname").ToUpper());

                    //Console.WriteLine(rd.GetString("sname"));
                }
                comboBox2.SelectedIndex = 0;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void loadCategories()
        {
            MySqlDataReader rd;
            String serverPassqord = new implementActions().serverPassword;
            String serverName = new implementActions().serverName;
            String serveruser = new implementActions().serveruser;

            MySqlConnection conn;
            string connetionString = null;
            connetionString = "server='" + serverName + "';database=ledger_horanadb;uid='" + serveruser + "';pwd='" + serverPassqord+"';";
            conn = new MySqlConnection(connetionString);
            String query;


            query = "SELECT * FROM ledger_horanadb.categories";
            try
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand(query, conn);
                rd = command.ExecuteReader();
                while (rd.Read())
                {
                    comboBox1.Items.Add(rd.GetString("cname").ToUpper());

                    //Console.WriteLine(rd.GetString("cname"));
                }
                comboBox1.SelectedIndex = 0;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtBmn.Text = "";
            txtInvoiceNo.Text = "";
            txtInvoiceValue.Text = "";
            txtPaidAmount.Text = "";
            
        }

        private void bunifuDropdown3_onItemSelected(object sender, EventArgs e)
        {
            if (bunifuDropdown3.selectedValue.ToString() == "CHEQUE")
            {
                new frmCheque(txtInvoiceValue.Text, txtInvoiceNo.Text,"CREDIT").Show();
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnVoucher_Click(object sender, EventArgs e)
        {
            new frmVoucherPrint("","","","","","").Show();
        }
    }
}
