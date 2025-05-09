using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DaftarMantanApp
{
    public class FormUpdate : Form
    {
        private int mantanId;

        private TextBox txtNama, txtCiri;
        private DateTimePicker dtpTanggal;
        private PictureBox pictureBoxFoto;
        private Button btnBrowse, btnUpdate;

        private string fotoPath = "";

        public FormUpdate(int id)
        {
            mantanId = id;
            this.Text = "Update Data Mantan";
            this.Size = new Size(400, 550);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblNama = new Label() { Text = "Nama:", Top = 20, Left = 20 };
            txtNama = new TextBox() { Top = 40, Left = 20, Width = 300 };

            Label lblTanggal = new Label() { Text = "Tanggal Lahir:", Top = 80, Left = 20 };
            dtpTanggal = new DateTimePicker() { Top = 100, Left = 20, Width = 200 };

            Label lblCiri = new Label() { Text = "Ciri-ciri:", Top = 140, Left = 20 };
            txtCiri = new TextBox() { Top = 160, Left = 20, Width = 300, Height = 80, Multiline = true };

            pictureBoxFoto = new PictureBox()
            {
                Top = 260,
                Left = 20,
                Width = 200,
                Height = 200,
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom
            };

            btnBrowse = new Button() { Text = "Browse Foto", Top = 470, Left = 20 };
            btnBrowse.Click += BtnBrowse_Click;

            btnUpdate = new Button() { Text = "Update", Top = 470, Left = 150 };
            btnUpdate.Click += BtnUpdate_Click;

            this.Controls.AddRange(new Control[] {
                lblNama, txtNama,
                lblTanggal, dtpTanggal,
                lblCiri, txtCiri,
                pictureBoxFoto, btnBrowse, btnUpdate
            });

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
                            txtNama.Text = reader["nama"].ToString();
                            dtpTanggal.Value = Convert.ToDateTime(reader["tanggal_lahir"]);
                            txtCiri.Text = reader["ciri_ciri"].ToString();
                            fotoPath = reader["foto_path"].ToString();

                            if (File.Exists(fotoPath))
                            {
                                pictureBoxFoto.Image = Image.FromFile(fotoPath);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal load data: " + ex.Message);
                }
            }
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                FileInfo file = new FileInfo(ofd.FileName);
                if (file.Length > 10 * 1024 * 1024)
                {
                    MessageBox.Show("Ukuran foto maksimal 10 MB.");
                    return;
                }

                fotoPath = ofd.FileName;
                pictureBoxFoto.Image = Image.FromFile(fotoPath);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            string nama = txtNama.Text;
            DateTime tanggal = dtpTanggal.Value;
            string ciri = txtCiri.Text;

            Database db = new Database();
            using (MySqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE mantan SET nama=@nama, tanggal_lahir=@tgl, ciri_ciri=@ciri, foto_path=@foto WHERE id=@id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nama", nama);
                    cmd.Parameters.AddWithValue("@tgl", tanggal);
                    cmd.Parameters.AddWithValue("@ciri", ciri);
                    cmd.Parameters.AddWithValue("@foto", fotoPath);
                    cmd.Parameters.AddWithValue("@id", mantanId);

                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Data berhasil diupdate.");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Gagal mengupdate data.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error update: " + ex.Message);
                }
            }
        }
    }
}
