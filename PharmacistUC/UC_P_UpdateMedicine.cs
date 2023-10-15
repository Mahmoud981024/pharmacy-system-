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
    public partial class UC_P_UpdateMedicine : UserControl
    {
        function fn = new function();
        string query;
        public UC_P_UpdateMedicine()
        {
            InitializeComponent();
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            if(txtMedId.Text!="")
            {
                query = "select * from medic where mid='" + txtMedId.Text + "'";
                DataSet ds = fn.getData(query);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    textMedName.Text = ds.Tables[0].Rows[0][2].ToString();
                    textMedNumber.Text = ds.Tables[0].Rows[0][3].ToString();
                    txtMDate.Text= ds.Tables[0].Rows[0][4].ToString();
                    txtEDate.Text= ds.Tables[0].Rows[0][5].ToString();
                    textAvalibQuantity.Text= ds.Tables[0].Rows[0][6].ToString();
                    textPrice.Text= ds.Tables[0].Rows[0][7].ToString();
                }
                else
                {
                    MessageBox.Show("No Medicine with id:" + txtMedId.Text + "exsit", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                clearAll();
            }

        }
        private void clearAll()
        {
            txtMedId.Clear();
            txtMDate.ResetText();
            txtEDate.ResetText();
            textAvalibQuantity.Clear();
            textMedName.Clear();
            textMedNumber.Clear();
            textPrice.Clear();
            textAddQuantity.Clear();
        }

        private void btnReaset_Click(object sender, EventArgs e)
        {
            clearAll();
        }
        Int64 totalQuantity;
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string mname = textMedName.Text;
            string mnumber= textMedNumber.Text;
            DateTime mdate=txtMDate.Value;
            DateTime edate=txtEDate.Value;
            Int64 quatity = Int64.Parse(textAvalibQuantity.Text);
            Int64 addquantity = 0;
            if (textAddQuantity.Text != "")
            {
               addquantity = Int64.Parse(textAddQuantity.Text);
            }
            Int64 price = Int64.Parse(textPrice.Text);
            totalQuantity = quatity + addquantity;
            query = "update medic set mname='" + mname + "',mnumber='" + mnumber + "',mDate='" + mdate + "',eDate='" + edate + "',quantity='" + totalQuantity + "',perUnit='"+price+"' where mid='"+txtMedId.Text+"'";
            fn.setData(query, "medicine updated");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
