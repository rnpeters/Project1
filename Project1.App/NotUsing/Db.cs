namespace Project1.NoMore
{
    public class Db : IEquatable<Db>{

        public string username;
        public string password;
        public Db(){}
        public Db(string username,string password){
            this.username = username;
            this.password = password;
        }
        
        public bool Equals(Db o){
            if(this.username == o.username && this.password==o.password){
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