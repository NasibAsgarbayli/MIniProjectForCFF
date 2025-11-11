using Domain.Models;

namespace Services.Services.Interfaces;

public interface IGroupService
{
    void Create(Group group);
    void Update(int id, Group group);
    void Delete(int id);
    Group GetById(int id);
    List<Group> GetAll();
    List<Group> GetAllByTeacherName(string teacherName);
    List<Group> GetAllByRoom(string room);
    List<Group> SearchByName(string name);

}
