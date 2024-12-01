using ADO.NET_CRUD_MVC.Data;
using ADO.NET_CRUD_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace ADO.NET_CRUD_MVC.Controllers
{
    public class LeadsController(ILeadRepository leadsRepo) : Controller
    {
        public IActionResult Index()
        {
            var leads = leadsRepo.GetLeads();
            return View(leads);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Lead lead)
        {
            if (!ModelState.IsValid)
            {
                return View(lead);
            }

            var check = leadsRepo.AddLead(lead);

            if (check)
            {
                return RedirectToAction("Index");
            }

            return View(lead);
        }

        public IActionResult Details(int id)
        {
            var lead = leadsRepo.GetLead(id);
            return View(lead);
        }

        public IActionResult Edit(int id)
        {
            var lead = leadsRepo.GetLead(id);
            return View(lead);
        }

        [HttpPost]
        public IActionResult Edit(int id, Lead lead)
        {
            if (!ModelState.IsValid)
            {
                return View(lead);
            }
            var check = leadsRepo.UpdateLead(id, lead);
            if (check)
            {
                return RedirectToAction("Index");
            }
            return View(lead);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var check = leadsRepo.DeleteLead(id);
            if (check)
            {
                return Json(new { success = true, message = "Lead deleted successfully." });
            }
            return Json(new { success = false, message = "Error deleting lead." });
        }


    }
}
