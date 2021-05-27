using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuizMahasiswa
{
    public partial class MasterBarang : Form
    {
        public MasterBarang()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            int id = int.Parse(txtID.Text);
            string item = txtItem.Text;
            string design = txtDesign.Text;
            string color = cbColor.Text;
            DateTime expiredDate = DateTime.Parse(dateExpired.Text);

            var data = new TB_M_PRODUCT
            {
                ID = id,
                itemName = item,
                color = color,
                design = design,
                expiredDate = expiredDate
            };

            db.TB_M_PRODUCTs.InsertOnSubmit(data);
            db.SubmitChanges();
            MessageBox.Show("Save Succesfully");
            txtDesign.Clear();
            txtItem.Clear();
            cbColor.Items.Clear();
            LoadData();
        }
    }
}
