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
    public partial class UC_P_Dashbord2 : UserControl
    {
        function fn = new function();
        string query;
        DataSet ds;
        Int64 count;
        public UC_P_Dashbord2()
        {
            InitializeComponent();
        }

        private void UC_P_Dashbord_Load(object sender, EventArgs e)
        {
            loadchart();

        }
        public void loadchart()
        {
            //where cast(eDate as date) >= cast(getdate() as date)
            query = "select count(mname) from medic where eDate >= getdate()  ";
            ds = fn.getData(query);
            count = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());
            this.chart1.Series["Valid Medicines"].Points.AddXY("Medicine Validity chart", count);

            query = "select count(mname) from medic where eDate<= getdate() ";
            ds = fn.getData(query);
            count = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());
            this.chart1.Series["Expired Medicines"].Points.AddXY("Medicine Validity chart", count);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            chart1.Series["Expired Medicines"].Points.Clear();
            chart1.Series["Valid Medicines"].Points.Clear();
            loadchart();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
