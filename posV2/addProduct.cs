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
    public partial class addProduct : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBconnection db = new DBconnection();
        Product product;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public addProduct(Product pro)
        {
            InitializeComponent();
            cn = new SqlConnection(db.Myconnection());
            product = pro;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void clear() {
            foreach (Control ctr in groupBox1.Controls)
            {
                if (ctr is TextBox)
                {
                    ctr.Text = "";
                }
                else if (ctr is CheckedListBox)
                {
                    CheckedListBox clb = (CheckedListBox)ctr;
                    foreach (int checkedItemIndex in clb.CheckedIndices)
                    {
                        clb.SetItemChecked(checkedItemIndex, false);
                    }
                }
                else if (ctr is CheckBox)
                {
                    ((CheckBox)ctr).Checked = false;
                }
                else if (ctr is ComboBox)
                {
                    ((ComboBox)ctr).SelectedIndex = 0;
                }
            }

        }
        public void setProduct(string productName,string price,string quantity,string barcode,string id) {
            this.productNameB.Text = productName;
            this.price.Text = price;
            this.qty.Text = quantity;
            this.barcode.Text = barcode;
            this.productId.Text = id;
        
        }
        public void setSubmitBtn(bool v,bool z) {
            this.submitBtn.Enabled = v;
            this.updateBtn.Enabled = z;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            try {
                cn.Open();
                cm = new SqlCommand("INSERT INTo product(name,qty,price,barcode)VALUES(@name,@qty,@price,@barcode)",cn);
                cm.Parameters.AddWithValue("@name",productNameB.Text);
                cm.Parameters.AddWithValue("@price", double.Parse(price.Text));
                cm.Parameters.AddWithValue("@qty", Int32.Parse(qty.Text));
                cm.Parameters.AddWithValue("@barcode",barcode.Text );
                cm.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Successfuly added","Successfully",MessageBoxButtons.OK);
                product.LoardRecords();
                clear();
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message.ToString());
                cn.Close();
                
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void addProduct_Load(object sender, EventArgs e)
        {

        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            try {
                cn.Open();
                cm = new SqlCommand("UPDATE product SET name=@name,qty=@qty,price=@price,barcode=@barcode WHERE id='"+this.productId.Text+"'",cn);
                cm.Parameters.AddWithValue("@name", this.productNameB.Text);
                cm.Parameters.AddWithValue("qty",Int32.Parse(this.qty.Text));
                cm.Parameters.AddWithValue("@price", double.Parse(price.Text));
                cm.Parameters.AddWithValue("@barcode", this.barcode.Text);
                cm.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Successfully updated","Successfully",MessageBoxButtons.OK);
                product.LoardRecords();
                clear();
                this.Dispose();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
