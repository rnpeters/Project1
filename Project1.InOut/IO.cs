using Project1.Logic;
using System.Net.NetworkInformation;
using System.Security;

namespace Project1.InOut{

    public class IO {

        public static void Welcome() {
            Console.WriteLine("Welcome");
        }
        public static void LoggedIn() {
            Console.WriteLine("Logged In!");
        }
        public static void errorTryAgain() {
            Console.WriteLine("Error, Try Again");
        }
        public static void NoReimburse() {
            Console.WriteLine("No Reimbursements");
        }
        public static void invalidInput() {
            Console.WriteLine("Invalid input try again");
        }
        public static string loginRegisterChoice() {
            Console.WriteLine("Login(1), Register(2)");
            return Console.ReadLine();
        }
        public static string loginRegisterChoiceError() {
            Console.WriteLine("Incorrect input, Try again");
            return loginRegisterChoice();
        }
        public static Tuple<string, string> login() {
            Console.Write("Username: ");
            string username = Console.ReadLine().TrimStart().TrimEnd();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            return Tuple.Create(username, password);
        }
        public static string userReimbursementOptions() {
            Console.WriteLine("Enter 1 for reimbursement, 2 to check reimbursement status, or 3 to edit account or anything else to logout");
            return Console.ReadLine();
        }
        public static string userReimbursementAmount() {
            Console.WriteLine("reimbursement:");
            Console.WriteLine("Enter amount:");
            return Console.ReadLine();
        }
        public static void userReimbursementAmountError() {
            Console.WriteLine("Amount must be a number");
        }
        public static string userReimbursementDescript()
        {
            Console.WriteLine("Enter Description");
            string description = Console.ReadLine().TrimStart().TrimEnd();
            while (description == "")
            {
                Console.WriteLine("Must have description");
                Console.WriteLine("Enter Description");
                description = Console.ReadLine().TrimStart().TrimEnd();
            }
            return description;
        }
        public static string userReimbursementChoice() {
            Console.WriteLine("reimbursement:");
            Console.WriteLine("Would you like to:");
            Console.WriteLine("See Reimbursments by: All reimbursements(1), Pending(2), Approved(3), Declined(4), Travel(5), Lodging(6), Food(7), Other(8)");
            return Console.ReadLine();
        }
        public static string financeManagerLogIn() {
            Console.WriteLine("Press 1 to get pending reimbursements, 2 to Promote/Demote users, or anything else to log out");
            return Console.ReadLine();
        }
        public static string selectUser() {
            Console.WriteLine("Select username or press 1 to go back");
            return Console.ReadLine();
        }
        public static string selectUserError() {
            Console.WriteLine("No user by that name, try again");
            return selectUser();
        }
        public static string selectAmount() {
            Console.WriteLine("Select amount for user:");
            return Console.ReadLine();
        }
        public static string selectAmountError() {
            Console.WriteLine("Not a value, try again");
            return selectAmount();
        }
        public static string selectApproveDecline() {
            Console.WriteLine("Would you like to approve(1) or decline(2) ammout:");
            return Console.ReadLine();
        }
        public static string selectApproveDeclineError() {
            Console.WriteLine("Invalid input try again");
            return selectApproveDecline();
        }
        public static void upToDate() {
            Console.WriteLine("Up to Date");
        }
        public static void loginError() {
            Console.WriteLine("Incorect username or password, Try again\n");
        }
        public static Tuple<string, string> register() {
            Console.Write("New Username: ");
            string username = Console.ReadLine().TrimStart().TrimEnd();
            Console.Write("New Password: ");
            string password = Console.ReadLine();
            return Tuple.Create(username, password);
        }
        public static void registerError() {
            Console.WriteLine("Account already created\n");
        }
        public static void emptyInputError() {
            Console.WriteLine("Both Username and Password must be filled");
        }
        public static void ManagerTickets(List<Tickets> reimburse) {
            foreach (Tickets s in reimburse)
            {
                Console.WriteLine($"Username: {s.username} Amount: {s.amount.ToString("0.00")} Description: {s.descript} Type:{s.type}");
            }
            Console.WriteLine();
        }
        public static void UserTickets(List<Tickets> tickets) {
            foreach (Tickets s in tickets)
            {
                Console.WriteLine($"Amount: {s.amount.ToString("0.00")} Description: {s.descript} Status: {s.stat} Type:{s.type}");
            }
            Console.WriteLine();

        }
        public void WriteLine(string s) {
            Console.WriteLine(s);
        }
        public string ReadLine() {
            return Console.ReadLine();
        }
       


    }

}