using System.Text;

namespace KarakterDekodoloCLI
{
    internal class Program
    {
        static void Main()
        {
            var bank = Beolvas("bank.txt");
            Console.WriteLine($"5. feladat: Karakterek száma: {bank.Count}");

            char bekert;
            bool siker;
            do
            {
                Console.Write("6. feladat: Kérek egy angol nagybetűt: ");
                siker = char.TryParse(Console.ReadLine(), out bekert);
            } while (!siker || bekert < 65 || bekert > 90);

            Console.Write("7. feladat: ");
            var mgj = bank.SingleOrDefault(k => k.Betu == bekert);
            if (mgj is null) Console.WriteLine("Nincs ilyen karakter a bankban!");
            else mgj.Megjelenit();

            var dekodol = Beolvas("dekodol.txt");

            Console.WriteLine("9. feladat:");
            foreach (var dk in dekodol)
            {
                var bk = bank.SingleOrDefault(k => k.Felismer(dk));
                Console.Write(bk is null ? '?' : bk.Betu);
            }
        }

        static List<Karakter> Beolvas(string fileNeve)
        {
            List<Karakter> karakterek = new();
            using StreamReader sr = new(
                @$"..\..\..\src\{fileNeve}",
                Encoding.UTF8);
            while (!sr.EndOfStream)
            {
                char betu = char.Parse(sr.ReadLine());
                var matrix = new bool[7, 4];
                for (int s = 0; s < matrix.GetLength(0); s++)
                {
                    string sor = sr.ReadLine();
                    for (int o = 0; o < sor.Length; o++)
                    {
                        matrix[s, o] = sor[o] == '1';
                    }
                }
                karakterek.Add(new(betu, matrix));
            }
            return karakterek;
        }
    }
}