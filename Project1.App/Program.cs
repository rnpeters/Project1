using Project1.Data;
using Project1.InOut;
using Project1.Logic;
using System;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;

namespace Project1.App {
    public class Program {

        public static void Main() {
            string connectionString = File.ReadAllText("/Revature/ConnectionStrings/PokeAppConnectionString.txt");

            IRepository repo = new SqlRepository(connectionString);
            //create a company object, this is where the company login and register proceeses are.
            Company company = new Company();

            //send a welcome
            IO.Welcome();
            while(true){
                //have the user select either loggin or register
                int selected = Int32.Parse(company.LogInOrRegister());
                if (selected == 1){
                    //go through the loggin proceess
                    company.loginUser();
                }
                else{
                    //goes through the register process
                    company.Register();
                }
            }
        }
    }
}