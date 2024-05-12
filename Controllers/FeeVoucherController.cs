using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymMGT.Models;




namespace GymMGT.Controllers
{
    public class FeeVoucherController : Controller
    {
        private readonly GymDbContext _context;

        public FeeVoucherController(GymDbContext context)
        {
            _context = context;
        }


        // GET: FeeVoucher
        public async Task<IActionResult> Index(string selected_rbt,string selectedDate)
        {

            ViewBag.Selectedbutton = selected_rbt;
            

            var _VoucherViewModel2 = VariableReturnExampleMethod(selected_rbt);

            return View(_VoucherViewModel2);

        }
        dynamic VariableReturnExampleMethod(string selected_rbt)
        {
            var _VoucherViewModel = new object();

            if(string.IsNullOrEmpty( selected_rbt))
            {
                selected_rbt = "list";
            }

           if (selected_rbt=="Paid")
            {
                _VoucherViewModel = from t in _context.Trainees
                                        join mfv in _context.MonthlyFeeVouchers on t.TraineeId equals mfv.TraineeId into fee_Details

                                        from mfv in fee_Details.DefaultIfEmpty()
                                        where
                                        (mfv.Status == selected_rbt)
                                        select new TraineeDetailsViewModel
                                        {
                                            MonthlyFeeVoucherVM = mfv,
                                            GymTraineeVM = t
                                        };
            }

            if (selected_rbt == "Un-Paid")
            {
                _VoucherViewModel = from t in _context.Trainees
                                         join mfv in _context.MonthlyFeeVouchers on t.TraineeId equals mfv.TraineeId into fee_Details

                                         from mfv in fee_Details.DefaultIfEmpty()
                                         where
                                         (mfv.FeeDate == null)
                                         select new TraineeDetailsViewModel
                                         {
                                             MonthlyFeeVoucherVM = mfv,
                                             GymTraineeVM = t
                                         };
            }
            if (selected_rbt == "list")
            {
                _VoucherViewModel = from t in _context.Trainees
                                    join mfv in _context.MonthlyFeeVouchers on t.TraineeId equals mfv.TraineeId into fee_Details

                                    from mfv in fee_Details.DefaultIfEmpty()
                                    
                                    select new TraineeDetailsViewModel
                                    {
                                        MonthlyFeeVoucherVM = mfv,
                                        GymTraineeVM = t
                                    };
            }

            return _VoucherViewModel;
        }


        // GET: FeeVoucher/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            FeeVoucherDetailsViewModel FeeVoucherDetailsViewModel = new FeeVoucherDetailsViewModel();


            if (id == null)
            {
                return NotFound();
            }

            FeeVoucherDetailsViewModel.GymTraineeVM = await _context.Trainees
                .FirstOrDefaultAsync(m => m.TraineeId == id);

            if (FeeVoucherDetailsViewModel == null)
            {
                return NotFound();
            }

            return View(FeeVoucherDetailsViewModel);
        }

        // GET: FeeVoucher/Create
        [HttpGet]
        public async Task<IActionResult> Create(int? Id=0)
        {

          
            FeeVoucherDetailsViewModel FeeVoucherDetailsViewModel = new FeeVoucherDetailsViewModel();

            if (Id == null)
            {
                return NotFound();
            }
            FeeVoucherDetailsViewModel.GymTraineeVM = await _context.Trainees.FirstOrDefaultAsync(m => m.TraineeId == Id);

            if (FeeVoucherDetailsViewModel == null)
            {
                return NotFound();
            }
            return View(FeeVoucherDetailsViewModel);
        }

        // POST: FeeVoucher/Create

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> Create_Post( FeeVoucherDetailsViewModel monthlyFeeVoucherObj)
        {
            MonthlyFeeVoucher monthlyFeeVoucher = new MonthlyFeeVoucher();
            monthlyFeeVoucher.FeeDate = System.DateTime.Now;
            monthlyFeeVoucher.TraineeId = monthlyFeeVoucherObj.MonthlyFeeVoucherVM.TraineeId;
            monthlyFeeVoucher.Remarks = monthlyFeeVoucherObj.MonthlyFeeVoucherVM.Remarks;

            



            if (ModelState.IsValid)
            {

                monthlyFeeVoucher.Status = "Paid";

                _context.Add(monthlyFeeVoucher);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "FeeVoucher");
            }
            return View(monthlyFeeVoucherObj);

        }


    }
}
