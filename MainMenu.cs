using System;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace DaftarMantanApp
{
    public class MainMenu : Form
    {
        private Button btnInput;
        private Button btnList;

        public MainMenu()
        {
            this.Text = "Daftar Mantan - Menu Utama";
            this.Size = new Size(300, 200);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblTitle = new Label()
            {
                Text = "Aplikasi Daftar Mantan",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = true,
                Top = 20,
                Left = 30
            };

            btnInput = new Button()
            {
                Text = "Tambah Mantan Baru",
                Top = 70,
                Left = 50,
                Width = 180
            };
            btnInput.Click += BtnInput_Click;

            btnList = new Button()
            {
                Text = "Lihat Daftar Mantan",
                Top = 110,
                Left = 50,
                Width = 180
            };
            btnList.Click += BtnList_Click;

            this.Controls.Add(lblTitle);
            this.Controls.Add(btnInput);
            this.Controls.Add(btnList);
        }

        private void BtnInput_Click(object sender, EventArgs e)
        {
            FormMain inputForm = new FormMain();
            inputForm.ShowDialog();
        }

        private void BtnList_Click(object sender, EventArgs e)
        {
            FormList listForm = new FormList();
            listForm.ShowDialog();
        }
    }
}
