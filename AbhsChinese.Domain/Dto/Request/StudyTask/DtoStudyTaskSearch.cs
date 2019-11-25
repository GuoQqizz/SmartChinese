using AbhsChinese.Domain.Enum;
using System;

namespace AbhsChinese.Domain.Dto.Request.StudyTask
{
    public class DtoStudyTaskSearch : DtoSearch
    {
        public int StudentId { get; set; }
        public StudyTaskTypeEnum TaskType { get; set; }
        public string TaskStatus { get; set; }
        public string[] TaskStatusParameter
        {
            get
            {
                string[] parameters = Array.Empty<string>();
                if (TaskStatus.HasValue())
                {
                    parameters = TaskStatus.Split(',');
                }
                return parameters;
            }
        }
    }
}