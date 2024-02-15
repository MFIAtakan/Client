namespace Client.Models
{
    public class UserOperationRequestModel
    {
        public List<int> Entries { get; set; }        

        public UserOperationRequestModel() { 
            Entries = [];
        }
    }
}
