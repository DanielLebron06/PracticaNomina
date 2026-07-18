using PracticaNomina.Service;
using PracticaNomina.Models;
using PracticaNomina.Vistas;

EmpleadoService service = new EmpleadoService();

bool salir = false;
int Opcion;

while (!salir)
{
    Console.Clear();
    Console.WriteLine("===== SISTEMA DE PAGOS =====");
    Console.WriteLine("1. Registrar empleado");
    Console.WriteLine("2. Ver empleados");
    Console.WriteLine("3. Actualizar empleado");
    Console.WriteLine("4. Generar reporte");
    Console.WriteLine("5. Salir");
    Console.WriteLine("\n===================");
    Console.Write("Elige una opcion: ");

    while (!int.TryParse(Console.ReadLine(), out Opcion))
    {
        Console.WriteLine("Ingrese un numero valido:");
    }

    switch (Opcion)
    {
        case 1:
            CrearEmpleado();
            break;
        case 2:
            Console.Clear();
            Console.WriteLine("Lista de empleados:\n");
            Console.WriteLine(service.VerEmpleados());
            Console.WriteLine("\nPulse una tecla para continuar");
            Console.ReadLine();
            break;
        case 3:
            ActualizarEmpleado();
            break;
        case 4:
            Console.Clear();
            Console.WriteLine("=============================================");
            Console.WriteLine("            REPORTE SEMANAL");
            Console.WriteLine("=============================================");
            Console.WriteLine(service.ReporteSemanal());
            Console.WriteLine("\nPulse una tecla para continuar");
            Console.ReadLine();
            break;
        case 5:
            Console.WriteLine("Gracias por usar. Pulse una tecla para salir");
            Console.ReadLine();
            salir = true;
            break;
        default:
            Console.WriteLine("Opcion no reconocida.");
            Console.ReadLine();
            break;
    }
}

void CrearEmpleado()
{
    int opcion;
    Console.Clear();
    Console.WriteLine("====================================");
    Console.WriteLine("       REGISTRAR EMPLEADO");
    Console.WriteLine("====================================");
    Console.WriteLine("1. Empleado Asalariado");
    Console.WriteLine("2. Empleado por Horas");
    Console.WriteLine("3. Empleado por Comision");
    Console.WriteLine("4. Empleado Asalariado por Comision");
    Console.WriteLine("5. Volver");
    Console.Write("\nElige una opcion: ");

    while (!int.TryParse(Console.ReadLine(), out opcion))
    {
        Console.WriteLine("Ingrese un numero valido:");
    }

    Empleado? nuevoEmpleado = null;

    switch (opcion)
    {
        case 1:
            nuevoEmpleado = EmpleadosUI.CapturarEmpleadoAsalariado();
            break;
        case 2:
            nuevoEmpleado = EmpleadosUI.CapturarEmpleadoPorHoras();
            break;
        case 3:
            nuevoEmpleado = EmpleadosUI.CapturarEmpleadoPorComision(service);
            break;
        case 4:
            nuevoEmpleado = EmpleadosUI.CapturarEmpleadoAsalariadoPorComision(service);
            break;
        case 5:
            return;
        default:
            Console.WriteLine("Opcion no reconocida.");
            Console.WriteLine("Pulse una tecla para volver");
            Console.ReadLine();
            return;
    }

    if (nuevoEmpleado != null)
    {
        service.AgregarEmpleado(nuevoEmpleado);
        Console.WriteLine("\nAnadido correctamente");
        Console.WriteLine("Pulse cualquier tecla para continuar...");
        Console.ReadLine();
    }
}

void ActualizarEmpleado()
{
    Console.Clear();
    Console.WriteLine("Lista de empleados:\n");
    Console.WriteLine(service.VerEmpleados());

    int CantidadEmpleados = service.CantidadEmpleados();

    if (CantidadEmpleados == 0)
    {
        Console.WriteLine("Aun no hay empleados registrados...");
        Console.WriteLine("\nPulse una tecla para continuar");
        Console.ReadLine();
        return;
    }

    Console.Write("Elige un empleado: ");
    int EmpleadoID;

    while (true)
    {
        if (int.TryParse(Console.ReadLine(), out EmpleadoID))
        {
            if (EmpleadoID > 0 && EmpleadoID <= CantidadEmpleados) break;
        }

        Console.Write("\nIngrese un numero valido> ");
    }

    Console.Clear();
    Console.WriteLine("Datos actuales:\n");
    Console.WriteLine(service.MostrarEmpleadoPorIndice(EmpleadoID));

    Console.WriteLine("\n=======Editar=======");
    Empleado EmpleadoParaEditar = service.ObtenerEmpleadoPorIndice(EmpleadoID);
    EmpleadosUI.EditarEmpleado(EmpleadoParaEditar, service);

    Console.WriteLine("\n=====CAMBIO FINAL=====\n");
    Console.WriteLine(service.MostrarEmpleadoPorIndice(EmpleadoID));
    Console.WriteLine("\nEmpleado Actualizado correctamente");
    Console.WriteLine("Pulse una tecla para continuar");
    Console.ReadLine();
}
