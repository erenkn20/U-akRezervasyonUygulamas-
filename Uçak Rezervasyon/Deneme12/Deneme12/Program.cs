using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OfficeOpenXml;

class Ucak
{
    public int Id { get; set; }
    public string Model { get; set; }
    public string Marka { get; set; }
    public string SeriNo { get; set; }
    public int Kapasitesi { get; set; }
}

class Lokasyon
{
    public int Id { get; set; }
    public string Sehir { get; set; }
    public string Ulke { get; set; }
    public string Havaalani { get; set; }
    public bool AktifPasif { get; set; }
}

class Ucus
{
    public int Id { get; set; }
    public int LokasyonId { get; set; }
    public DateTime Tarih { get; set; }
    public DateTime Saat { get; set; }
    public Ucak Ucak { get; set; }
    public bool AktifPasif { get; set; }
}

class Rezervasyon
{
    public int Id { get; set; }
    public Ucus Ucus { get; set; }
    public string Ad { get; set; }
    public string Soyad { get; set; }
    public int Yas { get; set; }
}

class Program
{
    static List<Ucak> ucaklar = new List<Ucak>();
    static List<Lokasyon> lokasyonlar = new List<Lokasyon>();
    static List<Ucus> ucuslar = new List<Ucus>();
    static List<Rezervasyon> rezervasyonlar = new List<Rezervasyon>();

    static void Main()
    {
        Console.WriteLine("Uçak Bilet Rezervasyon Sistemi");

        while (true)
        {
            Console.WriteLine("\n1. Uçak Ekle");
            Console.WriteLine("2. Lokasyon Ekle");
            Console.WriteLine("3. Uçuş Ekle");
            Console.WriteLine("4. Rezervasyon Yap");
            Console.WriteLine("5. Verileri Excel'e Kaydet");
            Console.WriteLine("6. Çıkış");
            Console.Write("Seçiminizi yapın (1-6): ");

            string secim = Console.ReadLine();

            switch (secim)
            {
                case "1":
                    UcakEkle();
                    break;

                case "2":
                    LokasyonEkle();
                    break;

                case "3":
                    UcusEkle();
                    break;

                case "4":
                    RezervasyonYap();
                    break;

                case "5":
                    VerileriExcelKaydet();
                    break;

                case "6":
                    Console.WriteLine("Çıkış yapılıyor...");
                    return;

                default:
                    Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
                    break;
            }
        }
    }

    static void UcakEkle()
    {
        // Uçak ekleme işlemleri
        Ucak ucak = new Ucak();

        Console.Write("Model: ");
        ucak.Model = Console.ReadLine();

        Console.Write("Marka: ");
        ucak.Marka = Console.ReadLine();

        Console.Write("Seri No: ");
        ucak.SeriNo = Console.ReadLine();

        Console.Write("Kapasite: ");
        string kapasiteStr = Console.ReadLine();

        if (int.TryParse(kapasiteStr, out int kapasite))
        {
            ucak.Kapasitesi = kapasite;
            ucak.Id = ucaklar.Count + 1;

            ucaklar.Add(ucak);
            Console.WriteLine("Uçak başarıyla eklendi.");
        }
        else
        {
            Console.WriteLine("Geçersiz giriş. Sayısal bir değer bekleniyor.");
        }
    }


    static void LokasyonEkle()
    {
        // Lokasyon ekleme işlemleri
        Lokasyon lokasyon = new Lokasyon();

        Console.Write("Şehir: ");
        lokasyon.Sehir = Console.ReadLine();

        Console.Write("Ülke: ");
        lokasyon.Ulke = Console.ReadLine();

        Console.Write("Havaalanı: ");
        lokasyon.Havaalani = Console.ReadLine();

        Console.Write("true/false: ");
        string aktifPasifStr = Console.ReadLine();

        if (bool.TryParse(aktifPasifStr, out bool aktifPasif))
        {
            lokasyon.AktifPasif = aktifPasif;
            lokasyon.Id = lokasyonlar.Count + 1;

            lokasyonlar.Add(lokasyon);
            Console.WriteLine("Lokasyon başarıyla eklendi.");
        }
        else
        {
            Console.WriteLine("Geçersiz giriş. true veya false değeri bekleniyor.");
        }
    }


