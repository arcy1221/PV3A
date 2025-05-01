<?php
include 'config.php';

// Ambil semua data mantan dari database
$sql = "SELECT * FROM mantan";
$result = $conn->query($sql);
?>

<!DOCTYPE html>
<html lang="id">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>Daftar Nama Mantan</title>
  <style>
    body {
      font-family: 'Poppins', sans-serif;
      background: #f0f2f5;
      margin: 0;
      padding: 2rem;
    }
    .container {
      background: white;
      padding: 2rem;
      border-radius: 10px;
      box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
    }
    h1 {
      margin-bottom: 1rem;
      color: #333;
    }
    table {
      width: 100%;
      border-collapse: collapse;
    }
    table, th, td {
      border: 1px solid #ccc;
    }
    th, td {
      padding: 0.75rem;
      text-align: center;
    }
    th {
      background-color: #6c63ff;
      color: white;
    }
    td a {
      margin: 0 0.5rem;
      color: #fff;
      background-color: #6c63ff;
      padding: 0.5rem 1rem;
      border-radius: 8px;
      text-decoration: none;
    }
    td a:hover {
      background-color: #5548c8;
    }
    .add-button {
      display: block;
      width: 200px;
      margin: 1rem 0;
      padding: 0.75rem;
      background-color: #6c63ff;
      color: white;
      text-align: center;
      border-radius: 8px;
      text-decoration: none;
    }
    .add-button:hover {
      background-color: #5548c8;
    }
  </style>
</head>
<body>

<div class="container">
  <h1>Daftar Nama Mantan</h1>
  <!-- Tombol untuk menambah daftar baru, mengarah ke index.html -->
  <a href="index.html" class="add-button">Tambah Daftar Baru</a>

  <!-- Tabel untuk menampilkan data -->
  <table>
    <thead>
      <tr>
        <th>No</th>
        <th>Nama Lengkap</th>
        <th>Aksi</th>
      </tr>
    </thead>
    <tbody>
      <?php
      // Menampilkan data dari database
      $no = 1;
      while ($row = $result->fetch_assoc()) {
        echo "<tr>";
        echo "<td>" . $no++ . "</td>";
        echo "<td>" . htmlspecialchars($row['nama']) . "</td>";
        echo "<td>
                <a href='read.php?id=" . $row['id'] . "'>Read</a>
                <a href='edit.php?id=" . $row['id'] . "'>Edit</a>
                <a href='delete.php?id=" . $row['id'] . "'>Delete</a>
              </td>";
        echo "</tr>";
      }
      ?>
    </tbody>
  </table>
</div>

</body>
</html>
