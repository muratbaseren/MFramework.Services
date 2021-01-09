namespace MFramework.Services.Entities.Abstract
{
    public interface IEntityCommand<T>
    {
        T Id { get; set; }
    }
}
