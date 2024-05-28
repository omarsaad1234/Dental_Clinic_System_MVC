using AutoMapper;
using Dental_Clinic.Data;
using Dental_Clinic.Dtos.GetResponse;
using Dental_Clinic.Interfaces;
using Dental_Clinic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System;
using System.IO;
using WebGrease.Activities;

namespace Dental_Clinic.Controllers
{
    public class DentalHistoriesController : Controller
    {
        private readonly IDentalHistoryRepo _dentalHistoryRepo;
        private readonly IPatientRepo _patientRepo;
        private readonly IImageRepo _imageRepo;
        private readonly IMapper _mapper;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DentalHistoriesController(IDentalHistoryRepo dentalHistoryRepo
            ,IPatientRepo patientRepo
            ,IImageRepo imageRepo
            ,IMapper mapper
            ,IToastNotification toastNotification
            ,IWebHostEnvironment webHostEnvironment)
        {
            _dentalHistoryRepo = dentalHistoryRepo;
            _patientRepo = patientRepo;
            _imageRepo = imageRepo;
            _mapper = mapper;
            _toastNotification = toastNotification;
            _webHostEnvironment = webHostEnvironment;
        }
        // GET: DentalHistoriesController
        public async Task<IActionResult> Index()
        {
            var dentalHistories = await _dentalHistoryRepo.GetAll();
            var dataToView = _mapper.Map<ICollection<DentalHistoryDtoResponse>>(dentalHistories);
            return View(dataToView);
        }
        public async Task<IActionResult> TeethDetailUpLeft(int id,int num)
        {
            var dentalHistories = await _dentalHistoryRepo.GetById(id);
            if (dentalHistories.Up_Left_Details != null)
            {
                ViewBag.TeethDetails = dentalHistories.Up_Left_Details[num];
            }
            else
                ViewBag.TeethDetails = " ";
            var images = await _imageRepo.GetByDentalHistId(id);
            ViewBag.TeethImgs = images
                .Where(i => i.Position == Position.Up_Left && i.Index == num)
                .Select(i => i.Url)
                .ToList();
            ViewBag.index = num;
            return View(dentalHistories);
        }
        public async Task<IActionResult> TeethDetailUpRight(int id, int num)
        {
            var dentalHistories = await _dentalHistoryRepo.GetById(id);
            if (dentalHistories.Up_Right_Details != null)
            {
                ViewBag.TeethDetails = dentalHistories.Up_Right_Details[num];
            }
            else
                ViewBag.TeethDetails = " ";
            var images = await _imageRepo.GetByDentalHistId(id);
            ViewBag.TeethImgs = images
                .Where(i => i.Position == Position.Up_Right && i.Index == num)
                .Select(i => i.Url)
                .ToList();
            ViewBag.index = num;
            return View(dentalHistories);
        }
        public async Task<IActionResult> TeethDetailDownRight(int id, int num)
        {
            var dentalHistories = await _dentalHistoryRepo.GetById(id);
            if (dentalHistories.Down_Right_Details != null)
            {
                ViewBag.TeethDetails = dentalHistories.Down_Right_Details[num];
            }
            else
                ViewBag.TeethDetails = " ";
            var images = await _imageRepo.GetByDentalHistId(id);
            ViewBag.TeethImgs = images
                .Where(i => i.Position == Position.Down_Right && i.Index == num)
                .Select(i => i.Url)
                .ToList();
            ViewBag.index = num;
            return View(dentalHistories);
        }
        public async Task<IActionResult> TeethDetailDownLeft(int id, int num)
        {
            var dentalHistories = await _dentalHistoryRepo.GetById(id);
            if (dentalHistories.Down_Left_Details != null)
            {
                ViewBag.TeethDetails = dentalHistories.Down_Left_Details[num];
            }
            else
                ViewBag.TeethDetails = " ";
            var images = await _imageRepo.GetByDentalHistId(id);
            ViewBag.TeethImgs = images
                .Where(i => i.Position == Position.Down_Left && i.Index == num)
                .Select(i => i.Url)
                .ToList();
            ViewBag.index = num;
            return View(dentalHistories);
        }

        // GET: DentalHistoriesController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (!_dentalHistoryRepo.DentalHistoryExists(id))
                return NotFound();
            var dentalHistory = await _dentalHistoryRepo.GetById(id);
            return View(dentalHistory);
        }
        public async Task<IActionResult> DetailsByPatMobile(string mobile)
        {

            var dentalHistory = await _dentalHistoryRepo.GetByPatMobile(mobile);
            if (dentalHistory is null)
                return View("NotFound");
            return View(dentalHistory);
        }
        public async Task<IActionResult> DetailsOfPat(int patId)
        {
            if (!_patientRepo.PatientExists(patId))
                return NotFound();
            var dentalHistory = await _dentalHistoryRepo.GetByPatId(patId);
            return View(dentalHistory);
        }

