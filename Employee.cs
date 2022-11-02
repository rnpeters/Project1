using System;
namespace p1
{
    public class Employee
    {
        private static List<Db> tempDb= new List<Db>();
        
        //private string username;
        //private string password;

        public static bool checkLogExists(string username, string password) {
            //Db find = new Db(username,password);
            //tempDb.Add(new Db(username,password));
            //Console.WriteLine(tempDb.Contains(new Db(username,password)));
            //Console.WriteLine("outside");
            if(tempDb.Contains(new Db(username,password))){
                //Console.WriteLine("inside check");
                return true;
            }
            return false;
            
        }
         public static bool checkUserExists(string username) {
            //Db find = new Db(username,password);
            //tempDb.Add(new Db(username,password));
            //Console.WriteLine(tempDb.Contains(new Db(username,password)));
            if(tempDb.Exists(x => x.username.Contains(username))){
                //Console.WriteLine("inside check");
                return true;
            }
            return false;
            
        }
        public static void create(string username, string password){
            tempDb.Add(new Db(username,password));
            Console.WriteLine("User Created");
        }
        public static void statusUpdate(string username, double amount, bool status){
            
            foreach (var m in FinanceManager.dbReimburses.Where(x=>x.username == username)){
                if(m.amount == amount && m.status=="pending"){
                    int indexx = FinanceManager.pending.FindIndex(x=>x.username==username && x.amount == amount);
                    FinanceManager.pending.RemoveAt(indexx);
                    if(status){
                        m.status = "Approved";
                        return;
                    }
                    else{
                        m.status = "Declined";
                        return;
                    }
                }
            }
            Console.WriteLine("Error, try again");
            
            // if(status){
            //     //FinanceManager.dbReimburses.Find(x=>x.username.Contains(username)).status = "Approved" ;
            //     foreach (var m in FinanceManager.dbReimburses.Where(x=>x.username == username)){
            //         if(m.amount == amount && m.description == description){
            //             m.status = "Approved";
            //             break;
            //         }
            //     }
            // }
            // else{
            //     foreach (var m in FinanceManager.dbReimburses.Where(x=>x.username == username)){
            //         if(m.amount == amount && m.description == description){
            //             m.status = "Approved";
            //             break;
            //         }
            //     }
            //}
        }
        


    }
}