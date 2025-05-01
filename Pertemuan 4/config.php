<?php
$servername = "localhost";  // Server MySQL
$username = "root";         // Username default di XAMPP
$password = "";             // Password kosong jika Anda tidak mengatur password di XAMPP
$dbname = "mantan_db";             // Nama database yang sudah Anda buat

// Membuat koneksi
$conn = new mysqli($servername, $username, $password, $dbname);

// Memeriksa koneksi
if ($conn->connect_error) {
  die("Koneksi gagal: " . $conn->connect_error);
}
?>
