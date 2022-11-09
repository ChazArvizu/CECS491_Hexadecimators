
﻿using Microsoft.Data.SqlClient;
using Microsoft.SqlServer;
using Microsoft.Identity.Client;



namespace Hexadecimators.BreazyFit.Registration
{


    class Registration
    {


        //Connection String to SQL server

        static string connectionString = @"Server=.\;Database=Hexadecimators.BreazyFit.Users;Integrated Security=True;Encrypt=False";


        public static Boolean inputValidate(string email, string pass, SqlConnection conn)
        {
            /* REQUIREMENTS: 
            EMAIL:
           -must be in valid email format
           -database cannot have the inputted email already

            PASSWORD:
            -must be minimum 8 chars
            -must use valid chars
            */



            //SELECT query for database

            string emailQuery = "SELECT [Email] FROM [Hexadecimators.BreazyFit.Users].[dbo].[Users] WHERE [Email]='" + email + "';";


            var command = new SqlCommand(emailQuery, conn);
            command.ExecuteNonQuery();

            //create a reader object to return the rows selected

            SqlDataReader reader = command.ExecuteReader();

            //**Email and User Validation**

            string nameChunk;
            string addressChunk;

            //check that @ char is present only once

            if (email.IndexOf('@') > 0 && email.Count(c => c == '@') == 1)
            {

                nameChunk = email.Substring(0, email.IndexOf('@'));
                addressChunk = email.Substring(email.IndexOf('@') + 1);


            }
            else
            {
                nameChunk = "";
                addressChunk = "";
            }


            //if email is not of valid formatting, do not create account

            if (nameChunk.Length <= 0 || addressChunk.Length <= 0 || addressChunk.EndsWith('.') || addressChunk.IndexOf('.') == -1)
            {

                Console.WriteLine("Invalid Email format.");
                reader.Close();
                return false;

            }

            //   sean@gmail.co.us

            //if SELECT query returns any rows, do not create account due to taken username

            if (reader.HasRows)
            {
                //if SELECT query returns any rows, do not create account due to taken username

                Console.WriteLine("Email has already been taken.");

                reader.Close();
                return false;
            };

            reader.Close();
            return true;
        }




        public static void databaseConnection(string email, string password)
        {
            //connect to database

            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();


                //first half of email string taken as username

                string userUsername = email.Substring(0, email.LastIndexOf('@'));

                Console.WriteLine("Email: " + email);
                Console.WriteLine("User: " + userUsername);

                //string that SQL uses to insert values

                string userQuery = "Insert into Users(Email,Username,Password) Values('" + email + "','" + userUsername + "','" + password + "');";


                //validate input before executing sql

                    if (inputValidate(email, password, connection) == true)
                    {

                        //pass our query string to the connection string

                        var command = new SqlDataAdapter(userQuery, connection);
                        command.SelectCommand.ExecuteNonQuery();

                        Console.WriteLine("User: " + userUsername + "\n Account created successfully.");
                    }




            }
            catch
            {
                Console.WriteLine("error registering.");
            }
            finally
            {
                connection.Close();
            }

        }

        private static void Main(string[] args)
        {
            databaseConnection("sean02@gmail.com", "seanpass");
        }

    }
}
