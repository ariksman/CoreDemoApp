using System.Collections.Generic;

namespace Repository.Core.DataModel
{
    public class Employer
    {
        public Employer()
        {
            
        }

        private int _employerId;
        public int EmployerId
        {
            get => _employerId;
            set
            {
                if (_employerId == value)
                {
                    return;
                }
                _employerId = value;
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (_name == value)
                {
                    return;
                }
                _name = value;
            }
        }

        // one-to-many relation for workers
        public virtual ICollection<Worker> Workers { get; set; }
    }
}