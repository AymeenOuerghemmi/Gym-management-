using GymMGT.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using Microsoft.Reporting.NETCore;


namespace GymMGT.Controllers
{
    public class ReportController : Controller
    {
        private readonly GymDbContext _context;
      
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ReportController(GymDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        public IActionResult Index()
        {
            return View();
        }


        public string ImageToBase64(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }
        public IActionResult EmployeeReport(int Id)
        {
            string renderFormat = "PDF";
            string extension = "pdf";
            string mimetype = "application/pdf";
            using var report = new LocalReport();


            var _VoucherViewModel = from t in _context.MonthlyFeeVouchers.Where(t => t.MonthlyFeeID == Id)
                                    join mfv in _context.Trainees on t.TraineeId equals mfv.TraineeId into fee_Details

                                    from mfv in fee_Details.DefaultIfEmpty()

                                    select new TraineeDetailsViewModel
                                    {
                                        MonthlyFeeVoucherVM = t,
                                        GymTraineeVM = mfv
                                    };


            string imageParam = "";
            var imagePath = $"{this._webHostEnvironment.WebRootPath}\\Images\\"+ _VoucherViewModel.FirstOrDefault().GymTraineeVM.ImageName;
            using (var b = new Bitmap(imagePath))
            {
                using (var ms = new MemoryStream())
                {
                    b.Save(ms, ImageFormat.Bmp);
                    imageParam = Convert.ToBase64String(ms.ToArray());

                }
            }






            var parametters = new[] {
                new ReportParameter("first_name", _VoucherViewModel.FirstOrDefault().GymTraineeVM.FirstName),
                new ReportParameter("last_name", _VoucherViewModel.FirstOrDefault().GymTraineeVM.LastName),
                new ReportParameter("gender", _VoucherViewModel.FirstOrDefault().GymTraineeVM.Gender),
                new ReportParameter("age", _VoucherViewModel.FirstOrDefault().GymTraineeVM.Age.ToString()),
                new ReportParameter("contact_no", _VoucherViewModel.FirstOrDefault().GymTraineeVM.ContactNo),
                new ReportParameter("address", _VoucherViewModel.FirstOrDefault().GymTraineeVM.Address),
                new ReportParameter("monthly_fee", _VoucherViewModel.FirstOrDefault().GymTraineeVM.MonthlyFee.ToString()),
                new ReportParameter("image", imageParam)
            };

            report.ReportPath = $"{this._webHostEnvironment.WebRootPath}\\Reports\\Report2.rdlc";
            report.SetParameters(parametters);

            var pdf = report.Render(renderFormat);


            return File(pdf, mimetype, "report." + extension);
        }
    }
}
