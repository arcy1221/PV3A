<?php
$koneksi = mysqli_connect("localhost", "root", "", "db_pv");
$data = mysqli_query($koneksi, "SELECT * FROM pengunjung");
?>

<h2>Daftar Nama:</h2>
<ul>
<?php while($row = mysqli_fetch_assoc($data)): ?>
    <li><?= htmlspecialchars($row['nama']) ?></li>
<?php endwhile; ?>
</ul>
