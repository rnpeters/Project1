using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Logic
{
    public class Tickets
    {
        //public int ticketId { get; set; }
        //public int usersId { get; set; }
        public string username { get; set; }
        public double amount { get; set; }
        public string descript { get; set; }
        public string stat { get; set; }
        public string type {get;set;}
        public string file { get; set; }

        public Tickets() { }
        //public Tickets(int ticketId, int usersId, double amount, string descript, string stat)
        //{
        //    this.ticketId = ticketId;
        //    this.usersId = usersId;
        //    this.descript = descript;
        //    this.stat = stat;
        //}
    }
}
