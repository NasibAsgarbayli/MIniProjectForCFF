using Domain.Models;
using Services.Helpers;
using Services.Services.Implementations;

namespace Presentation.Controllers;

public class AppController
{
    private readonly GroupService _groupService;
    private readonly StudentService _studentService;

    public AppController(GroupService groupService, StudentService studentService)
    {
        _groupService = groupService;
        _studentService = studentService;
    }

    public void CreateGroup()
    {
        Console.Write("Enter group name: ");
        string name = Helper.ReadLetterOrDigitString("Group name is not valid. Enter again:");
        Console.Write("Enter teacher name: ");
        string teacher = Helper.ReadValidatedString("Teacher name is not valid. Enter again:");
        Console.Write("Enter room: ");
        string room = Helper.ReadLetterOrDigitString("Room cannot be empty. Enter again:");

        try
        {
            _groupService.Create(new Group { Name = name, TeacherName = teacher, Room = room });
            Helper.ColorWrite(ConsoleColor.Green, "Group created successfully!");
            Helper.PlaySound("notify.wav");
        }
        catch (Exception ex)
        {
            Helper.ColorWrite(ConsoleColor.Red, ex.Message);
            Helper.PlaySound("chord.wav");
        }
    }

    public void UpdateGroup()
    {
        Console.WriteLine("Enter Id:");
        int id = Helper.ReadValidatedInt("Enter ID to update:");
        Console.Write("Enter new name: ");
        string newName = Console.ReadLine();
        Console.Write("Enter new teacher name: ");
        string newTeacher = Console.ReadLine();
        Console.Write("Enter new room: ");
        string newRoom = Console.ReadLine();

        try
        {
            _groupService.Update(id, new Group { Name = newName, TeacherName = newTeacher, Room = newRoom });
            Helper.ColorWrite(ConsoleColor.Green, "Group updated successfully!");
            Helper.PlaySound("notify.wav");
        }
        catch (Exception ex)
        {
            Helper.ColorWrite(ConsoleColor.Red, ex.Message);
            Helper.PlaySound("chord.wav");
        }
    }

    public void DeleteGroup()
    {
        int id = Helper.ReadValidatedInt("Enter ID to delete:");

        try
        {
            _groupService.Delete(id);
            Helper.ColorWrite(ConsoleColor.Green, "Group deleted successfully!");
            Helper.PlaySound("notify.wav");
        }
        catch (Exception ex)
        {
            Helper.ColorWrite(ConsoleColor.Red, ex.Message);
            Helper.PlaySound("chord.wav");
        }
    }

    public void GetGroupById()
    {
        int id = Helper.ReadValidatedInt("Enter group ID:");

        try
        {
            var group = _groupService.GetById(id);
            Console.WriteLine($"ID: {group.Id} | Name: {group.Name} | Teacher: {group.TeacherName} | Room: {group.Room}");
            Helper.PlaySound("notify.wav");
        }
        catch (Exception ex)
        {
            Helper.ColorWrite(ConsoleColor.Red, ex.Message);
            Helper.PlaySound("chord.wav");
        }
    }

    public void GetAllGroups()
    {
        try
        {
            var all = _groupService.GetAll();
            foreach (var g in all)
                Console.WriteLine($"ID: {g.Id}. Name: {g.Name} | Teacher: {g.TeacherName} | Room: {g.Room}");
            Helper.PlaySound("notify.wav");
        }
        catch (Exception ex)
        {
            Helper.ColorWrite(ConsoleColor.Red, ex.Message);
            Helper.PlaySound("chord.wav");
        }
    }

    public void GetGroupsByTeacher()
    {
        Console.Write("Enter teacher name: ");
        string teacher = Console.ReadLine();

        try
        {
            var list = _groupService.GetAllByTeacherName(teacher);
            foreach (var g in list)
                Console.WriteLine($"ID: {g.Id}. Name: {g.Name} | Teacher: {g.TeacherName}");
            Helper.PlaySound("notify.wav");
        }
        catch (Exception ex)
        {
            Helper.ColorWrite(ConsoleColor.Red, ex.Message);
            Helper.PlaySound("chord.wav");
        }
    }

