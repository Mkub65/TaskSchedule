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

                    Milestone resaultMilestone = milestoneBuilder.CreateMilestone(listCsv[i]);

                    taskList.Add(listCsv[i].ID, resaultMilestone);
                }
                else
                {
                    var taskBuilder = new TaskBuilder();

                    Task resaultTask = taskBuilder.CreateTask(listCsv[i]);

                    taskList.Add(listCsv[i].ID, resaultTask);
                }
            }

            var j = listCsv.Count - 1;

            var lastTaskBuilder = new TaskBuilder();

            Task resaultLastTask = lastTaskBuilder.CreateTask(listCsv[j]);

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

                TaskCsv resaultTaskCsv = taskCsvBuilder.CreateTaskCsv(list[i]);

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
