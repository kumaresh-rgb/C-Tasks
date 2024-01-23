using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;

namespace DepartmentModuleRoleExample
{
    class Program
    {
        static void Main()
        {

            Console.WriteLine("Choose a department (1: Development, 2: Quality):");
            int department;
            while (!int.TryParse(Console.ReadLine().Trim(), out department) || (department != 1 && department != 2))
            {
                Console.WriteLine("Please Choose Department Please enter 1 or 2 for the department.");
            }

            Console.WriteLine("Choose a module (1: Admin, 2: Setup, 3: Configure, 4: Customize, 5: Render Engine ):");
            int module;
            while (!int.TryParse(Console.ReadLine().Trim(), out module) || (module < 1 || module > 5))
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 5 for the module.");
            }

            Console.WriteLine("Choose a role (1: Manager, 2: Senior Dev, 3: Junior Dev, 4: Team Lead ):");
            int role;
            while (!int.TryParse(Console.ReadLine().Trim(), out role) || (role < 1 || role > 4))
            {
                Console.WriteLine(" Please Choose a Role Please enter a number between 1 and 4 for the role.");
            }

            string connectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=Organization;Integrated Security=True";

            // Call the method to retrieve the Organization table
            DisplayOrganizationTable(connectionString);
            //    and then choose user where to move ask to user
            Console.WriteLine();
            Console.WriteLine("1: Add Member, 2: Remove Member, 3: Move Member 4:Update Member ");
            int action = int.Parse(Console.ReadLine().Trim());
            int rowsAffected;

            switch (action)
            {
                // Add Member
                case 1:
                    Console.WriteLine("Enter member Name:");
                    string memberName = Console.ReadLine().Trim();

                    Console.WriteLine("Enter Your State:");
                    string State = Console.ReadLine().Trim();

                    Console.WriteLine("Enter Your Address:");
                    string Address = Console.ReadLine().Trim();

                    Console.WriteLine("Enter Your Date of Birth:");
                    string DOB = Console.ReadLine().Trim();

                    Console.WriteLine("Enter your CountryName:");
                    string CountryName = Console.ReadLine().Trim();

                    Console.WriteLine("Enter your CountryCode:");
                    string CountryCode = Console.ReadLine().Trim();

                    Console.WriteLine("Enter your Mobile No :");
                    string MobileNo = Console.ReadLine().Trim();

                    rowsAffected = SaveInformation(department, module, role, memberName, Address, State, CountryName, CountryCode, DOB, MobileNo );
                    if (rowsAffected > 0)
                        Console.WriteLine($"{memberName} Info added successfully.");
                    else
                        Console.WriteLine("Failed to add member.");
                    break;

                // Remove Member
                case 2:
                    Console.WriteLine("Enter member ID:");
                    int memberId = int.Parse(Console.ReadLine().Trim());
                    rowsAffected = RemoveMember(memberId);
                    if (rowsAffected > 0)
                        Console.WriteLine($"Member with ID {memberId} removed successfully.");
                    else
                        Console.WriteLine($"Failed to remove member with ID {memberId}.");
                    break;

                // Move Member
                case 3:
                    Console.WriteLine("Enter member ID:");
                    int memberIdToMove = int.Parse(Console.ReadLine().Trim());

                    Console.WriteLine("List of new department (1: Development, 2: Quality):");
                    int newDepartment = int.Parse(Console.ReadLine().Trim());

                    Console.WriteLine("Choose a new module (1: Admin, 2: Setup, 3: Configure, 4: Customize, 5: Render Engine):");
                    int newModule = int.Parse(Console.ReadLine().Trim());

                    Console.WriteLine("Choose a new role (1: Manager, 2: Senior Dev, 3: Junior Dev, 4: Team Lead):");
                    int newRole = int.Parse(Console.ReadLine().Trim());

                    rowsAffected = MoveMember(connectionString, memberIdToMove, newDepartment, newModule, newRole);
                    if (rowsAffected > 0)
                        Console.WriteLine($"Member with ID {memberIdToMove} moved successfully.");
                    else
                        Console.WriteLine($"Failed to move member with ID {memberIdToMove}.");
                    break;

                // Update Member

                case 4:
                    Console.WriteLine("Enter member ID:");
                    int memberIdToUpdate = int.Parse(Console.ReadLine().Trim());

                    Console.WriteLine("Enter the field to update (MemberName, StateName, Address, CountryName, CountryCode, DOB, MobileNumber):");
                    string fieldToUpdate = Console.ReadLine().Trim();

                    Console.WriteLine($"Enter the new value for {fieldToUpdate}:");
                    string newValue = Console.ReadLine().Trim();

                    rowsAffected = UpdateMember(connectionString, memberIdToUpdate, fieldToUpdate, newValue);
                    if (rowsAffected > 0)
                        Console.WriteLine($"Member with ID {memberIdToUpdate} updated successfully.");
                    else
                        Console.WriteLine($"Failed to update member with ID {memberIdToUpdate}.");
                    break;

                default:
                    Console.WriteLine("Invalid action selected.");
                    break;

            }
        }

