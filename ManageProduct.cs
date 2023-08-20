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

namespace StationeryManagementSystem
{
    public partial class ManageProduct : Form
    {
        public ManageProduct()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-R329GNQ\SQLEXPRESS;Initial Catalog=STM;Integrated Security=True");
        void fillcategory()
        {
            string query = "select * from CategoryTbl";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataReader rdr;
            try
            {
                Con.Open();
                DataTable dt = new DataTable();
                dt.Columns.Add("CatName", typeof(string));
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                CatCombo.ValueMember = "CatName";
                CatCombo.DataSource = dt;
                SearchCombo.ValueMember = "CatName";
                SearchCombo.DataSource = dt;
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
        void filterbycategory()
        {
            try
            {
                Con.Open();
                string Myquery = "select * from ProductTbl where ProdCat='" + SearchCombo.SelectedValue.ToString() + "'";
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



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void ManageProduct_Load(object sender, EventArgs e)
        {
            fillcategory();
            populateProduct();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into ProductTbl values(" + ProductIdTb.Text + ",'" + ProductNameTb.Text + "'," + ProdQtyTb.Text + "," + ProdPriceTb.Text + ",'" + ProdDiscTb.Text + "','" + CatCombo.SelectedValue.ToString() + "')", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show(" Product Successfully Added");
                Con.Close();
                populateProduct();
            }
            catch
            {
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("update ProductTbl set ProdName='" + ProductNameTb.Text +
               "',ProdQty=" + ProdQtyTb.Text + ",ProdPrice=" + ProdPriceTb.Text + ",ProdDesc='" + ProdDiscTb.Text + "',ProdCat='"
               + CatCombo.SelectedValue.ToString() + "' where ProdId=" + ProductIdTb.Text + " ", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Successfully Update");
                Con.Close();
                populateProduct();
            }
            catch
            {
            }
        }

        private void ProductsGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            {
                ProductsGV.CurrentRow.Selected = true;
                ProductIdTb.Text = ProductsGV.Rows[e.RowIndex].Cells["ProdId"].Value.ToString();
                ProductNameTb.Text = ProductsGV.Rows[e.RowIndex].Cells["ProdName"].Value.ToString();
                ProdQtyTb.Text = ProductsGV.Rows[e.RowIndex].Cells["ProdQty"].Value.ToString();
                ProdPriceTb.Text = ProductsGV.Rows[e.RowIndex].Cells["ProdPrice"].Value.ToString();
                ProdDiscTb.Text = ProductsGV.Rows[e.RowIndex].Cells["ProdDesc"].Value.ToString();
                CatCombo.SelectedValue = ProductsGV.Rows[e.RowIndex].Cells["ProdCat"].Value.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (ProductIdTb.Text == "")
            {
                MessageBox.Show("Enter the Product Id");
            }
            else
            {
                Con.Open();
                string myquery = "delete from ProductTbl where ProdId=" + ProductIdTb.Text + ";";
                SqlCommand cmd = new SqlCommand(myquery, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Successfully Deleted");
                Con.Close();
                populateProduct();
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            filterbycategory();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            populateProduct();
        }

        private void SearchCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void ManageProduct_Load_1(object sender, EventArgs e)
        {
            fillcategory();
            populateProduct();
        }
    }
}
