using System;
using System.Collections.Generic;
using System.Linq;
using EmulationOfServices.Models;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Tests.Regression.PositiveTests
{
    [TestFixture]
    public class StudyGroupTests : TestBase
    {
        //I didn't create separate folder and class for smoke category, but I would move the CreateStudyGroup test to the smoke as I described in TestPlan.txt
        [TestCase]
        public void CreateStudyGroup()
        {
            var studyGroupName = $"Chemistry group {RandomGroupId}";
            var group = new StudyGroup(RandomGroupId, studyGroupName, Subject.Chemistry, DateTime.Now, new List<User>());

            var result = StudyGroupController.CreateStudyGroup(group);
            ClassicAssert.IsTrue(result, $"StudyGroup: '{group.Name}' with Id: {group.StudyGroupId} wasn't created.");
        }

        [TestCase]
        public void JoinStudyGroup()
        {
            var studyGroupName = $"Math group {RandomGroupId}";
            var group = new StudyGroup(RandomGroupId, studyGroupName, Subject.Math, DateTime.Now, new List<User>());

            var studyGruGroupWasCreatedResult = StudyGroupController.CreateStudyGroup(group);
            ClassicAssert.IsTrue(studyGruGroupWasCreatedResult, $"StudyGroup: '{group.Name}' with Id: {group.StudyGroupId} wasn't created.");

            var user = new User(RandomUserId);

            var joinStudyGroupResult = StudyGroupController.JoinStudyGroup(RandomGroupId, user.Id);
            ClassicAssert.IsTrue(joinStudyGroupResult, $"User with Id {user.Id} didn't join StudyGroup: '{group.Name}' with Id: {group.StudyGroupId} successfully.");
        }

        [TestCase]
        public void LeaveStudyGroup()
        {
            var studyGroupName = $"Physics group {RandomGroupId}";
            var group = new StudyGroup(RandomGroupId, studyGroupName, Subject.Physics, DateTime.Now, new List<User>());

            var studyGruGroupWasCreatedResult = StudyGroupController.CreateStudyGroup(group);
            ClassicAssert.IsTrue(studyGruGroupWasCreatedResult, $"StudyGroup: '{group.Name}' with Id: {group.StudyGroupId} wasn't created.");

            var user = new User(RandomUserId);

            var joinStudyGroupResult = StudyGroupController.JoinStudyGroup(RandomGroupId, user.Id);
            ClassicAssert.IsTrue(joinStudyGroupResult, $"User with Id {user.Id} didn't join StudyGroup: '{group.Name}' with Id: {group.StudyGroupId} successfully.");

            var leaveStudyGroupResult = StudyGroupController.LeaveStudyGroup(RandomGroupId, user.Id);
            ClassicAssert.IsTrue(leaveStudyGroupResult, $"User with Id {user.Id} didn't leave StudyGroup: '{group.Name}' with Id: {group.StudyGroupId} successfully.");
        }

        [TestCase]
        public void FilterStudyGroupsBySubject()
        {
            StudyGroup group;

            var allGroups = StudyGroupController.GetStudyGroups();

            //If there should exist only one study group for single subject for all users then we need to implement logic like this almost in all tests
            //If each user can create a one study group for single subject then we don't need a such logic and can safely create new group in each test
            if (allGroups.Count > 0)
            {
                group = allGroups.First();
            }
            else
            {
                var studyGroupName = $"Math group {RandomGroupId}";
                group = new StudyGroup(RandomGroupId, studyGroupName, Subject.Math, DateTime.Now, new List<User>());

                var result = StudyGroupController.CreateStudyGroup(group);
                ClassicAssert.IsTrue(result, $"StudyGroup: '{group.Name}' with Id: {RandomGroupId} wasn't created.");
            }

            var studyGroups = StudyGroupController.SearchStudyGroups(Subject.Math.ToString());

            //To check additionally that there only one study group for the subject according to acceptance criteria
            ClassicAssert.AreEqual(studyGroups.Count, 1, $"There were more then one study groups for subject {group.Subject}.");
            ClassicAssert.AreEqual(studyGroups.Single().StudyGroupId, group.StudyGroupId, $"Study Group with Id {group.StudyGroupId} for subject {group.Subject} was not found.");
        }

        [TestCase]
        public void RetrieveAllStudyGroups()
        {
            var studyGroups = StudyGroupController.GetStudyGroups();

            var studyGroupName = $"Math group {RandomGroupId}";
            var group = new StudyGroup(RandomGroupId, studyGroupName, Subject.Math, DateTime.Now, new List<User>());

            var result = StudyGroupController.CreateStudyGroup(group);
            ClassicAssert.IsTrue(result, $"StudyGroup: '{group.Name}' with Id: {RandomGroupId} wasn't created.");

            studyGroups.Add(group);

            var studyGroupsAfterAddingNewGroups = StudyGroupController.GetStudyGroups();

            studyGroups.Sort();
            studyGroupsAfterAddingNewGroups.Sort();

            CollectionAssert.AreEqual(studyGroups, studyGroupsAfterAddingNewGroups);
        }
    }
}