        static int SaveInformation(int department, int module, int role, string memberName, string Address, string State, string CountryName, string CountryCode, string DOB, string MobileNo )
        {
            string connectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=Organization;Integrated Security=True";

            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
               
                string query = "INSERT INTO Organization (DepartmentId, ModuleId, RoleId, MemberName, StateName, Address, CountryName, CountryCode, DOB, MobileNumber ) VALUES (@Department, @Module, @Role, @MemberName, @StateName, @Address, @CountryName, @CountryCode, @DOB, @MobileNo )";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Department", department);
                    command.Parameters.AddWithValue("@Module", module);
                    command.Parameters.AddWithValue("@Role", role);
                    command.Parameters.AddWithValue("@MemberName", memberName);
                    command.Parameters.AddWithValue("@StateName", State);
                    command.Parameters.AddWithValue("@Address", Address);
                    command.Parameters.AddWithValue("@CountryName", CountryName);
                    command.Parameters.AddWithValue("@CountryCode", CountryCode);
                    command.Parameters.AddWithValue("@DOB", DOB);
                    command.Parameters.AddWithValue("@MobileNo", MobileNo);
                         
                    rowsAffected = command.ExecuteNonQuery();
                }
            }

            return rowsAffected;
        }

        static int RemoveMember(int memberId)
        {
            string connectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=Organization;Integrated Security=True";

            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "DELETE FROM Organization WHERE ID = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", memberId);

                    rowsAffected = command.ExecuteNonQuery();
                }
            }

            return rowsAffected;
        }

        //MoveMember

        static int MoveMember(string connectionString, int memberId, int newDepartment, int newModule, int newRole)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "UPDATE Organization SET DepartmentId = @NewDepartment, ModuleId = @NewModule, RoleId = @NewRole WHERE ID = @MemberId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NewDepartment", newDepartment);
                    command.Parameters.AddWithValue("@NewModule", newModule);
                    command.Parameters.AddWithValue("@NewRole", newRole);
                    command.Parameters.AddWithValue("@MemberId", memberId);

                    rowsAffected = command.ExecuteNonQuery();
                }
            }

            return rowsAffected;
        }

        static int UpdateMember(string connectionString, int memberId, string fieldToUpdate, string newValue)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = $"UPDATE Organization SET {fieldToUpdate} = @NewValue WHERE ID = @MemberId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NewValue", newValue);
                    command.Parameters.AddWithValue("@MemberId", memberId);

                    rowsAffected = command.ExecuteNonQuery();
                }
            }

            return rowsAffected;
        }


        static DataTable RetrieveOrganizationTable(string connectionString)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string organizationQuery = "SELECT * FROM Organization";

                using (SqlCommand command = new SqlCommand(organizationQuery, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }

        static void DisplayOrganizationTable(string connectionString)
        {
            DataTable organizationTable = RetrieveOrganizationTable(connectionString);

            // Display the retrieved data in the console
            foreach (DataRow row in organizationTable.Rows)
            {
                foreach (DataColumn col in organizationTable.Columns)
                {
                    Console.Write($"{col.ColumnName}: {row[col]}");
                }
                Console.WriteLine();
            }
        }
    }
}
