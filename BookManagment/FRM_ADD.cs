using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace BookManagment
{
    public partial class FRM_ADD : Form

    {
        // var for con sql
        SqlConnection con=new SqlConnection();
        SqlCommand cmd=new SqlCommand();
        List<String> List=new List<string>();
        public int state;

        public FRM_ADD()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
           this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form frm_cat=new FRM_CAT();
            bunifuTransition1.ShowSync(frm_cat);
        }

        private void   FRM_ADD_Load(object sender,EventArgs e)
        {
            try
            {
                con.ConnectionString = (@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ryadh\Desktop\Projects\U\BookManagment\BookManagment\DBBOOK.mdf;Integrated Security=TrueData Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ryadh\Desktop\Projects\U\BookManagment\BookManagment\DBBOOK.mdf;Integrated Security=True");
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "SELECT CAT FROM TBCAT";
                var rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    List.Add(Convert.ToString(rd[0]));
                }
                int i = 0;
                while (i < List.LongCount())
                {
                    txt_cat.Items.Add(List[i]);
                    i = i + 1;
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

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {

            if (txt_auther.Text == "" || txt_name.Text == "")
            {
                MessageBox.Show("إكمل معلومات الكتاب اولا ");
            }
            else
            {
                if (state == 0)
                {
                    //Insert
                    MemoryStream ma = new MemoryStream();
                    cover.Image.Save(ma, System.Drawing.Imaging.ImageFormat.Jpeg);
                    var _cover = ma.ToArray();

                    con.ConnectionString = (@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ryadh\Desktop\Projects\U\BookManagment\BookManagment\DBBOOK.mdf;Integrated Security=TrueData Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ryadh\Desktop\Projects\U\BookManagment\BookManagment\DBBOOK.mdf;Integrated Security=True");
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "INSERT INTO BOOKS (TITEL,AUTHER,PRICE,CAT,DATE,RATE,COVER) VALUES (@TITEL,@AUTHER,@PRICE,@CAT,@DATE,@RATE,@COVER)";
                    cmd.Parameters.AddWithValue("@TITEL", txt_name.Text);
                    cmd.Parameters.AddWithValue("@AUTHER", txt_auther.Text);
                    cmd.Parameters.AddWithValue("@PRICE", txt_price.Text);
                    cmd.Parameters.AddWithValue("@CAT", txt_cat.Text);
                    cmd.Parameters.AddWithValue("@DATE", txt_date.Value);
                    cmd.Parameters.AddWithValue("@RATE", txt_rate.Value);
                    cmd.Parameters.AddWithValue("@COVER", _cover);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Form frm_add = new FRM_DIADD();
                    frm_add.Show();
                    this.Close();
                }
                else
                {
                    //Edit
                    MemoryStream ma = new MemoryStream();
                    cover.Image.Save(ma, System.Drawing.Imaging.ImageFormat.Jpeg);
                    var _cover = ma.ToArray();

                    con.ConnectionString = (@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ryadh\Desktop\Projects\U\BookManagment\BookManagment\DBBOOK.mdf;Integrated Security=TrueData Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ryadh\Desktop\Projects\U\BookManagment\BookManagment\DBBOOK.mdf;Integrated Security=True");
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "UPDATE BOOKS SET TITEL=@TITEL,AUTHER=@AUTHER,PRICE=@PRICE,CAT=@CAT,DATE=@DATE,RATE=@RATE,COVER=@COVER WHERE ID=@ID";
                    cmd.Parameters.AddWithValue("@TITEL", txt_name.Text);
                    cmd.Parameters.AddWithValue("@AUTHER", txt_auther.Text);
                    cmd.Parameters.AddWithValue("@PRICE", txt_price.Text);
                    cmd.Parameters.AddWithValue("@CAT", txt_cat.Text);
                    cmd.Parameters.AddWithValue("@DATE", txt_date.Value);
                    cmd.Parameters.AddWithValue("@RATE", txt_rate.Value);
                    cmd.Parameters.AddWithValue("@COVER", _cover);
                    cmd.Parameters.AddWithValue("@ID", state);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Form frm_edit = new FRM_DIEDIT();
                    frm_edit.Show();
                    this.Close();
                }
        

            }
            cmd.Parameters.Clear();

        
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var dia=new OpenFileDialog();
            var result= dia.ShowDialog();
            if(result == DialogResult.OK)
            {
                cover.Image = Image.FromFile(dia.FileName);
            }
        }
    }
}
