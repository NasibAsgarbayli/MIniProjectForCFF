using Domain.Models;
using Repository.Repositories.Interfaces;
using Services.Exceptions;
using Services.Services.Interfaces;

namespace Services.Services.Implementations;

public class GroupService: IGroupService
{
    private readonly IGroupRepository _groupRepository;
    public GroupService(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }
    public void Create(Group courseGroup)
    {
        if (courseGroup is null)
            throw new ArgumentNullException("Course group cannot be null!");

        bool nameExists = _groupRepository.GetAll()
            .Any(g => g.Name.Equals(courseGroup.Name, StringComparison.OrdinalIgnoreCase));

        if (nameExists)
            throw new AlreadyExist($"A course group with name '{courseGroup.Name}' already exists!");

        _groupRepository.Create(courseGroup);
    }

    public void Delete(int id)
    {
        if (id < 0)
            Console.WriteLine("Id cannot be a negative");

        _groupRepository.Delete(id);
    }

    public List<Group> GetAll() => _groupRepository.GetAll();

    public List<Group> GetAllByRoom(string room)
    {
        var groups = _groupRepository.GetAll(cg =>
            cg != null &&
            !string.IsNullOrWhiteSpace(cg.Room) &&
            cg.Room.Contains(room, StringComparison.OrdinalIgnoreCase));

        if (groups.Count == 0)
            Console.WriteLine(" with the given room name have not group"); 

        return groups;
    }

    public List<Group> GetAllByTeacherName(string teacherName)
    {
        var groups = _groupRepository.GetAll(cg =>
            cg != null &&
            !string.IsNullOrWhiteSpace(cg.TeacherName) &&
            cg.TeacherName.Contains(teacherName, StringComparison.OrdinalIgnoreCase));

        if (groups.Count == 0)
           Console.WriteLine("No course groups found with the given teacher name.");

        return groups;
    }

    public Group GetById(int id)
    {
        if (id < 0)
           Console.WriteLine("Id has to be positive numbers!");

        var group = _groupRepository.GetById(id);
        if (group == null)
            Console.WriteLine("Course group not found!");

        return group;
    }

    public List<Group> SearchByName(string name)
    {
        var groups = _groupRepository.GetAll(cg =>
            cg != null &&
            !string.IsNullOrWhiteSpace(cg.Name) &&
            cg.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

        if (groups.Count == 0)
            throw new EmptyList("No groups found with the given keyword.");

        return groups;
    }

    public void Update(int id, Group courseGroup)
    {
        if (id < 0)
            Console.WriteLine("Id has to be positive numbers!"); 
        if (courseGroup is null)
            throw new ArgumentNullException("Course group cannot be null!");

        var existingGroup = _groupRepository.GetById(id);
        if (existingGroup == null)
            Console.WriteLine("Course group not found!");

        if (!string.IsNullOrWhiteSpace(courseGroup.Name))
        {
            bool nameExists = _groupRepository.GetAll()
                .Any(g => g.Id != id && g.Name.Equals(courseGroup.Name, StringComparison.OrdinalIgnoreCase));

            if (nameExists)
                throw new AlreadyExist($"A course group with name '{courseGroup.Name}' already exists!");

            existingGroup.Name = courseGroup.Name;
        }

        if (!string.IsNullOrWhiteSpace(courseGroup.TeacherName))
            existingGroup.TeacherName = courseGroup.TeacherName;

        if (!string.IsNullOrWhiteSpace(courseGroup.Room))
            existingGroup.Room = courseGroup.Room;

        _groupRepository.Update(id, existingGroup);
    }
}
