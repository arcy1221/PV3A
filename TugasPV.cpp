#include <iostream>
#include <string>

using namespace std;

int main() {
    // Mendeklarasikan variabel untuk menyimpan nama
    string nama;

    // Meminta input nama dari pengguna
    cout << "Masukkan nama Anda: ";
    getline(cin, nama);

    // Menampilkan pesan selamat datang
    cout << "Selamat datang, " << nama << "!" << endl;

    return 0;
}

