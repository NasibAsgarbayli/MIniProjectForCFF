using Domain.Models;

namespace Services.Services.Interfaces;

public interface IGroupService
{
    void Create(Group courseGroup);
    void Update(int id, CourseGroup courseGroup);
    void Delete(int id);
    CourseGroup GetById(int id);
    List<CourseGroup> GetAll();
    List<CourseGroup> GetAllByTeacherName(string teacherName);
    List<CourseGroup> GetAllByRoom(string room);
    List<CourseGroup> SearchByName(string name);

}
