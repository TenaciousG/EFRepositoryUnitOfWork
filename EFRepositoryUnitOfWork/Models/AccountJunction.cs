namespace EFRepositoryUnitOfWork.Models
{
    public class AccountJunction
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        public virtual Account Account { get; set; }
    }
}
