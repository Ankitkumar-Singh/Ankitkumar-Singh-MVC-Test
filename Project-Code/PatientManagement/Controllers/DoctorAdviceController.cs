using PatientManagement.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;

namespace PatientManagement.Controllers
{
    public class DoctorAdviceController : Controller
    {
        #region "Database Object Creation"
        private PatientManagementEntities db = new PatientManagementEntities();
        #endregion

        #region "Index Action Method"
        // GET: DoctorAdvice
        /// <summary>Indexes this instance.</summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (Session["UserEmail"] != null && Session["UserRole"].ToString() == "Doctor")
            {
                var adviceDetails = db.AdviceDetails.Include(a => a.PatientDetail);
                return View(adviceDetails.ToList());
            }
            Response.Write("<script>alert('Please Login')</script>");
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("SignIn", "Auth");
        }
        #endregion

        #region "Edit Action Method"
        // GET: DoctorAdvice/Edit/5
        /// <summary>Edits the specified identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>patient details.</returns>
        public ActionResult Edit(int? id)
        {
            if (Session["UserEmail"] != null && Session["UserRole"].ToString() == "Doctor")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AdviceDetail adviceDetail = db.AdviceDetails.Find(id);
                if (adviceDetail == null)
                {
                    return HttpNotFound();
                }
                ViewBag.PatientId = new SelectList(db.PatientDetails, "Patient_Id", "Name", adviceDetail.PatientId);
                return View(adviceDetail);
            }
            Response.Write("<script>alert('Please Login')</script>");
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("SignIn", "Auth");
        }

        // POST: DoctorAdvice/Edit/5
        /// <summary>Edits the specified advice detail.</summary>
        /// <param name="adviceDetail">The advice detail.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AdviceDetail adviceDetail)
        {
            if (Session["UserEmail"] != null && Session["UserRole"].ToString() == "Doctor")
            {
                if (ModelState.IsValid)
                {
                    AdviceDetail objAdviceDetail = db.AdviceDetails.Where(x => x.AdviceId == adviceDetail.AdviceId).FirstOrDefault();

                    objAdviceDetail.DoctorMessage = adviceDetail.DoctorMessage;

                    objAdviceDetail.AdviceTime = DateTime.Now;
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
