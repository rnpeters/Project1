using Project1.Data;
using Project1.InOut;
using Project1.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Project1.App
{
    public class APIClient
    {
        static HttpClient client = new HttpClient();
        //public IRepository repo = null;
        public APIClient() {
            //this.repo = "s";
            client.BaseAddress = new Uri("https://localhost:7149");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public List<Tickets> RunGetTicketsPending() {
            return RunAsyncGetTicketsPending().GetAwaiter().GetResult();
        }
        public List<Tickets> RunGetTicketsUser(string username) {
            return RunAsyncGetTicketsUser(username).GetAwaiter().GetResult();
        }
        public List<Tickets> RunGetTicketsUserSpecific(string username, string stat) {
            return RunAsyncGetTicketsUserSpecific(username, stat).GetAwaiter().GetResult();
        }
        public bool RunCheckLogExists(string username, string password, int managerStatus) {
            return RunAsyncCheckLogExists(username, password, managerStatus).GetAwaiter().GetResult();
        }
        public bool RunCheckUserExists(string username) {
            return RunAsyncCheckUserExists(username).GetAwaiter().GetResult();
        }
        public void RunAddTicket(string username, double amount, string descript, string type) {
            RunAsyncAddTickets(username, amount, descript, type).GetAwaiter().GetResult();
        }
        public int RunUpdate(string username, double amount, string stat) {
            return RunAsyncUpdate(username, amount, stat).GetAwaiter().GetResult();
        }
        public void RunCreate(string username, string password) {
            RunAsyncCreate(username, password).GetAwaiter().GetResult();
        }
        public List<string> RunGetAll() {
            return RunAsyncGetAll().GetAwaiter().GetResult();
        }
        public bool RunUserPosition(string username, int managerStatus) {
            return RunAsyncUserPosition(username, managerStatus).GetAwaiter().GetResult();
        }
        public void RunUpdateManagerStatus(string username, int managerStatus) {
            RunAsyncUpdateManagerStatus(username, managerStatus).GetAwaiter().GetResult();
        }
        public List<Tickets> RunGetTicketsByType(string username, string type) {
            return RunAsyncGetTicketsByType(username, type).GetAwaiter().GetResult();
        }
        public void RunAddReceipt(string username, double amount, string file) {
            RunAsyncAddReceipt(username, amount, file).GetAwaiter().GetResult();
        }
        public void RunUpdateName(string username, string name) {
            RunAsyncUpdateName(username, name).GetAwaiter().GetResult();
        }
        public void RunUpdateAddress(string username, string address) {
            UpdateAddress(username, address).GetAwaiter().GetResult();
        }
        public void RunUpdateProfile(string username, string file) {
            UpdateImage(username, file).GetAwaiter().GetResult();
        }
        public void RunGetImageTicket(string username, double amount, string file) {
            GetImageTicket(username,amount,file).GetAwaiter().GetResult();
        }
        public void RunGetImageUser(string username, string file) { 
            GetImageUser(username,file).GetAwaiter().GetResult();
        }




        static async Task<List<Tickets>> RunAsyncGetTicketsPending() {

            var usersPending = await GetTicketsPending();
            return usersPending;
        }
        static async Task<List<Tickets>> RunAsyncGetTicketsUser(string username) {
            var usersAll = await GetTicketsUser(username);
            return usersAll;
        }
        static async Task<List<Tickets>> RunAsyncGetTicketsUserSpecific(string username, string stat) {
            var usersSpecific = await GetTicketsUserSpecific(username, stat);
            //foreach (var user in usersSpecific){
            //    Console.WriteLine(user.amount + " " + user.descript + " " + user.stat);
            //}
            return usersSpecific;
        }
        static async Task<bool> RunAsyncCheckLogExists(string username, string password, int managerStatus) {
            return await checkLogExists(username, password, managerStatus);
            //return checkingLog;//Console.WriteLine(checkingLog);
        }
        static async Task<bool> RunAsyncCheckUserExists(string username) {
            return await checkUserExists(username);
            //return checkingUser;
        }
        static async Task RunAsyncAddTickets(string username, double amount, string descript, string type) {
            addTickets(username, amount, descript, type);
        }
        static async Task<int> RunAsyncUpdate(string username, double amount, string stat) {
            return await update(username, amount, stat);
        }
        static async Task RunAsyncCreate(string username, string password) {
            createUser(username, password);
        }
        static async Task<List<string>> RunAsyncGetAll() {
            return await getAll();
        }
        static async Task<bool> RunAsyncUserPosition(string username, int managerStatus) {
            return await UserPosition(username, managerStatus);
        }
        static async Task RunAsyncUpdateManagerStatus(string username, int managerStatus) {
            UpdateManagerStatus(username, managerStatus);
        }
        static async Task<List<Tickets>> RunAsyncGetTicketsByType(string username, string type) {
            return await GetTicketsByType(username, type);
        }
        static async Task RunAsyncAddReceipt(string username, double amount, string file) {
            await AddReceipt(username, amount, file);
        }
        static async Task RunAsyncUpdateName(string username, string name) {
            await UpdateName(username, name);
        }
        static async Task RunAsyncUpdateAddress(string username, string address) {
            await UpdateAddress(username, address);
        }
        static async Task RunAsyncUpdateImage(string username, string file) {
            await UpdateImage(username, file);
        }
        static async Task RunAsyncGetImageTicket(string username, double amount, string file) {
            await GetImageTicket(username, amount, file);
        }
        /*static async Task RunAsync() {
                client.BaseAddress = new Uri("https://localhost:7149");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var usersPending = await GetTicketsPending();
                    foreach (var user in usersPending)
                    {
                        Console.WriteLine(user.username + " " + user.amount + " " + user.descript);
                    }
                    var usersAll = await GetTicketsUser("Ryan");
                    foreach (var user in usersAll)
                    {
                        //Console.WriteLine(user);
                        Console.WriteLine(user.amount + " " + user.descript + " " + user.stat);
                    }
                    var usersSpecific = await GetTicketsUserSpecific("Ryan","Approved");
                    foreach (var user in usersSpecific) {
                        Console.WriteLine(user.amount + " " + user.descript + " " + user.stat);
                    }
                    var checkingLog = await checkLogExists("Ryan","123",0);
                    Console.WriteLine(checkingLog);
                    var checkingUser = await checkUserExists("Ryan");
                    Console.WriteLine(checkingUser);
                    //addTickets("Ryan",50.50,"testing inside the thing");
                    usersAll = await GetTicketsUser("Ryan");
                    foreach (var user in usersAll)
                    {
                        //Console.WriteLine(user);
                        Console.WriteLine(user.amount + " " + user.descript + " " + user.stat);
                    }

                    await update("Ryan", 50.50, "Approved");
                    usersAll = await GetTicketsUser("Ryan");
                    foreach (var user in usersAll)
                    {
                        //Console.WriteLine(user);
                        Console.WriteLine(user.amount + " " + user.descript + " " + user.stat);
                    }

                    Console.WriteLine(await checkUserExists("Bob"));
                    //createUser("Bob","123");
                    Console.WriteLine(await checkUserExists("Bob"));



                }
                catch (Exception ex) { }
            }*/
        static async Task<List<Tickets>> GetTicketsPending() {

            List<Tickets> userList = new List<Tickets>();
            var path = "/Tickets/Pending";
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode) {
                userList = await response.Content.ReadAsAsync<List<Tickets>>();
            }
            return userList;
        }
        static async Task<List<Tickets>> GetTicketsUser(string username) {
            List<Tickets> userList = new List<Tickets>();

            Tickets t = new();
            t.username = username;
            var response = await client.PostAsJsonAsync("Tickets/UserAll", t);
            if (response.IsSuccessStatusCode)
            {
                userList = await response.Content.ReadAsAsync<List<Tickets>>();
            }
            return userList;
        }
        static async Task<List<Tickets>> GetTicketsUserSpecific(string username, string stat) {
            List<Tickets> userList = new List<Tickets>();
            Tickets t = new();
            t.username = username;
            t.stat = stat;
            HttpResponseMessage response = await client.PostAsJsonAsync("/Tickets/UserSpecific", t);
            if (response.IsSuccessStatusCode)
            {
                userList = await response.Content.ReadAsAsync<List<Tickets>>();
            }
            return userList;
        }
        static async Task<bool> checkLogExists(string username, string password, int managerStatus) {
            bool result;
            User s = new(username, password, managerStatus);

            HttpResponseMessage response = await client.PostAsJsonAsync("/checkExists/Log", s);

            //if (response.IsSuccessStatusCode)
            //{
            result = await response.Content.ReadAsAsync<bool>();
            //}
            return result;
        }
        static async Task<bool> checkUserExists(string username) {
            bool result;
            User s = new();
            s.username = username;
            HttpResponseMessage response = await client.PostAsJsonAsync("/checkExists/User", s);

            return await response.Content.ReadAsAsync<bool>();
        }
        static async void addTickets(string username, double amount, string descript, string type) {
            Tickets t = new();
            t.username = username;
            t.amount = amount;
            t.descript = descript;
            t.type = type;
            HttpResponseMessage response = await client.PostAsJsonAsync("/Tickets", t);
        }
        static async void createUser(string username, string password) {
            User u = new();
            u.username = username;
            u.password = password;
            HttpResponseMessage response = await client.PostAsJsonAsync("users", u);
        }
        static async Task<int> update(string username, double amount, string stat) {
            Tickets t = new();
            t.username = username;
            t.amount = amount;
            t.stat = stat;
            HttpResponseMessage response = await client.PutAsJsonAsync("Tickets/update", t);
            return await response.Content.ReadAsAsync<int>();

        }
        static async Task<List<string>> getAll() {
            HttpResponseMessage response = await client.GetAsync("users/All");
            return await response.Content.ReadAsAsync<List<string>>();
        }
        static async Task<bool> UserPosition(string username, int managerStatus) {
            User u = new();
            u.username = username;
            u.managerStatus = managerStatus;
            HttpResponseMessage response = await client.PostAsJsonAsync("user/Position", u);
            return await response.Content.ReadAsAsync<bool>();
        }
        static async void UpdateManagerStatus(string username, int managerStatus) {
            User u = new();
            u.username = username;
            u.managerStatus = managerStatus;
            HttpResponseMessage response = await client.PutAsJsonAsync("user/UpdatePosition", u);

        }
        static async Task<List<Tickets>> GetTicketsByType(string username, string type) {
            Tickets t = new();
            t.username = username;
            t.type = type;
            HttpResponseMessage response = await client.PostAsJsonAsync("user/Type", t);
            return await response.Content.ReadAsAsync<List<Tickets>>();

        }
        static async Task AddReceipt(string username, double amount, string file) {
            Tickets t = new();
            t.username = username;
            t.amount = amount;
            t.file = file;
            HttpResponseMessage response = await client.PostAsJsonAsync("/Ticket/Image", t);

        }
        static async Task UpdateName(string username, string name) {
            User u = new();
            u.username = username;
            u.name = name;
            HttpResponseMessage response = await client.PostAsJsonAsync("/user/UpdateName", u);

        }
        static async Task UpdateAddress(string username, string address)
        {
            User u = new();
            u.username = username;
            u.address = address;
            HttpResponseMessage response = await client.PostAsJsonAsync("/user/UpdateAddress", u);

        }
        static async Task UpdateImage(string username, string file) {
            User u = new();
            u.username = username;
            u.file = file;
            HttpResponseMessage response = await client.PostAsJsonAsync("/user/Image", u);

        }
        static async Task GetImageTicket(string username, double amount, string file) { 
        Tickets t = new();
            t.username = username;
            t.amount = amount;
            t.file = file;
            HttpResponseMessage response = await client.PostAsJsonAsync("/Ticket/GetImage", t);
        }
        static async Task GetImageUser(string username, string file)
        {
            User u = new();
            u.username = username;
            u.file = file;
            HttpResponseMessage response = await client.PostAsJsonAsync("/user/GetImage", u);
        }
    }
}
