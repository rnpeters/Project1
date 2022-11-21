using System;
namespace Project1.NoMore
{
    public class FinanceManager
    {
        public static List<Db> tempDb= new List<Db>();
        public static List<DbReimburse> dbReimburses= new List<DbReimburse>();
        public static List<DbReimburse> pending = new List<DbReimburse>();
        //private string username;
        //private string password;

        public static bool checkExists(string username, string password) {
            Db find = new Db(username,password);
            
            if(tempDb.Contains(find)){
                return true;
            }
            return false;
            
        }
        public static void addReimbursement(string username,double amount,string description){
            dbReimburses.Add(new DbReimburse(username,amount,description,"pending")); 
            pending.Add(new DbReimburse(username,amount,description,"pending"));
        }
        public static bool getReimbursementsPending(){
            
            //List<DbReimburse> d = dbReimburses.FindAll(x=>x.status.Equals("pending"));
            //for(int i=0; i<d.Count;i++){
            for(int i=0;i<pending.Count;i++){
                Console.WriteLine($"Username: {pending[i].username}, Amount: {pending[i].amount}, Description: {pending[i].description}");
            }
            if(pending.Count ==0){
                return false;
            }
            else{return true;}
        }
        public static bool getReimbursementsUser(string username){
            
            List<DbReimburse> d = dbReimburses.FindAll(x=>x.username.Equals(username));
            for(int i=0; i<d.Count;i++){
                Console.WriteLine($"Username: {d[i].username}, Amount: {d[i].amount}, Description: {d[i].description}, status: {d[i].status}");
            }
            if(d.Count ==0){
                return false;
            }
            else{return true;}
        }
        


    }
}