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

namespace posV2
{
    public partial class Dashboard : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBconnection db = new DBconnection();
        public Dashboard()
        {
            InitializeComponent();
            try {
                cn = new SqlConnection(db.Myconnection());
                cn.Open();
                dbConnection.Text = "Connected";
                cn.Close();


            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message.ToString());
            }
            
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void product_Click(object sender, EventArgs e)
        {
            Product proFrm = new Product();
            //proFrm.TopMost = true;
            proFrm.TopLevel = false;
          
            proFrm.Dock = DockStyle.Fill;
           
            panel2.Controls.Add(proFrm);
            proFrm.BringToFront();
            proFrm.Show();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
