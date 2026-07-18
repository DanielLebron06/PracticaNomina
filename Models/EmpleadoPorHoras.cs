namespace PracticaNomina.Models
{
    public class EmpleadoPorHoras : Empleado
    {
        private decimal _sueldoPorHora;
        private decimal _horasTrabajadas;

        public decimal SueldoPorHora
        {
            get => _sueldoPorHora;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "El sueldo por hora no puede ser negativo.");
                }

                _sueldoPorHora = value;
            }
        }

        public decimal HorasTrabajadas
        {
            get => _horasTrabajadas;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Las horas trabajadas no pueden ser negativas.");
                }

                _horasTrabajadas = value;
            }
        }

        public override string Tipo => "Por horas";
        public override string AtributosExtra => $"""
            Horas trabajadas: {HorasTrabajadas}
            
            Sueldo por hora: {SueldoPorHora}
            """;

        public override string Calculo
        {
            get
            {
                if (HorasTrabajadas <= 40)
                {
                    return $"{SueldoPorHora} x {HorasTrabajadas}";
                }
                return $"""
                Pago comun:
                ({SueldoPorHora} x 40) = {SueldoPorHora * 40}
                MAS(+): 
                Horas extras:
                ({SueldoPorHora} x ({HorasTrabajadas - 40} x 1.5))
                = {(SueldoPorHora * 1.5m * (HorasTrabajadas - 40))}
                """;
            }
        }
        public override decimal CalcularPago()
        {
            if (HorasTrabajadas <= 40)
            {
                return SueldoPorHora * HorasTrabajadas;
            }

            return (SueldoPorHora * 40) + (SueldoPorHora * 1.5m * (HorasTrabajadas - 40));
        }
                      
    }
}
