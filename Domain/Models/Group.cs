namespace Domain.Models;

public class Group:BaseEntity
{
    public string TeacherName { get; set; }
    public string Room { get; set; }
    public static int id;
    public Group()
    {
        Id = id;
        id++;
    }
}


