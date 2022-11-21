using Project1.Logic;
using System.Data;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Xml.Linq;

namespace Project1.Data {

    public class SqlRepository : IRepository
    {
        public string connectionString;
        public SqlRepository() { }
        public SqlRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public void addReimbursement(string username, double amount, string description, string type)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            //find user ID based on name
            string cmdText = "SELECT UsersId FROM Project1.Users WHERE username = @username";
            using SqlCommand cmd = new SqlCommand(cmdText, connection);
            cmd.Parameters.AddWithValue("@username",username);

            using SqlDataReader reader = cmd.ExecuteReader();
            int userId=-1;
            while (reader.Read()) {
                userId = reader.GetInt32(0);
            }
            connection.Close();
            connection.Open();
            cmdText = "INSERT INTO Project1.Tickets (UsersId, Amount, Descript, Stat, Types) VALUES (@UsersId, @Amount, @Descript, 'Pending', @Type);";

            using SqlCommand cmd2 = new SqlCommand(cmdText, connection);

            cmd2.Parameters.AddWithValue("@UsersId", userId);
            cmd2.Parameters.AddWithValue("@Amount", amount);
            cmd2.Parameters.AddWithValue("@Descript", description);
            cmd2.Parameters.AddWithValue("@Type",type);

            cmd2.ExecuteNonQuery();
        }

        public bool checkLogExists(string username, string password, int managerStatus)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string cmdText = @"SELECT username, password FROM Project1.Users WHERE username = @username AND password = @password and AdminStatus = @managerStatus;";

            using SqlCommand cmd = new SqlCommand(cmdText, connection);

            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password",password);
            cmd.Parameters.AddWithValue("@managerStatus",managerStatus);

            using SqlDataReader reader = cmd.ExecuteReader();

