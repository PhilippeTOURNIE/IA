using System;

namespace TestZoomGps
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Test de logique floue");
            var z = new ZoomGps.ZoomGps();
            var cas1= z.DoInference(70, 35); 
            var cas2 = z.DoInference(70, 25); 
            var cas3 = z.DoInference(40, 72.5f); 
            var cas4 = z.DoInference(110, 100f);
            var cas5 = z.DoInference(160, 45f); 


            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine(" Calcul du zoom pour les cas suivant en utilisant la logique floue");
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine($"Cas 1 : Distance 70, vitesse 35 -> Zoom  :{cas1}");
            Console.WriteLine("");
            Console.WriteLine($"Cas 2 : Distance 70, vitesse 25 -> Zoom  :{cas2}");
            Console.WriteLine("");
            Console.WriteLine($"Cas 3 : Distance 40, vitesse 72.5 -> Zoom :{cas3}");
            Console.WriteLine("");
            Console.WriteLine($"Cas 4 : Distance 110, vitesse 100 -> Zoom :{cas4}");
            Console.WriteLine("");
            Console.WriteLine($"Cas 5 : Distance 160, vitesse 45 -> Zoom  :{cas5}");
            Console.WriteLine("");


            Console.ReadLine();

        }
    }
}
