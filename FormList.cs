using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DaftarMantanApp
{
    public class FormList : Form
    {
        private DataGridView dataGridView;

        public FormList()
        {
            this.Text = "Daftar Mantan";
            this.Size = new Size(700, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            Button btnCreate = new Button() { Text = "Create", Top = 10, Left = 10, Width = 100 };
            btnCreate.Click += BtnCreate_Click;

            dataGridView = new DataGridView()
            {
                Top = 50,
                Left = 10,
                Width = 660,
                Height = 400,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            dataGridView.CellClick += DataGridView_CellClick;

            this.Controls.Add(btnCreate);
            this.Controls.Add(dataGridView);

            LoadData();
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            FormMain formMain = new FormMain();
            formMain.FormClosed += (s, args) => LoadData(); // reload data setelah tambah
            formMain.Show();
        }

        private void LoadData()
        {
            Database db = new Database();
            DataTable dt = db.GetAllMantan();

            dataGridView.Columns.Clear();
            dataGridView.DataSource = dt;

            // Tambahkan kolom tombol hanya sekali
            AddButtonColumn("Read");
            AddButtonColumn("Update");
            AddButtonColumn("Delete");
        }

        private void AddButtonColumn(string name)
        {
            if (!dataGridView.Columns.Contains(name))
            {
                DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
                btnColumn.Name = name;
                btnColumn.HeaderText = name;
                btnColumn.Text = name;
                btnColumn.UseColumnTextForButtonValue = true;
                dataGridView.Columns.Add(btnColumn);
            }
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Jangan proses jika klik header atau invalid baris
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            string action = dataGridView.Columns[e.ColumnIndex].Name;

            if (!int.TryParse(dataGridView.Rows[e.RowIndex].Cells["id"].Value?.ToString(), out int id))
            {
                MessageBox.Show("ID tidak valid.");
                return;
            }

            if (action == "Read")
            {
                FormDetail detail = new FormDetail(id);
                detail.Show();
            }
            else if (action == "Update")
            {
                FormUpdate update = new FormUpdate(id);
                update.FormClosed += (s, args) => LoadData(); // reload data setelah update
                update.Show();
            }
            else if (action == "Delete")
            {
                var confirm = MessageBox.Show("Yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    DeleteMantan(id);
                    LoadData();
                }
            }
        }

        private void DeleteMantan(int id)
        {
            Database db = new Database();
            using (MySqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM mantan WHERE id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal menghapus data: " + ex.Message);
                }
            }
        }
    }
}
