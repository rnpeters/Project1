using Project1.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Data
{
    public interface IRepository
    {
        public bool checkLogExists(string name, string password, int managerStatus);
        public bool checkUserExists(string name);
        public void createUser(string username, string password);
        public int updateStatus(string username,double amount,string status);
        public List<Tickets> getReimbursementsPending();
        public void addReimbursement(string username, double amount, string description, string type);
        public List<Tickets> getReimbursementsUserAll(string username);
        public List<Tickets> getReimbursementsUserSpecific(string username, string status);

        public void UploadReceiptImage(string username, double amount, string file);
        public void GetReceiptImage(string username, double amount, string filef);
    }
}
