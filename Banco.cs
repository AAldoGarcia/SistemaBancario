internal class Banco
{
    private List<Cliente> clientes;

    public Banco()
    {
        this.clientes = new List<Cliente>();
    }

    public void AgregarCliente(Cliente cliente)
    {
        clientes.Add(cliente);
    }

    public Cliente BuscarCliente(string numeroIdentificacion)
    {
        return clientes.Find(c => c.NumeroIdentificacion == numeroIdentificacion);
    }

    private CuentaBancaria BuscarCuenta(string numeroCuenta)
    {
        foreach (var cliente in clientes)
        {
            var cuenta = cliente.ObtenerCuentaPorNumero(numeroCuenta);
            if (cuenta != null)
            {
                return cuenta;
            }
        }
        return null;
    }

    public void RealizarDeposito(string numeroCuenta, decimal monto)
    {
        CuentaBancaria cuenta = BuscarCuenta(numeroCuenta);
        if (cuenta != null)
        {
            cuenta.Depositar(monto);
        }
        else
        {
            Console.WriteLine("Cuenta no encontrada.");
        }
    }

    public void RealizarRetiro(string numeroCuenta, decimal monto)
    {
        CuentaBancaria cuenta = BuscarCuenta(numeroCuenta);
        if (cuenta != null)
        {
            cuenta.Retirar(monto);
        }
        else
        {
            Console.WriteLine("Cuenta no encontrada.");
        }
    }

    public void ConsultarSaldo(string numeroCuenta)
    {
        CuentaBancaria cuenta = BuscarCuenta(numeroCuenta);
        if (cuenta != null)
        {
            Console.WriteLine($"Saldo de la cuenta {numeroCuenta}: {cuenta.Saldo:C}");
        }
        else
        {
            Console.WriteLine("Cuenta no encontrada.");
        }
    }

    public void RealizarTransferencia(string numeroCuentaOrigen, string numeroCuentaDestino, decimal monto)
    {
        CuentaBancaria cuentaOrigen = BuscarCuenta(numeroCuentaOrigen);
        CuentaBancaria cuentaDestino = BuscarCuenta(numeroCuentaDestino);

        if (cuentaOrigen != null && cuentaDestino != null)
        {
            if (cuentaOrigen.Propietario == cuentaDestino.Propietario)
            {
                cuentaOrigen.Transferir(cuentaDestino, monto);
            }
            else
            {
                Console.WriteLine("Las cuentas deben pertenecer al mismo cliente para realizar una transferencia.");
            }
        }
        else
        {
            Console.WriteLine("Una o ambas cuentas no fueron encontradas.");
        }
    }
}

