namespace MFramework.Services.DataAccess.UnitOfWork
{
    public interface IUnitOfWorkGeneric : IUnitOfWork
    {
        TRepository GetRepository<TRepository>();
    }
}
