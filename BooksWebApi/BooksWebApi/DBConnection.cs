using BooksWebApi.Models;
using System.Data.SqlClient;
using static System.Reflection.Metadata.BlobBuilder;

namespace BooksWebApi
{
    public class DBConnection
    {
        public List<Book> GetAllBooks()
        {
            List<Book> books = new List<Book>();

            var cmd = GetSqlCommand();

            cmd.CommandText = "SELECT * FROM Books";

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var book = new Book()
                {
                    Id = int.Parse(reader["Id"].ToString()), // Giati to Id einai int
                    Title = reader["Title"].ToString(),
                    Pages = int.Parse( reader["Pages"].ToString()),
                };
                books.Add(book); // kai edw pairnoume ayto to book kai to vazoume sth lista pou ftiaksame prin

            }

            return books;
        }

        public Book GetBookById(int id)
        {
            var cmd = GetSqlCommand();

            cmd.CommandText = "SELECT * FROM Books WHERE Id = @id";

            cmd.Parameters.AddWithValue("id", id);

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var book = new Book()
                {
                    Id = int.Parse(reader["Id"].ToString()), // Giati to Id einai int
                    Title = reader["Title"].ToString(),
                    Pages = int.Parse(reader["Pages"].ToString()),
                };
                return book;

            }

            return null;
        }
        private SqlCommand GetSqlCommand()
        {
            string connectionString = "Data Source=localhost;Initial Catalog=BooksDB;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionString);
            
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;

            return cmd;
        }
    }
}
