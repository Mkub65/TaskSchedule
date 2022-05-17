namespace TaskScheduler
{
    using System.Globalization;
    using System.Text;
    using CsvHelper;
    using CsvHelper.Configuration;

    public class TaskCsv : IComparable<TaskCsv>
    {
        public TaskCsv()
        {
        }

        public string? ID { get; set; }

        public int? Priority { get; set; }

        public string? Description { get; set; }

        public string? Predecessors { get; set; }

        public int? Work { get; set; }

        public string? Responsible { get; set; }

        public DateTime MinStartDate { get; set; }

        public DateTime MaxEndDate { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public static List<AbstractTask> ToList(List<TaskCsv> listCsv)
        {
            var taskList = new Dictionary<string, AbstractTask>();

            for (int i = 0; i < listCsv.Count - 1; i++)
            {
                if (listCsv[i + 1].ID.Contains(listCsv[i].ID))
                {
                    var milestoneBuilder = new MilestoneBuilder();

                    milestoneBuilder.SetID(listCsv[i].ID);
                    milestoneBuilder.SetPriority(listCsv[i].Priority);
                    milestoneBuilder.SetDescription(listCsv[i].Description);
                    milestoneBuilder.SetPredecessors();
                    milestoneBuilder.SetWork(listCsv[i].Work);
                    milestoneBuilder.SetResponsible(listCsv[i].Responsible);
                    milestoneBuilder.SetMinStartDate(listCsv[i].MinStartDate);
                    milestoneBuilder.SetMaxEndDate(listCsv[i].MaxEndDate);
                    Milestone resaultMilestone = milestoneBuilder.Build();

                    taskList.Add(listCsv[i].ID, resaultMilestone);
                }
                else
                {
                    var taskBuilder = new TaskBuilder();

                    taskBuilder.SetID(listCsv[i].ID);
                    taskBuilder.SetPriority(listCsv[i].Priority);
                    taskBuilder.SetDescription(listCsv[i].Description);
                    taskBuilder.SetPredecessors();
                    taskBuilder.SetWork(listCsv[i].Work);
                    taskBuilder.SetResponsible(listCsv[i].Responsible);
                    taskBuilder.SetMinStartDate(listCsv[i].MinStartDate);
                    taskBuilder.SetMaxEndDate(listCsv[i].MaxEndDate);
                    Task resaultTask = taskBuilder.Build();

                    taskList.Add(listCsv[i].ID, resaultTask);
                }
            }

            var j = listCsv.Count - 1;

            var lastTaskBuilder = new TaskBuilder();

            lastTaskBuilder.SetID(listCsv[j].ID);
            lastTaskBuilder.SetPriority(listCsv[j].Priority);
            lastTaskBuilder.SetDescription(listCsv[j].Description);
            lastTaskBuilder.SetPredecessors();
            lastTaskBuilder.SetWork(listCsv[j].Work);
            lastTaskBuilder.SetResponsible(listCsv[j].Responsible);
            lastTaskBuilder.SetMinStartDate(listCsv[j].MinStartDate);
            lastTaskBuilder.SetMaxEndDate(listCsv[j].MaxEndDate);
            Task resaultLastTask = lastTaskBuilder.Build();

            taskList.Add(listCsv[j].ID, resaultLastTask);

            for (int i = 0; i < listCsv.Count; i++)
            {
                if (listCsv[i].Predecessors != string.Empty)
                {
                    foreach (var item in listCsv[i].Predecessors.Split(","))
                    {
                        taskList[listCsv[i].ID].Predecessors.Add(taskList[item.Trim()]);
                    }
                }
            }

            var taskList2 = taskList.Select(task => task.Value).ToList();

            foreach (var task in taskList2)
            {
                task.List = taskList2;
            }

            var hasPriorityTask = new List<AbstractTask>();
            var noPriorityTask = new List<AbstractTask>();

            foreach (var task in taskList2)
            {
                if (task.Priority != int.MaxValue)
                {
                    hasPriorityTask.Add(task);
                }
                else
                {
                    noPriorityTask.Add(task);
                }
            }

            hasPriorityTask.Sort(new TaskPriorityComparer());

            List<AbstractTask> taskList3 = hasPriorityTask.Join(noPriorityTask);

            foreach (var task in taskList3)
            {
                task.List = taskList3;
            }

            return taskList3;
        }

        public static List<TaskCsv> FromList(List<AbstractTask> list)
        {
            var taskList = new List<TaskCsv>();

            for (int i = 0; i < list.Count; i++)
            {
                var taskCsvBuilder = new TaskCsvBuilder();

                taskCsvBuilder.SetID(list[i].ID);
                taskCsvBuilder.SetPriority(list[i].Priority);
                taskCsvBuilder.SetDescription(list[i].Description);
                taskCsvBuilder.SetPredecessors();
                taskCsvBuilder.SetWork(list[i].Work);
                taskCsvBuilder.SetResponsible(list[i].Responsible);
                taskCsvBuilder.SetMinStartDate(list[i].MinStartDate);
                taskCsvBuilder.SetMaxEndDate(list[i].MaxEndDate);
                taskCsvBuilder.SetStartDate(list[i].StartDate);
                taskCsvBuilder.SetEndDate(list[i].EndDate);
                TaskCsv resaultTaskCsv = taskCsvBuilder.Build();

                taskList.Add(resaultTaskCsv);
            }

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Predecessors.Count > 0)
                {
                    foreach (var item in list[i].Predecessors)
                    {
                        taskList[i].Predecessors = $"{taskList[i].Predecessors}{item.ID},";
                    }
                }
            }

            return taskList;
        }

        public int CompareTo(TaskCsv other)
        {
            return this.ID.CompareTo(other.ID);
        }
    }
}