        // GET: DentalHistoriesController/Create
        public ActionResult Create(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        public async Task<IActionResult> TeethCreateUpLeft(int Id,int num)
        {
            ViewBag.index = num;
            ViewBag.patId = Id;
            return View();
        }
        public async Task<IActionResult> TeethCreateUpRight(int Id, int num)
        {
            ViewBag.index = num;
            ViewBag.patId = Id;
            return View();
        }
        public async Task<IActionResult> TeethCreateDownRight(int Id, int num)
        {
            ViewBag.index = num;
            ViewBag.patId = Id;
            return View();
        }
        public async Task<IActionResult> TeethCreateDownLeft(int Id, int num)
        {
            ViewBag.index = num;
            ViewBag.patId = Id;
            return View();
        }
        // POST: DentalHistoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUpLeft(int Index,int PatientId,string Detail,IFormFileCollection Images,string IsWorkingOn)
        {
            try
            {

                DentalHistory den = new DentalHistory
                {
                    PatId = PatientId
                };
                den.Up_Left_Details[Index]= Detail;
                if(IsWorkingOn=="on")
                    den.Up_Left_IsWorkingOn[Index] = true;
                _dentalHistoryRepo.Create(den);
                var dentalHistory = _dentalHistoryRepo.GetLast();
                List<Image> images = new List<Image>();
                foreach(var Image in Images)
                {
                    images.Add(new Image
                    {
                        Index = Index,
                        Position = Position.Up_Left,
                        DentalHistory = dentalHistory,
                        Url = UploadFile(Image)
                    });
                }
                _imageRepo.CreateRange(images);
                _toastNotification.AddSuccessToastMessage("Created Successfully");
                return RedirectToAction(nameof(Index));
            }
            catch(Exception)
            {
                _toastNotification.AddErrorToastMessage("Something Went Wrong");
                return View("Index");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUpRight(int Index, int PatientId, string Detail, IFormFileCollection Images, string IsWorkingOn)
        {
            try
            {
                DentalHistory den = new DentalHistory
                {
                    PatId = PatientId
                };

                den.Up_Right_Details[Index] = Detail;
                if (IsWorkingOn == "on")
                    den.Up_Right_IsWorkingOn[Index] = true;
                _dentalHistoryRepo.Create(den);
                var dentalHistory = _dentalHistoryRepo.GetLast();
                List<Image> images = new List<Image>();
                foreach (var Image in Images)
                {
                    images.Add(new Image
                    {
                        Index = Index,
                        Position = Position.Up_Right,
                        DentalHistory = dentalHistory,
                        Url = UploadFile(Image)

                    });
                }
                _imageRepo.CreateRange(images);
                _toastNotification.AddSuccessToastMessage("Created Successfully");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _toastNotification.AddErrorToastMessage("Something Went Wrong");
                return View("Index");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDownLeft(int Index, int PatientId, string Detail, IFormFileCollection Images, string IsWorkingOn)
        {
            try
            {
                DentalHistory den = new DentalHistory
                {
                    PatId = PatientId
                };

                den.Down_Left_Details[Index] = Detail;
                if (IsWorkingOn == "on")
                    den.Down_Left_IsWorkingOn[Index] = true;
                _dentalHistoryRepo.Create(den);
                var dentalHistory = _dentalHistoryRepo.GetLast();
                List<Image> images = new List<Image>();
                foreach (var Image in Images)
                {
                    images.Add(new Image
                    {
                        Index = Index,
                        Position = Position.Down_Left,
                        DentalHistory = dentalHistory,
                        Url = UploadFile(Image)
                    });
                }
                _imageRepo.CreateRange(images);
                _toastNotification.AddSuccessToastMessage("Created Successfully");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _toastNotification.AddErrorToastMessage("Something Went Wrong");
                return View("Index");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDownRight(int Index, int PatientId, string Detail, IFormFileCollection Images, string IsWorkingOn)
        {
            try
            {
                DentalHistory den = new DentalHistory
                {
                    PatId = PatientId
                };

                den.Down_Right_Details[Index] = Detail;
                if (IsWorkingOn == "on")
                    den.Down_Right_IsWorkingOn[Index] = true;
                _dentalHistoryRepo.Create(den);
                var dentalHistory = _dentalHistoryRepo.GetLast();
                List<Image> images = new List<Image>();
                foreach (var Image in Images)
                {
                    images.Add(new Image
                    {
                        Index = Index,
                        Position = Position.Down_Right,
                        DentalHistory = dentalHistory,
                        Url = UploadFile(Image)
                    });
                }
                _imageRepo.CreateRange(images);
                _toastNotification.AddSuccessToastMessage("Created Successfully");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _toastNotification.AddErrorToastMessage("Something Went Wrong");
                return View("Index");
            }
        }

        // GET: DentalHistoriesController/Edit/5
        public async Task<IActionResult> EditUpRight(int id,int index)
        {
            if (!_dentalHistoryRepo.DentalHistoryExists(id))
                return NotFound();
            var dentalhistory = await _dentalHistoryRepo.GetById(id);
            var images = await _imageRepo.GetByDentalHistId(id);
            ViewBag.teeth = dentalhistory.Up_Right_Details[index];
            ViewBag.index = index;
            ViewBag.TeethImgs = images
                .Where(i => i.Position == Position.Up_Right && i.Index == index)
                .Select(i => i.Url)
                .ToList();
            return View(dentalhistory);
        }
        public async Task<IActionResult> EditUpLeft(int id, int index)
        {
            if (!_dentalHistoryRepo.DentalHistoryExists(id))
                return NotFound();
            var dentalhistory = await _dentalHistoryRepo.GetById(id);    
            ViewBag.teeth = dentalhistory.Up_Left_Details[index];
            ViewBag.index = index;
            var images = await _imageRepo.GetByDentalHistId(id);
            ViewBag.TeethImgs = images
                .Where(i => i.Position == Position.Up_Left && i.Index == index)
                .Select(i => i.Url)
                .ToList();
            return View(dentalhistory);
        }
        public async Task<IActionResult> EditDownRight(int id, int index)
        {
            if (!_dentalHistoryRepo.DentalHistoryExists(id))
                return NotFound();
            var dentalhistory = await _dentalHistoryRepo.GetById(id);
            ViewBag.teeth = dentalhistory.Down_Right_Details[index];
            ViewBag.index = index;
            var images = await _imageRepo.GetByDentalHistId(id);
            ViewBag.TeethImgs = images
                .Where(i => i.Position == Position.Down_Right && i.Index == index)
                .Select(i => i.Url)
                .ToList();
            return View(dentalhistory);
        }
        public async Task<IActionResult> EditDownLeft(int id, int index)
        {
            if (!_dentalHistoryRepo.DentalHistoryExists(id))
                return NotFound();
            var dentalhistory = await _dentalHistoryRepo.GetById(id);
            ViewBag.teeth = dentalhistory.Down_Left_Details[index];
            ViewBag.index = index;
            var images = await _imageRepo.GetByDentalHistId(id);
            ViewBag.TeethImgs = images
                .Where(i => i.Position == Position.Down_Left && i.Index == index)
                .Select(i => i.Url)
                .ToList();
            return View(dentalhistory);
        }

        // POST: DentalHistoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUpLeft(int id,int Index, IFormFileCollection Images, string IsWorkingOn, string Detail)
        {
            if (!_dentalHistoryRepo.DentalHistoryExists(id))
                return NotFound();
            try
            {
                var EditedTeeth = await _dentalHistoryRepo.GetById(id);
                EditedTeeth.Up_Left_Details[Index] = Detail;
                if (IsWorkingOn == "on")
                    EditedTeeth.Up_Left_IsWorkingOn[Index] = true;
                else
                    EditedTeeth.Up_Left_IsWorkingOn[Index] = false;

                
                if (!_dentalHistoryRepo.Update(EditedTeeth))
                {
                    _toastNotification.AddErrorToastMessage("Something Went Wrong");
                    return View();
                }
                List<Image> images = new List<Image>();
                foreach (var Image in Images)
                {
                    images.Add(new Image
                    {
                        Index = Index,
                        Position = Position.Up_Left,
                        DentalHistory = EditedTeeth,
                        Url = UploadFile(Image)
                    });
                }
                _imageRepo.CreateRange(images);
                _toastNotification.AddSuccessToastMessage("Edited Successfully");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _toastNotification.AddErrorToastMessage("Something Went Wrong");
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDownLeft(int id, int Index, IFormFileCollection Images, string IsWorkingOn, string Detail)
        {
            if (!_dentalHistoryRepo.DentalHistoryExists(id))
                return NotFound();
            try
            {
                var EditedTeeth = await _dentalHistoryRepo.GetById(id);
                EditedTeeth.Down_Left_Details[Index] = Detail;
                if (IsWorkingOn == "on")
                    EditedTeeth.Down_Left_IsWorkingOn[Index] = true;
                else
                    EditedTeeth.Down_Left_IsWorkingOn[Index] = false;


                if (!_dentalHistoryRepo.Update(EditedTeeth))
                {
                    _toastNotification.AddErrorToastMessage("Something Went Wrong");
                    return View();
                }
                List<Image> images = new List<Image>();
                foreach (var Image in Images)
                {
                    images.Add(new Image
                    {
                        Index = Index,
                        Position = Position.Down_Left,
                        DentalHistory = EditedTeeth,
                        Url = UploadFile(Image)
                    });
                }
                _imageRepo.CreateRange(images);
                _toastNotification.AddSuccessToastMessage("Edited Successfully");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _toastNotification.AddErrorToastMessage("Something Went Wrong");
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUpRight(int id, int Index, IFormFileCollection Images, string IsWorkingOn, string Detail)
        {
            if (!_dentalHistoryRepo.DentalHistoryExists(id))
                return NotFound();
            try
            {
                var EditedTeeth = await _dentalHistoryRepo.GetById(id);
                EditedTeeth.Up_Right_Details[Index] = Detail;
                if (IsWorkingOn == "on")
                    EditedTeeth.Up_Right_IsWorkingOn[Index] = true;
                else
                    EditedTeeth.Up_Right_IsWorkingOn[Index] = false;


                if (!_dentalHistoryRepo.Update(EditedTeeth))
                {
                    _toastNotification.AddErrorToastMessage("Something Went Wrong");
                    return View();
                }
                List<Image> images = new List<Image>();
                foreach (var Image in Images)
                {
                    images.Add(new Image
                    {
                        Index = Index,
                        Position = Position.Up_Right,
                        DentalHistory = EditedTeeth,
                        Url = UploadFile(Image)
                    });
                }
                _imageRepo.CreateRange(images);
                _toastNotification.AddSuccessToastMessage("Edited Successfully");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _toastNotification.AddErrorToastMessage("Something Went Wrong");
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDownRight(int id, int Index, IFormFileCollection Images, string IsWorkingOn, string Detail)
        {
            if (!_dentalHistoryRepo.DentalHistoryExists(id))
                return NotFound();
            try
            {
                var EditedTeeth = await _dentalHistoryRepo.GetById(id);
                EditedTeeth.Down_Right_Details[Index] = Detail;
                if (IsWorkingOn == "on")
                    EditedTeeth.Down_Right_IsWorkingOn[Index] = true;
                else
                    EditedTeeth.Down_Right_IsWorkingOn[Index] = false;


                if (!_dentalHistoryRepo.Update(EditedTeeth))
                {
                    _toastNotification.AddErrorToastMessage("Something Went Wrong");
                    return View();
                }
                List<Image> images = new List<Image>();
                foreach (var Image in Images)
                {
                    images.Add(new Image
                    {
                        Index = Index,
                        Position = Position.Down_Right,
                        DentalHistory = EditedTeeth,
                        Url = UploadFile(Image)
                    });
                }
                _imageRepo.CreateRange(images);
                _toastNotification.AddSuccessToastMessage("Edited Successfully");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _toastNotification.AddErrorToastMessage("Something Went Wrong");
                return View();
            }
        }

        // GET: DentalHistoriesController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (!_dentalHistoryRepo.DentalHistoryExists(id))
                return NotFound();
            ViewBag.Id = id;
            var dentalHistory = await _dentalHistoryRepo.GetById(id);
            var denMap = _mapper.Map<DentalHistoryDtoResponse>(dentalHistory);
            return View(denMap);
        }

        // POST: DentalHistoriesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            if (!_dentalHistoryRepo.DentalHistoryExists(id))
                return NotFound();
            try
            {
                var dentalhistory = await _dentalHistoryRepo.GetById(id);
                var Images =await _imageRepo.GetByDentalHistId(id);
                
                if (!_dentalHistoryRepo.Delete(dentalhistory))
                {
                    _toastNotification.AddErrorToastMessage("Something Went Wrong");
                    return View();
                }
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                foreach (var file in Images.Select(i => i.Url))
                {                
                    string filePath = Path.Combine(uploadDir, file);
                    System.IO.File.Delete(filePath);
                }
                    
                _toastNotification.AddSuccessToastMessage("Deleted Successfully");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _toastNotification.AddErrorToastMessage("Something Went Wrong");
                return View();
            }
        }
        public string UploadFile(IFormFile file)
        {
            string fileName = null;
            if (file != null)
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
                string filePath=Path.Combine(uploadDir, fileName);
                using(var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            return fileName;
        }
        public IActionResult DeleteImage(string fileName,int id,int index,string actionName)
        {
            ViewBag.fileName = fileName;
            ViewBag.id = id;
            ViewBag.index = index;
            ViewBag.action = actionName;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteImagePost(string fileName,int id,int index,string action)
        {
            var image = await _imageRepo.GetByFileName(fileName);
            if(image is null)
            {
                _toastNotification.AddErrorToastMessage("Not Found Image");
                return View("Index");
            }
            _imageRepo.Delete(image);
            string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            string filePath = Path.Combine(uploadDir, fileName);
            System.IO.File.Delete(filePath);
            return RedirectToAction(action, new {id=id,index=index});
        }
        public IActionResult ViewPhoto(string src)
        {
            ViewBag.src = src;
            return View("PhotoView");
        }
    }
}
