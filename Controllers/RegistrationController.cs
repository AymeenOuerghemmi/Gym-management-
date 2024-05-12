using GymMGT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GYM.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly GymDbContext DB;
        public RegistrationController(GymDbContext db)
        {
            DB = db;
        }

        public IActionResult ShowStaff()
        {

            var query = (from q in DB.staff select q).ToList();
            query = query.OrderBy(o => o.Id).ToList();

            return View(query);
        }

        public IActionResult addStaff()
        {
            ViewBag.ServiceList = DB.staff.ToList();
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult addStaff(staff obj)
        {
            if (DB.staff.Where(x => x.Name == obj.Name).Count() > 0)
            {
                ViewBag.ServiceList = DB.staff.ToList();
                ViewBag.errorMessage = "L'employe existe";
                return View(obj);
            }
            DB.staff.Add(obj);
            DB.SaveChanges();
            return RedirectToAction("ShowStaff", "Registration");
        }

        // GET: Pets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await DB.staff
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var pet = await DB.staff.FindAsync(id);
            DB.staff.Remove(pet);
            await DB.SaveChangesAsync();
            return RedirectToAction(nameof(ShowStaff));
        }



        public IActionResult editStaff(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = DB.staff.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult editStaff(staff obj)
        {
            DB.staff.Update(obj);
            DB.SaveChanges();
            return RedirectToAction("ShowStaff", "Registration");
        }
        
        

    }
}
