
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GymMGT.Models
{
    public class GymDbContext : DbContext
    {
        public GymDbContext(DbContextOptions<GymDbContext> options) : base(options)
        {

        }

        public DbSet<GymTrainee> Trainees { get; set; }
        public DbSet<BloodGroup> BloodGroups { get; set; }
        public DbSet<TrainingLevel> TrainingLevels { get; set; }
        public  DbSet<User> Users { get; set; }
        public DbSet<MonthlyFeeVoucher> MonthlyFeeVouchers { get; set; }
        public DbSet<staff> staff { get; set; }

        


    }
}
