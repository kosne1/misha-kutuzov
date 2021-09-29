﻿using System.Collections.Generic;
using Isu.Entities;

namespace Isu.Models
{
    public interface IGroupService
    {
        Group AddGroup(string name);
        Student AddStudent(Group group, string name);

        Student GetStudent(int id);
        Student FindStudent(string name);
        List<Student> FindStudents(string groupName);
        List<Student> FindStudents(int courseNumber);

        Group FindGroup(string groupName);
        List<Group> FindGroups(int courseNumber);

        void ChangeStudentGroup(Student student, Group newGroup);
    }
}