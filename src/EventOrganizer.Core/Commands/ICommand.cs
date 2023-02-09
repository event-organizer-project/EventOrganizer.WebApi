namespace EventOrganizer.Core.Commands
{
    public interface ICommand<in TParameters, out TResult>
    {
        TResult Execute(TParameters parameters);
    }
}
