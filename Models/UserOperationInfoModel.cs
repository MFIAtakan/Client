namespace Client.Models
{
    public class UserOperationInfoModel
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<int> Entries { get; set; }
        public int Result { get; set; }
    }
}
