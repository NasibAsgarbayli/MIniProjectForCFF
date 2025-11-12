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
        if (id < 0)
            throw new ArgumentException("Id has to be positive numbers!");

        var student = _studentRepository.GetById(id);
        if (student == null)
            throw new NotFoundException($"Student with ID {id} not found!");

        return student;
    }

    public List<Student> GetAllStudents()
    {
        var students = _studentRepository.GetAll();
        if (!students.Any())
        {
            throw new EmptyList("No students found.");
        }

        return students;
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
    // StudentService - Update
    public void Update(int id, Student student)
    {
        if (id < 0)
            throw new ArgumentException("Id has to be positive numbers!");

        if (student == null)
            throw new ArgumentNullException("Student cannot be null!");

        // ID yoxlaması - əgər tapılmazsa NotFoundException atılır
        var existingStudent = _studentRepository.GetById(id)
            ?? throw new NotFoundException($"Student with ID {id} not found!");

        // Əgər group verilibsə, mövcud olub olmadığını yoxla
        if (student.Group != null)
        {
            var group = _groupService.GetById(student.Group.Id); // NotFoundException atacaq əgər yoxdursa
            existingStudent.Group = group;
        }

        // Name, Surname və Age update
        if (!string.IsNullOrWhiteSpace(student.Name))
            existingStudent.Name = student.Name;

        if (!string.IsNullOrWhiteSpace(student.Surname))
            existingStudent.Surname = student.Surname;

        if (student.Age > 0)
            existingStudent.Age = student.Age;

        _studentRepository.Update(id, existingStudent);
    }

    // StudentService - Delete
    public void Delete(int id)
    {
        if (id < 0)
            throw new ArgumentException("Id has to be positive numbers!");

        // ID yoxlaması - tapılmazsa NotFoundException atılır
        var existingStudent = _studentRepository.GetById(id)
            ?? throw new NotFoundException($"Student with ID {id} not found!");

        _studentRepository.Delete(id);
    }

}
