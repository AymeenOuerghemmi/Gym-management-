using System.Collections.Generic;
using System.Net;

namespace GymMGT.Models
{
    
    public class TraineeDetailsViewModel
    {
        //public GymTrainee GymTrainee { get; set; }

        public GymTrainee GymTraineeVM { get; set; }
        public  BloodGroup BloodGroupVM { get; set; }

        public TrainingLevel TrainingLevelVM { get; set; }


        public MonthlyFeeVoucher MonthlyFeeVoucherVM { get; set; }

        public string PageTitle { get; set; }
        public string PageHeader { get; set; }
    }
}
