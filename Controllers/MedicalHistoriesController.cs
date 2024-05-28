using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dental_Clinic.Data;
using Dental_Clinic.Models;
using Dental_Clinic.Interfaces;
using AutoMapper;
using Dental_Clinic.Dtos.CreateAndEditRequests;
using Dental_Clinic.Repositories;
using NToastNotify;

namespace Dental_Clinic.Controllers
{
    public class MedicalHistoriesController : Controller
    {
        private readonly IMedicalHistoryRepo _medicalHistoryRepo;
        private readonly IPatientRepo _patientRepo;
        private readonly IMapper _mapper;
        private readonly IToastNotification _toastNotification;

        public MedicalHistoriesController(IMedicalHistoryRepo medicalHistoryRepo
            ,IPatientRepo patientRepo
            ,IMapper mapper
            ,IToastNotification toastNotification)
        {
           _medicalHistoryRepo = medicalHistoryRepo;
            _patientRepo = patientRepo;
            _mapper = mapper;
            _toastNotification = toastNotification;
        }

        // GET: MedicalHistories
        public async Task<IActionResult> Index()
        {

            return View(await _medicalHistoryRepo.GetAll());
        }
        public async Task<IActionResult> MedHistOfPat(int id)
        {

            return View(await _medicalHistoryRepo.GetByPatId(id));
        }

        // GET: MedicalHistories/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (!_medicalHistoryRepo.MedicalHistoryExists(id))
                return NotFound();
            var medicalHistory = await _medicalHistoryRepo.GetById(id);
            if (medicalHistory == null)
            {
                return NotFound();
            }

            return View(medicalHistory);
        }
        public async Task<IActionResult> DetailsByPatMobile(string mobile)
        {

            var medicalHistory = await _medicalHistoryRepo.GetByPatMobile(mobile);
            if (medicalHistory == null)
            {
                return View("NotFound");
            }

            return View(medicalHistory);
        }

        // GET: MedicalHistories/Create
        public async Task<IActionResult> Create(int id)
        {
            SelectList patients = new SelectList(await _patientRepo.GetAll(),"Id","Name",id);
            ViewBag.Patients = patients;
            return View();
        }

        // POST: MedicalHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MedHistDtoRequest medicalHistoryCreate)
        {
            if (ModelState.IsValid)
            {
                var medicalHistoryMap = _mapper.Map<MedicalHistory>(medicalHistoryCreate);
                if (!_medicalHistoryRepo.Create(medicalHistoryMap))
                {
                    _toastNotification.AddErrorToastMessage("Something Went Wrong");
                    return View(medicalHistoryCreate);
                }
                    
                _toastNotification.AddSuccessToastMessage("Created Successfully");
                return RedirectToAction(nameof(Index));
            }
            SelectList patients = new SelectList(await _patientRepo.GetAll(), "Id", "Name");
            ViewBag.Patients = patients;
            _toastNotification.AddErrorToastMessage("Something Went Wrong");
            return View(medicalHistoryCreate);
        }

        // GET: MedicalHistories/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (!_medicalHistoryRepo.MedicalHistoryExists(id))
                return NotFound();

            var medicalHistory = await _medicalHistoryRepo.GetById(id);
            if (medicalHistory == null)
            {
                return NotFound();
            }
            SelectList patients = new SelectList(await _patientRepo.GetAll(), "Id", "Name",id);
            ViewBag.Patients = patients;
            var medicalHistoryMap = _mapper.Map<MedHistDtoEdit>(medicalHistory);
            return View(medicalHistoryMap);
        }
         
        // POST: MedicalHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id , MedHistDtoEdit medicalHistoryEdit)
        {
            if (id != medicalHistoryEdit.Id)
            {
                return NotFound();
            }
            if (!_medicalHistoryRepo.MedicalHistoryExists(id))
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var medicalHistoryMap = _mapper.Map<MedicalHistory>(medicalHistoryEdit);
                    if (!_medicalHistoryRepo.Update(medicalHistoryMap))
                    {
                        _toastNotification.AddErrorToastMessage("Something Went Wrong");
                        return View(medicalHistoryEdit);
                    }
                        
                    _toastNotification.AddSuccessToastMessage("Edited Successfully");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    _toastNotification.AddErrorToastMessage("Something Went Wrong");
                    return View(medicalHistoryEdit);
                }
                
            }
            SelectList patients = new SelectList(await _patientRepo.GetAll(), "Id", "Name", id);
            ViewBag.Patients = patients;
            _toastNotification.AddErrorToastMessage("Something Went Wrong");
            return View(medicalHistoryEdit);
        }

        // GET: MedicalHistories/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (!_medicalHistoryRepo.MedicalHistoryExists(id))
                return NotFound();

            var medicalHistory = await _medicalHistoryRepo.GetById(id);
            if (medicalHistory == null)
            {
                return NotFound();
            }

            return View(medicalHistory);
        }

        // POST: MedicalHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!_medicalHistoryRepo.MedicalHistoryExists(id))
                return NotFound();

            var medicalHistory = await _medicalHistoryRepo.GetById(id);

            if (medicalHistory != null)
                _medicalHistoryRepo.Delete(medicalHistory);

            _toastNotification.AddSuccessToastMessage("Deleted Successfully");
            return RedirectToAction(nameof(Index));
        }

    }
}
