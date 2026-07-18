using PracticaNomina.Models;
using PracticaNomina.Service;

namespace PracticaNomina.Vistas
{
    public class EmpleadosUI
    {
        public static void CapturarDatosBase(Empleado empleado)
        {
            empleado.PrimerNombre = LeerTextoObligatorio("Nombre");
            empleado.ApellidoPaterno = LeerTextoObligatorio("Apellido");
            empleado.NumeroSeguroSocial = LeerTextoObligatorio("Seguro Social");
        }

        public static EmpleadoAsalariado CapturarEmpleadoAsalariado()
        {
            var emp = new EmpleadoAsalariado();
            CapturarDatosBase(emp);
            emp.SalarioSemanal = LeerDecimalNoNegativo("Salario semanal");

            return emp;
        }

        public static EmpleadoPorHoras CapturarEmpleadoPorHoras()
        {
            var emp = new EmpleadoPorHoras();
            CapturarDatosBase(emp);
            emp.SueldoPorHora = LeerDecimalNoNegativo("Sueldo por hora");
            emp.HorasTrabajadas = LeerDecimalNoNegativo("Horas trabajadas");

            return emp;
        }

        public static EmpleadoPorComision CapturarEmpleadoPorComision(EmpleadoService servicio)
        {
            var emp = new EmpleadoPorComision();
            CapturarDatosBase(emp);
            emp.VentasBrutas = LeerDecimalNoNegativo("Ventas brutas");
            emp.TarifaComision = LeerTarifaComision(servicio, emp);

            return emp;
        }

        public static EmpleadoAsalariadoPorComision CapturarEmpleadoAsalariadoPorComision(EmpleadoService servicio)
        {
            var emp = new EmpleadoAsalariadoPorComision();
            CapturarDatosBase(emp);
            emp.SalarioBase = LeerDecimalNoNegativo("Salario base");
            emp.VentasBrutas = LeerDecimalNoNegativo("Ventas brutas");
            emp.TarifaComision = LeerTarifaComision(servicio, emp);

            return emp;
        }

        public static void EditarEmpleado(Empleado empleado, EmpleadoService servicio)
        {
            switch (empleado)
            {
                case EmpleadoAsalariadoPorComision emp:
                    EditarEmpleadoAsalariadoPorComision(emp, servicio);
                    break;
                case EmpleadoPorComision emp:
                    EditarEmpleadoPorComision(emp, servicio);
                    break;
                case EmpleadoPorHoras emp:
                    EditarEmpleadoPorHoras(emp);
                    break;
                case EmpleadoAsalariado emp:
                    EditarEmpleadoAsalariado(emp);
                    break;
                default:
                    Console.WriteLine("No existe editor para este tipo de empleado.");
                    break;
            }
        }

        private static void EditarDatosBase(Empleado empleado)
        {
            Console.WriteLine("\n--- Editar Datos Personales (Presione Enter para no cambiar) ---");
            empleado.PrimerNombre = LeerTextoOpcional("Nombre", empleado.PrimerNombre);
            empleado.ApellidoPaterno = LeerTextoOpcional("Apellido", empleado.ApellidoPaterno);
            empleado.NumeroSeguroSocial = LeerTextoOpcional("Seguro Social", empleado.NumeroSeguroSocial);
        }

        private static void EditarEmpleadoAsalariado(EmpleadoAsalariado emp)
        {
            EditarDatosBase(emp);
            Console.WriteLine("\n--- Editar Datos Financieros ---");
            emp.SalarioSemanal = LeerDecimalNoNegativoOpcional("Salario semanal", emp.SalarioSemanal);
        }

        private static void EditarEmpleadoPorHoras(EmpleadoPorHoras emp)
        {
            EditarDatosBase(emp);
            Console.WriteLine("\n--- Editar Datos Financieros ---");
            emp.SueldoPorHora = LeerDecimalNoNegativoOpcional("Sueldo por hora", emp.SueldoPorHora);
            emp.HorasTrabajadas = LeerDecimalNoNegativoOpcional("Horas trabajadas", emp.HorasTrabajadas);
        }

