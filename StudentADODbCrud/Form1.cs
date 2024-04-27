using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace StudentADODbCrud
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        public Form1()
        {
            InitializeComponent();
            string constr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            con = new SqlConnection(constr);
        }

        private void ClearForm()
        {
            txtRollNum.Clear();
            txtName.Clear();
            txtClass.Clear();
            txtAddress.Clear();
            txtMobile.Clear(); 
            txtEmail.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "insert into students values(@name, @std, @address, @mobile, @email)";
                cmd = new SqlCommand(qry, con);

                cmd.Parameters.AddWithValue("@name",txtName.Text);
                cmd.Parameters.AddWithValue("@std",txtClass.Text);
                cmd.Parameters.AddWithValue("@address",txtAddress.Text);
                cmd.Parameters.AddWithValue("@mobile", txtMobile.Text);
                cmd.Parameters.AddWithValue("@email",txtEmail.Text);

                con.Open();

                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Student Details saved...");
                    ClearForm();
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from students where rollNum = @rollNum";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@rollNum",txtRollNum.Text);

                con.Open();
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtName.Text = dr["name"].ToString();
                        txtClass.Text = dr["std"].ToString();
                        txtAddress.Text = dr["address"].ToString();
                        txtMobile.Text = dr["mobile"].ToString();
                        txtEmail.Text = dr["email"].ToString();

                    }
                }
                else
                {
                    MessageBox.Show("Student Not Found..");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { con.Close(); }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "update students set name=@name, std=@std, address=@address, mobile=@mobile, email=@email where rollNum=@rollNum";
                cmd = new SqlCommand(qry, con);

                cmd.Parameters.AddWithValue("@rollNum", txtRollNum.Text);
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@std", txtClass.Text);
                cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@mobile", txtMobile.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);

                con.Open();

                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Student Update Successfully...");
                    ClearForm();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "delete from students where rollNum = @rollNum";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@rollNum", txtRollNum.Text);
                con.Open();

                int result = cmd.ExecuteNonQuery();
                if(result >= 1)
                {
                    MessageBox.Show("Student Record Deleted...");
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally
            {
                con.Close();
            }
        }

        private void btnList_Click(object sender, EventArgs e)
        {

            try
            {
                string qry = "select * from students";
                cmd = new SqlCommand(qry, con);
                con.Open();
                dr = cmd.ExecuteReader();

                DataTable table = new DataTable();
                table.Load(dr);

                dataGridView1.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
    }
}
