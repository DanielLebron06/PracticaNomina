namespace PracticaNomina.Models
{
    public class EmpleadoAsalariadoPorComision : EmpleadoPorComision
    {
        private decimal _salarioBase;

        public decimal SalarioBase
        {
            get => _salarioBase;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "El salario base no puede ser negativo.");
                }

                _salarioBase = value;
            }
        }

        public override string Tipo => "Asalariado por comision";
        public override string AtributosExtra => $"""
            Salario base: {SalarioBase}
            Ventas: RD${VentasBrutas}
            Tarifa por comision: {TarifaComision}
            """;
        public override string Calculo => $"""
            Calculo:
            {VentasBrutas} x {TarifaComision}
            MAS(+):
            {SalarioBase}
            MAS(+):
            {SalarioBase} x 0.10 = {SalarioBase * 0.1m}
            """;
        public override decimal CalcularPago()
        {
            return base.CalcularPago() + SalarioBase + (SalarioBase*0.1m);
        }
        
    }
}
