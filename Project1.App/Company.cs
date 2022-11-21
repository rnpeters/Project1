using Project1.Data;
using Project1.InOut;
using Project1.Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.App
{
    public class Company
    {
        //Created to call api commands from APIClient
        static APIClient apiClient = new();

        public Company() { }

        //Lets the user decide if they want to log in or register
        public string LogInOrRegister()
        {
            string choice = IO.loginRegisterChoice();

            while (choice != "1" && choice != "2")
            {
                choice = IO.loginRegisterChoiceError();
            }
            return choice;
        }

        //If user picks loging in
        public void loginUser()
        {
            //Login
            Tuple<string, string> d = IO.login();
            string username = d.Item1;
            string password = d.Item2;

            //Check if already exists as an employee, if so then interact as employee
            if (apiClient.RunCheckLogExists(username, password, 0))
            {
                IO.LoggedIn();
                string c = "";
                do
                {
                    c = LoggedInUser(username);
                } while (c == "1" || c == "2");
            }//else check if they already exist as a manager
            else if (apiClient.RunCheckLogExists(username, password, 1))
            {
                IO.LoggedIn();

                string log = IO.financeManagerLogIn();

                while (log == "1" || log=="2" || log=="3")
                {
                    log = LoggedInManager(username, log);
                }
            }//otherwise send an error that they there username or password is incorrect
            else
            {
                IO.loginError();
            }
        }
            //IF they are logging in as an employee then give them employee options
            public string LoggedInUser(string username) {
            //lets them choose if the want to add a reimbursement, check reimbursements, or edit profile
            string c = IO.userReimbursementOptions();
            
            if (c == "1")
            {
                //make sure the amount for the reimbursemnt is an actually number
                string amount = IO.userReimbursementAmount();
                if (!Double.TryParse(amount, out double am))
                {
                    IO.userReimbursementAmountError();
                }
                else
                {
                    //add a description
                    string description = IO.userReimbursementDescript();

                    //give them the option to add a type like travel, lodging, food, other, or nothing
                    Console.WriteLine("Would you like to add a Type?");
                    Console.WriteLine("Select: Travel(1), Lodging(2), Food(3), Other(4), enter anything else to leave blank ");
                    string type = Console.ReadLine();
                    //add the ticket to the SQL DB
                    if (type == "1")
                    {
                        apiClient.RunAddTicket(username, am, description, "Travel");

                    }
                    else if (type == "2") { apiClient.RunAddTicket(username, am, description, "Lodging"); }
                    else if (type == "3") { apiClient.RunAddTicket(username, am, description, "Food"); }
                    else if (type == "4") { apiClient.RunAddTicket(username, am, description, "Other"); }
                    else { apiClient.RunAddTicket(username, am, description, ""); }

                    //give the option to add an image of the receipt
                    Console.WriteLine("Would you like to add a receipt, if so add file link otherwise enter 1:");
                    Console.WriteLine("Please write full path, ex. /Revature/Project1.reciept.jpg");
                    string option = Console.ReadLine();
                    if (option != "1")
                    {
                        apiClient.RunAddReciept(username, am, option);
                    }


                }
            }
            //if use wants to check their own reimbursements
            else if (c == "2")
            {
                //let them select among 8 options on what to show when requesting reimbuerments
                string sc = IO.userReimbursementChoice();
                if (sc == "1")
                {
                    noReimbursementsCheck(apiClient.RunGetTicketsUser(username));
                }
                else if (sc == "2")
                {
                    noReimbursementsCheck(apiClient.RunGetTicketsUserSpecific(username, "Pending"));
                }
                else if (sc == "3")
                {
                    noReimbursementsCheck(apiClient.RunGetTicketsUserSpecific(username, "Approved"));
                }
                else if (sc == "4")
                {
                    noReimbursementsCheck(apiClient.RunGetTicketsUserSpecific(username, "Denied"));
                }
                else if (sc == "5")
                {
                    noReimbursementsCheck(apiClient.RunGetTicketsByType(username, "Travel"));

                }
                else if (sc == "6")
                {
                    noReimbursementsCheck(apiClient.RunGetTicketsByType(username, "Lodging"));

                }
                else if (sc == "7")
                {
                    noReimbursementsCheck(apiClient.RunGetTicketsByType(username, "Food"));

                }
                else if (sc == "8")
                {
                    noReimbursementsCheck(apiClient.RunGetTicketsByType(username, "Other"));

                }
                else
                {
                    //read out an error if options 1-8 are not selected
                    IO.invalidInput();
                }
            }
            //give them the option to edit their profile
            else if (c == "3") {
                Console.WriteLine("What would you like to edit");
                Console.WriteLine("Name(1), address(2), Profile Picture(3)");
                string s = Console.ReadLine();
                //Handle the specific requests
                if (s == "1")
                {
                    Console.WriteLine("Enter name");
                    string name = Console.ReadLine();
                    apiClient.RunUpdateName(username,name);
                }
                else if (s == "2")
                {
                    Console.WriteLine("Enter address");
                    string address = Console.ReadLine();
                    apiClient.RunUpdateAddress(username,address);

                }
                else if (s == "3") {
                    Console.WriteLine("Enter File path:");
                    string path = Console.ReadLine();
                    apiClient.RunUpdateProfile(username,path);

                }
            }
            return c;
        }
        //log in as a manager
        public string LoggedInManager(string username, string log) {
            //give them the option to see tickets
            //option 1, to see reimbursements that need to be approved or declined
            if (log =="1") {
                List<Tickets> reimburse = apiClient.RunGetTicketsPending();
                //if there are tickets then show tickets pending otherwise show that it is up to date
                if (reimburse.Count > 0)
                {
                    IO.ManagerTickets(reimburse);

                    //Have them enter a username, if they want to exit then enter 1
                    string user = IO.selectUser();
                    if (user == "1")
                    {
                        return IO.financeManagerLogIn();
                    }
                    bool escape = false;
                    //make sure the user exists that they selected
                    while (!apiClient.RunCheckUserExists(user))
                    {
                        //if user doesnt exist exit out and try again
                        user = IO.selectUserError();
                        if (user == "1")
                        {
                            escape = true;
                            break;
                        }
                    }
                    if (escape)
                    {
                        return IO.financeManagerLogIn();
                        //continue;
                    }
                    //select the amount that cooresponds to the user selected and make sure it is valid
                    double validAmount = validateAmout();


                    //ask the manager to either accept or decline option
                    string statusUp = acceptOrDecline();
                    //update their status, if there is an error then print an error
                    if (apiClient.RunUpdate(user, validAmount, statusUp) == 0)
                    {
                        IO.errorTryAgain();
                    };
                }
                else
                {
                    //when there are no employees that have pending reimbursements
                    IO.upToDate();
                }
            }
            else {
               
                Console.WriteLine("Press 1 to get list of all users or enter the username, press 2 to exit");
                string user = Console.ReadLine();
                //if they dont press 2 proceed
                if (user!="2") {
                    //if they want list then retrieve the list from DB
                    if (user == "1")
                    {
                        List<string> users = apiClient.RunGetAll();
                        if (users.Count > 0)
                        {
                            //print out the list of users
                            foreach (string u in users)
                            {
                                Console.WriteLine(u);
                            }
                            //ask them to enter the name of the user they wish to promote or demote
                            Console.WriteLine("Enter user:");
                            string userToUpdate = Console.ReadLine();
                            PossiblePromotion(userToUpdate);
                        }
                        else
                        {
                            //print out no users if there is no users found
                            Console.WriteLine("No Users");
                        }
                    }
                    else {
                        //if they manager puts just the name of the user instead of asking for a list then find that user
                        //and give the option to promote or demote
                        PossiblePromotion(user);
                    }
                }
                    else {
                        return IO.financeManagerLogIn();
                    }
            }
            return IO.financeManagerLogIn();
        }
        //registering a new user
        public void Register()
        {
            //get username/email and password
            Tuple<string, string> reg = IO.register();
            string username = reg.Item1;
            string password = reg.Item2;
            //make sure it is not an empty username and password
            if (username.Length > 0 && password.Length > 0)
            {
                //check that the user is not alreay created
                if (!apiClient.RunCheckUserExists(username))
                {
                    apiClient.RunCreate(username, password);
                }
                else
                {
                    //if already created send an error
                    IO.registerError();
                }
            }
            else
            {
                IO.emptyInputError();
            }
        }
        //public bool checkIfUserExists(string username) {
        //    return apiClient.RunCheckUserExists(username);
        //}

        //print out the reimbursements if there are reimbursements otherise reutnr no rembursements
        public void noReimbursementsCheck(List<Tickets> list) {
            if (list.Count > 0)
            {
                IO.UserTickets(list);
            }
            else
            {
                IO.NoReimburse();
            }
        }
        //check that it is a valid number
        public double validateAmout() {
            string amm = IO.selectAmount();
            double validAmount;
            while (!double.TryParse(amm, out validAmount))
            {
                amm = IO.selectAmountError();
            }
            return validAmount;
        }
        //ask the manager to either accept or decine a reimbursement
        public string acceptOrDecline() {
            string appDec = IO.selectApproveDecline();
            while (appDec != "1" && appDec != "2")
            {
                appDec = IO.selectApproveDeclineError();
            }
            string statusUp;
            if (appDec == "1")
            {
                statusUp = "Approved";
            }
            else
            {
                statusUp = "Denied";
            }
            return statusUp;
        }
        //Promote or demote a user
        public void PossiblePromotion(string userToUpdate) {
            //ask manager if they want to promote or demote the specific user
            Console.WriteLine("Would you like to Promote(1) Or Demote(2) or Go Back(3)");
            string promotionStatus = Console.ReadLine();
            int validPromotionStatus = 0;
            //make sure it is a valid option
            while (!Int32.TryParse(promotionStatus, out validPromotionStatus) && (promotionStatus != "1" || promotionStatus != "2" || promotionStatus != "3"))
            {
                Console.WriteLine("Error, invalid number");
                Console.WriteLine("Would you like to Promote(1) Or Demote(2)");
                promotionStatus = Console.ReadLine();

            }
            //if they want to exit
            if (promotionStatus == "3") {
                return;
            }
            //remap the options to check if the option is valid and if so to promote or demote approriatly
            int checkValid;
            int updateUser;
            if (validPromotionStatus == 1)
            {
                checkValid = 0;
                updateUser = 1;
            }
            else
            {
                checkValid = 1;
                updateUser = 0;
            }
            //if options are satisfied and it is possible to promote or demote then do so
            if (apiClient.RunUserPosition(userToUpdate, checkValid))
            {
                apiClient.RunUpdateManagerStatus(userToUpdate,updateUser);
            }
            else
            {
                //send an error if the user is already in that position or if the username does not exist
                Console.WriteLine("Error, User does not exist or already in that postion");
            }
        }
    }
}
