using System;

namespace CalculadoraQuetzales
{
    class Program
    {
        static void Main(string[] args)
        {
            // Solicitar la cantidad al usuario
            Console.Write("Ingrese una cantidad en quetzales (entre 0 y 999.99): ");
            double cantidad = Convert.ToDouble(Console.ReadLine());

            // Descomponer la cantidad en billetes y monedas
            int billetes100 = (int)(cantidad / 100);
            cantidad %= 100;

            int billetes50 = (int)(cantidad / 50);
            cantidad %= 50;

            int billetes20 = (int)(cantidad / 20);
            cantidad %= 20;

            int billetes10 = (int)(cantidad / 10);
            cantidad %= 10;

            int billetes5 = (int)(cantidad / 5);
            cantidad %= 5;

            int monedas1 = (int)cantidad;
            cantidad -= monedas1;

            int monedas25Centavos = (int)(cantidad / 0.25);
            cantidad %= 0.25;

            int monedas1Centavo = (int)(cantidad / 0.01);

            // Mostrar los resultados
            Console.WriteLine($"Billetes de 100: {billetes100}");
            Console.WriteLine($"Billetes de 50: {billetes50}");
            Console.WriteLine($"Billetes de 20: {billetes20}");
            Console.WriteLine($"Billetes de 10: {billetes10}");
            Console.WriteLine($"Billetes de 5: {billetes5}");
            Console.WriteLine($"Monedas de 1: {monedas1}");
            Console.WriteLine($"Monedas de 25 centavos: {monedas25Centavos}");
            Console.WriteLine($"Monedas de 1 centavo: {monedas1Centavo}");
        }
    }
}

