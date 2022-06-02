using System;
using System.Linq;

namespace WolfPack
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Get Data From User

            Console.WriteLine("Sürü Büyüklüğünü Giriniz");
            int size = int.Parse(Console.ReadLine());

            while (size < Constraints.MinSize || size > Constraints.MaxSize)
            {
                Console.WriteLine($"Sürü Büyüklüğünü Sınırlarını Aştınız. Sınırlar 5 ile 2*10^5 Arasındadır");
                size = int.Parse(Console.ReadLine());
            }

            int[] wolfPacks = new int[size];

            Console.WriteLine("Boşluk İle Ayırarak Tür IDlerini Giriniz ");
            string data = Console.ReadLine();

            wolfPacks = data.Split(" ").Select(Int32.Parse).ToArray();

            while (wolfPacks.Length != size || wolfPacks.Any(w => w > Constraints.MaxKindId || w < Constraints.MinKindId))
            {
                Console.WriteLine($"\nKısıtlara Uymadınız, Bilgileri Tekrar Giriniz. Hedef Büyüklük : {size} || Maximum Tür ID : 5");
                Console.WriteLine("\nBoşluk İle Ayırarak Tür IDlerini Giriniz Örn: 1 2 5 5");

                data = Console.ReadLine();
                wolfPacks = data.Split(" ").Select(Int32.Parse).ToArray();
            }

            #endregion

            #region Fix Problem

            var wolfPacksCountByKind = (from wolf in wolfPacks
                                        group wolf by wolf into grp
                                        select new { Kind = grp.Key, Count = grp.Count() });

            int maxCount = wolfPacksCountByKind.Max(x => x.Count);

            int dominantKind = wolfPacksCountByKind
                        .Where(x => x.Count == maxCount)
                        .Min(x => x.Kind);
            #endregion

            Console.WriteLine($"BASKIN TÜR = {dominantKind} ");
            Console.ReadKey();
        }


        static class Constraints
        {
            public static int MinSize => 5;
            public static double MaxSize => 2 * Math.Pow(10, 5);
            public static int MinKindId => 1;
            public static int MaxKindId => 5;
        }
    }
}
