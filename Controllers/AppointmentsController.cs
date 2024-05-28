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
using Dental_Clinic.Dtos.CreateAndEditRequests;
using AutoMapper;
using Hangfire;
using NToastNotify;
using Microsoft.Ajax.Utilities;

namespace Dental_Clinic.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentRepo _appointmentRepo;
        private readonly IPatientRepo _patientRepo;
        private readonly IMapper _mapper;
        private readonly IToastNotification _toastNotification;

        public AppointmentsController(IAppointmentRepo appointmentRepo
            ,IPatientRepo patientRepo
            ,IMapper mapper
            , IToastNotification toastNotification)
        {
            _appointmentRepo = appointmentRepo;
            _patientRepo = patientRepo;
            _mapper = mapper;
            _toastNotification = toastNotification;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            return View(await _appointmentRepo.GetAll());
        }
        public async Task<IActionResult> AppointmentsWithDate(DateOnly date)
        {
            ViewBag.date = date;
            return View(await _appointmentRepo.GetByDate(date));
        }

        public async Task<IActionResult> AppointmentsOfPatient(int id)
        {
            return View(await _appointmentRepo.GetByPatId(id));
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (!_appointmentRepo.AppointmentExists(id))
                return NotFound();
            var appointment = await _appointmentRepo.GetById(id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        public async Task<IActionResult> Create(int? id)
        {
            var patients = await _patientRepo.GetAll();
            if (id is null)
            {
                SelectList patientsList = new SelectList(patients, "Id", "Name");
                ViewBag.Patients = patientsList;
            }
            else
            {
                SelectList patientsList = new SelectList(patients, "Id", "Name", id);
                ViewBag.Patients = patientsList;
            }
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppointmentDtoRequest appointmentCreate)
        {
            if (ModelState.IsValid&&_patientRepo.PatientExists(appointmentCreate.PatId))
            {
                var patient = await _patientRepo.GetById(appointmentCreate.PatId);
                var appointmentMap = _mapper.Map<Appointment>(appointmentCreate);
                if (!_appointmentRepo.Create(appointmentMap))
                {
                    _toastNotification.AddErrorToastMessage("Something Went Wrong While Saving");
                    return View();
                }
                _toastNotification.AddSuccessToastMessage("Created Successfully");
                return RedirectToAction(nameof(Index));
            }
            var patients = await _patientRepo.GetAll();
            SelectList patientsList = new SelectList(patients, "Id", "Name", appointmentCreate.PatId);
            ViewBag.Patients = patientsList;
            _toastNotification.AddErrorToastMessage("Something Went Wrong");
            return View(appointmentCreate);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (!_appointmentRepo.AppointmentExists(id))
                return NotFound();


            var appointment = await _appointmentRepo.GetById(id);
            if (appointment == null)
            {
                return NotFound();
            }
            var patients = await _patientRepo.GetAll();
            SelectList patientsList = new SelectList(patients, "Id", "Name", id);
            ViewBag.Patients = patientsList;
            var appointmentMap = _mapper.Map<AppointmentDtoEdit>(appointment);
            return View(appointmentMap);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AppointmentDtoEdit appointmentUpdate)
        {
            if (id != appointmentUpdate.Id)
            {
                return NotFound();
            }
            
            if (!_appointmentRepo.AppointmentExists(id))
                return NotFound();


            if (ModelState.IsValid)
            {
                try
                {
                    var appointmentMap = _mapper.Map<Appointment>(appointmentUpdate);
                    if (!_appointmentRepo.Update(appointmentMap))
                    {
                        _toastNotification.AddErrorToastMessage("Something Went Wrong");
                        return View(appointmentUpdate);
                    }
                        
                }
                catch (Exception)
                {
                    _toastNotification.AddErrorToastMessage("Something Went Wrong");
                    return View(appointmentUpdate);
                }
                _toastNotification.AddSuccessToastMessage("Edited Successfully");
                return RedirectToAction(nameof(Index));
            }
            var patients = await _patientRepo.GetAll();
            SelectList patientsList = new SelectList(patients, "Id", "Name");
            ViewBag.Patients = patientsList;
            _toastNotification.AddErrorToastMessage("Something Went Wrong");
            return View(appointmentUpdate);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (!_appointmentRepo.AppointmentExists(id))
                return NotFound();

            var appointment = await _appointmentRepo.GetById(id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _appointmentRepo.GetById(id);
            if (appointment != null)
            {
                _appointmentRepo.Delete(appointment);
            }
            _toastNotification.AddSuccessToastMessage("Deleted Successfully");
            return RedirectToAction(nameof(Index));
        }
    }
}
