using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetDemo
{
    public class ProductDal // Veritabanından veri çekme, veri tabanına yeni veri gönderme, silme , güncelleme gibi operasyonları içerir.
    {
        SqlConnection _connection = new SqlConnection(@"server=(localdb)\mssqllocaldb; initial catalog=ETrade; integrated security=true");
        // Veritabanınıa bağlantı nesnesini oluşturduk.
        // Eğer uzaktan bağlanıcaksan false yapıp, uid= ; password= ; girmen gerekiyor.
        // Alt çizgi _ kullanılma sebebi, isimlendirme kuralları gereği class içinde, method dışında tanımlanmasıdır.

        // List kullanarak Liste halinde veri çekme.
        public List<Product> GetAllWithList()
        {
            ConnectionControl(); // Sürekli yapıldığı için bunu ayrı bir method haline getirip direk ihtiyaç halinde çağırıyoruz.

            SqlCommand command = new SqlCommand("Select * from Products", _connection);

            SqlDataReader reader = command.ExecuteReader();
            // Komutumuzu çalıştırmamız gerekiyor. SqlDataReader sınıfında olduğu için ona eşitledik.

            // Komutu çalıştırdık ama okuduğumuz verileri bir Listeye çekmemiz gerekiyor.

            List<Product> products = new List<Product>(); // Bir List oluşturalım.

            while (reader.Read()) // Reader daki kayıtları tek tek oku. Okunacak kayıt olduğu müddetçe devam et.
            {                     // Yani Listeyi tek tek dön ve oku.
                Product product = new Product
                {
                    Id = (int)reader["Id"], // Int e çevirdik.
                    Name = (string)reader["Name"],  // Bunlar veritabanından gelen isimlerdir.
                    StockAmount = (int)reader["StockAmount"],
                    UnitPrice = (decimal)reader["UnitPrice"]

                };

                products.Add(product);// Her kaydı product listesinin içine ekle.
            }

            reader.Close(); // Okumayı kapatalım.

            _connection.Close(); // Bağlantıyı kapatalım. İş bitti.

            return products; // Oluşturduğumuz DataTable ı döndürelim.

        }

        


        // DataTable kullanımı ile veri çekme. (Pek Tercih Edilmez.)
        public DataTable GetAllWithDataTable()
        {

            ConnectionControl(); // Connection kontrol ederek bağlantımızı açıyoruz.

            SqlCommand command = new SqlCommand("Select * from Products", _connection);

            SqlDataReader reader = command.ExecuteReader();
            // Komutumuzu çalıştırmamız gerekiyor. SqlDataReader sınıfında olduğu için ona eşitledik.

            // Komutu çalıştırdık ama bir listeye çevirmemiz gerekiyor. DataTable, yani data tablosu formatına çevirelim. 

            DataTable dataTable = new DataTable(); // Bir DataTable oluşturalım.

            dataTable.Load(reader); // Okuduklarımızı DataTable a yükleyelim.

            reader.Close(); // Okumayı kapatalım.

            _connection.Close(); // Bağlantıyı kapatalım. İş bitti.

            return dataTable; // Oluşturduğumuz DataTable ı döndürelim.

        }

        // Ekleme işleminin yapılması:
        public void Add(Product product) // Bana bir product ver ve ekleyeyim.
        {
            ConnectionControl(); // SqlConnection kontrol ediyor ve bağlantıyı açıyoruz.
            SqlCommand command = new SqlCommand("Insert into Products values(@name,@unitPrice,@StockAmount)", _connection); // SQL komutumuzu yazıyoruz.
            command.Parameters.AddWithValue("@name", product.Name); // Parametreler olduğu için onları işliyoruz.
            command.Parameters.AddWithValue("@unitPrice", product.UnitPrice);
            command.Parameters.AddWithValue("@stockAmount", product.StockAmount);
            command.ExecuteNonQuery(); // Çalıştır.

        }

        private void ConnectionControl()
        {
            if (_connection.State == ConnectionState.Closed) // Bağlantımızı açıyoruz. 
            {                                               // Hata vermemesi için önce durumunu sorguluyoruz. Kapalıysa açıyoruz.
                _connection.Open();
            }
        }

        // Güncelleme operasyonu. Verilerde değişiklik yapmamıza olanak sağlar.

        public void Update(Product product) // Bana bir product ver ve ekleyeyim.
        {
            ConnectionControl(); // SqlConnection kontrol ediyor ve bağlantıyı açıyoruz.
            SqlCommand command = new SqlCommand("Update Products set Name=@name, UnitPrice=@unitPrice, StockAmount=@stockAmount where ID=@id", _connection); // SQL komutumuzu yazıyoruz.
            command.Parameters.AddWithValue("@name", product.Name); // Parametreler olduğu için onları işliyoruz.
            command.Parameters.AddWithValue("@unitPrice", product.UnitPrice);
            command.Parameters.AddWithValue("@stockAmount", product.StockAmount);
            command.Parameters.AddWithValue("@id", product.Id);
            command.ExecuteNonQuery();
            // Çalıştır.

        }

        public void Delete(int id) // Bana bir product ver ve ekleyeyim.
        {
            ConnectionControl(); // SqlConnection kontrol ediyor ve bağlantıyı açıyoruz.
            SqlCommand command = new SqlCommand("Delete from Products where Id=@id", _connection); // SQL komutumuzu yazıyoruz.
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery(); // Çalıştır.
           
            _connection.Close();
           

        }
    }
}
