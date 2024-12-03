using System; // Sisteme ait temel sınıfları kullanmak için eklenir
using System.Collections.Generic; // Koleksiyonları (List, Dictionary vb.) kullanabilmek için eklenir

namespace BasitAgacOrnekleri // Programın çalışacağı ad alanı
{
    class Program // Program sınıfı, ana uygulama buraya yazılır
    {
        // İkili Arama Ağacı (Binary Search Tree)
        class IkiliAramaAgaci
        {
            // Dugum sınıfı, ağacın her bir düğümünü temsil eder
            class Dugum
            {
                public int Deger; // Düğümdeki değer
                public Dugum Sol, Sag; // Sol ve sağ alt düğümleri tutan referanslar

                // Düğüm sınıfının yapıcı metodu, bir değer alır ve değerini ayarlar
                public Dugum(int deger)
                {
                    Deger = deger;
                }
            }

            private Dugum kok; // Ağacın kök düğümü

            // Yeni bir değer ekler
            public void Ekle(int deger)
            {
                kok = EkleRec(kok, deger); // Kökten başlayarak değeri ekler
            }

            // Rekürsif ekleme metodu
            private Dugum EkleRec(Dugum dugum, int deger)
            {
                if (dugum == null) return new Dugum(deger); // Eğer düğüm yoksa yeni bir düğüm oluştur

                if (deger < dugum.Deger) dugum.Sol = EkleRec(dugum.Sol, deger); // Değer küçükse, sol alt ağa ekle
                else dugum.Sag = EkleRec(dugum.Sag, deger); // Değer büyükse, sağ alt ağa ekle

                return dugum; // Değeri ekledikten sonra mevcut düğümü döndür
            }

            // Ağacın sırasını (in-order traversal) yazdırır
            public void Sira()
            {
                SiraRec(kok); // Rekürsif sıralama fonksiyonunu çağırır
                Console.WriteLine(); // Satır sonu ekler
            }

            // Rekürsif sıralama fonksiyonu
            private void SiraRec(Dugum dugum)
            {
                if (dugum == null) return; // Eğer düğüm boşsa çık

                SiraRec(dugum.Sol); // Önce sol alt ağacı ziyaret et
                Console.Write(dugum.Deger + " "); // Düğümün değerini yazdır
                SiraRec(dugum.Sag); // Sonra sağ alt ağacı ziyaret et
            }
        }

        // Sözlük Ağacı (Trie)
        class SozlukAgaci
        {
            // Trie düğümü
            class Dugum
            {
                public Dictionary<char, Dugum> Cocuklar = new Dictionary<char, Dugum>(); // Çocukları (harfleri) tutan bir dictionary
                public bool KelimeSonu = false; // Eğer kelime sonu ise true
            }

            private Dugum kok = new Dugum(); // Ağacın kök düğümü

            // Kelime ekler
            public void Ekle(string kelime)
            {
                var dugum = kok; // Başlangıç olarak kök düğümüne başla

                // Her harfi sırayla kontrol eder ve ekler
                foreach (char harf in kelime)
                {
                    // Eğer harf mevcut değilse, yeni bir düğüm oluştur
                    if (!dugum.Cocuklar.ContainsKey(harf))
                        dugum.Cocuklar[harf] = new Dugum();

                    dugum = dugum.Cocuklar[harf]; // Bir sonraki harfe geç
                }

                dugum.KelimeSonu = true; // Son harfe ulaşıldığında, kelimenin sonu olduğunu işaretle
            }

            // Kelimeyi arar
            public bool Ara(string kelime)
            {
                var dugum = kok; // Başlangıç olarak kök düğümüne başla

                foreach (char harf in kelime)
                {
                    // Eğer harf bulunmazsa, kelime yok demektir
                    if (!dugum.Cocuklar.ContainsKey(harf))
                        return false;

                    dugum = dugum.Cocuklar[harf]; // Bir sonraki harfe geç
                }

                return dugum.KelimeSonu; // Eğer kelimenin sonu ise true döndür
            }
        }

        // Min-Heap (Küçük Küme Öncelikli Yığın)
        class MinHeap
        {
            private List<int> yigin = new List<int>(); // Yığın (Liste) tutulur

            // Yeni değer ekler
            public void Ekle(int deger)
            {
                yigin.Add(deger); // Yeni değeri ekle
                yigin.Sort(); // Listeyi küçükten büyüğe sıralar
            }

            // En küçük değeri çıkarır
            public int Cikar()
            {
                if (yigin.Count == 0)
                {
                    Console.WriteLine("Yığın boş!"); // Yığın boşsa uyarı
                    return -1; // Boşsa -1 döndür
                }

                int min = yigin[0]; // En küçük değeri al
                yigin.RemoveAt(0); // O değeri yığından çıkar
                return min; // Çıkarılan değeri döndür
            }
        }

        static void Main(string[] args)
        {
            // İkili Arama Ağacı
            Console.WriteLine("İkili Arama Ağacı:");
            var ikiliAgac = new IkiliAramaAgaci(); // İkili arama ağacı nesnesi oluştur
            ikiliAgac.Ekle(50); // Değer ekle
            ikiliAgac.Ekle(30); // Değer ekle
            ikiliAgac.Ekle(70); // Değer ekle
            ikiliAgac.Ekle(20); // Değer ekle
            ikiliAgac.Ekle(40); // Değer ekle
            ikiliAgac.Sira(); // Sıralı yazdır (Çıktı: 20 30 40 50 70)

            // Sözlük Ağacı
            Console.WriteLine("\nSözlük Ağacı:");
            var sozluk = new SozlukAgaci(); // Sözlük ağacı nesnesi oluştur
            sozluk.Ekle("elma"); // Kelime ekle
            sozluk.Ekle("armut"); // Kelime ekle
            Console.WriteLine("elma var mı? " + sozluk.Ara("elma")); // True, kelime var
            Console.WriteLine("muz var mı? " + sozluk.Ara("muz"));   // False, kelime yok

            // Min-Heap
            Console.WriteLine("\nMin-Heap:");
            var heap = new MinHeap(); // Min-Heap nesnesi oluştur
            heap.Ekle(10); // Değer ekle
            heap.Ekle(15); // Değer ekle
            heap.Ekle(5); // Değer ekle
            Console.WriteLine("Çıkar: " + heap.Cikar()); // En küçük değeri çıkar (5)
            Console.WriteLine("Çıkar: " + heap.Cikar()); // En küçük değeri çıkar (10)
        }
    }
}
