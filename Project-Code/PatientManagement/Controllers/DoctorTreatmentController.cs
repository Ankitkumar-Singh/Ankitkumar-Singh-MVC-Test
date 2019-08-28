using PatientManagement.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;

namespace PatientManagement.Controllers
{
    public class DoctorTreatmentController : Controller
    {
        #region "Database Object Creation"
        private PatientManagementEntities db = new PatientManagementEntities();
        #endregion

        #region "Index Action Method"
        // GET: DoctorTreatment
        /// <summary>Indexes the specified from.</summary>
        /// <param name="From">From date</param>
        /// <param name="To">To date</param>
        /// <returns></returns>
        public ActionResult Index(DateTime? From, DateTime? To)
        {
            if (Session["UserEmail"] != null && Session["UserRole"].ToString() == "Doctor")
            {
                var treatmentDetails = db.TreatmentDetails.Include(t => t.AppointmentDetail).Include(t => t.PatientDetail);
                var treatmentDetail = db.TreatmentDetails.AsQueryable();
                if (From == null && To == null)
                {
                    return View(treatmentDetails.ToList().OrderByDescending(u => u.TreatmentDate));
                }
                else
                {
                    treatmentDetail = treatmentDetail.Where(e => e.TreatmentDate >= From && e.TreatmentDate <= To);
                    return View(treatmentDetail.ToList());
                }
            }
            Response.Write("<script>alert('Please Login')</script>");
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("SignIn", "Auth");
        }
        #endregion

        #region "Create Action Method"
        // GET: DoctorTreatment/Create
        /// <summary>Creates the specified identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Appointment deatisl</returns>
        public ActionResult Create(int? id)
        {
            if (Session["UserEmail"] != null && Session["UserRole"].ToString() == "Doctor")
            {
                AppointmentDetail objAppointmentDetails = db.AppointmentDetails.Include(t => t.PatientDetail).Where(x => x.AppointmentId == id).FirstOrDefault();

                if (objAppointmentDetails.AppointmentDate.Date == DateTime.Now.Date)
                {
                    ViewBag.AppointmentId = objAppointmentDetails.AppointmentId;
                    ViewBag.DiseaseInfo = objAppointmentDetails.DiseaseInfo;
                    ViewBag.patientName = objAppointmentDetails.PatientDetail.Name;
                    ViewBag.PatientId = objAppointmentDetails.PatientDetail.Patient_Id;
                    return View();
                }
                else
                {
                    return View();
                }
            }
            Response.Write("<script>alert('Please Login')</script>");
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("SignIn", "Auth");
        }

        /// <summary>Creates the specified treatment detail.</summary>
        /// <param name="treatmentDetail">The treatment detail.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PatientId,TreatmentDate,Treatment,Bill,TreatmentId,AppointmentId")] TreatmentDetail treatmentDetail)
        {
            if (Session["UserEmail"] != null && Session["UserRole"].ToString() == "Doctor")
            {

                if (ModelState.IsValid)
                {
                    treatmentDetail.TreatmentDate = DateTime.Now.Date;
                    db.TreatmentDetails.Add(treatmentDetail);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.AppointmentId = new SelectList(db.AppointmentDetails, "AppointmentId", "DiseaseInfo", treatmentDetail.AppointmentId);
                ViewBag.PatientId = new SelectList(db.PatientDetails, "Patient_Id", "Name", treatmentDetail.PatientId);
                return View(treatmentDetail);
            }
            Response.Write("<script>alert('Please Login')</script>");
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("SignIn", "Auth");
        }
        #endregion

        #region "Edit Action Method"
        // GET: DoctorTreatment/Edit/5
        /// <summary>Edits the specified identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            if (Session["UserEmail"] != null && Session["UserRole"].ToString() == "Doctor")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TreatmentDetail treatmentDetail = db.TreatmentDetails.Find(id);
                if (treatmentDetail == null)
                {
                    return HttpNotFound();
                }
                ViewBag.AppointmentId = new SelectList(db.AppointmentDetails, "AppointmentId", "DiseaseInfo", treatmentDetail.AppointmentId);
                ViewBag.PatientId = new SelectList(db.PatientDetails, "Patient_Id", "Name", treatmentDetail.PatientId);
                return View(treatmentDetail);
            }
            Response.Write("<script>alert('Please Login')</script>");
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("SignIn", "Auth");
        }

        // POST: DoctorTreatment/Edit/5
        /// <summary>Edits the specified treatment detail.</summary>
        /// <param name="treatmentDetail">The treatment detail.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TreatmentDetail treatmentDetail)
        {
            if (Session["UserEmail"] != null && Session["UserRole"].ToString() == "Doctor")
            {
                if (ModelState.IsValid)
                {
                    db.Entry(treatmentDetail).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.AppointmentId = new SelectList(db.AppointmentDetails, "AppointmentId", "DiseaseInfo", treatmentDetail.AppointmentId);
                ViewBag.PatientId = new SelectList(db.PatientDetails, "Patient_Id", "Name", treatmentDetail.PatientId);
                return View(treatmentDetail);
            }
            Response.Write("<script>alert('Please Login')</script>");
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("SignIn", "Auth");
        }
        #endregion

        #region "Delete Action Method"
        // GET: DoctorTreatment/Delete/5
        /// <summary>Deletes the specified identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            if (Session["UserEmail"] != null && Session["UserRole"].ToString() == "Doctor")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TreatmentDetail treatmentDetail = db.TreatmentDetails.Find(id);
                if (treatmentDetail == null)
                {
                    return HttpNotFound();
                }
                return View(treatmentDetail);
            }
            Response.Write("<script>alert('Please Login')</script>");
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("SignIn", "Auth");
        }
        #endregion

        #region "Delete Confirmed Action Method"
        // POST: DoctorTreatment/Delete/5
        /// <summary>Deletes the confirmed.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Delete details of specified user.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["UserEmail"] != null && Session["UserRole"].ToString() == "Doctor")
            {
                TreatmentDetail treatmentDetail = db.TreatmentDetails.Find(id);
                db.TreatmentDetails.Remove(treatmentDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            Response.Write("<script>alert('Please Login')</script>");
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("SignIn", "Auth");
        }
        #endregion

        #region "Dispose Database"
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
