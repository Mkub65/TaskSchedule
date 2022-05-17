namespace TaskScheduler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TaskBuilder : IBuilder
    {
        private readonly Task task = new ();

        public void SetID(string iD)
        {
            this.task.ID = iD;
        }

        public void SetPriority(int? priority)
        {
            if (priority == null)
            {
                this.task.Priority = int.MaxValue;
            }
            else
            {
                this.task.Priority = (int)priority;
            }
        }

        public void SetDescription(string description)
        {
            this.task.Description = description;
        }

        public void SetPredecessors()
        {
            this.task.Predecessors = new List<AbstractTask>();
        }

        public void SetWork(int? work)
        {
            this.task.Work = work;
        }

        public void SetResponsible(string responsible)
        {
            this.task.Responsible = responsible;
        }

        public void SetMinStartDate(DateTime minStartDate)
        {
            this.task.MinStartDate = minStartDate;
        }

        public void SetMaxEndDate(DateTime maxEndDate)
        {
            this.task.MaxEndDate = maxEndDate;
        }

        public Task Build()
        {
            Task resault = this.task;
            return resault;
        }

        public Task CreateTask(TaskCsv task)
        {
            var taskBuilder = new TaskBuilder();
            taskBuilder.SetID(task.ID);
            taskBuilder.SetPriority(task.Priority);
            taskBuilder.SetDescription(task.Description);
            taskBuilder.SetPredecessors();
            taskBuilder.SetWork(task.Work);
            taskBuilder.SetResponsible(task.Responsible);
            taskBuilder.SetMinStartDate(task.MinStartDate);
            taskBuilder.SetMaxEndDate(task.MaxEndDate);

            Task resaultTask = taskBuilder.Build();

            return resaultTask;
        }
    }
}