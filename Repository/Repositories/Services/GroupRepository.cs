using Domain.Models;
using Repository.Context;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories.Services;

public class GroupRepository:IGroupRepository
{
    public void Create(Group model)
    {
        AppDbContext<Group>.Models.Add(model);
    }

    public void Delete(int id)
    {
        var entity = AppDbContext<Group>.Models.Find(cg => cg.Id == id);
        AppDbContext<Group>.Models.Remove(entity);
    }

    public List<Group> GetAll(Predicate<Group?> predicate = null)
    {
        if (predicate == null)
        {
            return AppDbContext<Group>.Models;
        }
        return AppDbContext<Group>.Models.FindAll(predicate);
    }

    public Group GetById(int id)
    {
        return AppDbContext<Group>.Models.Find(cg => cg.Id == id);
    }

    public void Update(int id, Group model)
    {
        var existingEntity = AppDbContext<Group>.Models.Find(cg => cg.Id == id);
        existingEntity.TeacherName = model.TeacherName;
        existingEntity.Room = model.Room;
    }
}