    public void GetGroupsByRoom()
    {
        Console.Write("Enter room: ");
        string room = Console.ReadLine();

        try
        {
            var list = _groupService.GetAllByRoom(room);
            foreach (var g in list)
                Console.WriteLine($"ID: {g.Id}. Name: {g.Name} | Room: {g.Room}");
            Helper.PlaySound("notify.wav");
        }
        catch (Exception ex)
        {
            Helper.ColorWrite(ConsoleColor.Red, ex.Message);
            Helper.PlaySound("chord.wav");
        }
    }

    public void SearchGroupsByName()
    {
        Console.Write("Enter keyword: ");
        string keyword = Console.ReadLine();

        try
        {
            var list = _groupService.SearchByName(keyword);
            foreach (var g in list)
                Console.WriteLine($"ID: {g.Id}. Name: {g.Name} | Teacher: {g.TeacherName} | Room: {g.Room}");
            Helper.PlaySound("notify.wav");
        }
        catch (Exception ex)
        {
            Helper.ColorWrite(ConsoleColor.Red, ex.Message);
            Helper.PlaySound("chord.wav");
        }
    }


    public void GroupMenu()
    {
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n----- GROUP MENU -----");
            Console.WriteLine("1. Create");
            Console.WriteLine("2. Update");
            Console.WriteLine("3. Delete");
            Console.WriteLine("4. GetById");
            Console.WriteLine("5. GetAll");
            Console.WriteLine("6. GetAllByTeacherName");
            Console.WriteLine("7. GetAllByRoom");
            Console.WriteLine("8. SearchByName");
            Console.WriteLine("0. Back");
            Console.ResetColor();

        Input:
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Enter your choice: ");
            Console.ResetColor();

            string input = Console.ReadLine();
            if (!int.TryParse(input, out int choice))
            {
                Helper.ColorWrite(ConsoleColor.Red, "Input type is not correct!");
                goto Input;
            }

            switch (choice)
            {
                case 1:
                    CreateGroup();
                    break;
                case 2:
                    UpdateGroup();
                    break;
                case 3:
                    DeleteGroup();
                    break;
                case 4:
                    GetGroupById();
                    break;
                case 5:
                    GetAllGroups();
                    break;
                case 6:
                    GetGroupsByTeacher();
                    break;
                case 7:
                    GetGroupsByRoom();
                    break;
                case 8:
                    SearchGroupsByName();
                    break;
                case 0:
                    return;
                default:
                    Helper.ColorWrite(ConsoleColor.Red, "Invalid choice!");
                    break;
            }
        }
    }

    public void CreateStudent()
    {
        Console.WriteLine("--- Create New Student ---");
        Console.Write("Enter student name: ");
        string name = Helper.ReadValidatedString("Name is not valid. Enter again:");
        Console.Write("Enter student surname: ");
        string surname = Helper.ReadValidatedString("Surname is not valid. Enter again:");
        Console.Write("Enter student age: ");
        int age = Helper.ReadValidatedInt("Age must be a valid number. Enter again:");
        Console.Write("Enter group ID: ");
        int groupId = Helper.ReadValidatedInt("Group ID must be a valid number. Enter again:");

        try
        {
            _studentService.Create(groupId, new Student { Name = name, Surname = surname, Age = age });
            Helper.ColorWrite(ConsoleColor.Green, "Student created successfully!");
            Helper.PlaySound("notify.wav");
        }
        catch (Exception ex)
        {
            Helper.ColorWrite(ConsoleColor.Red, ex.Message);
            Helper.PlaySound("chord.wav");
        }
    }

    public void UpdateStudent()
    {
        Console.WriteLine("--- Update Student ---");
        Console.Write("Enter student ID to update: ");
        int id = Helper.ReadValidatedInt("ID must be a valid number. Enter again:");
        Console.Write("Enter new student name: ");
        string newName = Helper.ReadValidatedString("Name is not valid. Enter again:");
        Console.Write("Enter new student surname: ");
        string newSurname = Helper.ReadValidatedString("Surname is not valid. Enter again:");
        Console.Write("Enter new student age: ");
        int newAge = Helper.ReadValidatedInt("Age must be a valid number. Enter again:");

        try
        {
            _studentService.Update(id, new Student { Name = newName, Surname = newSurname, Age = newAge });
            Helper.ColorWrite(ConsoleColor.Green, "Student updated successfully!");
            Helper.PlaySound("notify.wav");
        }
        catch (Exception ex)
        {
            Helper.ColorWrite(ConsoleColor.Red, ex.Message);
            Helper.PlaySound("chord.wav");
        }
    }

    public void DeleteStudent()
    {
        Console.WriteLine("--- Delete Student ---");
        Console.Write("Enter student ID to delete: ");
        int id = Helper.ReadValidatedInt("ID must be a valid number. Enter again:");

        try
        {
            _studentService.Delete(id);
            Helper.ColorWrite(ConsoleColor.Green, "Student deleted successfully!");
            Helper.PlaySound("notify.wav");
        }
        catch (Exception ex)
        {
            Helper.ColorWrite(ConsoleColor.Red, ex.Message);
            Helper.PlaySound("chord.wav");
        }
    }

    public void GetStudentById()
    {
        Console.WriteLine("--- View Student Details ---");
        Console.Write("Enter student ID: ");
        int id = Helper.ReadValidatedInt("ID must be a valid number. Enter again:");

        try
        {
            var student = _studentService.GetById(id);
            Console.WriteLine($"ID: {student.Id} | Name: {student.Name} {student.Surname} | Age: {student.Age} | Group: {student.Group?.Name}");
            Helper.PlaySound("notify.wav");
        }
        catch (Exception ex)
        {
            Helper.ColorWrite(ConsoleColor.Red, ex.Message);
            Helper.PlaySound("chord.wav");
        }
    }

  

    public void GetStudentsByAge()
    {
        Console.WriteLine("--- List Students by Age ---");
        Console.Write("Enter age: ");
        int age = Helper.ReadValidatedInt("Age must be a valid number. Enter again:");

        try
        {
            var list = _studentService.GetAllByAge(age);
            foreach (var s in list)
                Console.WriteLine($"ID: {s.Id} | Name: {s.Name} {s.Surname} | Age: {s.Age}");
            Helper.PlaySound("notify.wav");
        }
        catch (Exception ex)
        {
            Helper.ColorWrite(ConsoleColor.Red, ex.Message);
            Helper.PlaySound("chord.wav");
        }
    }

    public void GetStudentsByGroup()
    {
        Console.WriteLine("--- List Students by Group ---");
        Console.Write("Enter group ID: ");
        int groupId = Helper.ReadValidatedInt("Group ID must be a valid number. Enter again:");

        try
        {
            var list = _studentService.GetAllByGroupId(groupId);
            foreach (var s in list)
                Console.WriteLine($"ID: {s.Id} | Name: {s.Name} {s.Surname}");
            Helper.PlaySound("notify.wav");
        }
        catch (Exception ex)
        {
            Helper.ColorWrite(ConsoleColor.Red, ex.Message);
            Helper.PlaySound("chord.wav");
        }
    }

    public void SearchStudents()
    {
        Console.WriteLine("--- Search Students ---");
        Console.Write("Enter keyword (name or surname): ");
        string keyword = Console.ReadLine();

        try
        {
            var list = _studentService.SearchByNameOrSurname(keyword);
            foreach (var s in list)
                Console.WriteLine($"ID: {s.Id} | Name: {s.Name} {s.Surname} | Age: {s.Age}");
            Helper.PlaySound("notify.wav");
        }
        catch (Exception ex)
        {
            Helper.ColorWrite(ConsoleColor.Red, ex.Message);
            Helper.PlaySound("chord.wav");
        }
    }

 
    public void StudentMenu()
    {
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n -----STUDENT MENU----");
            Console.WriteLine("1. Create");
            Console.WriteLine("2. Update");
            Console.WriteLine("3. Delete");
            Console.WriteLine("4. GetById");
            Console.WriteLine("5. GetAllByAge");
            Console.WriteLine("6. GetAllByGroupId");
            Console.WriteLine("7. SearchByNameOrSurname");
            Console.WriteLine("0. Back");
            Console.ResetColor();

            Console.Write("Enter your choice: ");
            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Helper.ColorWrite(ConsoleColor.Red, "Invalid input!");
                continue;
            }

            switch (choice)
            {
                case 1:
                    CreateStudent();
                    break;
                case 2:
                    UpdateStudent();
                    break;
                case 3:
                    DeleteStudent();
                    break;
                case 4:
                    GetStudentById();
                    break;
                case 5:
                    GetStudentsByAge();
                    break;
                case 6:
                    GetStudentsByGroup();
                    break;
                case 7:
                    SearchStudents();
                    break;
                case 0:
                    return;
                default:
                    Helper.ColorWrite(ConsoleColor.Red, "Invalid choice!");
                    break;
            }
        }
    }
}

