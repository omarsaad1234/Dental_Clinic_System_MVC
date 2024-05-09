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
using NToastNotify;

namespace Dental_Clinic.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly IInvoiceRepo _invoiceRepo;
        private readonly IPatientRepo _patientRepo;
        private readonly IMapper _mapper;
        private readonly IToastNotification _toastNotification;

        public InvoicesController(IInvoiceRepo invoiceRepo
            ,IPatientRepo patientRepo
            ,IMapper mapper
            ,IToastNotification toastNotification)
        {
            _invoiceRepo = invoiceRepo;
            _patientRepo = patientRepo;
            _mapper = mapper;
            _toastNotification = toastNotification;
        }

        // GET: Invoices
        public async Task<IActionResult> Index()
        {
           
            return View(await _invoiceRepo.GetAll());
        }
        public async Task<IActionResult> InvoiceOfPat(int id)
        {

            return View(await _invoiceRepo.GetByPatId(id));
        }

        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (!_invoiceRepo.InvoiceExists(id))
                return NotFound();
            var invoice = await _invoiceRepo.GetById(id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }
        public async Task<IActionResult> DetailsByPatMobile(string mobile)
        {
            var invoice = await _invoiceRepo.GetByPatMobile(mobile);
            if (invoice == null)
            {
                return View("NotFound");
            }

            return View(invoice);
        }

        // GET: Invoices/Create
        public async Task<IActionResult> Create(int id)
        {
            SelectList patients = new SelectList(await _patientRepo.GetAll(), "Id", "Name",id);
            ViewBag.Patients = patients;
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InvoiceDtoRequest invoiceCreate)
        {
            if (ModelState.IsValid)
            {
                var invoiceMap = _mapper.Map<Invoice>(invoiceCreate);
                if (!_invoiceRepo.Create(invoiceMap))
                {
                    _toastNotification.AddErrorToastMessage("Something Went Wrong");  
                    return View();
                }
                _toastNotification.AddSuccessToastMessage("Created Successfully");
                return RedirectToAction(nameof(Index));
            }
            SelectList patients = new SelectList(await _patientRepo.GetAll(), "Id", "Name");
            ViewBag.Patients = patients;
            _toastNotification.AddErrorToastMessage("Something Went Wrong");
            return View(invoiceCreate);
        }

        // GET: Invoices/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (!_invoiceRepo.InvoiceExists(id))
                return NotFound();
            var invoice = await _invoiceRepo.GetById(id);
            if (invoice == null)
            {
                return NotFound();
            }
            SelectList patients = new SelectList(await _patientRepo.GetAll(), "Id", "Name",invoice.PatId);
            ViewBag.Patients = patients;
            var invoiceEdit = _mapper.Map<InvoiceDtoEdit>(invoice);
            return View(invoiceEdit);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(InvoiceDtoEdit invoiceEdit)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var invoiceMap = _mapper.Map<Invoice>(invoiceEdit);
                    if (!_invoiceRepo.Update(invoiceMap))
                    {
                        _toastNotification.AddErrorToastMessage("Something Went Wrong");
                        return View(invoiceEdit);
                    }
                    _toastNotification.AddSuccessToastMessage("Edited Successfully");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    _toastNotification.AddErrorToastMessage("Something Went Wrong");
                    return View(invoiceEdit);
                }
                
            }
            SelectList patients = new SelectList(await _patientRepo.GetAll(), "Id", "Name", invoiceEdit.PatId);
            ViewBag.Patients = patients;
            _toastNotification.AddErrorToastMessage("Something Went Wrong");
            return View(invoiceEdit);
        }

        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (!_invoiceRepo.InvoiceExists(id))
                return NotFound();

            var invoice = await _invoiceRepo.GetById(id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!_invoiceRepo.InvoiceExists(id))
                return NotFound();
            var invoice = await _invoiceRepo.GetById(id);
            if (invoice != null)
            {
                if (!_invoiceRepo.Delete(invoice))
                {
                    _toastNotification.AddErrorToastMessage("Something Went Wrong");
                    return View(invoice);
                }
                   
            }
            _toastNotification.AddSuccessToastMessage("Deleted Successfully");
            return RedirectToAction(nameof(Index));
        }
    }
}
