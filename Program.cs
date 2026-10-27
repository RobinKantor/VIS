using Mysqlx.Crud;
using vis3.DTO;

namespace vis3
{
    public class Program
    {
        static void Main(string[] args)
        {
            DbInitializer.Initialize();

            OrderService service = new OrderService();
            UnitOfWork unitOfWork = new UnitOfWork();

            Console.WriteLine("Jde o osobu nebo firmu? (Zadejte 'o' nebo 'f'): ");
            string input = Console.ReadLine();

            CustomerDto customer;
            if (input != null && input.ToUpper() == "O")
            {
                customer = HandlePersonCustomer(service, unitOfWork);
            }
            else
            {
                customer = HandleCompanyCustomer(service, unitOfWork);
            }

            service.SaveChanges(unitOfWork);

            Console.Write("Zadejte šířku: ");
            if (!int.TryParse(Console.ReadLine(), out int width))
            {
                Console.WriteLine("Neplatný vstup! Musíte zadat celé číslo.");
            }

            Console.Write("Zadejte výšku: ");
            if (!int.TryParse(Console.ReadLine(), out int height))
            {
                Console.WriteLine("Neplatný vstup! Musíte zadat celé číslo.");
            }

            Console.Write("Zadejte hloubku: ");
            if (!int.TryParse(Console.ReadLine(), out int depth))
            {
                Console.WriteLine("Neplatný vstup! Musíte zadat celé číslo.");
            }

            Console.Write("Materiál: ");
            string material = Console.ReadLine();

            Console.Write("Poznámka: ");
            string note = Console.ReadLine();

            try
            {
                OrderDto order = service.CreateOrder(customer.CustomerId, width, height, depth, material, note, unitOfWork);

                Console.WriteLine();
                service.PrintOrder(order.OrderId);

                Console.WriteLine("Chcete objednávku potvrdit? (A/N): ");
                string response = Console.ReadLine();

                if (response != null && response.ToUpper() == "A")
                {
                    service.ConfirmOrder(order.OrderId, unitOfWork);
                    Console.WriteLine("Objednávka byla úspěšně potvrzena.");
                }
                else
                {
                    Console.WriteLine("Potvrzení objednávky bylo zrušeno.");
                }

                Console.WriteLine("Ukládám změny...");
                service.SaveChanges(unitOfWork);
                Console.WriteLine("Změny uloženy");

                Console.WriteLine("\n--- Přehled všech objednávek ---");
                service.ListOrders();
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
                Console.WriteLine($"Chyba při zpracování objednávky: {ex.Message}");
            }
        }

        static CustomerDto HandlePersonCustomer(OrderService service, UnitOfWork unitOfWork)
        {
            Console.WriteLine("Zadejte jméno: ");
            string firstName = Console.ReadLine();
            Console.WriteLine("Zadejte příjmení: ");
            string lastName = Console.ReadLine();
            Console.WriteLine("Zadejte email: ");
            string email = Console.ReadLine();
            Console.WriteLine("Zadejte telefon: ");
            string phone = Console.ReadLine();
            Console.WriteLine("Zadejte adresu: ");
            string address = Console.ReadLine();
            Console.WriteLine("Zadejte datum narození (YYYY-MM-DD): ");
            DateTime? birthDate = null;
            if (DateTime.TryParse(Console.ReadLine(), out DateTime parsedDate))
            {
                birthDate = parsedDate;
            }

            CustomerDto personCustomer = service.CreatePersonCustomer(
                firstName,
                lastName,
                birthDate,
                email,
                phone,
                address,
                unitOfWork
                );

            return personCustomer;
        }

        static CustomerDto HandleCompanyCustomer(OrderService service, UnitOfWork unitOfWork)
        {
            Console.WriteLine("Zadejte jméno firmy: ");
            string companyName = Console.ReadLine();
            Console.WriteLine("Zadejte ico: ");
            string ico = Console.ReadLine();
            Console.WriteLine("Zadejte dic: ");
            string dic = Console.ReadLine();
            Console.WriteLine("Zadejte kontaktni osobu: ");
            string contactPerson = Console.ReadLine();
            Console.WriteLine("Zadejte email: ");
            string email = Console.ReadLine();
            Console.WriteLine("Zadejte telefon: ");
            string phone = Console.ReadLine();
            Console.WriteLine("Zadejte adresu: ");
            string address = Console.ReadLine();

            CustomerDto personCustomer = service.CreateCompanyCustomer(
                companyName,
                ico,
                dic,
                contactPerson,
                email,
                phone,
                address,
                unitOfWork
                );

            return personCustomer;
        }
    }
}