            //User? tmp;
            string name = null;
            //string pass = null;
            while (reader.Read())
            {
                name = reader.GetString(0);
                //pass = reader.GetString(1);
                //tmp = new User(reader.GetString(0), reader.GetString(1));
            }
            connection.Close();
            if (string.IsNullOrEmpty(name))
            {
                return false; 
            }
            return true;
        }

        public bool checkUserExists(string username)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string cmdText =@"SELECT username FROM Project1.Users WHERE username = @username;";

            using SqlCommand cmd = new SqlCommand(cmdText, connection);

            cmd.Parameters.AddWithValue("@username", username);

            using SqlDataReader reader = cmd.ExecuteReader();

            //User? tmp;
            string name= null;
            //string pass = null;
            while (reader.Read())
            {
                name = reader.GetString(0);
                //pass = reader.GetString(1);
                //tmp = new User(reader.GetString(0), reader.GetString(1));
            }
            connection.Close();
            if (string.IsNullOrEmpty(name)) {
                return false; ;
            }
            return true;
            // noUser = new();
            //return noUser;
        }

        public void createUser(string username, string password)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string cmdText =
                "INSERT INTO Project1.Users (Username, Password, Adminstatus) VALUES (@username, @password, @adminStatus);";

            using SqlCommand cmd = new SqlCommand(cmdText, connection);

            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@adminStatus", 0);

            cmd.ExecuteNonQuery();
        }

        public List<Tickets> getReimbursementsPending()
        {
            List<Tickets> userList = new List<Tickets>();

            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            //                         0        1        2       3
            string cmdText = "SELECT Username, Amount, Descript, Types From Project1.Tickets JOIN Project1.Users ON Project1.Tickets.UsersId = Project1.Users.UsersId Where Stat ='Pending';";

            using SqlCommand cmd = new SqlCommand(cmdText, connection);

            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                //string s = $"Username: {reader.GetString(0)} Amount: {reader.GetDouble(1)} Description: {reader.GetString(2)}";
                //userList.Add(s);
                Tickets user = new Tickets();
                user.username = reader.GetString(0);
                user.amount = reader.GetDouble(1);
                user.descript = reader.GetString(2);
                user.type = reader.GetString(3);
                userList.Add(user);
            }
            connection.Close();
            reader.Close();
            cmd.Dispose();
            return userList;
        }

        public List<Tickets> getReimbursementsUserAll(string username)
        {
            List<Tickets> userList = new List<Tickets>();


            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            //                         0        1       2
            string cmdText = "SELECT Amount, Descript, Stat, Types From Project1.Tickets JOIN Project1.Users ON Project1.Tickets.UsersId = Project1.Users.UsersId Where username = @username;";

            using SqlCommand cmd = new SqlCommand(cmdText, connection);
            cmd.Parameters.AddWithValue("@username",username);
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Tickets t = new();
                t.amount = reader.GetDouble(0);
                t.descript = reader.GetString(1);
                t.stat = reader.GetString(2);
                t.type = reader.GetString(3);
                userList.Add(t);
                //string s = $"Amount: {reader.GetDouble(0)} Description: {reader.GetString(1)} Status: {reader.GetString(2)}"; userList.Add(s);
            }
            connection.Close();
            return userList;
        }

        public List<Tickets> getReimbursementsUserSpecific(string username, string status)
        {
            List<Tickets> userList = new List<Tickets>();


            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            //                         0        1       2
            string cmdText = "SELECT Amount, Descript, Stat, Types From Project1.Tickets JOIN Project1.Users ON Project1.Tickets.UsersId = Project1.Users.UsersId Where username = @username AND Stat = @Stat;";

            using SqlCommand cmd = new SqlCommand(cmdText, connection);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@Stat",status);
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Tickets t = new();
                t.amount = reader.GetDouble(0);
                t.descript = reader.GetString(1);
                t.stat = reader.GetString(2);
                t.type = reader.GetString(3);
                //string s = $"Amount: {reader.GetDouble(0)} Description: {reader.GetString(1)} Status: {reader.GetString(2)}";
                userList.Add(t);
            }
            connection.Close();
            return userList;
        }
        public List<Tickets> getReimbursementsByType(string username, string type)
        {
            List<Tickets> userList = new List<Tickets>();


            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            //                         0        1       2
            string cmdText = "SELECT Amount, Descript, Stat, Types From Project1.Tickets JOIN Project1.Users ON Project1.Tickets.UsersId = Project1.Users.UsersId Where username = @username AND Types = @Type;";

            using SqlCommand cmd = new SqlCommand(cmdText, connection);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@Type", type);
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Tickets t = new();
                t.amount = reader.GetDouble(0);
                t.descript = reader.GetString(1);
                t.stat = reader.GetString(2);
                t.type = type;
                //string s = $"Amount: {reader.GetDouble(0)} Description: {reader.GetString(1)} Status: {reader.GetString(2)}";
                userList.Add(t);
            }
            connection.Close();
            return userList;
        }
        public int updateStatus(string username, double amount, string status)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string cmdText = "SELECT TicketId From Project1.Users JOIN Project1.Tickets ON Project1.Tickets.UsersId = Project1.Users.UsersId Where username = @username AND Amount = @amount AND Stat = 'Pending';";
            using SqlCommand cmd = new SqlCommand(cmdText, connection);

            cmd.Parameters.AddWithValue("@amount", amount);
            cmd.Parameters.AddWithValue("@username", username);

            int TicketId = -1;

            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) {
                TicketId = reader.GetInt32(0);
            }
            connection.Close();
            connection.Open();


            //cmdText = "UPDATE Project1.Tickets SET Stat = @status From Project1.Users JOIN Project1.Tickets ON Project1.Tickets.UsersId = Project1.Users.UsersId Where username = @username AND Amount = @amount AND Stat = 'Pending';";
            cmdText = "UPDATE Project1.Tickets SET Stat = @status FROM Project1.Users JOIN Project1.Tickets ON Project1.Tickets.UsersId = Project1.Users.UsersId WHERE TicketId = @TicketId;";
            using SqlCommand cmd2 = new SqlCommand(cmdText, connection);

            cmd2.Parameters.AddWithValue("@status", status);
            cmd2.Parameters.AddWithValue("@TicketId",TicketId);
            //cmd.Parameters.AddWithValue("@amount", amount);
            //cmd.Parameters.AddWithValue("@username", username);

            return cmd2.ExecuteNonQuery();
        }

        public List<string> getAllUsers() {

            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string cmdText = "SELECT username From Project1.Users";
            using SqlCommand cmd = new SqlCommand(cmdText, connection);

            List<string> list = new();

            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(reader.GetString(0));
            }
            connection.Close();
            return list;

        }
        public bool UserPostion(string username, int adminstatus) {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string cmdText = "SELECT username From Project1.Users where username=@username and AdminStatus=@adminstatus";
            using SqlCommand cmd = new SqlCommand(cmdText, connection);
            cmd.Parameters.AddWithValue("@username",username);
            cmd.Parameters.AddWithValue("@adminstatus", adminstatus);

            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(adminstatus);
                Console.WriteLine(reader.GetString(0));
                return true;
            }
            connection.Close();
            return false;
        }
        public void UpdateManagerStatus(string username, int AdminStatus) {

            using SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            //cmdText = "UPDATE Project1.Tickets SET Stat = @status From Project1.Users JOIN Project1.Tickets ON Project1.Tickets.UsersId = Project1.Users.UsersId Where username = @username AND Amount = @amount AND Stat = 'Pending';";
            string cmdText = "UPDATE Project1.Users SET AdminStatus = @AdminStatus FROM Project1.Users WHERE username = @username;";
            using SqlCommand cmd = new SqlCommand(cmdText, connection);

            cmd.Parameters.AddWithValue("@AdminStatus", AdminStatus);
            cmd.Parameters.AddWithValue("@username", username);

            cmd.ExecuteNonQuery();
        }

        public void UploadReciptImage(string username,double amount,string file) {
            //string username = "Ryan";
            //double amount = 123;
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string cmdText = "SELECT TicketId From Project1.Users JOIN Project1.Tickets ON Project1.Tickets.UsersId = Project1.Users.UsersId Where username = @username AND Amount = @amount AND Stat = 'Pending';";
            using SqlCommand cmd = new SqlCommand(cmdText, connection);

            cmd.Parameters.AddWithValue("@amount", amount);
            cmd.Parameters.AddWithValue("@username", username);

            int TicketId = -1;

            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                TicketId = reader.GetInt32(0);
            }
            connection.Close();
            connection.Open();
        
            //byte[] f = File.ReadAllBytes("/Revature/Project1/Receipt.jpg");
            byte[] f = File.ReadAllBytes(file);

            Console.WriteLine(f.Length);

            SqlCommand cmd3 = new SqlCommand("UPDATE Project1.Tickets SET Image = @img FROM Project1.Users JOIN Project1.Tickets ON Project1.Tickets.UsersId = Project1.Users.UsersId WHERE TicketId = @TicketId;\r\n", connection) ;
            cmd3.Parameters.Add("@img", SqlDbType.VarBinary).Value = f;
            cmd3.Parameters.AddWithValue("@TicketId",TicketId);
            cmd3.ExecuteNonQuery();
            connection.Close();

        }
        public void GetReciptImage(string filef)
        {
            string username = "Ryan";
            double amount = 123;

            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string cmdText = "SELECT TicketId From Project1.Users JOIN Project1.Tickets ON Project1.Tickets.UsersId = Project1.Users.UsersId Where username = @username AND Amount = @amount AND Stat = 'Pending';";
            using SqlCommand cmd = new SqlCommand(cmdText, connection);

            cmd.Parameters.AddWithValue("@amount", amount);
            cmd.Parameters.AddWithValue("@username", username);

            int TicketId = -1;

            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                TicketId = reader.GetInt32(0);
            }
            connection.Close();
            connection.Open();

            SqlCommand cmd3 = new SqlCommand("Select Image FROM Project1.Tickets WHERE TicketId = @TicketId;", connection);

            cmd3.Parameters.AddWithValue("@TicketId", TicketId);
            using SqlDataReader reader2 = cmd3.ExecuteReader();
            byte[] image = null;
            while (reader2.Read())
            {
                image = (byte[])reader2["Image"];
            }
            connection.Close();
            string path = "/Revature/Project1/ReceiptNew.jpg";
            File.WriteAllBytes(path, image);
        }
        public void UpdateName(string username,string name) { 
        using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            //cmdText = "UPDATE Project1.Tickets SET Stat = @status From Project1.Users JOIN Project1.Tickets ON Project1.Tickets.UsersId = Project1.Users.UsersId Where username = @username AND Amount = @amount AND Stat = 'Pending';";
            string cmdText = "UPDATE Project1.Users SET name = @name FROM Project1.Users Where username =@username;";
            using SqlCommand cmd = new SqlCommand(cmdText, connection);

            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@username",username);

            cmd.ExecuteNonQuery();
        }
         public void UpdateAddress(string username,string address) { 
        using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            //cmdText = "UPDATE Project1.Tickets SET Stat = @status From Project1.Users JOIN Project1.Tickets ON Project1.Tickets.UsersId = Project1.Users.UsersId Where username = @username AND Amount = @amount AND Stat = 'Pending';";
            string cmdText = "UPDATE Project1.Users SET Address = @address FROM Project1.Users Where username =@username;";
            using SqlCommand cmd = new SqlCommand(cmdText, connection);

            cmd.Parameters.AddWithValue("@address", address);
            cmd.Parameters.AddWithValue("@username",username);

            cmd.ExecuteNonQuery();
        }
        public void UpdateProfile(string username, string file) {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            //byte[] f = File.ReadAllBytes("/Revature/Project1/Receipt.jpg");
            byte[] f = File.ReadAllBytes(file);

            SqlCommand cmd3 = new SqlCommand("UPDATE Project1.Users SET Image = @img FROM Project1.Users WHERE username = @username;", connection);
            cmd3.Parameters.Add("@img", SqlDbType.VarBinary).Value = f;
            cmd3.Parameters.AddWithValue("@username", username);
            
            cmd3.ExecuteNonQuery();
            connection.Close();
        }
    }
}
