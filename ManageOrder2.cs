using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace StationeryManagementSystem
{
    public partial class ManageOrder2 : Form
    {
        public ManageOrder2()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-R329GNQ\SQLEXPRESS;Initial Catalog=STM;Integrated Security=True");
        void populate()
        {
            try
            {
                Con.Open();
                string Myquery = "select * from CustomersTbl";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                CustomersGV.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch
            {

            }
        }
        void populateProduct()
        {
            try
            {
                Con.Open();
                string Myquery = "select * from ProductTbl";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                ProductsGV.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch
            {
            }
        }
        private void ManageOrder2_Load(object sender, EventArgs e)
        {
           
            populate();
            populateProduct();
            OrderGv.ColumnCount = 5;
            OrderGv.Columns[0].Name = "No.";
            OrderGv.Columns[1].Name = "Product Name";
            OrderGv.Columns[2].Name = "Price";
            OrderGv.Columns[3].Name = "Qnt";
            OrderGv.Columns[4].Name = "Total Price";
            OrderGv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            OrderGv.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            OrderGv.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            OrderGv.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            OrderGv.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        int num = 0;
        int uprice, totprice, qty;
        string product;

        private void CustomersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CustomersGV.CurrentRow.Selected = true;
            CustomerIdTb.Text = CustomersGV.Rows[e.RowIndex].Cells["CustId"].Value.ToString();
            CustomerNameTb.Text = CustomersGV.Rows[e.RowIndex].Cells["CustName"].Value.ToString();
        }
        int flag = 0;
        int stock;
        int id;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ProductsGV.CurrentRow.Selected = true;
            ProductIdTb.Text = ProductsGV.Rows[e.RowIndex].Cells["ProdId"].Value.ToString();
            ProductNameTb.Text = ProductsGV.Rows[e.RowIndex].Cells["ProdName"].Value.ToString();
            ProdPriceTb.Text = ProductsGV.Rows[e.RowIndex].Cells["ProdPrice"].Value.ToString();
            StockTb.Text = ProductsGV.Rows[e.RowIndex].Cells["ProdQty"].Value.ToString();
            id = Convert.ToInt32(ProductIdTb.Text);
            stock = Convert.ToInt32(StockTb.Text);
            QtyTb.Text = "";
            flag = 1;
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }
        int sum = 0;
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void OrderIdTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void CustomerIdTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void CustomerNameTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void OrderDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (QtyTb.Text == "")
                MessageBox.Show("enter the Quantity of Product");
            else if (flag == 0)
                MessageBox.Show("select the product");
            else if (Convert.ToInt32(QtyTb.Text) >= stock)
                MessageBox.Show("No Enough Stock Available");
            else
            {
                num = num + 1;
                product = ProductNameTb.Text;
                uprice = Convert.ToInt32(ProdPriceTb.Text);
                qty = Convert.ToInt32(QtyTb.Text);
                totprice = qty * uprice;
                OrderGv.Rows.Add(num, product, uprice, qty, totprice);
                flag = 0;

            }
        }

        private void QtyTb_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
