namespace TaskScheduler
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using CsvHelper;
    using CsvHelper.Configuration;

    public class Task : AbstractTask
    {
        private DateTime startDate;

        public Task()
        {
            this.StartDateCalculated = false;
            this.startDate = this.MinStartDate;
        }

        public override DateTime StartDate
        {
            get
            {
                // We should based only on already calculated Task
                if (!this.StartDateCalculated)
                {
                    this.startDate = this.FindFirstEmptySlot(this.MinStartDate.Max(this.Predecessors), this.List.Where(x => x.StartDateCalculated && x.Responsible == this.Responsible).ToList());
                    this.StartDateCalculated = true;
                }

                return this.startDate;
            }
        }

        public override DateTime EndDate
        {
            get { return this.StartDate.AddWorkDays(this.Work.GetValueOrDefault(0)); }
        }

        public DateTime FindFirstEmptySlot(DateTime startDate, List<AbstractTask> taskList)
        {
            taskList.Sort();
            if (taskList.Count > 0 && startDate.AddWorkDays(this.Work.GetValueOrDefault(0)) < taskList[0].StartDate)
            {
                return startDate;
            }

            for (int i = 0; i < taskList.Count - 1; i++)
            {
                if (taskList[i].EndDate.AddWorkDays(this.Work.GetValueOrDefault(0)) < taskList[i + 1].StartDate)
                {
                    return taskList[i].EndDate;
                }
            }

            if (taskList.Count == 0 || startDate > taskList[^1].EndDate)
            {
                return startDate;
            }
            else
            {
                return taskList[^1].EndDate;
            }
        }
    }
}
