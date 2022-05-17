namespace TaskScheduler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MilestoneBuilder : IBuilder
    {
        private readonly Milestone task = new ();

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

        public Milestone Build()
        {
            Milestone resault = this.task;
            return resault;
        }
    }
}