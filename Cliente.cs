class Cliente
{
    private string nombre;
    private string apellido;
    private string numeroIdentificacion;
    private List<CuentaBancaria> cuentas;

    public Cliente(string nombre, string apellido, string numeroIdentificacion)
    {
        this.nombre = nombre;
        this.apellido = apellido;
        this.numeroIdentificacion = numeroIdentificacion;
        this.cuentas = new List<CuentaBancaria>();
    }

    public string Nombre { get { return nombre; } }
    public string Apellido { get { return apellido; } }
    public string NumeroIdentificacion { get { return numeroIdentificacion; } }

    public void AgregarCuenta(CuentaBancaria cuenta)
    {
        cuentas.Add(cuenta);
    }

    public List<CuentaBancaria> ObtenerCuentas()
    {
        return cuentas;
    }

    public CuentaBancaria ObtenerCuentaPorNumero(string numeroCuenta)
    {
        return cuentas.FirstOrDefault(c => c.NumeroCuenta == numeroCuenta);
    }
}
