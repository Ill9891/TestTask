using System;
using EmulationOfServices;
using NUnit.Framework;

namespace Tests
{
    public class TestBase
    {
        protected StudyGroupController StudyGroupController;
        protected int RandomGroupId;
        protected int RandomUserId;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            StudyGroupController = new StudyGroupController();
        }

        [SetUp]
        public void SetUp()
        {
            if (TestContext.CurrentContext.Test.FullName.Contains("StudyGroup"))
            {
                RandomGroupId = new Random().Next(1, 1001);
                RandomUserId = new Random().Next(1, 1001);
            }
        }
    }
}