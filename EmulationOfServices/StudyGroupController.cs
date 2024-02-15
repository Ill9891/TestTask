using System.Collections.Generic;
using EmulationOfServices.Models;

namespace EmulationOfServices
{
    //Just to emulate the service instead of creating a mock
    public class StudyGroupController
    {
        public bool CreateStudyGroup(StudyGroup studyGroup)
        {
            return true;
        }

        public List<StudyGroup> GetStudyGroups()
        {
            return new List<StudyGroup>();
        }

        public List<StudyGroup> SearchStudyGroups(string subject)
        {
            return new List<StudyGroup>();
        }

        public bool JoinStudyGroup(int studyGroupId, int userId)
        {
            return true;
        }

        public bool LeaveStudyGroup(int studyGroupId, int userId)
        {
            return true;
        }
    }
}