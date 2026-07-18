namespace PracticaNomina.Models
{
    public class EmpleadoAsalariado : Empleado
    {
        private decimal _salarioSemanal;

        public decimal SalarioSemanal
        {
            get => _salarioSemanal;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "El salario semanal no puede ser negativo.");
                }

                _salarioSemanal = value;
            }
        }

        public override string Tipo => "Asalariado";
        public override string AtributosExtra => $"Salario semanal: RD${SalarioSemanal}";
        public override string Calculo => "Calculo no necesario";
        
        public override decimal CalcularPago()
        { 
            return SalarioSemanal;
        }

    }
}
