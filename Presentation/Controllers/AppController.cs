using Domain.Models;
using Services.Exceptions;
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
        string name = Helper.CheckLetterOrDigit("Group name is not valid. Enter again:");
        Console.Write("Enter teacher name: ");
        string teacher = Helper.CheckString("Teacher name is not valid. Enter again:");
        Console.Write("Enter room: ");
        string room = Helper.CheckLetterOrDigit("Room cannot be empty. Enter again:");

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

    // UpdateGroup - FIXED
    public void UpdateGroup()
    {
        while (true)
        {
            try
            {
                Console.Write("Enter Id: ");
                int id = Helper.CheckInt("Enter ID to update:");

                // Əvvəlcə ID-nin mövcud olub-olmadığını yoxlayırıq
                var existing = _groupService.GetById(id);

                // ID düzgündürsə, indi digər məlumatları istəyirik
                Console.Write("Enter new name: ");
                string newName = Console.ReadLine();
                Console.Write("Enter new teacher name: ");
                string newTeacher = Console.ReadLine();
                Console.Write("Enter new room: ");
                string newRoom = Console.ReadLine();

                _groupService.Update(id, new Group { Name = newName, TeacherName = newTeacher, Room = newRoom });
                Helper.ColorWrite(ConsoleColor.Green, "Group updated successfully!");
                Helper.PlaySound("notify.wav");
                break;
            }
            catch (NotFoundException ex)
            {
                Helper.ColorWrite(ConsoleColor.Red, ex.Message);
                Helper.PlaySound("chord.wav");
                Console.WriteLine("Please enter a valid ID or press '0' to cancel:");
                var input = Console.ReadLine();
                if (input == "0") break;
            }
            catch (ArgumentException ex)
            {
                Helper.ColorWrite(ConsoleColor.Red, ex.Message);
                Helper.PlaySound("chord.wav");
                Console.WriteLine("Please try again or press '0' to cancel.");
                var input = Console.ReadLine();
                if (input == "0") break;
            }
            catch (Exception ex)
            {
                Helper.ColorWrite(ConsoleColor.Red, ex.Message);
                Helper.PlaySound("chord.wav");
                Console.WriteLine("Please try again or press '0' to cancel.");
                var input = Console.ReadLine();
                if (input == "0") break;
            }
        }
    }


    // DeleteGroup - FIXED
    public void DeleteGroup()
    {
        while (true)
        {
            try
            {
                Console.Write("Enter Id for delete: ");
                int id = Helper.CheckInt("Enter ID to delete:");

                // ID-nin mövcud olub-olmadığını yoxlayırıq
                var existing = _groupService.GetById(id);

                _groupService.Delete(id);
                Helper.ColorWrite(ConsoleColor.Green, "Group deleted successfully!");
                Helper.PlaySound("notify.wav");
                break;
            }
            catch (NotFoundException ex)
            {
                Helper.ColorWrite(ConsoleColor.Red, ex.Message);
                Helper.PlaySound("chord.wav");
                Console.WriteLine("Please try again or press '0' to cancel.");
                var input = Console.ReadLine();
                if (input == "0") break;
            }
            catch (ArgumentException ex)
            {
                Helper.ColorWrite(ConsoleColor.Red, ex.Message);
                Helper.PlaySound("chord.wav");
                Console.WriteLine("Please try again or press '0' to cancel.");
                var input = Console.ReadLine();
                if (input == "0") break;
            }
        }
    }


    public void GetGroupById()
    {
        int id = Helper.CheckInt("Enter group ID:");

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
        string name = Helper.CheckString("Name is not valid. Enter again:");
        Console.Write("Enter student surname: ");
        string surname = Helper.CheckString("Surname is not valid. Enter again:");
        Console.Write("Enter student age: ");
        int age = Helper.CheckInt("Age must be a valid number. Enter again:");
        Console.Write("Enter group ID: ");
        int groupId = Helper.CheckInt("Group ID must be a valid number. Enter again:");

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
        while (true)
        {
            try
            {
                Console.WriteLine("--- Update Student ---");
                Console.Write("Enter student ID to update: ");
                int id = Helper.CheckInt("ID must be a valid number. Enter again:");

                // Ən başda yoxlama
                var existing = _studentService.GetById(id);

                Console.Write("Enter new student name (or leave empty): ");
                string newName = Console.ReadLine();
                Console.Write("Enter new student surname (or leave empty): ");
                string newSurname = Console.ReadLine();
                Console.Write("Enter new student age (or leave empty): ");
                string ageInput = Console.ReadLine();
                int newAge = 0;
                if (!string.IsNullOrWhiteSpace(ageInput))
                {
                    if (!int.TryParse(ageInput, out newAge))
                        throw new ArgumentException("Age must be a valid number!");
                }

                var studentToUpdate = new Student
                {
                    Name = newName,
                    Surname = newSurname,
                    Age = newAge > 0 ? newAge : existing.Age,
                    Group = existing.Group // default, changed only if user provides new group later
                };

                _studentService.Update(id, studentToUpdate);
                Helper.ColorWrite(ConsoleColor.Green, "Student updated successfully!");
                Helper.PlaySound("notify.wav");
                break;
            }
            catch (NotFoundException ex)
            {
                Helper.ColorWrite(ConsoleColor.Red, ex.Message);
                Helper.PlaySound("chord.wav");
                Console.WriteLine("Please enter a valid ID or press '0' to cancel:");
                var input = Console.ReadLine();
                if (input == "0") break;
            }
            catch (ArgumentException ex)
            {
                Helper.ColorWrite(ConsoleColor.Red, ex.Message);
                Helper.PlaySound("chord.wav");
                Console.WriteLine("Please try again or press '0' to cancel.");
                var input = Console.ReadLine();
                if (input == "0") break;
            }
            catch (Exception ex)
            {
                Helper.ColorWrite(ConsoleColor.Red, ex.Message);
                Helper.PlaySound("chord.wav");
                Console.WriteLine("Please try again or press '0' to cancel.");
                var input = Console.ReadLine();
                if (input == "0") break;
            }
        }
    }


    public void DeleteStudent()
    {
        while (true)
        {
            try
            {
                Console.WriteLine("--- Delete Student ---");
                Console.Write("Enter student ID to delete: ");
                int id = Helper.CheckInt("ID must be a valid number. Enter again:");

                _studentService.Delete(id);
                Helper.ColorWrite(ConsoleColor.Green, "Student deleted successfully!");
                Helper.PlaySound("notify.wav");
                break;
            }
            catch (Exception ex)
            {
                Helper.ColorWrite(ConsoleColor.Red, ex.Message);
                Helper.PlaySound("chord.wav");
                Console.WriteLine("Please try again or press '0' to cancel.");
                string input = Console.ReadLine();
                if (input == "0")
                    break;
            }
        }
    }

    public void GetStudentById()
    {
        Console.WriteLine("--- View Student Details ---");
        Console.Write("Enter student ID: ");
        int id = Helper.CheckInt("ID must be a valid number. Enter again:");

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

    public void GetAllStudents()
    {
        Console.WriteLine("--- List All Students ---");
        try
        {
            var list = _studentService.GetAllStudents();
            foreach (var s in list)
                Console.WriteLine($"ID: {s.Id} | Name: {s.Name} {s.Surname} | Age: {s.Age} | Group: {s.Group?.Name}");
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
        int age = Helper.CheckInt("Age must be a valid number. Enter again:");

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
        int groupId = Helper.CheckInt("Group ID must be a valid number. Enter again:");

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
            Console.WriteLine("5. GetAllStudents");
            Console.WriteLine("6. GetAllByAge");
            Console.WriteLine("7. GetAllByGroupId");
            Console.WriteLine("8. SearchByNameOrSurname");
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
                    GetAllStudents();
                    break;
                case 6:
                    GetStudentsByAge();
                    break;
                case 7:
                    GetStudentsByGroup();
                    break;
                case 8:
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

