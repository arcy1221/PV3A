<?php
include 'config.php';

// Ambil data berdasarkan ID
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
}

// Proses form saat disubmit
if ($_SERVER['REQUEST_METHOD'] == 'POST') {
  $nama = $_POST['nama'];
  $tanggal = $_POST['tanggal'];
  $ciri = $_POST['ciri'];
  $fotoLama = $data['foto']; // Foto lama yang akan disimpan jika tidak ada foto baru

  // Proses upload foto baru jika ada
  if (isset($_FILES['foto']) && $_FILES['foto']['error'] == 0) {
    $foto = $_FILES['foto'];
    $namaFoto = uniqid() . '-' . basename($foto['name']);
    $targetDir = 'uploads/';
    $targetFile = $targetDir . $namaFoto;

    // Cek apakah file adalah gambar
    $imageFileType = strtolower(pathinfo($targetFile, PATHINFO_EXTENSION));
    $validExtensions = ['jpg', 'jpeg', 'png', 'gif'];

    if (in_array($imageFileType, $validExtensions)) {
      if (move_uploaded_file($foto['tmp_name'], $targetFile)) {
        // Hapus foto lama jika ada foto baru
        if (!empty($fotoLama) && file_exists($targetDir . $fotoLama)) {
          unlink($targetDir . $fotoLama);
        }
      } else {
        echo "Gagal mengunggah foto.";
      }
    } else {
      echo "Hanya file gambar yang diperbolehkan.";
    }
  } else {
    $namaFoto = $fotoLama; // Tetap gunakan foto lama jika tidak ada foto baru
  }

  // Update data di database
  $sqlUpdate = "UPDATE mantan SET nama = ?, tanggal = ?, ciri = ?, foto = ? WHERE id = ?";
  $stmt = $conn->prepare($sqlUpdate);
  $stmt->bind_param("ssssi", $nama, $tanggal, $ciri, $namaFoto, $id);

  if ($stmt->execute()) {
    header('Location: tampil.php'); // Kembali ke halaman tampil setelah update
  } else {
    echo "Gagal mengupdate data.";
  }
}
?>

<!DOCTYPE html>
<html lang="id">
<head>
  <meta charset="UTF-8">
  <title>Edit Data Mantan</title>
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
    }
    .form-group {
      margin-bottom: 1rem;
    }
    input, textarea {
      width: 100%;
      padding: 0.75rem;
      border-radius: 8px;
      border: 1px solid #ccc;
      font-size: 1rem;
    }
    button {
      background-color: #6c63ff;
      color: white;
      padding: 0.75rem 1rem;
      border-radius: 8px;
      border: none;
      cursor: pointer;
    }
    button:hover {
      background-color: #5548c8;
    }
    .foto-container img {
      width: 150px;
      height: 150px;
      object-fit: cover;
      border-radius: 8px;
      margin-top: 1rem;
    }
  </style>
</head>
<body>

<div class="container">
  <h1>Edit Data Mantan</h1>
  <form method="POST" enctype="multipart/form-data">
    <div class="form-group">
      <label for="nama">Nama Lengkap</label>
      <input type="text" id="nama" name="nama" value="<?= htmlspecialchars($data['nama']) ?>" required>
    </div>

    <div class="form-group">
      <label for="tanggal">Tanggal Lahir</label>
      <input type="date" id="tanggal" name="tanggal" value="<?= htmlspecialchars($data['tanggal']) ?>" required>
    </div>

    <div class="form-group">
      <label for="ciri">Ciri-ciri</label>
      <textarea id="ciri" name="ciri" rows="3"><?= htmlspecialchars($data['ciri']) ?></textarea>
    </div>

    <div class="form-group">
      <label for="foto">Foto (Opsional)</label>
      <input type="file" id="foto" name="foto" accept="image/*">
      <?php if (!empty($data['foto'])): ?>
        <div class="foto-container">
          <img src="uploads/<?= htmlspecialchars($data['foto']) ?>" alt="Foto Mantan">
        </div>
      <?php endif; ?>
    </div>

    <button type="submit">Update Data</button>
  </form>
</div>

</body>
</html>
