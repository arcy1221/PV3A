<!DOCTYPE html>
<html lang="id">
<head>
    <meta charset="UTF-8">
    <title>Selamat Datang</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: white;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }

        .container {
            width: 320px;
            text-align: center;
        }

        .title {
            font-style: italic;
            font-size: 14px;
            margin-bottom: 30px;
        }

        input {
            width: 100%;
            padding: 10px;
            background-color: #D9D9D9;
            border: none;
            border-radius: 5px;
            font-size: 14px;
            text-align: center;
            margin-bottom: 20px;
        }

        button {
            width: 80%;
            padding: 10px;
            background-color: #E6E6E6;
            border: none;
            border-radius: 5px;
            font-weight: bold;
            cursor: pointer;
            margin-bottom: 30px;
        }

        button:hover {
            background-color: #ccc;
        }

        #hasil {
            font-weight: bold;
            font-style: italic;
        }
    </style>
</head>
<body>
    <div class="container">
        <div class="title">MAHESA DIFA RAMADHAN</div>

        <input type="text" id="nama" placeholder="NAMA LENGKAP">
        <br>
        <button onclick="tampilkanPesan()">SUBMIT</button>

        <p id="hasil"></p>
    </div>

    <script>
        function tampilkanPesan() {
            const nama = document.getElementById("nama").value.trim();
            const hasil = document.getElementById("hasil");

            if (nama === "") {
                hasil.innerText = "Nama tidak boleh kosong!";
            } else {
                hasil.innerText = `SELAMAT DATANG, ( ${nama} )`;
            }
        }
    </script>
</body>
</html>
