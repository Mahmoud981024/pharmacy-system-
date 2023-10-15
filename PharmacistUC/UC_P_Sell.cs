using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace PharmacyManagmentSystem.PharmacistUC
{
    public partial class UC_P_Sell : UserControl
    {
        function fn = new function();
        string query;
        DataSet ds;
        private const string AccountSid = "AC0f25b729067dd0b759755eacb9b504c3";
        private const string AuthToken = "3412f8280ed5eb3185304a3bd19f7f5b";
        private const string TwilioPhoneNumber = "(231) 359-4044";
        public UC_P_Sell()
        {
            InitializeComponent();
        }

        private void UC_P_Sell_Load(object sender, EventArgs e)
        {
            listBoxMedicine.Items.Clear();
            query = "select mname from medic where eDate>=getdate() and quantity>'0'";
            ds=fn.getData(query);
            for(int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                listBoxMedicine.Items.Add(ds.Tables[0].Rows[i][0].ToString()); 
            }

        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            UC_P_Sell_Load(this, null);
        }

        private void txtBoxSearch_TextChanged(object sender, EventArgs e)
        {
            listBoxMedicine.Items.Clear();
            query = "select mname from medic where mname like'"+txtBoxSearch.Text+"%' and eDate>=getdate() and quantity>'0'";
            ds = fn.getData(query);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                listBoxMedicine.Items.Add(ds.Tables[0].Rows[i][0].ToString());
            }

        }

        private void listBoxMedicine_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBoxNumOfUnit.Clear();
            string name = listBoxMedicine.GetItemText(listBoxMedicine.SelectedItem);
            txtMedicineName.Text=name;
            query="select mid ,eDate,perUnit from medic where mname='"+name+"'";
            ds=fn.getData(query);
            txtMedId.Text = ds.Tables[0].Rows[0][0].ToString(); 
            txtExpireDate.Text= ds.Tables[0].Rows[0][1].ToString();
            txtboxPricePerUnit.Text= ds.Tables[0].Rows[0][2].ToString();
        }

        private void txtBoxNumOfUnit_TextChanged(object sender, EventArgs e)
        {
            if (txtBoxNumOfUnit.Text != "")
            {
                Int64 unitPrice = Int64.Parse(txtboxPricePerUnit.Text);
                Int64 numOfUnit=Int64.Parse(txtBoxNumOfUnit.Text);
                Int64 total = numOfUnit * unitPrice;
                txtBoxTotaalPrice.Text = total.ToString();
            }
            else
            {
                txtBoxTotaalPrice.Clear();
            }
        }
        protected int n,totalAmount=0;
        protected Int64 quantity, newQuantity;

       

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (txtMedId.Text != "")
            {
                query = "select quantity from medic where mid='" + txtMedId.Text + "'";
                ds = fn.getData(query);
                quantity = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());
                newQuantity = quantity - Int64.Parse(txtBoxNumOfUnit.Text);
                if (newQuantity >= 0)
                {
                    try {
                        n = dataGridView1.Rows.Add();
                        dataGridView1.Rows[n].Cells[0].Value = txtMedId.Text;
                        dataGridView1.Rows[n].Cells[1].Value = txtMedicineName.Text;
                        dataGridView1.Rows[n].Cells[2].Value = txtExpireDate.Text;
                        dataGridView1.Rows[n].Cells[3].Value = txtboxPricePerUnit.Text;
                        dataGridView1.Rows[n].Cells[4].Value = txtBoxNumOfUnit.Text;
                        dataGridView1.Rows[n].Cells[5].Value = txtBoxTotaalPrice.Text;
                        totalAmount = totalAmount + int.Parse(txtBoxTotaalPrice.Text);
                        TotalLabel.Text = "RS. " + totalAmount.ToString();
                        query = "update medic set quantity='" + newQuantity + "'where mid='" + txtMedId.Text + "' ";
                        fn.setData(query, "Medicine Added");
                    } catch { }
                    
                  
                }
                else
                {
                    MessageBox.Show("Medicine out of stock \n only"+quantity+"left","warning ",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
                ClearAll();
                UC_P_Sell_Load(this, null);
            }
            else
            {
                MessageBox.Show("select Medicine first ", "Information!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
          

        }
        int valueAmount;
        string valueId;
        protected Int64 numOfUnit;

        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                valueAmount = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());
                valueId = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                numOfUnit = Int64.Parse(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());

            }
            catch
            {

            }
        }

        

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            if (valueId != null)
            {
                try
                {
                    dataGridView1.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
                }
                catch (Exception ex)
                {
                    //Handle or log the exception
                Console.WriteLine(ex.Message);
                }
                finally
                {
                    query = "select quantity from medic where mid='" + valueId + "'";
                    ds = fn.getData(query);
                    quantity = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());
                    newQuantity = quantity + numOfUnit;
                    query = "update medic set quantity ='" + newQuantity + "'where mid ='" + valueId + "'";
                    fn.setData(query, "medicine remove from Cart");
                    totalAmount = totalAmount - valueAmount;
                    TotalLabel.Text = "RS. " + totalAmount.ToString();

                }


                UC_P_Sell_Load(this, null);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            var recipientPhoneNumber = textBox1.Text; // Enter the recipient's phone number in E.164 format
            var messageBody = textBox2.Text;

            TwilioClient.Init(AccountSid, AuthToken);

            var messageOptions = new CreateMessageOptions(new PhoneNumber(recipientPhoneNumber))
            {
                From = new PhoneNumber(TwilioPhoneNumber), // Specify the sender (your Twilio phone number)
                Body = messageBody
            };

            try
            {
                var message = MessageResource.Create(messageOptions);
                MessageBox.Show($"SMS sent with SID: {message.Sid}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending SMS: {ex.Message}");
            }
        }

        private void BtnPurchase_Click(object sender, EventArgs e)
        {
            DGVPrinter print = new DGVPrinter();
            print.Title = "Medicine Bill";
            print.SubTitle = string.Format("Date:-{0}", DateTime.Now);
            print.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            print.PageNumbers = true;
            print.PageNumberInHeader = false;
            print.PorportionalColumns = true;
            print.HeaderCellAlignment = StringAlignment.Near;
            print.Footer = "total payable amount= " + TotalLabel.Text;
            print.FooterSpacing = 15;
            print.PrintDataGridView(dataGridView1);
            totalAmount = 0;
            TotalLabel.Text = "RS:00";
            dataGridView1.DataSource = 0 ;
            dataGridView1.DataSource = null;


        }
        protected void ClearAll()
        {
            txtMedId.Clear();
            txtMedicineName.Clear();
            txtExpireDate.ResetText();
            txtboxPricePerUnit.Clear();
            txtBoxNumOfUnit.Clear();
            txtBoxTotaalPrice.Clear();
        }
    }
}
