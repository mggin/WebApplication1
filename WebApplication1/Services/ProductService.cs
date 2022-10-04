using Microsoft.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class ProductService
    {
        private static string db_source = "webtestdatabase.database.windows.net";
        private static string db_user = "sqladmin";
        private static string db_password = "Th@ngg1n@azure";
        private static string db_database = "appdb";
        
        private SqlConnection GetConnection()
        {
            var _builder = new SqlConnectionStringBuilder
            {
                InitialCatalog = db_database,
                DataSource = db_source,
                UserID = db_user,
                Password = db_password
            };
            return new SqlConnection(_builder.ConnectionString);
        }

        public List<Product> GetProducts()
        {
            SqlConnection conn = GetConnection();
            List<Product> _productList = new List<Product>();
            string statement = "SELECT ProductID,ProductName,Quantity from Products";
            conn.Open();

            SqlCommand cmd = new SqlCommand(statement, conn);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        ProductID = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2)
                    };
                    _productList.Add(product);
                }
            }
            conn.Close();
            return _productList;
        }
    }


}
