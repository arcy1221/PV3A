<?php
include 'config.php';

if ($_SERVER['REQUEST_METHOD'] == 'POST') {
  $nama = $_POST['nama'];
  $tanggal = $_POST['tanggal'];
  $ciri = $_POST['ciri'];
  $fotoName = '';

  if (isset($_FILES['foto']) && $_FILES['foto']['error'] == 0) {
    $foto = $_FILES['foto'];
    $targetDir = "uploads/";
    $fotoName = uniqid() . "_" . basename($foto['name']);
    $targetFile = $targetDir . $fotoName;

    if (move_uploaded_file($foto['tmp_name'], $targetFile)) {
      // Berhasil upload
    } else {
      $fotoName = ''; // Upload gagal, kosongkan nama foto
    }
  }

  $sql = "INSERT INTO mantan (nama, tanggal, ciri, foto) VALUES ('$nama', '$tanggal', '$ciri', '$fotoName')";

  if ($conn->query($sql) === TRUE) {
    header("Location: tampil.php"); // Langsung ke tampil
    exit;
  } else {
    echo "Error: " . $conn->error;
  }
}

$conn->close();
?>
