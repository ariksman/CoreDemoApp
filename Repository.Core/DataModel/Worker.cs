
using System;

namespace Repository.Core.DataModel
{
    public class Worker 
    {
        public Worker() {}

        public Guid WorkerId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public virtual Employer Employer1 { get; set; }
    }
}
