﻿using AutoMapper;
using Dental_Clinic.Dtos.GetResponse;
using Dental_Clinic.Interfaces;
using Dental_Clinic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace Dental_Clinic.Controllers
{
    public class DentalHistoriesController : Controller
    {
        private readonly IDentalHistoryRepo _dentalHistoryRepo;
        private readonly IPatientRepo _patientRepo;
        private readonly IMapper _mapper;
        private readonly IToastNotification _toastNotification;

        public DentalHistoriesController(IDentalHistoryRepo dentalHistoryRepo
            ,IPatientRepo patientRepo
            ,IMapper mapper
            ,IToastNotification toastNotification)
        {
            _dentalHistoryRepo = dentalHistoryRepo;
            _patientRepo = patientRepo;
            _mapper = mapper;
            _toastNotification = toastNotification;
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
            ViewBag.Teeth = dentalHistories.Up_Left[num];
            ViewBag.index = num;
            return View(dentalHistories);
        }
        public async Task<IActionResult> TeethDetailUpRight(int id, int num)
        {
            var dentalHistories = await _dentalHistoryRepo.GetById(id);
            ViewBag.Teeth = dentalHistories.Up_Right[num];
            ViewBag.index = num;
            return View(dentalHistories);
        }
        public async Task<IActionResult> TeethDetailDownRight(int id, int num)
        {
            var dentalHistories = await _dentalHistoryRepo.GetById(id);
            ViewBag.Teeth = dentalHistories.Down_Right[num];
            ViewBag.index = num;
            return View(dentalHistories);
        }
        public async Task<IActionResult> TeethDetailDownLeft(int id, int num)
        {
            var dentalHistories = await _dentalHistoryRepo.GetById(id);
            ViewBag.Teeth = dentalHistories.Down_Left[num];
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
        public ActionResult CreateUpLeft(IFormCollection collection)
        {
            try
            {
                string[] test = new string[8];
                test[Convert.ToInt32(collection["Index"])] = collection["Detail"];
                DentalHistory den = new DentalHistory
                {
                    PatId = Convert.ToInt32(collection["PatientId"]),
                    Up_Left = test,
                };
                _dentalHistoryRepo.Create(den);
                _toastNotification.AddSuccessToastMessage("Created Successfully");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _toastNotification.AddErrorToastMessage("Something Went Wrong");
                return View("TeethCreateUpLeft");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUpRight(IFormCollection collection)
        {
            try
            {
                string[] test = new string[8];
                test[Convert.ToInt32(collection["Index"])] = collection["Detail"];
                DentalHistory den = new DentalHistory
                {
                    PatId = Convert.ToInt32(collection["PatientId"]),
                    Up_Right = test,
                };
                _dentalHistoryRepo.Create(den);
                _toastNotification.AddSuccessToastMessage("Created Successfully");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _toastNotification.AddErrorToastMessage("Something Went Wrong");
                return View("TeethCreateUpRight");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDownLeft(IFormCollection collection)
        {
            try
            {
                string[] test = new string[8];
                test[Convert.ToInt32(collection["Index"])] = collection["Detail"];
                DentalHistory den = new DentalHistory
                {
                    PatId = Convert.ToInt32(collection["PatientId"]),
                    Down_Left = test,
                };
                _dentalHistoryRepo.Create(den);
                _toastNotification.AddSuccessToastMessage("Created Successfully");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _toastNotification.AddErrorToastMessage("Something Went Wrong");
                return View("TeethCreateDownLeft");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDownRight(IFormCollection collection)
        {
            try
            {
                string[] test = new string[8];
                test[Convert.ToInt32(collection["Index"])] = collection["Detail"];
                DentalHistory den = new DentalHistory
                {
                    PatId = Convert.ToInt32(collection["PatientId"]),
                    Down_Right = test,
                };
                _dentalHistoryRepo.Create(den);
                _toastNotification.AddSuccessToastMessage("Created Successfully");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _toastNotification.AddErrorToastMessage("Something Went Wrong");
                return View("TeethCreateDownRight");
            }
        }

        // GET: DentalHistoriesController/Edit/5
        public async Task<IActionResult> EditUpRight(int id,int index)
        {
            if (!_dentalHistoryRepo.DentalHistoryExists(id))
                return NotFound();
            var dentalhistory = await _dentalHistoryRepo.GetById(id);
            ViewBag.teeth = dentalhistory.Up_Right[index];
            ViewBag.index = index;
            return View(dentalhistory);
        }
        public async Task<IActionResult> EditUpLeft(int id, int index)
        {
            if (!_dentalHistoryRepo.DentalHistoryExists(id))
                return NotFound();
            var dentalhistory = await _dentalHistoryRepo.GetById(id);
            ViewBag.teeth = dentalhistory.Up_Left[index];
            ViewBag.index = index;
            return View(dentalhistory);
        }
        public async Task<IActionResult> EditDownRight(int id, int index)
        {
            if (!_dentalHistoryRepo.DentalHistoryExists(id))
                return NotFound();
            var dentalhistory = await _dentalHistoryRepo.GetById(id);
            ViewBag.teeth = dentalhistory.Down_Right[index];
            ViewBag.index = index;
            return View(dentalhistory);
        }
        public async Task<IActionResult> EditDownLeft(int id, int index)
        {
            if (!_dentalHistoryRepo.DentalHistoryExists(id))
                return NotFound();
            var dentalhistory = await _dentalHistoryRepo.GetById(id);
            ViewBag.teeth = dentalhistory.Down_Left[index];
            ViewBag.index = index;
            return View(dentalhistory);
        }

        // POST: DentalHistoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUpLeft(int id, IFormCollection collection)
        {
            if (!_dentalHistoryRepo.DentalHistoryExists(id))
                return NotFound();
            try
            {
                var EditedTeeth = await _dentalHistoryRepo.GetById(id);
                EditedTeeth.Up_Left[Convert.ToInt32(collection["index"])] = collection["Detail"];
                if (!_dentalHistoryRepo.Update(EditedTeeth))
                {
                    _toastNotification.AddErrorToastMessage("Something Went Wrong");
                    return View();
                }
                    
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
        public async Task<IActionResult> EditDownLeft(int id, IFormCollection collection)
        {
            if (!_dentalHistoryRepo.DentalHistoryExists(id))
                return NotFound();
            try
            {
                var EditedTeeth = await _dentalHistoryRepo.GetById(id);
                EditedTeeth.Down_Left[Convert.ToInt32(collection["index"])] = collection["Detail"];
                if (!_dentalHistoryRepo.Update(EditedTeeth))
                {
                    _toastNotification.AddErrorToastMessage("Something Went Wrong");
                    return View();
                }
                   
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
        public async Task<IActionResult> EditUpRight(int id, IFormCollection collection)
        {
            if (!_dentalHistoryRepo.DentalHistoryExists(id))
                return NotFound();
            try
            {
                var EditedTeeth = await _dentalHistoryRepo.GetById(id);
                EditedTeeth.Up_Right[Convert.ToInt32(collection["index"])] = collection["Detail"];
                if (!_dentalHistoryRepo.Update(EditedTeeth))
                {
                    _toastNotification.AddErrorToastMessage("Something Went Wrong");
                    return View();
                }
                    
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
        public async Task<IActionResult> EditDownRight(int id, IFormCollection collection)
        {
            if (!_dentalHistoryRepo.DentalHistoryExists(id))
                return NotFound();
            try
            {
                var EditedTeeth = await _dentalHistoryRepo.GetById(id);
                EditedTeeth.Down_Right[Convert.ToInt32(collection["index"])] = collection["Detail"];
                if (!_dentalHistoryRepo.Update(EditedTeeth))
                {
                    _toastNotification.AddErrorToastMessage("Something Went Wrong");
                    return View();
                }
                    
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
                if (!_dentalHistoryRepo.Delete(dentalhistory))
                {
                    _toastNotification.AddErrorToastMessage("Something Went Wrong");
                    return View();
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
    }
}