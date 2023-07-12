namespace VampireLike.Core
{
    public interface IBuilder<TResult>
    {
        TResult Build();
    }
}