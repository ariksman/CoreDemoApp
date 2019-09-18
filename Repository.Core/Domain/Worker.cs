
namespace Repository.Core.Domain
{
    public class Worker 
    {
        public Worker()
        {
            
        }

        private int _workerId;
        public int WorkerId
        {
            get { return _workerId; }
            set
            {
                if (_workerId == value)
                {
                    return;
                }
                _workerId = value;
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value)
                {
                    return;
                }
                _name = value;
            }
        }

        private int _age;
        public int Age
        {
            get { return _age; }
            set
            {
                if (_age == value)
                {
                    return;
                }
                _age = value;
            }
        }

        public virtual Employer Employer1 { get; set; }
    }
}
