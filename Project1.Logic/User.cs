using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Logic
{
    public class User
    {
        public string username { get; set; }
        public string password { get; set; }
        public int managerStatus { get; set; }
        public string name { get;set; }
        public string address { get; set; }
        public string file { get; set; }
        public User() { }
        public User(string username,string password,int managerStatus) { 
            this.username = username;
            this.password = password;
            this.managerStatus = managerStatus;
        }
    }
}
