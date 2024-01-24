using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Text.RegularExpressions;

namespace DepartmentModuleRoleExample
{
    class Program
    {
        static void Main()
        {
            string gs_connectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=Organization;Integrated Security=True";
            Console.WriteLine("List of department");


            Display_Department_Table(gs_connectionString);

            Console.WriteLine("Please Choose Any One Department Please enter 1 or 2 for the department.");
            int gi_department;
            while (!int.TryParse(Console.ReadLine().Trim(), out gi_department) || (gi_department != 1 && gi_department != 2))
            {
                Console.WriteLine("Please Choose Any One Department Please enter 1 or 2 for the department.");
            }
            switch (gi_department)
            {
                case 1:
                    Console.WriteLine("List Of Development Module (1: Admin, 2: Setup, 3: Configure, 4: Customize, 5: Render Engine ):");

                    break;
                case 2:
                    Console.WriteLine("List Of Quality Module (1: Admin, 2: Setup, 3: Configure, 4: Customize, 5: Render Engine ):");
                    break;
            };

            Console.WriteLine();
            Display_Module_Table(gs_connectionString);
            Console.WriteLine("Please Choose a Module number between 1 and 5 for the module.");

            int gi_module;
            while (!int.TryParse(Console.ReadLine().Trim(), out gi_module) || (gi_module < 1 || gi_module > 5))
            {
                Console.WriteLine("Please Choose a Module number between 1 and 5 for the module.");
            }


            Console.WriteLine();
            Display_Roles_Table(gs_connectionString);
            Console.WriteLine(" Please Choose a Role Please enter a number between 1 and 4 for the role.");

            int gi_role;
            while (!int.TryParse(Console.ReadLine().Trim(), out gi_role) || (gi_role < 1 || gi_role > 4))
            {
                Console.WriteLine(" Please Choose a Role Please enter a number between 1 and 4 for the role.");
            }

            switch (gi_role)
            {

                case 1:
                    // Manager 1
                    string gs_connectionString1 = @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=Organization;Integrated Security=True";
                    Console.WriteLine("List Of Mangers Members");
                    DisplayMembersByRole(gs_connectionString1, 1);
                    break;
                // 
                case 2:
                    string gs_connectionString2 = @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=Organization;Integrated Security=True";
                    Console.WriteLine("List Of Senior Developer Members");
                    DisplayMembersByRole(gs_connectionString2, 2);
                    break;
                case 3:
                    string gs_connectionString3 = @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=Organization;Integrated Security=True";
                    Console.WriteLine("List Of Junior Developer Members");
                    DisplayMembersByRole(gs_connectionString3, 3);
                    break;
                case 4:
                    string gs_connectionString4 = @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=Organization;Integrated Security=True";
                    Console.WriteLine("List Of Team Lead Developer Members");
                    DisplayMembersByRole(gs_connectionString4, 4);
                    break;
                default:
                    Console.WriteLine("Invalid Input ,Please Choose Anyone One Role");
                    break;
            }

            // Call the method to retrieve the Organization table
            //  DisplayOrganizationTable(gs_connectionString);
            //    and then choose user where to move ask to user
            Console.WriteLine();
            Console.WriteLine("1: Move Member, 2: Remove Member, 3: Add Member 4:Update Member ");
            int gi_action = int.Parse(Console.ReadLine().Trim());
            int gs_rowsAffected;

            switch (gi_action)
            {
                // Add Member

                case 3:
                    Console.WriteLine("Enter member Name:");
                    string gs_memberName = Console.ReadLine().Trim();
                    while (string.IsNullOrEmpty(gs_memberName))
                    {
                        Console.WriteLine("Invalid Input. Please type a valid Name:");
                        gs_memberName = Console.ReadLine().Trim();
                    }

                    Console.WriteLine("Enter Your State:");
                    string gs_State = Console.ReadLine().Trim();
                    while (string.IsNullOrEmpty(gs_State))
                    {
                        Console.WriteLine("Invalid Input. Please type a valid State:");
                        gs_State = Console.ReadLine().Trim();
                    }

                    Console.WriteLine("Enter Your Address:");
                    string gs_Address = Console.ReadLine().Trim();
                    while (string.IsNullOrEmpty(gs_Address))
                    {
                        Console.WriteLine("Invalid Input. Please type a valid Address:");
                        gs_Address = Console.ReadLine().Trim();
                    }

                    Console.WriteLine("Enter Your Date of Birth (yyyy-mm-dd):");
                    string gs_DOB = Console.ReadLine().Trim();
                    while (string.IsNullOrEmpty(gs_DOB))
                    {
                        Console.WriteLine("Invalid Input. Please type a valid Date Of Birth (yyyy-mm-dd):");
                        gs_DOB = Console.ReadLine().Trim();
                    }

                    Console.WriteLine("Enter your CountryName:");
                    string gs_CountryName = Console.ReadLine().Trim();
                    while (string.IsNullOrEmpty(gs_CountryName))
                    {
                        Console.WriteLine("Invalid Input. Please type a valid Country Name:");
                        gs_CountryName = Console.ReadLine().Trim();
                    }

                    Console.WriteLine("Enter your CountryCode (Ex: IND):");
                    string gs_CountryCode = Console.ReadLine().Trim();
                    while (string.IsNullOrEmpty(gs_CountryCode))
                    {
                        Console.WriteLine("Invalid Input. Please type a valid Country Code (Ex: IND):");
                        gs_CountryCode = Console.ReadLine().Trim();
                    }

                    Console.WriteLine("Enter your Mobile No (Example: 2010977360):");
                    string gs_MobileNo = Console.ReadLine().Trim();




                    gs_rowsAffected = SaveInformation(gi_department, gi_module, gi_role, gs_memberName, gs_Address, gs_State, gs_CountryName, gs_CountryCode, gs_DOB, gs_MobileNo);
                    if (gs_rowsAffected > 0)
                        Console.WriteLine($"{gs_memberName} Info added successfully.");
                    else
                        Console.WriteLine("Failed to add member.");
                    break;


                // Remove Member
                case 2:
                    Console.WriteLine("Enter member ID:");
                    int gi_memberId = int.Parse(Console.ReadLine().Trim());
                    gs_rowsAffected = RemoveMember(gi_memberId);
                    if (gs_rowsAffected > 0)
                        Console.WriteLine($"Member with ID {gi_memberId} removed successfully.");
                    else
                        Console.WriteLine($"Failed to remove member with ID {gi_memberId}.");
                    break;


                // Move Member
                case 1:
                    Console.WriteLine("Enter member ID:");
                    int gi_memberIdToMove = int.Parse(Console.ReadLine().Trim());

                    //     Console.WriteLine("List of new department (1: Development, 2: Quality):");
                    //     int gi_newDepartment = int.Parse(Console.ReadLine().Trim());

                    // Dynamic Dispalay Data
                    switch (gi_module)
                    {
                        case 1:
                            Console.WriteLine("List Of  Remaining  Module To Move ");
                            Console.WriteLine("2: Setup, 3: Configure, 4: Customize, 5: Render Engine):");
                            break;
                        case 2:
                            Console.WriteLine("List Of  Remaining  Module To Move "); ;
                            Console.WriteLine("1: Admin, 3: Configure, 4: Customize, 5: Render Engine):");

                            break;
                        case 3:
                            Console.WriteLine("List Of  Remaining  Module To Move ");
                            Console.WriteLine("1: Admin, 2: Setup, 4: Customize, 5: Render Engine):");

                            break;
                        case 4:
                            Console.WriteLine("List Of  Remaining  Module To Move ");
                            Console.WriteLine("1: Admin, 2: Setup, 3: Configure, 5 Render Engine):");
                            break;
                        case 5:
                            Console.WriteLine("List Of  Remaining  Module To Move ");
                            Console.WriteLine("1: Admin, 2: Setup, 3: Configure, 4: Customize");
                            break;
                        default:
                            Console.WriteLine("Invalid Pls Choose Module");
                            break;
                    }
                    int gi_newModule = int.Parse(Console.ReadLine().Trim());
                    Console.WriteLine();
                    switch (gi_role)
                    {
                        case 1:
                            Console.WriteLine("List Of  Remaining  Roles To Move ");
                            Console.WriteLine("2: Senior Developer, 3: Junior Developer, 4: TeamLead):");
                            break;
                        case 2:
                            Console.WriteLine("List Of  Remaining  Roles To Move "); ;
                            Console.WriteLine("1: Manager, 3: Junior Developer, 4: TeamLead):");

                            break;
                        case 3:
                            Console.WriteLine("List Of  Remaining  Roles To Move ");
                            Console.WriteLine("1: Manager, 2: Senior Developer, 4: TeamLead):");

                            break;
                        case 4:
                            Console.WriteLine("List Of  Remaining  Roles To Move ");
                            Console.WriteLine("1: Manager, 2: Senior Developer, 3:Junior Developer ");
                            break;
                       
                        default:
                            Console.WriteLine("Invalid Pls Choose Roles");
                            break;
                    }
                   


                  
                    int gi_newRole = int.Parse(Console.ReadLine().Trim());

                    gs_rowsAffected = MoveMember(gs_connectionString, gi_memberIdToMove, gi_newModule, gi_newRole);
                    if (gs_rowsAffected > 0)
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

                    gs_rowsAffected = UpdateMember(gs_connectionString, gi_memberIdToUpdate, gs_fieldToUpdate, gs_newValue);
                    if (gs_rowsAffected > 0)
                        Console.WriteLine($"Member with ID {gi_memberIdToUpdate} updated successfully.");
                    else
                        Console.WriteLine($"Failed to update member with ID {gi_memberIdToUpdate}.");
                    break;

                default:
                    Console.WriteLine("Invalid action selected.");
                    break;

            }
        }

