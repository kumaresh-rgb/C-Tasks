using System;
using System.Collections.Generic;

namespace MyNamespace
{
    public class SampleClass
    {
        static Dictionary<string, List<string>> TeamAInitializeRoles()
        {
            return new Dictionary<string, List<string>>
            {
                { "Manager", new List<string> { "ProductManager1", "ProductManager2" } },
                { "Developer", new List<string> { "Developer1", "Developer2" } },
                { "SeniorDeveloper", new List<string> { "SeniorDeveloper1", "SeniorDeveloper2" } },
                { "JuniorDeveloper", new List<string> { "JuniorDeveloper1", "JuniorDeveloper2" } },
                { "QualityAnalyst", new List<string> { "QA1", "QA2" } }
            };
        }

        static Dictionary<string, List<string>> TeamBInitializeRoles()
        {
            return new Dictionary<string, List<string>>
            {
                { "Manager", new List<string> { "ProductManager3", "ProductManager4" } },
                { "Developer", new List<string> { "Developer3", "Developer4" } },
                { "SeniorDeveloper", new List<string> { "SeniorDeveloper3", "SeniorDeveloper4" } },
                { "JuniorDeveloper", new List<string> { "JuniorDeveloper3", "JuniorDeveloper4" } },
                { "QualityAnalyst", new List<string> { "QA3", "QA4" } }
            };
        }

        public static void Main()
        {
            Dictionary<string, List<string>> roles = TeamAInitializeRoles();
            Dictionary<string, List<string>> roles1 = TeamBInitializeRoles();

            try
            {
                Console.WriteLine("Enter the team (TeamA or TeamB):");
                string selectedTeam = Console.ReadLine().Trim();

                Console.WriteLine("Enter the department (DevTeam or QATeam):");
                string selectedDepartment = Console.ReadLine().Trim();

                Console.WriteLine("Enter the role to move: (Manager, Developer, SeniorDeveloper, JuniorDeveloper, QualityAnalyst)");
                string roleToMove = Console.ReadLine().Trim();

                Console.WriteLine("Enter the person's name to move:");
                string personToMove = Console.ReadLine().Trim();


                Console.WriteLine("Enter the Which Position We Want");
                int PositionToMove = int.Parse(Console.ReadLine().Trim());

                bool isValid=false;

                if (selectedTeam == "TeamA" && roles.ContainsKey(roleToMove) && roles[roleToMove].Contains(personToMove))
                {
                    roles[roleToMove].Remove(personToMove); // Remove from the current role
                    roles1[roleToMove].Add(personToMove);  // Add to related roles in TeamB
                    isValid = true;
                    Console.WriteLine($"{personToMove} moved to {selectedDepartment} in TeamB");
                }
                else if (selectedTeam == "TeamB" && roles1.ContainsKey(roleToMove) && roles1[roleToMove].Contains(personToMove))
                {
                    try
                    {
                        roles1[roleToMove].Remove(personToMove); // Remove from the current role
                        roles[roleToMove].Insert(PositionToMove, personToMove);     // Add to related roles in TeamA
                        isValid = true;
                        Console.WriteLine($"{personToMove} moved to {selectedDepartment} in {selectedTeam}");

                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message);
                        Console.WriteLine("You Entered out Length our Dictnaries");
                    }

                    
                    
                }
                else
                {
                    Console.WriteLine("Invalid team, role, or person selected.");
                }

               // 
                Console.WriteLine("Updated Roles in TeamA:");
                foreach (var role in roles)
                {
                    Console.WriteLine($"{role.Key}: {string.Join(", ", role.Value)}");
                }

                Console.WriteLine("Updated Roles in TeamB:");
                foreach (var role in roles1)
                {
                    Console.WriteLine($"{role.Key}: {string.Join(", ", role.Value)}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
