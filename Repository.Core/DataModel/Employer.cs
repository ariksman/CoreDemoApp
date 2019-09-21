using System.Collections.Generic;

namespace Repository.Core.DataModel
{
    public class Employer
    {
        public Employer() {}

        public int EmployerId { get; set; }
        public string Name { get; set; }

        // one-to-many relation for workers
        public virtual ICollection<Worker> Workers { get; set; }
    }
}