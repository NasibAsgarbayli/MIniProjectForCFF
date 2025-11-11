using Presentation.Controllers;
using Repository.Repositories.Services;
using Services.Helpers;
using Services.Services.Implementations;

internal class Program
{
    private static void Main(string[] args)
    {
      

        
        GroupRepository groupRepository = new();
        StudentRepository studentRepository = new();
        GroupService groupService = new(groupRepository);
        StudentService studentService = new(studentRepository, groupService);

        AppController appController = new(groupService, studentService);

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("\n ----MAIN MENU---- ");
            Console.WriteLine("1. Group Menu");
            Console.WriteLine("2. Student Menu");
            Console.WriteLine("0. Exit");
            Console.ResetColor();

        MainInput:
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Enter your choice: ");
            Console.ResetColor();

            string input = Console.ReadLine();
            if (!int.TryParse(input, out int mainChoice))
            {
                Helper.ColorWrite(ConsoleColor.Red, "Input type is not correct! Please enter a number.");
                goto MainInput;
            }

            switch (mainChoice)
            {
                case 1:
                    appController.GroupMenu();
                    break;
                case 2:
                    appController.StudentMenu();
                    break;
                case 0:
                    Helper.ColorWrite(ConsoleColor.Green, "Bye!");
                    return;
                default:
                    Helper.ColorWrite(ConsoleColor.Red, "Invalid menu choice!");
                    goto MainInput;
            }
        }
    }
}
