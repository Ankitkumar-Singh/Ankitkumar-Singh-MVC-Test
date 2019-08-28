using PatientManagement.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace PatientManagement.Controllers
{
    public class PatientAdviceController : Controller
    {
        #region "Database Object Creation"
        private PatientManagementEntities db = new PatientManagementEntities();
        #endregion

        #region "Index CAtioon Method"
        /// <summary>Indexes this instance.</summary>
        public ActionResult Index()
        {
            if (Session["UserEmail"] != null && Session["UserRole"].ToString() == "Patient")
            {
                var id = Convert.ToInt32(Session["UserId"]);
                var adviceDetails = db.AdviceDetails.Include(a => a.PatientDetail).Where(x => x.PatientId == id);
                return View(adviceDetails.ToList());
            }
            Response.Write("<script>alert('Please Login')</script>");
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("SignIn", "Auth");

        }
        #endregion

        #region "Create Action Method"
        // GET: PatientAdvice/Create
        /// <summary>Creates this instance.</summary>
        public ActionResult Create()
        {
            if (Session["UserEmail"] != null && Session["UserRole"].ToString() == "Patient")
            {
                ViewBag.PatientId = new SelectList(db.PatientDetails, "Patient_Id", "Name");
                return View();
            }
            Response.Write("<script>alert('Please Login')</script>");
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("SignIn", "Auth");
        }

        // POST: PatientAdvice/Create
        /// <summary>Creates the specified advice detail.</summary>
        /// <param name="adviceDetail">The advice detail.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AdviceDetail adviceDetail)
        {
            if (Session["UserEmail"] != null && Session["UserRole"].ToString() == "Patient")
            {
                if (ModelState.IsValid)
                {
                    adviceDetail.PatientId = Convert.ToInt32(Session["UserId"]);
                    adviceDetail.AdviceTime = DateTime.Now;
                    adviceDetail.DoctorMessage = "Please wait for reply.";
                    db.AdviceDetails.Add(adviceDetail);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.PatientId = new SelectList(db.PatientDetails, "Patient_Id", "Name", adviceDetail.PatientId);
                return View(adviceDetail);
            }
            Response.Write("<script>alert('Please Login')</script>");
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("SignIn", "Auth");
        }
        #endregion

        #region "Dispose Database Object"
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}
