using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace posV2
{
    public partial class Product : Form

    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBconnection db = new DBconnection();
        public Product()
        {
            InitializeComponent();
            cn = new SqlConnection(db.Myconnection());
            LoardRecords();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            addProduct product = new addProduct(this);
            product.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //dd pag imud kun nanu an gn click sa user
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                addProduct pr = new addProduct(this);
                pr.setProduct(dataGridView1[3, e.RowIndex].Value.ToString(), dataGridView1[4, e.RowIndex].Value.ToString(), dataGridView1[5, e.RowIndex].Value.ToString(), dataGridView1[2, e.RowIndex].Value.ToString(), dataGridView1[1, e.RowIndex].Value.ToString());
                pr.setSubmitBtn(false, true);
                pr.ShowDialog();
            }
            else if (colName == "Delete") {
                if (MessageBox.Show("Are u sure u want to Delete?", "DELETE", MessageBoxButtons.YesNo) == DialogResult.Yes) {

                    cn.Open();
                    cm = new SqlCommand("DELETE from product WHERE id ='" + dataGridView1[1, e.RowIndex].Value.ToString() + "'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Successfully Deleted", "Deleted", MessageBoxButtons.OK);
                    LoardRecords();
                }

                try { 
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void LoardRecords()
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("select * from product order by name", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                decimal _rating = 0;
                decimal.TryParse(dr["price"].ToString(), out _rating);
                i += 1;
                dataGridView1.Rows.Add(i, dr["id"].ToString(),dr["barcode"].ToString(), dr["name"].ToString(),_rating.ToString("F"),dr["qty"].ToString());

            }
            dr.Close();
            cn.Close();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
