/*Se debe crear una aplicación que realice el cálculo del salario mensual de los docentes por hora, los docentes de contrato fijo y los empleados administrativos. 
En el caso de los docentes por hora, se asignará una tarifa de 800 por cada hora trabajada. Asimismo, la aplicación deberá contemplar el cálculo de bonificaciones para 
los empleados administrativos y los docentes de contrato fijo, tomando en cuenta que si empleado o docente alcanzo la meta entonces se le paga un salario en caso 
contrario se le paga la mitad.*/


/*Asumiendo que el sueldo de un docente mensual es de: 179,000 pesos*/

using System;

abstract class Empleado
{
    public int IdEmpleado { get; set; }
    public decimal Sueldo { get; protected set; }

    public abstract decimal CalcularSueldo();
}

class EmpleadoPorHora : Empleado
{
    private const decimal TarifaPorHora = 800;
    private int horasTrabajadas;

    public EmpleadoPorHora(int horasTrabajadas)
    {
        if (horasTrabajadas <= 0)
        {
            throw new ArgumentException("Las horas trabajadas deben ser mayores a 0.");
        }
        this.horasTrabajadas = horasTrabajadas;
    }

    public override decimal CalcularSueldo()
    {
        Sueldo = horasTrabajadas * TarifaPorHora;
        return Sueldo;
    }
}

class EmpleadoFijo : Empleado
{
    private const decimal SueldoFijo = 179000;
    private bool metaAlcanzada;

    public EmpleadoFijo(bool metaAlcanzada)
    {
        this.metaAlcanzada = metaAlcanzada;
    }

    public override decimal CalcularSueldo()
    {
        Sueldo = metaAlcanzada ? SueldoFijo : SueldoFijo / 2;
        return Sueldo;
    }
}

class EmpleadoAdministrativo : Empleado
{
    private const decimal SueldoBase = 189000;
    private bool metaAlcanzada;

    public EmpleadoAdministrativo(bool metaAlcanzada)
    {
        this.metaAlcanzada = metaAlcanzada;
    }

    public override decimal CalcularSueldo()
    {
        Sueldo = metaAlcanzada ? SueldoBase : SueldoBase / 2;
        return Sueldo;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Seleccione el tipo de empleado:\n1. Docente por hora\n2. Docente de contrato fijo\n3. Empleado administrativo");
        int tipoEmpleado = Convert.ToInt32(Console.ReadLine());

        Empleado empleado = null;

        switch (tipoEmpleado)
        {
            case 1:
                Console.Write("Ingrese las horas trabajadas: ");
                int horasTrabajadas;
                while (!int.TryParse(Console.ReadLine(), out horasTrabajadas) || horasTrabajadas <= 0)
                {
                    Console.Write("Por favor, ingrese un número válido de horas trabajadas (mayor a 0): ");
                }
                empleado = new EmpleadoPorHora(horasTrabajadas);
                break;

            case 2:
                Console.Write("¿Alcanzó la meta? (si/no): ");
                string respuestaFijo = Console.ReadLine()?.ToLower();
                while (respuestaFijo != "si" && respuestaFijo != "no")
                {
                    Console.Write("Por favor, ingrese una respuesta válida (si/no): ");
                    respuestaFijo = Console.ReadLine()?.ToLower();
                }
                bool metaFijo = respuestaFijo == "si";
                empleado = new EmpleadoFijo(metaFijo);
                break;

            case 3:
                Console.Write("¿Alcanzó la meta? (si/no): ");
                string respuestaAdministrativo = Console.ReadLine()?.ToLower();
                while (respuestaAdministrativo != "si" && respuestaAdministrativo != "no")
                {
                    Console.Write("Por favor, ingrese una respuesta válida (si/no): ");
                    respuestaAdministrativo = Console.ReadLine()?.ToLower();
                }
                bool metaAdministrativo = respuestaAdministrativo == "si";
                empleado = new EmpleadoAdministrativo(metaAdministrativo);
                break;

            default:
                Console.WriteLine("Opción inválida.");
                return;
        }

        decimal sueldo = empleado.CalcularSueldo();
        Console.WriteLine($"El sueldo calculado del empleado es: {sueldo:C}");
    }
}

