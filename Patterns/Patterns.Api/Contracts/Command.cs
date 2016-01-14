namespace Patterns.Api.Contracts
{
    public interface Command : Command<Unit> { }
    public interface Command<out TReponse> { }
}