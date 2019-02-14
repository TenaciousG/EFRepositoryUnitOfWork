namespace EFRepositoryUnitOfWork.Models
{
    public class Account
    {
        public int Id { get; set; }
        public int AccountNumber { get; set; }
        public string Type { get; set; }
        public decimal Balance { get; set; }
    }
}
