using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GymMGT.Models;


namespace GymMGT.Controllers
{
    public class GymtraineeController : Controller
    {
        private readonly GymDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public GymtraineeController(GymDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: Pets
        public async Task<IActionResult> Index()
        {


            var traineeViewModel = from t in _context.Trainees
                                   join bg in _context.BloodGroups on t.BloodGroupID equals bg.BloodGroupID
                                   join tl in _context.TrainingLevels on t.TrainingLevelID equals tl.TrainingLevelID
                                   select new TraineeDetailsViewModel
                                   {
                                       GymTraineeVM = t,
                                       BloodGroupVM = bg,
                                       TrainingLevelVM = tl
                                   };

            var orderByDescendingResult  = traineeViewModel.OrderByDescending(o => o.GymTraineeVM.CreationDate);

            return View(orderByDescendingResult);

        }
        


        // GET: Pets/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            FeeVoucherDetailsViewModel FeeVoucherDetailsViewModel = new FeeVoucherDetailsViewModel();


            if (id == null)
            {
                return NotFound();
            }


            FeeVoucherDetailsViewModel.GymTraineeVM= await _context.Trainees
                .FirstOrDefaultAsync(m => m.TraineeId == id);

            if (FeeVoucherDetailsViewModel == null)
            {
                return NotFound();
            }

            return View(FeeVoucherDetailsViewModel);
        }

        // GET: Pets/Create
        public IActionResult AddOrEdit(int Id=0)
        {
            GymTrainee trainee = new GymTrainee();

            List<SelectListItem> ObjList = new List<SelectListItem>();

            var bglist = _context.BloodGroups.ToList();

            foreach (var temp in bglist)
            {
                ObjList.Add(new SelectListItem() { Text = temp.BloodGroupName, Value = temp.BloodGroupID.ToString() });
            }
            ViewBag.BloodGroups = ObjList;


            var TLlist = _context.TrainingLevels.ToList();

            List<SelectListItem> ObjTrainingLevelList = new List<SelectListItem>();
            foreach (var temp in TLlist)
            {
                ObjTrainingLevelList.Add(new SelectListItem() { Text = temp.TrainingLevelName, Value = temp.TrainingLevelID.ToString() });
            }
            ViewBag.DPL_TL = ObjTrainingLevelList;

            List<SelectListItem> HeightList = new List<SelectListItem>();
            HeightList.Add(new SelectListItem() { Text = "1.50+ ", Value = "1.5' " });
            HeightList.Add(new SelectListItem() { Text = "1.60+ ", Value = "1.6' " });
            HeightList.Add(new SelectListItem() { Text = "1.70+ ", Value = "1.7' " });
            HeightList.Add(new SelectListItem() { Text = "1.80+ ", Value = "1.8' " });
            HeightList.Add(new SelectListItem() { Text = "1.90+ ", Value = "1.9' " });


            ViewBag.Height_TL = HeightList;


            if (Id == 0)
                return View(trainee);//  new Trainee());
            else
                return View(_context.Trainees.Find(Id));
        }

        // POST: Pets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

                                                                                                                                  
        public async Task<IActionResult> AddOrEdit([Bind("TraineeId, FirstName, LastName,ContactNo,Age,Height, Weight,Gender, Address,BloodGroupID,TrainingLevelID,MonthlyFee,ImageFile")] GymTrainee traineeObj)
        {

            if (ModelState.IsValid)
            {
                if (traineeObj.TraineeId == 0){

                    //Save image to wwwroot/image

                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(traineeObj.ImageFile.FileName);
                    string extension = Path.GetExtension(traineeObj.ImageFile.FileName);
                    traineeObj.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/Images/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await traineeObj.ImageFile.CopyToAsync(fileStream);
                    }
                    traineeObj.CreationDate = System.DateTime.Now;
                    _context.Add(traineeObj);
                }
                else
                {


                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(traineeObj.ImageFile.FileName);
                    string extension = Path.GetExtension(traineeObj.ImageFile.FileName);
                    traineeObj.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/Images/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await traineeObj.ImageFile.CopyToAsync(fileStream);
                    }

                    traineeObj.CreationDate = System.DateTime.Now;

                    _context.Update(traineeObj);
                  
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                List<SelectListItem> ObjList = new List<SelectListItem>();

                var bglist = _context.BloodGroups.ToList();

                foreach (var temp in bglist)
                {
                    ObjList.Add(new SelectListItem() { Text = temp.BloodGroupName, Value = temp.BloodGroupID.ToString() });
                }
                ViewBag.Locations = ObjList;

                var TLlist = _context.TrainingLevels.ToList();

                List<SelectListItem> ObjTrainingLevelList = new List<SelectListItem>();
                foreach (var temp in TLlist)
                {
                    ObjTrainingLevelList.Add(new SelectListItem() { Text = temp.TrainingLevelName, Value = temp.TrainingLevelID.ToString() });
                }
                ViewBag.DPL_TL = ObjTrainingLevelList;

            }
            return View(traineeObj);
        }

        // GET: Pets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Trainees.FindAsync(id);
            if (pet == null)
            {
                return NotFound();
            }
            return View(pet);
        }

       

        // GET: Pets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Trainees
                .FirstOrDefaultAsync(m => m.TraineeId == id);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        // POST: Pets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pet = await _context.Trainees.FindAsync(id);
            _context.Trainees.Remove(pet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PetExists(int id)
        {
            return _context.Trainees.Any(e => e.TraineeId == id);
        }
        


    }
}
