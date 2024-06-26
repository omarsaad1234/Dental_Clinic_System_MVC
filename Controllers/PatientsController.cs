﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dental_Clinic.Data;
using Dental_Clinic.Models;
using Dental_Clinic.Interfaces;
using Dental_Clinic.Dtos.CreateAndEditRequests;
using AutoMapper;
using NToastNotify;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace Dental_Clinic.Controllers
{
    [Authorize]
    public class PatientsController : Controller
    {
        private readonly IPatientRepo _patientRepo;
        private readonly IAppointmentRepo _appointmentRepo;
        private readonly IInvoiceRepo _invoiceRepo;
        private readonly IMedicalHistoryRepo _medicalHistory;
        private readonly IDentalHistoryRepo _dentalHistoryRepo;
        private readonly IImageRepo _imageRepo;
        private readonly IMapper _mapper;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PatientsController(IPatientRepo patientRepo,IAppointmentRepo appointmentRepo
            ,IInvoiceRepo invoiceRepo
            ,IMedicalHistoryRepo medicalHistory
            ,IDentalHistoryRepo dentalHistoryRepo
            ,IImageRepo imageRepo
            ,IMapper mapper
            ,IToastNotification toastNotification
            ,IWebHostEnvironment webHostEnvironment)
        {
            _patientRepo = patientRepo;
            _appointmentRepo = appointmentRepo;
            _invoiceRepo = invoiceRepo;
            _medicalHistory = medicalHistory;
            _dentalHistoryRepo = dentalHistoryRepo;
            _imageRepo = imageRepo;
            _mapper = mapper;
            _toastNotification = toastNotification;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Patients
        public async Task<IActionResult> Index()
        {
            return View(await _patientRepo.GetAll());
        }

        // GET: Patients/Details/5
        public async Task<IActionResult> Details(int id)
        {
            
            if (!_patientRepo.PatientExists(id))
                return NotFound();

            var patient = await _patientRepo.GetById(id);

            if (patient == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            ViewBag.Appointments = await _appointmentRepo.GetByPatId(id);
            ViewBag.Invoice =await _invoiceRepo.GetByPatId(id);
            ViewBag.MedHistory = await _medicalHistory.GetByPatId(id);
            ViewBag.DenHistory = await _dentalHistoryRepo.GetByPatId(id);

            return View(patient);
        }
        public async Task<IActionResult> DetailsByMobile(string mobile)
        {

            var patient = await _patientRepo.GetByMobile(mobile);

            if (patient == null)
                return View("NotFound");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            ViewBag.Appointments = await _appointmentRepo.GetByPatId(patient.Id);
            ViewBag.Invoice = await _invoiceRepo.GetByPatId(patient.Id);
            ViewBag.MedHistory = await _medicalHistory.GetByPatId(patient.Id);
            ViewBag.DenHistory = await _dentalHistoryRepo.GetByPatId(patient.Id);

            return View(patient);
        }


        // GET: Patients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PatientDtoRequest patientCreate)
        {
            var patient = _mapper.Map<Patient>(patientCreate);
            if (ModelState.IsValid&& _patientRepo.Create(patient))
            {
                _toastNotification.AddSuccessToastMessage("Created Successfully");
                return RedirectToAction(nameof(Index));
            }
            _toastNotification.AddErrorToastMessage("Something Went Wrong");
            return View(patient);
        }

        //GET: Patients/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (!_patientRepo.PatientExists(id))
                return NotFound();

            var patient = await _patientRepo.GetById(id);

            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,Patient patient)
        {
            if (id != patient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (!_patientRepo.Update(patient))
                    {
                        ModelState.AddModelError("", "Something Went Wrong");
                        _toastNotification.AddErrorToastMessage("Something Went Wrong");

                        return View(patient);
                    }
                    _toastNotification.AddSuccessToastMessage("Edited Successfully");

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    _toastNotification.AddErrorToastMessage("Something Went Wrong");

                    return View(patient);
                }
                
            }
            _toastNotification.AddErrorToastMessage("Something Went Wrong");

            return View(patient);
        }

        // GET: Patients/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

            if (!_patientRepo.PatientExists(id))
                return NotFound();
            var patient = await _patientRepo.GetById(id);
                
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        //POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!_patientRepo.PatientExists(id))
                return NotFound();
            var patient = await _patientRepo.GetById(id);
            if (!_patientRepo.Delete(patient))
            {
                _toastNotification.AddErrorToastMessage("Something Went Wrong");

                return View(patient);

            }
            _toastNotification.AddSuccessToastMessage("Deleted Successfully");
            return RedirectToAction(nameof(Index));
        }

    }
}
