<?php
$koneksi = mysqli_connect("localhost", "root", "", "db_pv");

if (!$koneksi) {
    die("Koneksi gagal: " . mysqli_connect_error());
}

if (isset($_POST['nama'])) {
    $nama = $_POST['nama'];

    $sql = "INSERT INTO pengunjung (nama) VALUES ('$nama')";

    if (mysqli_query($koneksi, $sql)) {
        echo "Data berhasil disimpan. <a href='tampil.php'>Lihat semua nama</a>";
    } else {
        echo "Gagal menyimpan data: " . mysqli_error($koneksi);
    }
} else {
    echo "Tidak ada data yang dikirim.";
}
?>
