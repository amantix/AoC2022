namespace Common;

public interface ISolver<out T>
{
    public T Solve(string[] lines);
}