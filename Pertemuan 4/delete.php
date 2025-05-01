<?php
include 'config.php';

if (isset($_GET['id'])) {
  $id = $_GET['id'];

  $sql = "DELETE FROM mantan WHERE id=$id";

  if ($conn->query($sql) === TRUE) {
    header("Location: tampil.php");
    exit;
  } else {
    echo "Gagal menghapus data: " . $conn->error;
  }
} else {
  echo "ID tidak ditemukan.";
}
?>
