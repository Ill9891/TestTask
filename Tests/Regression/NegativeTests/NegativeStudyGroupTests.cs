using System;
using System.Collections.Generic;
using EmulationOfServices.Models;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Tests.Regression.NegativeTests
{
    [TestFixture]
    public class NegativeStudyGroupTests : TestBase
    {
        [TestCase("Chem")]
        [TestCase("Invalid Study Group name that has more than thirty characters 1234")]
        public void CreateStudyGroupWithInvalidNameLength(string studyGroupName)
        {
            var group = new StudyGroup(RandomGroupId, studyGroupName, Subject.Physics, DateTime.Now, new List<User>());

            var result = StudyGroupController.CreateStudyGroup(group);
            ClassicAssert.IsFalse(result, $"StudyGroup: '{group.Name}' with Id: {group.StudyGroupId} was created.");
        }

        [TestCase]
        public void CreateStudyGroupWithInvalidSubject()
        {
            var studyGroupName = "Biology Study Group";
            var group = new StudyGroup(RandomGroupId, studyGroupName, Subject.Biology, DateTime.Now, new List<User>());

            var result = StudyGroupController.CreateStudyGroup(group);
            ClassicAssert.IsFalse(result, $"StudyGroup: '{group.Name}' with Id: {group.StudyGroupId} for subject {group.Subject} was created.");
        }

        [TestCase]
        public void CreateStudyGroupForTheSameSubject()
        {
            var firstStudyGroupName = $"Math group {RandomGroupId}";
            var firstGroup = new StudyGroup(RandomGroupId, firstStudyGroupName, Subject.Chemistry, DateTime.Now, new List<User>());

            RandomGroupId++;

            var secondStudyGroupName = $"Math group {RandomGroupId}";
            var secondGroup = new StudyGroup(RandomGroupId, secondStudyGroupName, Subject.Chemistry, DateTime.Now, new List<User>());

            var firstGroupResult = StudyGroupController.CreateStudyGroup(firstGroup);
            ClassicAssert.IsTrue(firstGroupResult, $"StudyGroup: '{firstGroup.Name}' with Id: {firstGroup.StudyGroupId} for subject {firstGroup.Subject} wasn't created.");

            var secondGroupResult = StudyGroupController.CreateStudyGroup(secondGroup);
            ClassicAssert.IsFalse(secondGroupResult, $"StudyGroup: '{secondGroup.Name}' with Id: {secondGroup.StudyGroupId} for subject {secondGroup.Subject} was created.");
        }

        [TestCase]
        public void CreateStudyGroupWithTheSameId()
        {
            var firstStudyGroupName = $"Math group {RandomGroupId}";
            var firstGroup = new StudyGroup(RandomGroupId, firstStudyGroupName, Subject.Math, DateTime.Now, new List<User>());

            var secondStudyGroupName = $"Chemistry group {RandomGroupId}";
            var secondGroup = new StudyGroup(RandomGroupId, secondStudyGroupName, Subject.Chemistry, DateTime.Now, new List<User>());

            var firstGroupResult = StudyGroupController.CreateStudyGroup(firstGroup);
            ClassicAssert.IsTrue(firstGroupResult, $"StudyGroup: '{firstGroup.Name}' with Id: {firstGroup.StudyGroupId} for subject {firstGroup.Subject} wasn't created.");

            var secondGroupResult = StudyGroupController.CreateStudyGroup(secondGroup);
            ClassicAssert.IsFalse(secondGroupResult, $"StudyGroup: '{secondGroup.Name}' with Id: {secondGroup.StudyGroupId} for subject {secondGroup.Subject} was created.");
        }
    }
}