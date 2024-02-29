class Program
{
    static void Main(string[] args)
    {
        string sNombre;
        string sEdad;
        string sCarrera;
        string sCarne;

        Console.Write("Ingrese su nombre: ");
        sNombre = Console.ReadLine();

        Console.Write("Ingrese su edad: ");
        sEdad = Console.ReadLine();

        Console.Write("Ingrese su carrera a la que pertenece: ");
        sCarrera = Console.ReadLine();

        Console.Write("Ingrese su numero de carne: ");
        sCarne = Console.ReadLine();

        Console.WriteLine("Mi Segundo Programa");
        Console.WriteLine("Nombre: " + sNombre);
        Console.WriteLine("Edad: " + sEdad);
        Console.WriteLine("Carrera: " + sCarrera);
        Console.WriteLine("Carne: " + sCarne);

        Console.WriteLine("Soy " + sNombre + ", tengo " + sEdad + " años y estudio la carrera de " + sCarrera + ".");
        Console.WriteLine("Mi numero de Carne es: " + sCarne);
        Console.ReadKey();
    }
}