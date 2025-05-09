using System;
using System.Collections.Generic;

namespace DaftarMantanApp
{
    public class Mantan
    {
        public int Id { get; set; }
        public string Nama { get; set; }
        public DateTime TanggalLahir { get; set; }  // ← DateTime butuh 'using System'
        public string CiriCiri { get; set; }
        public string FotoPath { get; set; }
    }
}
