using PracticaNomina.Models;

namespace PracticaNomina.Service
{
    public class EmpleadoService
    {
        private List<Empleado> Empleados = new List<Empleado>();

        public EmpleadoService()
        {
            Empleados.Add(new EmpleadoAsalariado
            {
                PrimerNombre = "Juan",
                ApellidoPaterno = "Perez",
                NumeroSeguroSocial = "001-0000001-1",
                SalarioSemanal = 15000m
            });

            Empleados.Add(new EmpleadoAsalariado
            {
                PrimerNombre = "Maria",
                ApellidoPaterno = "Gomez",
                NumeroSeguroSocial = "001-0000002-2",
                SalarioSemanal = 22000m
            });

            Empleados.Add(new EmpleadoPorHoras
            {
                PrimerNombre = "Ana",
                ApellidoPaterno = "Lopez",
                NumeroSeguroSocial = "001-0000003-3",
                SueldoPorHora = 200m,
                HorasTrabajadas = 35
            });

            Empleados.Add(new EmpleadoPorHoras
            {
                PrimerNombre = "Carlos",
                ApellidoPaterno = "Martinez",
                NumeroSeguroSocial = "001-0000004-4",
                SueldoPorHora = 300m,
                HorasTrabajadas = 45
            });

            Empleados.Add(new EmpleadoPorHoras
            {
                PrimerNombre = "Luis",
                ApellidoPaterno = "Fernandez",
                NumeroSeguroSocial = "001-0000005-5",
                SueldoPorHora = 180m,
                HorasTrabajadas = 50
            });

            Empleados.Add(new EmpleadoPorComision
            {
                PrimerNombre = "Sofia",
                ApellidoPaterno = "Ramirez",
                NumeroSeguroSocial = "001-0000006-6",
                VentasBrutas = 100000m,
                TarifaComision = 0.10m
            });

            Empleados.Add(new EmpleadoPorComision
            {
                PrimerNombre = "Pedro",
                ApellidoPaterno = "Diaz",
                NumeroSeguroSocial = "001-0000007-7",
                VentasBrutas = 75000m,
                TarifaComision = 0.08m
            });

            Empleados.Add(new EmpleadoAsalariadoPorComision
            {
                PrimerNombre = "Laura",
                ApellidoPaterno = "Hernandez",
                NumeroSeguroSocial = "001-0000008-8",
                VentasBrutas = 120000m,
                TarifaComision = 0.12m,
                SalarioBase = 8000m
            });

            Empleados.Add(new EmpleadoAsalariadoPorComision
            {
                PrimerNombre = "Miguel",
                ApellidoPaterno = "Castillo",
                NumeroSeguroSocial = "001-0000009-9",
                VentasBrutas = 95000m,
                TarifaComision = 0.09m,
                SalarioBase = 10000m
            });

            Empleados.Add(new EmpleadoAsalariado
            {
                PrimerNombre = "Elena",
                ApellidoPaterno = "Torres",
                NumeroSeguroSocial = "001-0000010-0",
                SalarioSemanal = 18000m
            });
        }

        public void AgregarEmpleado(Empleado empleado)
        {
            Empleados.Add(empleado);
        }

        public string VerEmpleados()
        {
            string EmpleadosLista = "";
            int i = 0;

            foreach (Empleado e in Empleados)
            {
                i++;
                EmpleadosLista += MostrarEmpleado(e, i);
            }

            return EmpleadosLista;
        }

        public string ReporteSemanal()
        {
            string Reporte = "";

            foreach (Empleado e in Empleados)
            {
                Reporte += $"""

                Empleado: {e.PrimerNombre} {e.ApellidoPaterno}
                Tipo: {e.Tipo}
                Seguridad Social: {e.NumeroSeguroSocial}

                {e.AtributosExtra}

                {e.Calculo}

                Pago final: {e.CalcularPago()}
                =============================================
                """;
            }

            Reporte += "\n";
            return Reporte;
        }

        public Empleado ObtenerEmpleadoPorIndice(int Indice)
        {
            return Empleados[Indice - 1];
        }

        public string MostrarEmpleadoPorIndice(int Indice)
        {
            Empleado emp = Empleados[Indice - 1];
            return MostrarEmpleado(emp, Indice);
        }

        public int CantidadEmpleados()
        {
            return Empleados.Count();
        }

        public bool VerificarYAsignarTarifa(EmpleadoPorComision empleado, decimal tarifa)
        {
            if (!EmpleadoPorComision.EsTarifaValida(tarifa))
            {
                return false;
            }

            empleado.TarifaComision = tarifa;
            return true;
        }

        private string MostrarEmpleado(Empleado emp, int indice)
        {
            return $"""
                {indice}- Nombre: {emp.PrimerNombre} {emp.ApellidoPaterno}
                Tipo: {emp.Tipo}

                Pago final: {emp.CalcularPago()}
                -----------------------------
                """;
        }
    }
}
