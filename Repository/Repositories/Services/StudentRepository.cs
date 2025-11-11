using Domain.Models;
using Repository.Context;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories.Services;

public class StudentRepository:IStudentRepository
{
    public void Create(Student entity)
    {
        AppDbContext<Student>.Models.Add(entity);
    }

    public void Delete(int id)
    {
        var entity = AppDbContext<Student>.Models.Find(s => s.Id == id);
        AppDbContext<Student>.Models.Remove(entity);
    }

    public List<Student> GetAll(Predicate<Student?> predicate = null)
    {
        if (predicate == null)
        {
            return AppDbContext<Student>.Models;
        }
        return AppDbContext<Student>.Models.FindAll(predicate);
    }

    public Student GetById(int id)
    {
        return AppDbContext<Student>.Models.Find(s => s.Id == id);
    }

    public void Update(int id, Student entity)
    {
        var existingEntity = AppDbContext<Student>.Models.Find(s => s.Id == id);
        existingEntity.Name = entity.Name;
        existingEntity.Surname = entity.Surname;
        existingEntity.Age = entity.Age;
        existingEntity.Group = entity.Group;
    }
}
