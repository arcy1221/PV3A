using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms; // Dibutuhkan untuk MessageBox

namespace DaftarMantanApp
{
    public class Database
    {
        private readonly string connectionString = "server=localhost;user=root;password=;database=daftar_mantan_db;";
        private MySqlConnection connection;

        public Database()
        {
            connection = new MySqlConnection(connectionString);
        }

        public MySqlConnection GetConnection()
        {
            return connection;
        }

        // Mengambil seluruh daftar mantan
        public DataTable GetAllMantan()
        {
            DataTable dt = new DataTable();

            try
            {
                connection.Open();
                string query = "SELECT id, nama FROM mantan";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                // Menampilkan pesan kesalahan ke user
                MessageBox.Show("Terjadi kesalahan saat mengambil data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        // Menyimpan data mantan ke database
        public bool InsertMantan(string nama, DateTime tanggalLahir, string ciriCiri, string fotoPath)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO mantan (nama, tanggal_lahir, ciri_ciri, foto_path) VALUES (@nama, @tanggal, @ciri, @foto)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nama", nama);
                        cmd.Parameters.AddWithValue("@tanggal", tanggalLahir);
                        cmd.Parameters.AddWithValue("@ciri", ciriCiri);
                        cmd.Parameters.AddWithValue("@foto", fotoPath);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Menampilkan pesan kesalahan
                MessageBox.Show("Gagal menyimpan data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
