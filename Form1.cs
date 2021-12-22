using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdoNetDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ProductDal _productDal = new ProductDal(); // GetAll ve Add methodlarını kullanabilmek için o sınıfı çağırıp newliyoruz.
                                                  // Class içinde ama method dışında olduğu için alt çizgi kullanırı.
                

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadProducts(); // Verileri Görüntüleme. Yeniden kullanılmak üzere method halinde yazıldı.

        }

        private void LoadProducts()
        {
            dgwProducts.DataSource = _productDal.GetAllWithList(); // DataGridView a verileri yerleştirmek için DataSource komutunu kullanıcaz.
                                                                   // Object değer girmemiz gerekiyor, GetAll ile yüklediğimiz DataTable ı girelim.

            // Yada GetAllWithDataTable(); 
            // Ancak pek tercih edilmez, ben yine de ikisini de uyguladım.
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) // Butona çift tıklayıp buraya ulaşabilirsin. Buton Eventi.
        {
            _productDal.Add(new Product
            {
                Name = tbxName.Text,
                UnitPrice = Convert.ToDecimal(tbxUnitPrice.Text),
                StockAmount = Convert.ToInt32(tbxStockAmount.Text)
            }) ;

            LoadProducts(); // Buraya bunu yazmamız sayesinde ekleme işleminden sonra eklediğimiz veriyi tabloda görebilicez.
            MessageBox.Show("Product added.");

        }

        private void groupBox1_Enter_1(object sender, EventArgs e)
        {

        }

        private void tbxName_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgwProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // CellClick eventi herhangi bir hücreye tıklanması olayıdır.
            tbxNameUpdate.Text = dgwProducts.CurrentRow.Cells[1].Value.ToString();
            tbxUnitPriceUpdate.Text = dgwProducts.CurrentRow.Cells[2].Value.ToString();
            tbxStockAmountUpdate.Text = dgwProducts.CurrentRow.Cells[3].Value.ToString();
        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Product product = new Product
            {
                Id = Convert.ToInt32(dgwProducts.CurrentRow.Cells[0].Value), // ID ulaşma kodu.
                Name = tbxNameUpdate.Text,
                UnitPrice= Convert.ToDecimal(tbxUnitPriceUpdate.Text),
                StockAmount= Convert.ToInt32(tbxStockAmountUpdate.Text)
            };

            _productDal.Update(product);
            LoadProducts();
            MessageBox.Show("Updated!");
        }

        private void dgwProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgwProducts.CurrentRow.Cells[0].Value); // Id ye ulaşalım, silme işlemini ona göre yapalım.
            _productDal.Delete(id);
            LoadProducts();
            MessageBox.Show("Deleted!");
        }
    }
}
