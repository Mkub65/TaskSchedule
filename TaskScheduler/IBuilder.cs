namespace TaskScheduler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IBuilder
    {
        void SetID(string iD);

        void SetPriority(int? priority);

        void SetDescription(string description);

        void SetPredecessors();

        void SetWork(int? work);

        void SetResponsible(string responsible);

        void SetMinStartDate(DateTime minStartDate);

        void SetMaxEndDate(DateTime maxEndDate);
    }
}
