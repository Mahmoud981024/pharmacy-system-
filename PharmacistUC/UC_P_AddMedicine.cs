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
    public partial class UC_P_AddMedicine : UserControl
    {
        function fn = new function();
        string query;
        public UC_P_AddMedicine()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(textMedName.Text!=""&&textMedNum.Text!="" && textPriceUnit.Text!="" && textQuantity.Text!="" &&txtMedId.Text!="")
            {
                string mid=txtMedId.Text;
                string mname = textMedName.Text;
                string mnumber=textMedNum.Text;
                DateTime mdate=txtManfuctDate.Value;
                DateTime edate=txtExpireDate.Value;
                Int64 quantity = Int64.Parse(textQuantity.Text);
                Int64 perUnit = Int64.Parse(textPriceUnit.Text);
                
                query = "insert into medic (mid,mname,mnumber,mDate,eDate,quantity,perUnit) values  ('"+mid+"', '"+mname+"' , '"+mnumber+"' ,' "+mdate+"' ,'"+edate+"' ,'"+quantity+"',  '"+perUnit+"')";
                fn.setData(query, "Medicine Added to Database");
            }
            else
            {
                MessageBox.Show("Enter all Data","Information",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }
        public void clearAll()
        {
            txtMedId.Clear();
            textMedName.Clear();
            textPriceUnit.Clear();
            textQuantity.Clear();
            textMedNum.Clear();
            
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }

}
