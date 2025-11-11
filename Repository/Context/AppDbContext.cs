namespace Repository.Context;

public class AppDbContext<T>
{
    public static List<T> Models { get; set; }

    static AppDbContext()
    {
        Models = new List<T>();
    }
}
