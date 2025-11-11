using Domain.Models;

namespace Services.Services.Interfaces;

public interface IGroupService
{
    void Create(Group courseGroup);
    void Update(int id, Group courseGroup);
    void Delete(int id);
    Group GetById(int id);
    List<Group> GetAll();
    List<Group> GetAllByTeacherName(string teacherName);
    List<Group> GetAllByRoom(string room);
    List<Group> SearchByName(string name);

}