        private static void EditarEmpleadoPorComision(EmpleadoPorComision emp, EmpleadoService servicio)
        {
            EditarDatosBase(emp);
            Console.WriteLine("\n--- Editar Datos Financieros ---");
            emp.VentasBrutas = LeerDecimalNoNegativoOpcional("Ventas brutas", emp.VentasBrutas);
            emp.TarifaComision = LeerTarifaComisionOpcional(servicio, emp, emp.TarifaComision);
        }

        private static void EditarEmpleadoAsalariadoPorComision(EmpleadoAsalariadoPorComision emp, EmpleadoService servicio)
        {
            EditarDatosBase(emp);
            Console.WriteLine("\n--- Editar Datos Financieros ---");
            emp.SalarioBase = LeerDecimalNoNegativoOpcional("Salario base", emp.SalarioBase);
            emp.VentasBrutas = LeerDecimalNoNegativoOpcional("Ventas brutas", emp.VentasBrutas);
            emp.TarifaComision = LeerTarifaComisionOpcional(servicio, emp, emp.TarifaComision);
        }

        private static string LeerTextoObligatorio(string mensaje)
        {
            while (true)
            {
                Console.Write($"{mensaje}: ");
                string? entrada = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(entrada))
                {
                    return entrada.Trim();
                }

                Console.WriteLine("Error: este campo no puede estar vacio.");
            }
        }

        private static string LeerTextoOpcional(string mensaje, string valorActual)
        {
            Console.Write($"{mensaje} [{valorActual}]: ");
            string? entrada = Console.ReadLine();

            return string.IsNullOrWhiteSpace(entrada) ? valorActual : entrada.Trim();
        }

        private static decimal LeerDecimalNoNegativo(string mensaje)
        {
            while (true)
            {
                Console.Write($"{mensaje}: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal valor) && valor >= 0)
                {
                    return valor;
                }

                Console.WriteLine("Error: ingrese un numero mayor o igual a 0.");
            }
        }

        private static decimal LeerDecimalNoNegativoOpcional(string mensaje, decimal valorActual)
        {
            while (true)
            {
                Console.Write($"{mensaje} [{valorActual}]: ");
                string? entrada = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(entrada))
                {
                    return valorActual;
                }

                if (decimal.TryParse(entrada, out decimal valor) && valor >= 0)
                {
                    return valor;
                }

                Console.WriteLine("Error: ingrese un numero mayor o igual a 0.");
            }
        }

        private static decimal LeerTarifaComision(EmpleadoService servicio, EmpleadoPorComision empleado)
        {
            Console.WriteLine("Tarifa de la comision");
            Console.WriteLine("Use un numero mayor que 0 y menor que 1. Ejemplo: 24% = 0.24");

            while (true)
            {
                Console.Write("Ingrese la tarifa: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal tarifa)
                    && servicio.VerificarYAsignarTarifa(empleado, tarifa))
                {
                    return tarifa;
                }

                Console.WriteLine("Error: la tarifa debe ser mayor que 0 y menor que 1.");
            }
        }

        private static decimal LeerTarifaComisionOpcional(EmpleadoService servicio, EmpleadoPorComision empleado, decimal valorActual)
        {
            Console.WriteLine("Tarifa de la comision");
            Console.WriteLine("Use un numero mayor que 0 y menor que 1. Ejemplo: 24% = 0.24");

            while (true)
            {
                Console.Write($"Tarifa de la comision [{valorActual}]: ");
                string? entrada = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(entrada))
                {
                    return valorActual;
                }

                if (decimal.TryParse(entrada, out decimal tarifa)
                    && servicio.VerificarYAsignarTarifa(empleado, tarifa))
                {
                    return tarifa;
                }

                Console.WriteLine("Error: la tarifa debe ser mayor que 0 y menor que 1.");
            }
        }
    }
}
