namespace GymMGT.Models
{
    public class FeePaidStatus 
    {


        private string _feepaid_status = "unknown";
        public string Feepaid_Status
        {
            get
            {
                return _feepaid_status;
            }
            set
            {
                _feepaid_status = value;
            }
        }
    }
}
