/*namespace TaskScheduler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal static class AlgorithmMethods
    {
        public static List<Task> CountStartDate(List<Task> list)
        {
            var responsibles = list.Select(x => x.Responsible).Distinct();
            var projectStartDate = list[0].MinStartDate;
            var newList = new List<Task>();

            foreach (var responsible in responsibles)
            {
                var recordList = list.Where(x => x.Responsible == responsible).OrderBy(x => x.Priority.HasValue ? x.Priority : int.MaxValue).ToList();
                newList.AddRange(recordList);
            }

            return newList;
        }
    }
}*/
