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


            string gs_connectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=Organization;Integrated Security=True";
            Display_Department_Table(gs_connectionString);
          
            Console.WriteLine("Choose a department (1: Development, 2: Quality):");
            int gi_department;
            while (!int.TryParse(Console.ReadLine().Trim(), out gi_department) || (gi_department != 1 && gi_department != 2))
            {
                Console.WriteLine("Please Choose Department Please enter 1 or 2 for the department.");
            }
            Display_Module_Table(gs_connectionString);
            Console.WriteLine("Choose a module (1: Admin, 2: Setup, 3: Configure, 4: Customize, 5: Render Engine ):");
            int gi_module;
            while (!int.TryParse(Console.ReadLine().Trim(), out gi_module) || (gi_module < 1 || gi_module > 5))
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 5 for the module.");
            }
            Display_Roles_Table(gs_connectionString);
            Console.WriteLine("Choose a roles (1: Manager, 2: Senior Dev, 3: Junior Dev, 4: Team Lead ):");
            int gi_role;
            while (!int.TryParse(Console.ReadLine().Trim(), out gi_role) || (gi_role < 1 || gi_role > 4))
            {
                Console.WriteLine(" Please Choose a Role Please enter a number between 1 and 4 for the role.");
            }

            // Call the method to retrieve the Organization table
            DisplayOrganizationTable(gs_connectionString);
            //    and then choose user where to move ask to user
            Console.WriteLine();
            Console.WriteLine("1: Add Member, 2: Remove Member, 3: Move Member 4:Update Member ");
            int gi_action = int.Parse(Console.ReadLine().Trim());
            int rowsAffected;

            switch (gi_action)
            {
                // Add Member
                case 1:
                    Console.WriteLine("Enter member Name:");
                    string gs_memberName = Console.ReadLine().Trim();

                    Console.WriteLine("Enter Your State:");
                    string gs_State = Console.ReadLine().Trim();

                    Console.WriteLine("Enter Your Address:");
                    string gs_Address = Console.ReadLine().Trim();

                    Console.WriteLine("Enter Your Date of Birth:");
                    string gs_DOB = Console.ReadLine().Trim();

                    Console.WriteLine("Enter your CountryName:");
                    string gs_CountryName = Console.ReadLine().Trim();

                    Console.WriteLine("Enter your CountryCode:");
                    string gs_CountryCode = Console.ReadLine().Trim();

                    Console.WriteLine("Enter your Mobile No :");
                    string gs_MobileNo = Console.ReadLine().Trim();

             
                    rowsAffected = SaveInformation(gi_department, gi_module, gi_role, gs_memberName, gs_Address, gs_State, gs_CountryName, gs_CountryCode, gs_DOB, gs_MobileNo);
                    if (rowsAffected > 0)
                        Console.WriteLine($"{gs_memberName} Info added successfully.");
                    else
                        Console.WriteLine("Failed to add member.");
                    break;

                // Remove Member
                case 2:
                    Console.WriteLine("Enter member ID:");
                    int gi_memberId = int.Parse(Console.ReadLine().Trim());
                    rowsAffected = RemoveMember(gi_memberId);
                    if (rowsAffected > 0)
                        Console.WriteLine($"Member with ID {gi_memberId} removed successfully.");
                    else
                        Console.WriteLine($"Failed to remove member with ID {gi_memberId}.");
                    break;

                // Move Member
                case 3:
                    Console.WriteLine("Enter member ID:");
                    int gi_memberIdToMove = int.Parse(Console.ReadLine().Trim());

                    Console.WriteLine("List of new department (1: Development, 2: Quality):");
                    int gi_newDepartment = int.Parse(Console.ReadLine().Trim());

                    Console.WriteLine("Choose a new module (1: Admin, 2: Setup, 3: Configure, 4: Customize, 5: Render Engine):");
                    int gi_newModule = int.Parse(Console.ReadLine().Trim());

                    Console.WriteLine("Choose a new role (1: Manager, 2: Senior Dev, 3: Junior Dev, 4: Team Lead):");
                    int gi_newRole = int.Parse(Console.ReadLine().Trim());

                    rowsAffected = MoveMember(gs_connectionString, gi_memberIdToMove, gi_newDepartment, gi_newModule, gi_newRole);
                    if (rowsAffected > 0)
                        Console.WriteLine($"Member with ID {gi_memberIdToMove} moved successfully.");
                    else
                        Console.WriteLine($"Failed to move member with ID {gi_memberIdToMove}.");
                    break;

                // Update Member

                case 4:
                    Console.WriteLine("Enter member ID:");
                    int gi_memberIdToUpdate = int.Parse(Console.ReadLine().Trim());

                    Console.WriteLine("Enter the field to update (MemberName, StateName, Address, CountryName, CountryCode, DOB, MobileNumber):");
                    string gs_fieldToUpdate = Console.ReadLine().Trim();

                    Console.WriteLine($"Enter the new value for {gs_fieldToUpdate}:");
                    string gs_newValue = Console.ReadLine().Trim();

                    rowsAffected = UpdateMember(gs_connectionString, gi_memberIdToUpdate, gs_fieldToUpdate, gs_newValue);
                    if (rowsAffected > 0)
                        Console.WriteLine($"Member with ID {gi_memberIdToUpdate} updated successfully.");
                    else
                        Console.WriteLine($"Failed to update member with ID {gi_memberIdToUpdate}.");
                    break;

                default:
                    Console.WriteLine("Invalid action selected.");
                    break;

            }
        }

        static int SaveInformation(int gi_department, int gi_module, int gi_role, string gs_memberName, string gs_Address, string gs_State, string gs_CountryName, string gs_CountryCode, string gs_DOB, string gs_MobileNo )
        {
            string gs_connectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=Organization;Integrated Security=True";

            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(gs_connectionString))
            {
                connection.Open();
               
                string ls_query = "INSERT INTO Organization (DepartmentId, ModuleId, RoleId, MemberName, StateName, Address, CountryName, CountryCode, DOB, MobileNumber ) VALUES (@Department, @Module, @Role, @MemberName, @StateName, @Address, @CountryName, @CountryCode, @DOB, @MobileNo )";

                using (SqlCommand command = new SqlCommand(ls_query, connection))
                {
                    command.Parameters.AddWithValue("@Department", gi_department);
                    command.Parameters.AddWithValue("@Module", gi_module);
                    command.Parameters.AddWithValue("@Role", gi_role);
                    command.Parameters.AddWithValue("@MemberName", gs_memberName);
                    command.Parameters.AddWithValue("@StateName", gs_State);
                    command.Parameters.AddWithValue("@Address", gs_Address);
                    command.Parameters.AddWithValue("@CountryName", gs_CountryName);
                    command.Parameters.AddWithValue("@CountryCode", gs_CountryCode);
                    command.Parameters.AddWithValue("@DOB", gs_DOB);
                    command.Parameters.AddWithValue("@MobileNo", gs_MobileNo);
                         
                    rowsAffected = command.ExecuteNonQuery();
                }
            }

            return rowsAffected;
        }

        static int RemoveMember(int gi_memberId)
        {
            string gs_connectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=Organization;Integrated Security=True";

            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(gs_connectionString))
            {
                connection.Open();

                string ls_query = "DELETE FROM Organization WHERE ID = @ID";

                using (SqlCommand command = new SqlCommand(ls_query, connection))
                {
                    command.Parameters.AddWithValue("@ID", gi_memberId);

                    rowsAffected = command.ExecuteNonQuery();
                }
            }

            return rowsAffected;
        }

        //MoveMember

        static int MoveMember(string gs_connectionString, int gi_memberId, int gi_newDepartment, int gi_newModule, int gi_newRole)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(gs_connectionString))
            {
                connection.Open();

                string ls_query = "UPDATE Organization SET DepartmentId = @NewDepartment, ModuleId = @NewModule, RoleId = @NewRole WHERE ID = @MemberId";

                using (SqlCommand command = new SqlCommand(ls_query, connection))
                {
                    command.Parameters.AddWithValue("@NewDepartment", gi_newDepartment);
                    command.Parameters.AddWithValue("@NewModule", gi_newModule);
                    command.Parameters.AddWithValue("@NewRole", gi_newRole);
                    command.Parameters.AddWithValue("@MemberId", gi_memberId);

                    rowsAffected = command.ExecuteNonQuery();
                }
            }

            return rowsAffected;
        }

        static int UpdateMember(string gs_connectionString, int memberId, string fieldToUpdate, string newValue)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(gs_connectionString))
            {
                connection.Open();

                string ls_query = $"UPDATE Organization SET {fieldToUpdate} = @NewValue WHERE ID = @MemberId";

                using (SqlCommand command = new SqlCommand(ls_query, connection))
                {
                    command.Parameters.AddWithValue("@NewValue", newValue);
                    command.Parameters.AddWithValue("@MemberId", memberId);

                    rowsAffected = command.ExecuteNonQuery();
                }
            }

            return rowsAffected;
        }

        static DataTable RetrieveOrganizationTable(string gs_connectionString)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(gs_connectionString))
            {
                connection.Open();

                string ls_organizationQuery = "SELECT * FROM Organization";

                using (SqlCommand command = new SqlCommand(ls_organizationQuery, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }

        static void DisplayOrganizationTable(string gs_connectionString)
        {
            DataTable organizationTable = RetrieveOrganizationTable(gs_connectionString);

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

        // Retrieve Department Tables 

        static DataTable Department_Table(string gs_connectionString)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(gs_connectionString))
            {
                connection.Open();

                String gs_Department_table_query = "SELECT DepartmentID,DepartmentName FROM Department";

                using (SqlCommand command = new SqlCommand(gs_Department_table_query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }

        static void Display_Department_Table(string gs_connectionString)
        {
            DataTable Display_Table = Department_Table(gs_connectionString);

            // Display the retrieved data in the console
            foreach (DataRow row in Display_Table.Rows)
            {
                foreach (DataColumn col in Display_Table.Columns)
                {
                    Console.Write($"{col.ColumnName}: {row[col]}");
                }
                Console.WriteLine();
            }
        }

        // Retrieve Module
        static DataTable Module_Table(string gs_connectionString)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(gs_connectionString))
            {
                connection.Open();

                String gs_Department_table_query = " SELECT ModuleID,ModuleName FROM Module";

                using (SqlCommand command = new SqlCommand(gs_Department_table_query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }

        static void Display_Module_Table(string gs_connectionString)
        {
            DataTable Display_Table = Module_Table(gs_connectionString);

            // Display the retrieved data in the console
            foreach (DataRow row in Display_Table.Rows)
            {
                foreach (DataColumn col in Display_Table.Columns)
                {
                    Console.Write($"{col.ColumnName}:: {row[col]} ::");
                }
                Console.WriteLine();
            }
        }

        // Retrieve Roles Table


        // Retrieve Module
        static DataTable Roles_Table(string gs_connectionString)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(gs_connectionString))
            {
                connection.Open();

                String gs_Department_table_query = " SELECT RoleID,RoleName FROM Role";

                using (SqlCommand command = new SqlCommand(gs_Department_table_query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }

        static void Display_Roles_Table(string gs_connectionString)
        {
            DataTable Display_Table = Roles_Table(gs_connectionString);

            // Display the retrieved data in the console
            foreach (DataRow row in Display_Table.Rows)
            {
                foreach (DataColumn col in Display_Table.Columns)
                {
                    Console.Write($"{col.ColumnName}:: {row[col]} ::");
                }
                Console.WriteLine();
            }
        }


    }
}
