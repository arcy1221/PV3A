using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DaftarMantanApp
{
    public class FormMain : Form
    {
        private TextBox txtNama;
        private DateTimePicker dtpTanggalLahir;
        private TextBox txtCiriCiri;
        private PictureBox pictureBoxFoto;
        private Button btnSubmit;
        private Button btnLihatDaftar;
        private string fotoPath = "";

        public FormMain()
        {
            this.Text = "Tambah Mantan";
            this.Size = new Size(400, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblNama = new Label() { Text = "Nama:", Top = 20, Left = 20, Width = 100 };
            txtNama = new TextBox() { Top = 45, Left = 20, Width = 300 };

            Label lblTanggal = new Label() { Text = "Tanggal Lahir:", Top = 85, Left = 20, Width = 100 };
            dtpTanggalLahir = new DateTimePicker() { Top = 110, Left = 20, Width = 300 };

            Label lblCiriCiri = new Label() { Text = "Ciri-ciri:", Top = 150, Left = 20, Width = 100 };
            txtCiriCiri = new TextBox() { Top = 175, Left = 20, Width = 300, Height = 60, Multiline = true };

            Label lblFoto = new Label() { Text = "Foto:", Top = 250, Left = 20, Width = 100 };
            pictureBoxFoto = new PictureBox()
            {
                Top = 275,
                Left = 20,
                Width = 100,
                Height = 100,
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom
            };

            Button btnBrowseFoto = new Button() { Text = "Pilih Foto", Top = 385, Left = 20 };
            btnBrowseFoto.Click += BtnBrowseFoto_Click;

            btnSubmit = new Button() { Text = "Submit", Top = 430, Left = 20, Width = 100 };
            btnLihatDaftar = new Button() { Text = "Lihat Daftar", Top = 430, Left = 140, Width = 100 };

            btnSubmit.Click += BtnSubmit_Click;
            btnLihatDaftar.Click += BtnLihatDaftar_Click;

            this.Controls.Add(lblNama);
            this.Controls.Add(txtNama);
            this.Controls.Add(lblTanggal);
            this.Controls.Add(dtpTanggalLahir);
            this.Controls.Add(lblCiriCiri);
            this.Controls.Add(txtCiriCiri);
            this.Controls.Add(lblFoto);
            this.Controls.Add(pictureBoxFoto);
            this.Controls.Add(btnBrowseFoto);
            this.Controls.Add(btnSubmit);
            this.Controls.Add(btnLihatDaftar);
        }

        private void BtnBrowseFoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                FileInfo fileInfo = new FileInfo(ofd.FileName);
                if (fileInfo.Length > 10 * 1024 * 1024)
                {
                    MessageBox.Show("Ukuran file terlalu besar. Maksimum 10 MB.");
                    return;
                }

                string imageFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
                if (!Directory.Exists(imageFolder)) Directory.CreateDirectory(imageFolder);

                string destFileName = Path.Combine(imageFolder, Path.GetFileName(ofd.FileName));
                File.Copy(ofd.FileName, destFileName, true);

                fotoPath = destFileName;
                pictureBoxFoto.Image = Image.FromFile(fotoPath);
            }
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            string nama = txtNama.Text.Trim();
            DateTime tanggal = dtpTanggalLahir.Value;
            string ciri = txtCiriCiri.Text.Trim();

            if (string.IsNullOrEmpty(nama) || string.IsNullOrEmpty(ciri) || string.IsNullOrEmpty(fotoPath))
            {
                MessageBox.Show("Semua field harus diisi termasuk foto.");
                return;
            }

            try
            {
                Database db = new Database();
                bool result = db.InsertMantan(nama, tanggal, ciri, fotoPath);

                if (result)
                {
                    MessageBox.Show("Data mantan berhasil disimpan.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Gagal menyimpan data.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
        }

        private void BtnLihatDaftar_Click(object sender, EventArgs e)
        {
            FormList listForm = new FormList();
            listForm.Show();
        }
    }
}
