using Domain.Models;

namespace Services.Services.Interfaces;

public interface IStudentService
{
    void Create(int groupId, Student student);
    void Update(int id, Student student);
    void Delete(int id);
    Student GetById(int id);
    List<Student> GetAllByAge(int age);
    List<Student> GetAllByGroupId(int groupId);
    List<Student> SearchByNameOrSurname(string keyword);
}
