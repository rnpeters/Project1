
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using Project1.App;
using Project1.Data;
using Project1.InOut;
using Project1.Logic;
using System.Reflection;
using System.Text;

namespace Project1.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Company_LogInOrRegister_Valid() {
            Company c = new Company();
            var stringReader = new StringReader("1");
            Console.SetIn(stringReader);

            var t = c.LogInOrRegister();
            Assert.Equal("1", t);
        }
        [Fact]
        public void Company_LogInOrRegister_InvalidTryAgain()
        {

            Company c = new Company();

            var stringReader = new StringReader("3 \n1");
            Console.SetIn(stringReader);
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            var t = c.LogInOrRegister();

            Assert.Equal("Login(1), Register(2)\r\nIncorrect input, Try again\r\nLogin(1), Register(2)\r\n", stringWriter.ToString());

            //Assert.Equal("Login(1), Register(2)\r\n", stringWriter.ToString());


        }
        [Theory]
        [InlineData("1")]
        [InlineData("2")]
        [InlineData("3")]
        [InlineData("4")]
        public void Company_LoggedInUser_Select2_Valid(string input) {

            Company company = new Company();

            var stringReader = new StringReader("2\n" + input);
            Console.SetIn(stringReader);

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var t = company.LoggedInUser(""); //testing on an imposible user
            string[] s = stringWriter.ToString().Split("\r\n");
            Assert.Equal("No Reimbursements", s[s.Length - 2]); //get last of all strings
            Assert.Equal("2", t);

        }
        [Fact]
        public void Company_LoggedInUser_Select2_Invalid(){
            Company company = new Company();

            var stringReader = new StringReader("2\n44");
            Console.SetIn(stringReader);

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var t = company.LoggedInUser(""); //testing on an imposible user
            string[] s = stringWriter.ToString().Split("\r\n");
            Assert.Equal("Invalid input try again", s[s.Length - 2]); //get last of all strings
            Assert.Equal("2", t);
        }
        [Fact]
        public void Company_LoggedInUser_invalidInput() {
            Company company = new Company();

            var stringReader = new StringReader("1\nlll");
            Console.SetIn(stringReader);

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var t = company.LoggedInUser(""); //testing on an imposible user
            string[] s = stringWriter.ToString().Split("\r\n");
            Assert.Equal("Amount must be a number", s[s.Length - 2]); //get last of all strings
            Assert.Equal("1", t);
        }
        [Fact]
        public void Company_ValidateAmount_ValidAmount() {
            Company company = new Company();

            var stringReader = new StringReader("200\n");
            Console.SetIn(stringReader);

            var t = company.validateAmout(); //testing on an imposible user
            Assert.Equal(200, t);
        }
        [Fact]
        public void Company_validateAmount_InvalidAmount()
        {
            Company company = new Company();

            var stringReader = new StringReader("abc\n123");
            Console.SetIn(stringReader);
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var t = company.validateAmout(); //testing on an imposible user
            Assert.Equal("Select amount for user:\r\nNot a value, try again\r\nSelect amount for user:\r\n", stringWriter.ToString());
            Assert.Equal(123, t);
        }
        [Theory]
        [InlineData("1","Approved")]
        [InlineData("2","Denied")]
        public void Company_acceptOrDecline_ValidAppoveDecline(string input,string check)
        {
            Company company = new Company();

            var stringReader = new StringReader(input);
            Console.SetIn(stringReader);
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var t = company.acceptOrDecline(); //testing on an imposible user
            Assert.Equal(check, t);
        }
        [Fact]
        public void Company_acceptOrDecline_InvalidAppoveDecline()
        {
            Company company = new Company();

            var stringReader = new StringReader("abc\n1");
            Console.SetIn(stringReader);
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            var t = company.acceptOrDecline(); //testing on an imposible user
            string[] s = stringWriter.ToString().Split("\r\n");
            Assert.Equal("Invalid input try again", s[s.Length - 3]);
            Assert.Equal("Approved", t);
        }
        [Fact]
        public void Company_noReimbursemensCheck_valid()
        {
            Company company = new Company();

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            List<Tickets> ticket = new();
            Tickets newTicket = new Tickets();
            newTicket.username = "Ryan";
            newTicket.amount = 100;
            newTicket.stat = "Pending";
            newTicket.descript = "Testing";
            ticket.Add(newTicket);


            company.noReimbursementsCheck(ticket); //testing on an imposible user
            string[] s = stringWriter.ToString().Split("\r\n");
            Assert.Equal($"Amount: {newTicket.amount.ToString("0.00")} Description: {newTicket.descript} Status: {newTicket.stat} Type:{newTicket.type}", s[0].ToString());
        }
        [Fact]
        public void Company_noReimbursemensCheck_Empty()
        {
            Company company = new Company();

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            List <Tickets> ticket= new();

            company.noReimbursementsCheck(ticket); //testing on an imposible user
            string[] s = stringWriter.ToString().Split("\r\n");
            Assert.Equal("No Reimbursements", s[s.Length - 2]);
        }
        [Fact]
        public void IO_LoginRegisterChoice_Valid() {
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            var stringReader = new StringReader("1");
            Console.SetIn(stringReader);
            IO.loginRegisterChoice();
            Assert.Equal("Login(1), Register(2)\r\n", stringWriter.ToString());
        }
        [Fact]
        public void IO_LoginRegisterChoice_Error() {
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            var stringReader = new StringReader("1");
            Console.SetIn(stringReader);
            IO.loginRegisterChoiceError();
            Assert.Equal("Incorrect input, Try again\r\nLogin(1), Register(2)\r\n", stringWriter.ToString());
        }
        
        [Fact]
        public void User_variables_valid() {

            User test = new User("john", "123",0);

            Assert.Equal("john", test.username);

        }
        [Fact]
        public void User_Empty() {
            User test = new();
            Assert.Null(test.username);
        }
        [Fact]
        public void Tickets_Empty() {
            Tickets t = new();
            Assert.Equal(0,t.amount);
        }
        
       
    }
}