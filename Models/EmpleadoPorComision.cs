namespace PracticaNomina.Models
{
    public class EmpleadoPorComision : Empleado
    {
        public const decimal TarifaMinima = 0m;
        public const decimal TarifaMaxima = 1m;

        private decimal _tarifaComision;
        private decimal _ventasBrutas;

        public decimal VentasBrutas
        {
            get => _ventasBrutas;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Las ventas brutas no pueden ser negativas.");
                }

                _ventasBrutas = value;
            }
        }

        public decimal TarifaComision 
        { 
            get => _tarifaComision;
            set
            {
                if (!EsTarifaValida(value))
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        "La tarifa de comision debe ser mayor que 0 y menor que 1."
                    );
                }

                _tarifaComision = value;
            }
        }
        public override string Tipo => "Por comision";
        public override string AtributosExtra => $"""
            Ventas: RD${VentasBrutas}
            Tarifa por comision: {TarifaComision}
            """;
        public override string Calculo => $"""
            Calculo:
            {VentasBrutas} x {TarifaComision}
            """;

        public override decimal CalcularPago()
        {
            return VentasBrutas * TarifaComision;
        }

        public static bool EsTarifaValida(decimal tarifa)
        {
            return tarifa > TarifaMinima && tarifa < TarifaMaxima;
        }
    }
}
