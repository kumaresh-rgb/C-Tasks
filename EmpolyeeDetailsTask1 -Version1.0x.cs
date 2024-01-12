

public class HelloWorld
{
    public static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Welcome to the Employee Portal!");

            bool addMoreEmployees = true;
            int employeeCount = 0;

            while (addMoreEmployees)
            {
                string name = "";
                int age = 0;
                string position = "";
                double salary = 0;

                // Get employee name
                do
                {
                    Console.Write("Enter employee name: ");
                    name = Console.ReadLine().Trim();
                } while (string.IsNullOrEmpty(name));

                // Get employee age
                bool isValidAge = false;
                do
                {
                    Console.Write("Enter employee age: ");
                    string ageInput = Console.ReadLine().Trim();
                    try
                    {
                        age = Convert.ToInt32(ageInput);
                        isValidAge = true;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Please enter a valid age (numeric value).");
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("The entered value is too large or too small for an integer.");
                    }
                } while (!isValidAge);

                // Get employee position
                do
                {
                    Console.Write("Enter employee position: ");
                    position = Console.ReadLine().Trim();
                } while (string.IsNullOrEmpty(position));

                // Get employee salary
                bool isValidSalary = false;
                do
                {
                    Console.Write("Enter employee salary: ");
                    string salaryInput = Console.ReadLine().Trim();
                    try
                    {
                        salary = Convert.ToDouble(salaryInput);
                        isValidSalary = true;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Please enter a valid salary (numeric value).");
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("The entered value is too large or too small for a double.");
                    }
                } while (!isValidSalary);

                // Generate employee ID
                employeeCount++;
                string employeeID = $"BHC-{employeeCount:D3}";

                // Display entered employee information including ID
                Console.WriteLine("\nEmployee Information:");
                Console.WriteLine($"Employee ID: {employeeID}");
                Console.WriteLine($"Name: {name}");
                Console.WriteLine($"Age: {age}");
                Console.WriteLine($"Position: {position}");
                Console.WriteLine($"Salary: {salary:C}");

                // Check if user wants to add more employees
                Console.Write("\nDo you want to add another employee? (yes/no): ");
                string addMore = Console.ReadLine().Trim().ToLower();
               
                addMoreEmployees = addMore == "yes" ;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
