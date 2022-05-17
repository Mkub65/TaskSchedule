namespace TaskScheduler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TaskCsvBuilder : IBuilder
    {
        private readonly TaskCsv task = new ();

        public void SetID(string iD)
        {
            this.task.ID = iD;
        }

        public void SetPriority(int? priority)
        {
            if (priority == int.MaxValue)
            {
                this.task.Priority = null;
            }
            else
            {
                this.task.Priority = priority;
            }
        }

        public void SetDescription(string description)
        {
            this.task.Description = description;
        }

        public void SetPredecessors()
        {
            this.task.Predecessors = string.Empty;
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

        public void SetStartDate(DateTime startDate)
        {
            this.task.StartDate = startDate;
        }

        public void SetEndDate(DateTime endDate)
        {
            this.task.EndDate = endDate;
        }

        public TaskCsv Build()
        {
            TaskCsv resault = this.task;
            return resault;
        }
    }
}