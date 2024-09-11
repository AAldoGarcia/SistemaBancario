using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaBancario
{
    internal class Menu
    {
        List<Opcion> opciones;
        Banco banco;

        public Menu()
        {

            this.banco = new Banco();
            opciones = new List<Opcion>() {
            new Opcion("Agregar Cliente", AgregarCliente),
            new Opcion("Buscar Cliente", BuscarCliente),
            new Opcion("Crear Cuenta", CrearCuenta),
            new Opcion("Depositar", Depositar),
            new Opcion("Retirar", Retirar),
            new Opcion("Transferir", Transferir),
            new Opcion("Consultar Saldo", ConsultarSaldo),
            new Opcion("Salir", () => Environment.Exit(0))
        };

            while (true)
            {
                MostrarMenu();
                seleccionarOpcion();
            }
        }

        public void MostrarMenu()
        {
            foreach (Opcion opcion in opciones)
            {
                Console.WriteLine(opciones.IndexOf(opcion) + "." + opcion.Message);
            }
        }

        public void seleccionarOpcion()
        {
            int numOpcion = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            opciones[numOpcion].Action.Invoke();


        }


        public void SeleccionarOpcion()
        {
            int numOpcion;
            if (int.TryParse(Console.ReadLine(), out numOpcion) && numOpcion >= 0 && numOpcion < opciones.Count)
            {
                Console.Clear();
                opciones[numOpcion].Action.Invoke();
            }
            else
            {
                Console.WriteLine("Opción inválida. Intente de nuevo.");
            }
        }



        private void AgregarCliente()
        {

            Console.WriteLine("Agregar Cliente");
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();
            Console.Write("Apellido: ");
            string apellido = Console.ReadLine();
            Console.Write("Númerode Identificación: ");

            string numeroIdentificacion = Console.ReadLine();

            Cliente nuevoCliente = new Cliente(nombre, apellido, numeroIdentificacion);

            banco.AgregarCliente(nuevoCliente);
            Console.WriteLine("Cliente agregado exitosamente.");
        }

        private void BuscarCliente()
        {
            Console.WriteLine("Buscar Cliente");
            Console.Write("Número de Identificación: ");

            string numeroIdentificacion = Console.ReadLine();

            Cliente clienteEncontrado = banco.BuscarCliente(numeroIdentificacion);

            if (clienteEncontrado != null)
            {
                Console.WriteLine("Cliente encontrado:");
                Console.WriteLine($"Nombre: {clienteEncontrado.Nombre}");
                Console.WriteLine($"Apellido: {clienteEncontrado.Apellido}");
                Console.WriteLine($"Número de Identificación: {clienteEncontrado.NumeroIdentificacion}");
            }
            else
            {
                Console.WriteLine("Cliente no encontrado.");
            }
        }
        private void CrearCuenta()
        {
            Console.WriteLine("Crear Cuenta");
            Console.Write("Número de Identificación del Cliente: ");
            string numeroIdentificacionCliente = Console.ReadLine();

            Cliente cliente = banco.BuscarCliente(numeroIdentificacionCliente);
            if (cliente != null)
            {
                Console.Write("Número de Cuenta: ");
                string numeroCuenta = Console.ReadLine();

                // Verificar si la cuenta ya existe para el cliente
                if (cliente.ObtenerCuentaPorNumero(numeroCuenta) != null)
                {
                    Console.WriteLine("La cuenta ya existe para este cliente.");
                    return;
                }

                // Crear la nueva cuenta
                CuentaBancaria nuevaCuenta = new CuentaBancaria(numeroCuenta, cliente);
                cliente.AgregarCuenta(nuevaCuenta);

                Console.WriteLine("Cuenta creada exitosamente.");
            }
            else
            {
                Console.WriteLine("Cliente no encontrado.");
            }
        }


        private void Depositar()
        {
            Console.WriteLine("Depositar");
            Console.Write("Número de Cuenta: ");
            string numeroCuenta = Console.ReadLine();
            Console.Write("Monto a Depositar: ");

            if (decimal.TryParse(Console.ReadLine(), out decimal monto))
            {
                try
                {
                    banco.RealizarDeposito(numeroCuenta, monto);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error en el depósito: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Monto inválido.");
            }
        }

        private void Retirar()
        {
            Console.WriteLine("Retirar");
            Console.Write("Número de Cuenta: ");
            string numeroCuenta = Console.ReadLine();
            Console.Write("Monto a Retirar: ");

            if (decimal.TryParse(Console.ReadLine(), out decimal monto))
            {
                try
                {
                    banco.RealizarRetiro(numeroCuenta, monto);
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"Error en el retiro: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Monto Invalido");
            }


        }
        private void Transferir()
        {
            Console.WriteLine("----- Transferir -----");
            Console.Write("Número de Cuenta Origen: ");
            string numeroCuentaOrigen = Console.ReadLine();
            Console.Write("Número de Cuenta Destino: ");
            string numeroCuentaDestino = Console.ReadLine();
            Console.Write("Monto a Transferir: ");

            if (decimal.TryParse(Console.ReadLine(), out decimal monto))
            {
                banco.RealizarTransferencia(numeroCuentaOrigen, numeroCuentaDestino, monto);
            }
            else
            {
                Console.WriteLine("Monto inválido.");
            }
        }

        private void ConsultarSaldo()
        {
            Console.WriteLine("Consultar Saldo");
            Console.Write("Número de Cuenta: ");
            string numeroCuenta = Console.ReadLine();

            banco.ConsultarSaldo(numeroCuenta);
        }
       
    }
}


