namespace p1{
    public class DbReimburse : IEquatable<DbReimburse>{

        public string username;
        public double amount;
        public string description;
        public string status;
        public DbReimburse(){}
        public DbReimburse(string username,double amount,string description, string status){
            this.username = username;
            this.amount = amount;
            this.description = description;
            this.status = status;
        }
        
        public bool Equals(DbReimburse o){
            if(this.username == o.username){
                return true;
            }
            //if(this.username==o.username){
            //    return true;
            //}
            else{
                return false;
            }
        }
    }
}