        static int SaveInformation(int gi_department, int gi_module, int gi_role, string gs_memberName, string gs_Address, string gs_State, string gs_CountryName, string gs_CountryCode, string gs_DOB, string gs_MobileNo)
        {
            string gs_connectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=Organization;Integrated Security=True";

            int gs_rowsAffected = 0;

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

                    gs_rowsAffected = command.ExecuteNonQuery();
                }
            }

            return gs_rowsAffected;
        }

        static int RemoveMember(int gi_memberId)
        {
            string gs_connectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=Organization;Integrated Security=True";

            int gs_rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(gs_connectionString))
            {
                connection.Open();

                string ls_query = "DELETE FROM Organization WHERE ID = @ID";

                using (SqlCommand command = new SqlCommand(ls_query, connection))
                {
                    command.Parameters.AddWithValue("@ID", gi_memberId);

                    gs_rowsAffected = command.ExecuteNonQuery();
                }
            }

            return gs_rowsAffected;
        }

        //MoveMember

        static int MoveMember(string gs_connectionString, int gi_memberId,  int gi_newModule, int gi_newRole)
        {
            int gs_rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(gs_connectionString))
            {
                connection.Open();

                string ls_query = "UPDATE Organization SET  ModuleId = @NewModule, RoleId = @NewRole WHERE ID = @MemberId";

                using (SqlCommand command = new SqlCommand(ls_query, connection))
                {
                    
                    command.Parameters.AddWithValue("@NewModule", gi_newModule);
                    command.Parameters.AddWithValue("@NewRole", gi_newRole);
                    command.Parameters.AddWithValue("@MemberId", gi_memberId);

                    gs_rowsAffected = command.ExecuteNonQuery();
                }
            }

            return gs_rowsAffected;
        }

        static int UpdateMember(string gs_connectionString, int memberId, string fieldToUpdate, string newValue)
        {
            int gs_rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(gs_connectionString))
            {
                connection.Open();

                string ls_query = $"UPDATE Organization SET {fieldToUpdate} = @NewValue WHERE ID = @MemberId";

                using (SqlCommand command = new SqlCommand(ls_query, connection))
                {
                    command.Parameters.AddWithValue("@NewValue", newValue);
                    command.Parameters.AddWithValue("@MemberId", memberId);

                    gs_rowsAffected = command.ExecuteNonQuery();
                }
            }

            return gs_rowsAffected;
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

           // Initialize row number

            // Display the retrieved data in the console
            foreach (DataRow row in Display_Table.Rows)
            {
               ; // Display the row number

                foreach (DataColumn col in Display_Table.Columns)
                {
                    Console.Write($"{row[col]} "); // Display the column value
                }

                Console.WriteLine();
                // Increment row number for the next iteration
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
                    Console.Write($"{row[col]} ");
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
                    Console.Write($"{row[col]} ");
                }
                Console.WriteLine();
            }
        }

        static void DisplayMembersByRole(string connectionString, int roleId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Organization WHERE RoleId = @RoleId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RoleId", roleId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            Console.WriteLine($"Members with Role ID {roleId}:");
                            while (reader.Read())
                            {
                                Console.WriteLine($"Member ID: {reader["ID"]}, Name: {reader["MemberName"]}, Role ID: {reader["RoleId"]}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"No members found for Role ID {roleId}.");
                        }
                    }
                }
            }
        }

      

    }
}
