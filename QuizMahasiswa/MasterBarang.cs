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

namespace QuizMahasiswa
{
    public partial class MasterBarang : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-MHR7TR8T;Initial Catalog=QuizMahasiswa;Integrated Security=True;");
        public MasterBarang()
        {
            InitializeComponent();
        }
        DataClasses1DataContext db = new DataClasses1DataContext();
        private void btnSave_Click(object sender, EventArgs e)
        {

            int id = int.Parse(txtID.Text);
            string namabarang = txtNamaBarang.Text;
            int harga = int.Parse(txtHarga.Text);
            int stok = int.Parse(txtStock.Text);
            string supplier = cbSupplier.Text;
            DateTime expiredDate = DateTime.Parse(dateExpired.Text);

            var data = new tbl_barang
            {
                id_barang = id,
                nama_barang = namabarang,
                harga = harga,
                stok = stok,
                nama_supplier = supplier,
         
            };

            db.tbl_barangs.InsertOnSubmit(data);
            db.SubmitChanges();
            MessageBox.Show("Save Succesfully");
            txtHarga.Clear();
            txtNamaBarang.Clear();
            txtStock.Clear();
            cbSupplier.Items.Clear();
            LoadData();
        }

        void LoadData()
        {
            var st = from tb in db.tbl_barangs select tb;
            dataGridView1.DataSource = st;
        }

        private void MasterBarang_Load(object sender, EventArgs e)
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select isnull(max (cast (ID as int)),0) + 1 from tbl_barang", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            txtID.Text = dt.Rows[0][0].ToString();
            LoadData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string namabarang = txtNamaBarang.Text;
            int harga = int.Parse(txtHarga.Text);
            int stok = int.Parse(txtStock.Text);
            string supplier = cbSupplier.Text;
            DateTime expiredDate = DateTime.Parse(dateExpired.Text);

            var st = (from s in db.tbl_barangs where s.id_barang == int.Parse(txtID.Text) select s).First();
            st.nama_barang = namabarang;
            st.harga = harga;
            st.stok = stok;
            st.nama_supplier = supplier;
            db.SubmitChanges();

            MessageBox.Show("Update Succesfuly");
            txtStock.Clear();
            txtHarga.Clear();
            cbSupplier.Items.Clear();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var st = from s in db.tbl_barangs where s.id_barang == txtIdBarangCari.Text || s.nama_barang == txtNamaBarang.Text select s;
            dataGridView1.DataSource = st;
        }

        /* private void btnDelete_Click(object sender, EventArgs e)
         {
             var delete = from s in db.tbl_barangs where s.id_barang == txtIdBarangCari.Text select s;
             foreach (var t in delete)
             {
                 db.tbl_barangs.DeleteOnSubmit(t);
             }
             db.SubmitChanges();
             MessageBox.Show("Delete Succesfully");
             txtNamaCari.Clear();
             txtIdBarangCari.Clear();
             LoadData();
         }*/
    }
}
