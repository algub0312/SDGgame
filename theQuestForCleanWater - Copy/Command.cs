public class Command
{
    public string Verb { get; private set; }
    public string Object { get; private set; }

    public Command(string verb, string obj)
    {
        Verb = verb;
        Object = obj;
    }
}