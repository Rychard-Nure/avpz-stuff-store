namespace DataLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGameCategoryInterface GameCategories { get; }
        IGameInterface Games { get; }
        ICommentInterface Comments { get; }
        IOrderInterface Orders { get; }
        IOrderDetailInterface OrderDetails { get; }
        Task SaveAsync();
    }
}
