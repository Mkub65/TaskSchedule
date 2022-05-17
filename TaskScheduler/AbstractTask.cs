namespace TaskScheduler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public abstract class AbstractTask : IComparable<AbstractTask>
    {
        public List<AbstractTask>? List { get; set; }

        public string? ID { get; set; }

        public int Priority { get; set; }

        public string? Description { get; set; }

        public List<AbstractTask>? Predecessors { get; set; }

        public int? Work { get; set; }

        public string? Responsible { get; set; }

        public DateTime MinStartDate { get; set; }

        public DateTime MaxEndDate { get; set; }

        public bool StartDateCalculated { get; set; }

        public virtual DateTime StartDate { get; set; }

        public virtual DateTime EndDate { get; set; }

        public int CompareTo(AbstractTask other)
        {
            if (this.StartDateCalculated && other.StartDateCalculated)
            {
                return this.StartDate.CompareTo(other.StartDate);
            }
            else if (this.StartDateCalculated)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
