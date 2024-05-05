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

namespace Dental_Clinic.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly IInvoiceRepo _invoiceRepo;
        private readonly IPatientRepo _patientRepo;
        private readonly IMapper _mapper;

        public InvoicesController(IInvoiceRepo invoiceRepo,IPatientRepo patientRepo,IMapper mapper)
        {
            _invoiceRepo = invoiceRepo;
            _patientRepo = patientRepo;
            _mapper = mapper;
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
                    return View();
                return RedirectToAction(nameof(Index));
            }
            SelectList patients = new SelectList(await _patientRepo.GetAll(), "Id", "Name");
            ViewBag.Patients = patients;
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
                        return View(invoiceEdit);
                }
                catch (Exception)
                {
                    return View(invoiceEdit);
                }
                return RedirectToAction(nameof(Index));
            }
            SelectList patients = new SelectList(await _patientRepo.GetAll(), "Id", "Name", invoiceEdit.PatId);
            ViewBag.Patients = patients;
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
                    return View(invoice);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
