<?php
include 'config.php';

if (isset($_GET['id'])) {
  $id = $_GET['id'];
  $sql = "SELECT * FROM mantan WHERE id = $id";
  $result = $conn->query($sql);

  if ($result->num_rows == 1) {
    $data = $result->fetch_assoc();
  } else {
    echo "Data tidak ditemukan.";
    exit;
  }
} else {
  echo "ID tidak ditemukan.";
  exit;
}
?>

<!DOCTYPE html>
<html lang="id">
<head>
  <meta charset="UTF-8">
  <title>Detail Mantan</title>
  <style>
    body {
      font-family: 'Poppins', sans-serif;
      padding: 2rem;
      background: #f0f2f5;
    }
    .container {
      background: white;
      padding: 2rem;
      max-width: 800px;
      margin: auto;
      border-radius: 10px;
      box-shadow: 0 4px 15px rgba(0, 0, 0, 0.1);
      display: flex;
      justify-content: space-between;
      align-items: center;
      flex-wrap: wrap;
    }
    .text-info {
      flex: 1;
      margin-right: 2rem;
    }
    .text-info h1 {
      margin-bottom: 1rem;
      color: #333;
    }
    .text-info p {
      margin: 0.5rem 0;
    }
    .foto-container {
  flex-shrink: 0;
  width: 400px; /* Ukuran foto lebih besar */
  height: 400px; /* Ukuran foto lebih besar */
  border-radius: 10px;
  overflow: hidden;
  margin-left: 2rem; /* Memberikan jarak agar tidak terlalu rapat */
  display: flex;
  justify-content: center; /* Memastikan gambar tetap terpusat */
  align-items: center; /* Memastikan gambar tetap terpusat */
}

.foto-container img {
  width: 100%;
  height: 100%;
  object-fit: contain; /* Memastikan gambar tidak terpotong dan tetap proporsional */
}

    
    a {
      display: inline-block;
      margin-top: 1rem;
      text-decoration: none;
      background-color: #6c63ff;
      color: white;
      padding: 0.5rem 1rem;
      border-radius: 5px;
    }
    a:hover {
      background-color: #5548c8;
    }
  </style>
</head>
<body>

<div class="container">
  <div class="text-info">
    <h1>Detail Mantan</h1>
    <p><strong>Nama:</strong> <?= htmlspecialchars($data['nama']) ?></p>
    <p><strong>Tanggal Lahir:</strong> <?= htmlspecialchars($data['tanggal']) ?></p>
    <p><strong>Ciri-ciri:</strong> <?= nl2br(htmlspecialchars($data['ciri'])) ?></p>
    <a href="tampil.php">Kembali</a>
  </div>

  <?php if (!empty($data['foto'])): ?>
    <div class="foto-container">
      <img src="uploads/<?= htmlspecialchars($data['foto']) ?>" alt="Foto Mantan">
    </div>
  <?php endif; ?>
</div>

</body>
</html>

<?php
$conn->close();
?>
