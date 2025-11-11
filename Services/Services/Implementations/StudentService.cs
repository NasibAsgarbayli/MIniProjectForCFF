using Domain.Models;
using Repository.Repositories.Interfaces;
using Services.Exceptions;
using Services.Services.Interfaces;

namespace Services.Services.Implementations;

public class StudentService : IStudentService
{

    private readonly IStudentRepository _studentRepository;
    private readonly IGroupService _groupService;
    public StudentService(IStudentRepository studentRepository, IGroupService groupService)
    {
        _studentRepository = studentRepository;
        _groupService = groupService;
    }
    public void Create(int groupId, Student student)
    {
        var group = _groupService.GetById(groupId);
        if (group == null)
        {

            Console.WriteLine("There is no group with given ID! Create a new group!");
        }

        if (student == null)
        {

            throw new ArgumentNullException("Student cant be null!");
        }

        student.Group = group;
        _studentRepository.Create(student);
    }

    public void Delete(int id)
    {
        if (id < 0)
        {

            Console.WriteLine("Id has to be positive numbers!");
        }

        _studentRepository.Delete(id);
    }



    public List<Student> GetAllByAge(int age)
    {
        if (age < 0)
        {

            Console.WriteLine("Age has to be positive numbers!");
        }

        var students = _studentRepository.GetAll(s => s != null && s.Age == age);
        if (!students.Any())
        {

            throw new EmptyList("No students found with the given age.");
        }

        return students;
    }

    public List<Student> GetAllByGroupId(int groupId)
    {
        if (groupId < 0)
        {

            Console.WriteLine("Group ID has to be positive numbers!");
        }

        var students = _studentRepository.GetAll(s => s != null && s.Group != null && s.Group.Id == groupId);
        if (!students.Any())
        {

            throw new EmptyList("No students found in the given group.");
        }

        return students;
    }

    public Student GetById(int id)
    {
        var student = _studentRepository.GetById(id);
        if (student == null)
        {

            Console.WriteLine("There is no student with given ID!");
        }

        return student;
    }

    public List<Student> SearchByNameOrSurname(string keyword)
    {
        var students = _studentRepository.GetAll(s =>
            s != null &&
            (s.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
             s.Surname.Contains(keyword, StringComparison.OrdinalIgnoreCase)));

        if (!students.Any())
        {

            throw new EmptyList("No students found with the given keyword.");
        }

        return students;
    }

    public void Update(int id, Student student)
    {
        if (id < 0)
        {

            Console.WriteLine("Id has to be positive numbers!");
        }
        if (student == null)
        {

            throw new ArgumentNullException("Student cannot be null!");
        }

        var existingStudent = _studentRepository.GetById(id);
        if (existingStudent == null)
        {

            Console.WriteLine("Student not found!");
        }

        if (!string.IsNullOrWhiteSpace(student.Name))
        {

            existingStudent.Name = student.Name;
        }

        if (!string.IsNullOrWhiteSpace(student.Surname))
        {

            existingStudent.Surname = student.Surname;
        }

        if (student.Age > 0)
        {

            existingStudent.Age = student.Age;
        }

        if (student.Group != null)
        {
            var group = _groupService.GetById(student.Group.Id);
            if (group == null)
                Console.WriteLine("The provided CourseGroup does not exist!");

            existingStudent.Group = group;
        }

        _studentRepository.Update(id, existingStudent);
    }
}
