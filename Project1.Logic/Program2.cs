/*using Project1.Data;
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
            APIClient ApiClient = new APIClient(repo);
            Company company = new Company(repo);

            ApiClient.RunGetTicketsPending();
            IO.Welcome();
            while(true){
                //login:
                //string choice = IO.loginRegisterChoice();

                //while (choice != "1" && choice != "2"){
                //    IO.loginRegisterChoiceError();
                //}
                
                string choice = cc.LogInOrRegister();

                int selected = Int32.Parse(choice);

                if (selected == 1){

                    //Login
                    Tuple<string,string> d = IO.login();
                    string username = d.Item1;
                    string password = d.Item2;

                    //Check if already exists
                    if (repo.checkLogExists(username, password, 0)){ 
                        IO.LoggedIn();
                        string c = "";
                        do{
                            c = IO.userReimbursementOptions();
                            //while(c=="1" || c=="2"){
                            if (c == "1"){
                                string amount = IO.userReimbursementAmount();
                                if (!Double.TryParse(amount, out double am)){
                                    IO.userReimbursementAmountError();
                                }
                                else{
                                    string description = IO.userReimbursementDescript();

                                    
                                    repo.addReimbursement(username, am, description);

                                }



                            }
                            else if (c == "2"){
                                string sc = IO.userReimbursementChoice();
                                if (sc == "1"){
                                    List<string> personalList = null;// repo.getReimbursementsUserAll(username);
                                    if (personalList.Count > 0) {
                                        foreach (string s in personalList) {
                                            Console.WriteLine(s);
                                        }
                                    }
                                    else {
                                        IO.NoReimburse();
                                    }
                                }
                                else if (sc == "2"){
                                    List<string> personalList = null;//repo.getReimbursementsUserSpecific(username, "Pending");
                                    if (personalList.Count > 0){
                                        foreach (string s in personalList) {
                                            Console.WriteLine(s);
                                        }
                                    }
                                    else {
                                        IO.NoReimburse();
                                    }
                                }
                                else if (sc == "3"){
                                    List<string> personalList = null;// repo.getReimbursementsUserSpecific(username, "Approved");
                                    if (personalList.Count > 0) {
                                        foreach (string s in personalList) {
                                            Console.WriteLine(s);
                                        }
                                    }
                                    else {
                                        IO.NoReimburse();
                                    }
                                }
                                else if (sc == "4"){
                                    List<string> personalList = null;// repo.getReimbursementsUserSpecific(username, "Denied");
                                    if (personalList.Count > 0) {
                                        foreach (string s in personalList) {
                                            Console.WriteLine(s);
                                        }
                                    }
                                    else {
                                        IO.NoReimburse();
                                    }
                                }
                                else{
                                    IO.invalidInput();
                                }
                            }
                        } while (c == "1" || c == "2");



                    }
                    else if (repo.checkLogExists(username, password, 1)){
                        IO.LoggedIn();

                        string log = IO.financeManagerLogIn();
                        
                        while (log == "1"){
                            //List<string> reimburse = repo.getReimbursementsPending();
                            List<Tickets> reimburse =repo.getReimbursementsPending();
                            if (reimburse.Count > 0){
                                //foreach (string s in reimburse){
                                foreach (Tickets s in reimburse) {
                                    //Console.WriteLine(s.amount);
                                    Console.WriteLine($"Username: {s.username} Amount: {s.amount} Description: {s.descript}");
                                }
                                string user = IO.selectUser();
                                if (user == "1"){
                                    log = IO.financeManagerLogIn();
                                    continue;
                                }
                                bool escape = false;
                                while (!repo.checkUserExists(user)){
                                    user = IO.selectUserError();
                                    if (user == "1"){
                                        escape = true;
                                        break;
                                    }
                                }
                                if (escape){
                                    log = IO.financeManagerLogIn();
                                    continue;
                                }
                                string amm = IO.selectAmount();
                                double validAmount;
                                while (!double.TryParse(amm, out validAmount)){
                                    amm = IO.selectAmountError();
                                }



                                string appDec = IO.selectApproveDecline();
                                while (appDec != "1" && appDec != "2"){
                                    appDec = IO.selectApproveDeclineError();
                                }
                                string statusUp;
                                if (appDec == "1"){
                                    statusUp = "Approved";
                                }
                                else{
                                    statusUp = "Denied";
                                }
                                if (repo.updateStatus(user, validAmount, statusUp) == 0){
                                    IO.errorTryAgain();
                                };
                            }
                            else{
                                IO.upToDate();
                            }
                            log = IO.financeManagerLogIn();
                        }//while(log =="1");
                    }
                    else{
                        IO.loginError();
                    }
                }
                else{
                    Tuple<string,string> reg = IO.register();
                    string username = reg.Item1;
                    string password = reg.Item2;
                    if (username.Length > 0 && password.Length > 0) {
                        if (!repo.checkUserExists(username)){
                            repo.createUser(username, password);
                        }
                        else{
                            IO.registerError();
                        }
                    }
                    else{
                        IO.emptyInputError();
                    }
                }
            }
        }
    }
}*/