    static void UcusEkle()
    {
        // Uçuş ekleme işlemleri
        Ucus ucus = new Ucus();

        Console.Write("Lokasyon ID: ");
        int lokasyonId = Convert.ToInt32(Console.ReadLine());
        Lokasyon lokasyon = lokasyonlar.FirstOrDefault(l => l.Id == lokasyonId);

        if (lokasyon != null)
        {
            ucus.LokasyonId = lokasyonId;

            Console.Write("Tarih (yyyy-MM-dd): ");
            string tarihStr = Console.ReadLine();

            if (DateTime.TryParse(tarihStr, out DateTime tarih))
            {
                ucus.Tarih = tarih;

                Console.Write("Saat (HH:mm): ");
                string saatStr = Console.ReadLine();

                if (DateTime.TryParseExact(saatStr, "HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime saat))
                {
                    ucus.Saat = saat;

                    Console.Write("Uçak ID: ");
                    int ucakId = Convert.ToInt32(Console.ReadLine());
                    Ucak ucak = ucaklar.FirstOrDefault(u => u.Id == ucakId);

                    if (ucak != null)
                    {
                        ucus.Ucak = ucak;

                        Console.Write("Aktif/Pasif (true/false): ");
                        string aktifPasifStr = Console.ReadLine();

                        if (bool.TryParse(aktifPasifStr, out bool aktifPasif))
                        {
                            ucus.AktifPasif = aktifPasif;
                            ucus.Id = ucuslar.Count + 1;

                            ucuslar.Add(ucus);
                            Console.WriteLine("Uçuş başarıyla eklendi.");
                        }
                        else
                        {
                            Console.WriteLine("Geçersiz giriş. true veya false değeri bekleniyor.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Uçak ID'si ile eşleşen bir uçak bulunamadı.");
                    }
                }
                else
                {
                    Console.WriteLine("Geçersiz giriş. Saat formatı HH:mm olarak bekleniyor.");
                }
            }
            else
            {
                Console.WriteLine("Geçersiz giriş. Tarih formatı yyyy-MM-dd olarak bekleniyor.");
            }
        }
        else
        {
            Console.WriteLine("Lokasyon ID'si ile eşleşen bir lokasyon bulunamadı.");
        }
    }

    static void RezervasyonYap()
    {
        // Rezervasyon yapma işlemleri
        Rezervasyon rezervasyon = new Rezervasyon();

        Console.Write("Uçuş ID: ");
        int ucusId = Convert.ToInt32(Console.ReadLine());
        Ucus ucus = ucuslar.FirstOrDefault(u => u.Id == ucusId);

        if (ucus != null)
        {
            rezervasyon.Ucus = ucus;

            Console.Write("Ad: ");
            rezervasyon.Ad = Console.ReadLine();

            Console.Write("Soyad: ");
            rezervasyon.Soyad = Console.ReadLine();

            Console.Write("Yaş: ");
            string yasStr = Console.ReadLine();

            if (int.TryParse(yasStr, out int yas))
            {
                rezervasyon.Yas = yas;
                rezervasyon.Id = rezervasyonlar.Count + 1;

                // Koltuk kapasitesi kontrolü
                if (rezervasyon.Ucus.Ucak.Kapasitesi > rezervasyonlar.Count(r => r.Ucus.Id == ucusId))
                {
                    rezervasyonlar.Add(rezervasyon);
                    Console.WriteLine("Rezervasyon başarıyla yapıldı.");
                }
                else
                {
                    Console.WriteLine("Üzgünüz, uçuşun koltuk kapasitesi dolu.");
                }
            }
            else
            {
                Console.WriteLine("Geçersiz giriş. Sayısal bir değer bekleniyor.");
            }
        }
        else
        {
            Console.WriteLine("Uçuş ID'si ile eşleşen bir uçuş bulunamadı.");
        }
    }

    static void VerileriExcelKaydet()
    {
        string dosyaAdi = "Veriler.xlsx";

        // Dosyanın var olup olmadığını kontrol et
        bool dosyaVarMi = File.Exists(dosyaAdi);

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        // Verileri Excel dosyasına kaydetme işlemleri
        using (var package = new ExcelPackage(new FileInfo(dosyaAdi)))
        {
            var worksheet = package.Workbook.Worksheets.Count > 0
                ? package.Workbook.Worksheets[0]
                : package.Workbook.Worksheets.Add("Veriler");

            // Uçak verilerini Excel'e yazma
            worksheet.Cells["A1"].Value = "Uçaklar";
            worksheet.Cells["A2"].LoadFromCollection(ucaklar, true);

            // Lokasyon verilerini Excel'e yazma
            worksheet.Cells["A4"].Value = "Lokasyonlar";
            worksheet.Cells["A5"].LoadFromCollection(lokasyonlar, true);

            // Uçuş verilerini Excel'e yazma
            worksheet.Cells["A7"].Value = "Uçuşlar";
            worksheet.Cells["A8"].LoadFromCollection(ucuslar, true);

            // Rezervasyon verilerini Excel'e yazma
            worksheet.Cells["A10"].Value = "Rezervasyonlar";
            worksheet.Cells["A11"].LoadFromCollection(rezervasyonlar, true);

            package.Save();
        }

        if (dosyaVarMi)
        {
            Console.WriteLine("Veriler Excel dosyasına başarıyla eklendi.");
        }
        else
        {
            Console.WriteLine("Excel dosyası oluşturuldu ve veriler başarıyla kaydedildi.");
        }
    }
}