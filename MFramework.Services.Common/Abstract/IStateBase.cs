namespace MFramework.Services.Common.Abstract
{
    public interface IStateBase<T>
    {
        T State { get; set; }
    }
}
