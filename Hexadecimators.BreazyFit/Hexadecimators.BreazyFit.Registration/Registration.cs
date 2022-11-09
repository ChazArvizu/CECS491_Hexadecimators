
﻿using Microsoft.Data.SqlClient;
using Microsoft.SqlServer;
using Microsoft.Identity.Client;
using Hexadecimators.BreazyFit.Models;
using Hexadecimators.BreazyFit.SqlDataAccess;
using Microsoft.VisualBasic;

namespace Hexadecimators.BreazyFit.Registration
{


    class Registration
    {


        //Connection String to SQL server

        static string connectionString = @"Server=.\;Database=Hexadecimators.BreazyFit.Users;Integrated Security=True;Encrypt=False";
        static string logConnectionString = @"Server=.\;Database=Hexadecimators.BreazyFit.Logs;Integrated Security=True;Encrypt=False";


        static private readonly SqlDAO dao = new SqlDAO(connectionString);
        static private readonly SqlDAO logDao = new SqlDAO(logConnectionString);



        public static Boolean Validate(string email, string pass)
        {
            /* REQUIREMENTS: 
            EMAIL:
           -must be in valid email format
           -database cannot have the inputted email already

            PASSWORD:
            -must be minimum 8 chars
            -must use valid chars
            */

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




            //SELECT query for database

            string emailQuery = "SELECT [Username] FROM [Hexadecimators.BreazyFit.Users].[dbo].[Users] WHERE [Username]='" + nameChunk + "';";
            
            Result result = dao.ExecuteSql(emailQuery);
            LogModel logModel = new LogModel();
            logModel.TimeStamp= DateTime.Now;
            var logResult = logDao.LogData(logModel);





            //if email is not of valid formatting, do not create account

            if (nameChunk.Length <= 0 ||
                addressChunk.Length <= 0 ||
                addressChunk.EndsWith('.') ||
                addressChunk.IndexOf('.') == -1 ||
                addressChunk.Contains(".."))

            {

                Console.WriteLine("Invalid Email format.");
                return false;

            }



            //if SELECT query returns any rows, do not create account due to taken username

            if (dao.rowExists == true)
            {

                Console.WriteLine("Email or Username has already been taken.");
                return false;
            };


            //Password validation

            char[] invalidChars = { '{', '}', '?', '=', '[', ']', '(', ')', '#', '$', '%', '^', '<', '>', '&', '*', '.', '+', '=', '_', '|', '\'', '`', '~' };

            //check to see  valid length and if any invalid characters are used

            if (pass.Length < 7 || pass.IndexOfAny(invalidChars) != -1)
            {
                Console.WriteLine("Password is invalid.");
                return false;

            }

            return true;
        }




        public static void UserCreation(string email, string password)
        {
            try
            {


                //first half of email string taken as username

                string userUsername = email.Substring(0, email.LastIndexOf('@'));

                Console.WriteLine("Email: " + email);

                //string that SQL uses to insert values


                //validate input before executing sql

                    if (Validate(email, password) == true)
                    {
                    //pass our query string to the connection string
                    dao.ExecuteSql("Insert into Users(Email,Username,Password) Values('" + email + "','" + userUsername + "','" + password + "');");


                    Console.WriteLine("User: " + userUsername + " Account created successfully.");
                    }


            }
            catch
            {
                Console.WriteLine("error registering.");
            }


        }

        private static void Main(string[] args)
        {
            UserCreation("email1000@aol.com", "TESTRRRRR");
        }

    }



}
