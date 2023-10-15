using PharmacyManagmentSystem.PharmacistUC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacyManagmentSystem
{
    public partial class Pharmacist : Form
    {
        public Pharmacist()
        {
            InitializeComponent();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Form1 form1=new Form1();
            form1.Show();
            this.Hide();
        }

       
        private void Pharmacist_Load(object sender, EventArgs e)
        {
            uC_P_Dashbord21.Visible=false;
            uC_P_ViewMedicines1.Visible = false;
            uC_P_UpdateMedicine1.Visible = false;
            uC_P_Sell2.Visible = false;
            uC_P_MedicineValid1.Visible = false;
            uC_P_AddMedicine1.Visible = false;
            btnDashbord.PerformClick();
        }

        private void uC_P_Dashbord1_Load(object sender, EventArgs e)
        {

        }

        private void btnDashbord_Click(object sender, EventArgs e)
        {
            uC_P_Dashbord21.Visible = true;
            uC_P_Dashbord21.BringToFront();
        }

        private void btnAddMedicine_Click(object sender, EventArgs e)
        {
            uC_P_AddMedicine1.Visible = true;
            uC_P_AddMedicine1.BringToFront();
        }

        private void btnViewMedicine_Click(object sender, EventArgs e)
        {
            uC_P_ViewMedicines1.Visible = true;
            uC_P_ViewMedicines1.BringToFront();
        }

        private void btnModifyMedicine_Click(object sender, EventArgs e)
        {
            uC_P_UpdateMedicine1.Visible = true;
            uC_P_UpdateMedicine1.BringToFront();
        }

        private void uC_P_UpdateMedicine1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
         uC_P_MedicineValid1.Visible = true;
            uC_P_MedicineValid1.BringToFront();
        }

        private void btn_sell_Medicine_Click(object sender, EventArgs e)
        {
            uC_P_Sell2.Visible = true;
            uC_P_Sell2.BringToFront();
        }
    }
}
