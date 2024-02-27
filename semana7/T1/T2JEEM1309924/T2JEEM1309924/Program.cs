class Program
{
    static void Main(string[] args)
    {
        // Solicitar los valores al usuario
        Console.Write("Ingresa la velocidad inicial (V0, en m/s): ");
        double v0 = Convert.ToDouble(Console.ReadLine());

        Console.Write("Ingresa la aceleración (a, en m/s^2): ");
        double a = Convert.ToDouble(Console.ReadLine());

        Console.Write("Ingresa el tiempo (t, en segundos): ");
        double t = Convert.ToDouble(Console.ReadLine());

        // Calcular la velocidad final
        double vf = v0 + a * t;

        // Mostrar el resultado
        Console.WriteLine($"La velocidad final es: {vf} m/s");
    }
}