using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacyManagmentSystem.PharmacistUC
{
    public partial class UC_P_MedicineValid : UserControl
    {
        function fn = new function();
        
        public UC_P_MedicineValid()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                var query = "select * from medic where eDate >= getDate()";
                DataSet ds=fn.getData(query);
                dataGridView1.DataSource = ds.Tables[0];
                label3.Text = "Valid Medicine";

            }
            else if (comboBox1.SelectedIndex == 1)
            {
                var query = "select * from medic where eDate <= getDate()";
                DataSet ds = fn.getData(query);
                dataGridView1.DataSource = ds.Tables[0];
                label3.Text = "Expird Medicine";
                label3.ForeColor = Color.Red;
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                var query = "select * from medic ";
                DataSet ds = fn.getData(query);
                dataGridView1.DataSource = ds.Tables[0];
                label3.Text = "All Medicine";
                label3.ForeColor = Color.Black;

            }

        }

        private void UC_P_MedicineValid_Load(object sender, EventArgs e)
        {
            label3.Text = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
