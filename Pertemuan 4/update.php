<?php
$conn = new mysqli("localhost", "root", "", "mantan_db");
if ($conn->connect_error) {
  die("Koneksi gagal: " . $conn->connect_error);
}

$id = intval($_POST['id']);
$nama = $conn->real_escape_string($_POST['nama']);
$tanggal = $conn->real_escape_string($_POST['tanggal']);
$ciri = $conn->real_escape_string($_POST['ciri']);

// Cek apakah ada file baru diupload
if ($_FILES['foto']['name']) {
    $foto = $_FILES['foto']['name'];
    $tmp = $_FILES['foto']['tmp_name'];
    move_uploaded_file($tmp, "uploads/" . $foto);

    $sql = "UPDATE mantan SET nama='$nama', tanggal='$tanggal', ciri='$ciri', foto='$foto' WHERE id=$id";
} else {
    $sql = "UPDATE mantan SET nama='$nama', tanggal='$tanggal', ciri='$ciri' WHERE id=$id";
}

if ($conn->query($sql) === TRUE) {
    header("Location: tampil.php");
} else {
    echo "Error updating record: " . $conn->error;
}
?>



