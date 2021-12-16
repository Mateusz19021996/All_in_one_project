using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * In that class:
 * 1. Yield key word
 * 
 */
namespace LINQTrainingApp
{
    public static class LINQ_One
    {

        public static List<int> getInts()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            return list;
        }

        public static void YieldTest()
        {
            var list = getInts();

            //to się wykona odrazu
            Filter(list);

            //poniższa metoda się nie wykona jesli po niej nie przeiterujemy, jest deferred.
            FilterYield(list);

            //tutaj juz znowu metoda się wykona, bo strzelamy coutem
            var o = FilterYield(list).Count();
            Console.WriteLine(o);


            Console.WriteLine("-----------------------------------");
            foreach (var item in Filter(list))
            {
                if (item > 5)
                {
                    Console.WriteLine(item);
                }
            }

            foreach (var item in FilterYield(list))
            {
                if (item > 5)
                {
                    Console.WriteLine(item);
                }
            }
        }

        public static IEnumerable<int> FilterYield(List<int> list)
        {
            Console.WriteLine("Yeld:");
            foreach (var item in list)
            {
                Console.WriteLine("in yield loop");
                yield return item;
            }
        }

        public static IEnumerable<int> Filter(List<int> list)
        {
            Console.WriteLine("No Yeld:");
            var lista = new List<int>();
            foreach (var item in list)
            {
                Console.WriteLine("in no yield loop");
                lista.Add(item);
                Console.WriteLine("we add to list in yield");
            }

            return lista;
        }
    }
}

/*
 * Yield & LINQ:
 * 
 * 1. Silnie typowane
 * 2. Nie zwraca całej kolekcji naraz, tylko zwraca elementy jeden po drugim
 * 3. Nie musimy tworzyć dodatkowej listy (przykład)
 * 4. Dopiero wywołanie toList(), toArray(), Count() etc. "materializuje" naszą deferred data structure
 * 5. Daje nam możliwość tzn. deferred execution czyli odroczonego wykonania danej metody
 *      5.1 Korzyści:
 *          - możemy wywołać metodę dopiero kiedy jej potrzebujemy, bardzo często optymalizuje nam to działanie naszej aplikacji          
 *      5.2 Minusy:
 *          - jeśli wykonamy metodę Count() a potem np. toList() iteracja wykona się dwukrotnie
 *          - trzeba uważać na wyjątki. Jeśli np. w bloku try catch mamy bezpośrednią execute, to wyjątek zostanie obsłużony
 *            jednak jeśli execute odbędzie się "na zewnątrz" try catch to dostaniemy nieobsłużony wyjątek (przykład notatki) 
 * 
 */
