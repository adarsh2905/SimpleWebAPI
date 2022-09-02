using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ProductRepositories

{
    public class ProductRepository
    {
        private string _conStr = null;

        public ProductRepository(string conStr)
        {
            this._conStr = conStr;
        }

        
        public List<ProductDbo> GetAllProducts()
        {

            List<ProductDbo> products = new List<ProductDbo>();
            string displayQuery = "SELECT * FROM Product";
            using (SqlConnection sqlConnection = new SqlConnection(_conStr))
            {
                sqlConnection.Open();
                SqlCommand displayCommand = new SqlCommand(displayQuery, sqlConnection);
                SqlDataReader dataReader = displayCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    products.Add(new ProductDbo
                    {
                        Id = Convert.ToInt32(dataReader["Id"]),
                        Name = dataReader["Name"].ToString(),
                        Category = dataReader["Category"].ToString(),
                        Price = Convert.ToDecimal(dataReader["Price"])
                    });
                }
                dataReader.Close();
                return products;
            }
        }

        public ProductDbo GetProductById(int id)
        {
            ProductDbo product = new ProductDbo();
            string displaySingleUser = "SELECT * FROM Product WHERE Id = @id";
            using (SqlConnection sqlConnection = new SqlConnection(_conStr))
            {
                sqlConnection.Open();
                SqlCommand displayCommand = new SqlCommand(displaySingleUser, sqlConnection);
                displayCommand.Parameters.AddWithValue("@id", id);
                SqlDataReader dataReader = displayCommand.ExecuteReader();
                if (dataReader.Read())
                {
                    product.Id = Convert.ToInt32(dataReader["Id"]);
                    product.Name = dataReader["Name"].ToString();
                    product.Category = dataReader["Category"].ToString();
                    product.Price = Convert.ToDecimal(dataReader["Price"]);
                }
                dataReader.Close();
                return product;
            }     
        }

        public int PostProduct(ProductDbo product)
        {
            ProductDbo productDbo = new ProductDbo();
            string insertQuery = "INSERT INTO Product(Name, Category, Price) VALUES(@Name, @Category, @Price); select SCOPE_IDENTITY();";
            using (SqlConnection sqlConnection = new SqlConnection(_conStr))
            {
                sqlConnection.Open();
                SqlCommand insertCommand = new SqlCommand(insertQuery, sqlConnection);
                insertCommand.Parameters.AddWithValue("@Name", product.Name);
                insertCommand.Parameters.AddWithValue("@Category", product.Category);
                insertCommand.Parameters.AddWithValue("@Price", product.Price);

                int idOfInsertedProduct =  Convert.ToInt32(insertCommand.ExecuteScalar());
                return idOfInsertedProduct;
            }
        }

        public int UpdateProduct(int id, ProductDbo product)
        {
            string updateQuery = "UPDATE Product SET Name=@Name, Category = @Category, Price = @Price WHERE Id = @id";
            using (SqlConnection sqlConnection = new SqlConnection(_conStr))
            {
                sqlConnection.Open();
                SqlCommand updateCommand = new SqlCommand(updateQuery, sqlConnection);
                updateCommand.Parameters.AddWithValue("@Name", product.Name);
                updateCommand.Parameters.AddWithValue("@Category", product.Category);
                updateCommand.Parameters.AddWithValue("@Price", product.Price);
                updateCommand.Parameters.AddWithValue("@Id", id);
                int noOfAffectedRows = updateCommand.ExecuteNonQuery();
                return noOfAffectedRows;
            }
        }

        public int DeleteProduct(int id)
        {
            string DeleteQuery = "DELETE FROM Product WHERE Id = @id";
            using (SqlConnection sqlConnection = new SqlConnection(_conStr))
            {
                sqlConnection.Open();
                SqlCommand deleteCommand = new SqlCommand(DeleteQuery, sqlConnection);
                deleteCommand.Parameters.AddWithValue("@id", id);
                int numberOfAffectedRows = deleteCommand.ExecuteNonQuery();
                return numberOfAffectedRows;
            }
        }
    }
}
