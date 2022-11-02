using System;
namespace p1 {
    public class Program {

        public static void Main() { 
            FinanceManager.tempDb.Add(new Db("admin","123"));
            Console.WriteLine("Welcome");
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
                if (Employee.checkLogExists(username,password)) { 
                    Console.WriteLine("logged it!");
                    string c="";
                    do{
                        Console.WriteLine("Enter 1 for reimbursement, 2 to check reimbursement status or anything else to logout");
                        //Console.WriteLine("Login(1), Register(2)");
                        c = Console.ReadLine();
                        //while(c=="1" || c=="2"){
                        if(c == "1"){
                            Console.WriteLine("reimbursement:");
                            Console.WriteLine("Enter amount:");
                            string amount = Console.ReadLine();
                            if(!Double.TryParse(amount,out double am)){
                                Console.WriteLine("Amount must be a number");
                            }
                            else{
                            Console.WriteLine("Enter Description");
                            string description = Console.ReadLine().TrimStart().TrimEnd();
                            while(description == ""){
                                Console.WriteLine("Must have description");
                                Console.WriteLine("Enter Description");
                                description = Console.ReadLine().TrimStart().TrimEnd();
                            }
                            
                            FinanceManager.addReimbursement(username,am,description);
                            
                            }
                            

                            
                        }
                        else if(c == "2"){
                             Console.WriteLine("reimbursement:");
                             FinanceManager.getReimbursementsUser(username);
                        }
                    }while(c=="1" || c=="2");
            


                }
                else if (FinanceManager.checkExists(username,password)) { 
                    Console.WriteLine("logged it!");
                    
                        //Console.WriteLine("Press 1 to get pending reimbursements or anything else to log out");
                        string log; //= Console.ReadLine();
                        Console.WriteLine("Press 1 to get pending reimbursements or anything else to log out");
                        log = Console.ReadLine();
                    do{
                        //Console.WriteLine("Press 1 to get pending reimbursements or anything else to log out");
                        //log = Console.ReadLine();
                    if(FinanceManager.getReimbursementsPending()){

                    Console.WriteLine("Select username or press 1 to go back");
                    string user = Console.ReadLine();
                    if(user == "1"){
                        Console.WriteLine("Press 1 to get pending reimbursements or anything else to log out");
                        log = Console.ReadLine();
                        continue;
                    }
                    bool escape = false;
                    while(!Employee.checkUserExists(user)){
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
                    bool statusUp;
                    if(appDec =="1"){
                        statusUp =true;
                    }
                    else{
                        statusUp=false;
                    }
                    Employee.statusUpdate(user,validAmount,statusUp);
                    }
                    else{
                        Console.WriteLine("Up to date");
                        //break;
                    }
                    Console.WriteLine("Press 1 to get pending reimbursements or anything else to log out");
                    log = Console.ReadLine();
                }while(log =="1");

                    


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
                if (!Employee.checkUserExists(username) && !FinanceManager.checkExists(username,password)) { 
                    Employee.create(username,password);
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