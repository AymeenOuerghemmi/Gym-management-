using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymMGT.Models
{
    public class MonthlyFeeVoucher
    {


        [Key]
        public int MonthlyFeeID { get; set; }

        //[Timestamp]

        [DataType(DataType.Date)]
        public DateTime FeeDate { get; set; }=DateTime.Now;

        public string Remarks { get; set; }

        public string Status { get; set; }

        [ForeignKey("GymTrainee")]
        public int TraineeId { get; set; }
        public GymTrainee GymTrainee { get; set; }


      


    }
}
