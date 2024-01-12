using System;
using System.Linq.Expressions;

namespace MyNamespace
{
    public class SampleClass
    {


        public static void Main()
        {
            List<string> Roles = new List<String> { "Manager", "Developer", "IT Support ", "Marketing", "senior Developer" };

            string UserEnteredRoles;
            int UserEnterPosition;
            bool ValidorNot = false;


            try
            {
                do
                {
                    Console.WriteLine("Enter your Role");
                    UserEnteredRoles = Console.ReadLine().Trim();
                    Console.WriteLine("Enter Your Position You Want");
                    UserEnterPosition = int.Parse(Console.ReadLine());
                }


                while (string.IsNullOrEmpty(UserEnteredRoles));
                {
                    try
                    {

                        if (Roles.Contains(UserEnteredRoles))
                        {
                            Roles.Remove(UserEnteredRoles);
                            Roles.Insert(UserEnterPosition, UserEnteredRoles);
                            ValidorNot = true;

                        }
                        else
                        {
                            Console.WriteLine($"Your Role : {UserEnteredRoles} Not present in Array");
                        }



                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: {0}", ex.Message);
                        Console.WriteLine($"You Entered {UserEnterPosition} Wrong Postion Array Length Our Max Array Length Is: {Roles.Count}");
                    }

                }

                if (ValidorNot)
                {
                    Console.WriteLine($"you Added this role {UserEnteredRoles} at this Position: {UserEnterPosition}");


                    //   Console.WriteLine($"Total Roles are :{Roles[i]} Toltal Length {Roles.Length}");
                    for (int j = 0; j < Roles.Count; j++)
                    {
                        Console.WriteLine($"Total Roles Position are :{j} Roles Are :{Roles[j]}");
                    }


                }



            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
            }
        }


    }
}
