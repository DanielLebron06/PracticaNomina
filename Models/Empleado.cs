namespace PracticaNomina.Models
{
    abstract public class Empleado
    {
        public string PrimerNombre { get; set; } = string.Empty;
        public string ApellidoPaterno { get; set; } = string.Empty;
        public string NumeroSeguroSocial { get; set; } = string.Empty;

        public abstract string Tipo { get; }
        public abstract string AtributosExtra { get; }
        public abstract string Calculo { get; }

        public abstract decimal CalcularPago();
    }
}
