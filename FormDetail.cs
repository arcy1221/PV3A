using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DaftarMantanApp
{
    public class FormDetail : Form
    {
        private int mantanId;

        private Label lblJudul;
        private Label lblNama;
        private Label lblTanggal;
        private Label lblCiri;
        private PictureBox pictureBoxFoto;

        public FormDetail(int id)
        {
            this.mantanId = id;
            this.Text = "Detail Data Mantan";
            this.Size = new Size(400, 550);
            this.StartPosition = FormStartPosition.CenterScreen;

            lblJudul = new Label()
            {
                Text = "Detail Data Mantan",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = true,
                Top = 20,
                Left = 20
            };

            lblNama = new Label()
            {
                Text = "Nama: ",
                AutoSize = true,
                Top = 70,
                Left = 20
            };

            lblTanggal = new Label()
            {
                Text = "Tanggal Lahir: ",
                AutoSize = true,
                Top = 100,
                Left = 20
            };

            lblCiri = new Label()
            {
                Text = "Ciri-ciri: ",
                AutoSize = true,
                Top = 130,
                Left = 20
            };

            pictureBoxFoto = new PictureBox()
            {
                Top = 170,
                Left = 20,
                Width = 300,
                Height = 300,
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom
            };

            this.Controls.Add(lblJudul);
            this.Controls.Add(lblNama);
            this.Controls.Add(lblTanggal);
            this.Controls.Add(lblCiri);
            this.Controls.Add(pictureBoxFoto);

            LoadData();
        }

        private void LoadData()
        {
            Database db = new Database();
            using (MySqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM mantan WHERE id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", mantanId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string nama = reader["nama"].ToString();
                            string tanggal = Convert.ToDateTime(reader["tanggal_lahir"]).ToString("dd MMMM yyyy");
                            string ciri = reader["ciri_ciri"].ToString();
                            string fotoPath = reader["foto_path"].ToString();

                            lblNama.Text = "Nama: " + nama;
                            lblTanggal.Text = "Tanggal Lahir: " + tanggal;
                            lblCiri.Text = "Ciri-ciri: " + ciri;

                            if (!string.IsNullOrEmpty(fotoPath) && File.Exists(fotoPath))
                            {
                                pictureBoxFoto.Image = Image.FromFile(fotoPath);
                            }
                            else
                            {
                                pictureBoxFoto.Image = null;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat data: " + ex.Message);
                }
            }
        }
    }
}
