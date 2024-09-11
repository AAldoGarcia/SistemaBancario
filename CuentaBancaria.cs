using SistemaBancario;

internal class CuentaBancaria
{
    private string numeroCuenta;
    private decimal saldo;
    public Cliente Propietario { get; private set; }  // Propiedad solo lectura

    public CuentaBancaria(string numeroCuenta, Cliente propietario)
    {
        this.numeroCuenta = numeroCuenta;
        this.Propietario = propietario;
        this.saldo = 0; // Saldo inicial en cero
    }

    public string NumeroCuenta { get { return numeroCuenta; } }
    public decimal Saldo { get { return saldo; } }

    public void Depositar(decimal monto)
    {
        if (monto > 0)
        {
            saldo += monto;
            Console.WriteLine($"Depósito de {monto:C} realizado con éxito. Nuevo saldo: {saldo:C}");
        }
        else
        {
            throw new ArgumentException("El monto del depósito debe ser positivo.");
        }
    }

    public void Retirar(decimal monto)
    {
        if (monto > 0 && monto <= saldo)
        {
            saldo -= monto;
            Console.WriteLine($"Retiro de {monto:C} realizado con éxito. Nuevo saldo: {saldo:C}");
        }
        else
        {
            throw new InvalidOperationException("Saldo insuficiente o monto de retiro inválido.");
        }
    }

    public void Transferir(CuentaBancaria cuentaDestino, decimal monto)
    {
        if (monto > 0 && monto <= saldo && cuentaDestino != null)
        {
            this.Retirar(monto);
            cuentaDestino.Depositar(monto);
            Console.WriteLine($"Transferencia de {monto:C} a la cuenta {cuentaDestino.NumeroCuenta} realizada con éxito.");
        }
        else
        {
            throw new InvalidOperationException("Transferencia no válida. Verifique el saldo y la cuenta destino.");
        }
    }
}
