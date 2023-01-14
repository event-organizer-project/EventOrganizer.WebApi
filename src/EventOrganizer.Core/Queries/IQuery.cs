namespace EventOrganizer.Core.Queries
{
    public interface IQuery<in TParameters, out TResult>
    {
        TResult Execute(TParameters parameters);
    }
}
