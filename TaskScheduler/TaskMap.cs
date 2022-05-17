namespace TaskScheduler
{
    using System.Globalization;
    using CsvHelper;
    using CsvHelper.Configuration;

    public class TaskMap : ClassMap<TaskCsv>
    {
        public TaskMap()
        {
            this.Map(m => m.ID);
            this.Map(m => m.Priority);
            this.Map(m => m.Description);
            this.Map(m => m.Predecessors);
            this.Map(m => m.Work);
            this.Map(m => m.Responsible);
            this.Map(m => m.MinStartDate).TypeConverterOption.Format("dd.MM.yyyy");
            this.Map(m => m.MaxEndDate).TypeConverterOption.Format("dd.MM.yyyy");
            this.Map(m => m.StartDate).TypeConverterOption.Format("dd.MM.yyyy");
            this.Map(m => m.EndDate).TypeConverterOption.Format("dd.MM.yyyy");
        }
    }
}
