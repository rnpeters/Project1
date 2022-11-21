/*
using Project1.Data;
using Project1.InOut;
using System;
namespace Project1.App {
    public class Program {

        public static void Main() { 
            string connectionString = File.ReadAllText("/Revature/ConnectionStrings/PokeAppConnectionString.txt");
            IRepository repo = new SqlRepository(connectionString);

            //FinanceManager.tempDb.Add(new Db("admin","123"));
            Console.WriteLine("Welcome");
            //IO.Welcome();
            while(true){
            //login:
            Console.WriteLine("Login(1), Register(2)");
            string choice = Console.ReadLine();

            //while(!Int32.TryParse(choice,out int selected)){
            while(choice != "1" && choice != "2"){
                Console.WriteLine("Incorrect input, Try again");
                Console.WriteLine("Login(1), Register(2)");
                choice = Console.ReadLine();
            }
            int selected = Int32.Parse(choice);

            if(selected == 1){
            
                //Login
                Console.Write("Username: ");
                string username = Console.ReadLine().TrimStart().TrimEnd();
                Console.Write("Password: ");
                string password = Console.ReadLine();

                //Check if already exists
                if (repo.checkLogExists(username,password,0)) {//Employee.checkLogExists(username,password)) { 
                    Console.WriteLine("logged it!");
                    string c="";
                        do {
                            Console.WriteLine("Enter 1 for reimbursement, 2 to check reimbursement status or anything else to logout");
                            //Console.WriteLine("Login(1), Register(2)");
                            c = Console.ReadLine();
                            //while(c=="1" || c=="2"){
                            if (c == "1") {
                                Console.WriteLine("reimbursement:");
                                Console.WriteLine("Enter amount:");
                                string amount = Console.ReadLine();
                                if (!Double.TryParse(amount, out double am)) {
                                    Console.WriteLine("Amount must be a number");
                                }
                                else {
                                    Console.WriteLine("Enter Description");
                                    string description = Console.ReadLine().TrimStart().TrimEnd();
                                    while (description == "") {
                                        Console.WriteLine("Must have description");
                                        Console.WriteLine("Enter Description");
                                        description = Console.ReadLine().TrimStart().TrimEnd();
                                    }

                                    repo.addReimbursement(username, am, description);

                                }



                            }
                            else if (c == "2") {
                                Console.WriteLine("reimbursement:");
                                Console.WriteLine("Would you like to:");
                                Console.WriteLine("See All reimbursements(1), See Pending(2), See Approved(3), See Declined(4)");
                                string sc = Console.ReadLine();
                                if (sc == "1")
                                {
                                    List<string> personalList = repo.getReimbursementsUserAll(username);
                                    foreach (string s in personalList)
                                    {
                                        Console.WriteLine(s);
                                    }
                                }
                                else if (sc == "2")
                                {
                                    List<string> personalList = repo.getReimbursementsUserSpecific(username, "Pending");
                                    foreach (string s in personalList)
                                    {
                                        Console.WriteLine(s);
                                    }
                                }
                                else if (sc == "3")
                                {
                                    List<string> personalList = repo.getReimbursementsUserSpecific(username, "Approved");
                                    foreach (string s in personalList)
                                    {
                                        Console.WriteLine(s);
                                    }
                                }
                                else if (sc == "4")
                                {
                                    List<string> personalList = repo.getReimbursementsUserSpecific(username,"Denied");
                                    foreach (string s in personalList)
                                    {
                                        Console.WriteLine(s);
                                    }
                                }
                                else {
                                    Console.WriteLine("Error, not valid input");
                                }
                        }
                    }while(c=="1" || c=="2");
            


                }
                else if (repo.checkLogExists(username,password,1)) { 
                    Console.WriteLine("logged it!");
                    
                        //Console.WriteLine("Press 1 to get pending reimbursements or anything else to log out");
                        string log; //= Console.ReadLine();
                        Console.WriteLine("Press 1 to get pending reimbursements or anything else to log out");
                        log = Console.ReadLine();
                while(log == "1") { 
                    //Console.WriteLine("Press 1 to get pending reimbursements or anything else to log out");
                    //log = Console.ReadLine();
                    List<string> reimburse= repo.getReimbursementsPending();
                    if (reimburse.Count > 0) {
                        foreach (string s in reimburse) {
                            Console.WriteLine(s);
                        }
                    Console.WriteLine("Select username or press 1 to go back");
                    string user = Console.ReadLine();
                    if(user == "1"){
                        Console.WriteLine("Press 1 to get pending reimbursements or anything else to log out");
                        log = Console.ReadLine();
                        continue;
                    }
                    bool escape = false;
                    while(!repo.checkUserExists(user)){//!Employee.checkUserExists(user)){
                        Console.WriteLine("No user by that name, try again");
                        Console.WriteLine("Select username");
                        user = Console.ReadLine();
                        if(user == "1"){
                            escape=true;
                            break;
                        }
                    }
                    if(escape){
                        Console.WriteLine("Press 1 to get pending reimbursements or anything else to log out");
                        log = Console.ReadLine();
                        continue;
                    }
                    Console.WriteLine("Select amount for user:");
                    string amm = Console.ReadLine();
                    double validAmount;
                    while(!double.TryParse(amm,out validAmount)){
                        Console.WriteLine("Error, not a value, try again \nSelect amount for user:");
                        amm = Console.ReadLine();
                    }

                    
                    // while(!Employee.checkUserExists(user)){
                    //     Console.WriteLine("No user by that name, try again");
                    //     user = Console.ReadLine();
                    // }
                    Console.WriteLine("Would you like to approve(1) or decline(2) ammout:");
                    string appDec = Console.ReadLine();
                    while(appDec !="1" && appDec!="2"){
                    Console.WriteLine("Invalid input try again, \nWould you like to approve(1) or decline(2) ammout:");
                    appDec = Console.ReadLine();
                    }
                    string statusUp;
                    if(appDec =="1"){
                        statusUp ="Approved";
                    }
                    else{
                        statusUp= "Denied";
                    }
                    if (repo.updateStatus(user, validAmount, statusUp) ==0) {
                        Console.WriteLine("Error, Try again");
                     };
                    //Employee.statusUpdate(user,validAmount,statusUp);
                    }
                    else{
                        Console.WriteLine("Up to date");
                        //break;
                    }
                    Console.WriteLine("Press 1 to get pending reimbursements or anything else to log out");
                    log = Console.ReadLine();
                }//while(log =="1");

                    


                }
                else { 
                    Console.WriteLine("Incorect username or password, Try again\n");
                }
            }
            else{
                Console.Write("New Username: ");
                string username = Console.ReadLine().TrimStart().TrimEnd();
                Console.Write("New Password: ");
                string password = Console.ReadLine();
                if (!repo.checkUserExists(username)) {//!Employee.checkUserExists(username) && !FinanceManager.checkExists(username,password)) {
                    //Employee.create(username,password);
                    repo.createUser(username,password);
                    
                }
                else { 
                    Console.WriteLine("Account already created\n");
                }

                
            }
            }

            //Console.WriteLine("Press any key to exit");
            //Console.ReadLine();
        }

    }
}
*/