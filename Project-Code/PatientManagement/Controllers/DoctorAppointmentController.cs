using PagedList;
using PatientManagement.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;

namespace PatientManagement.Controllers
{
    public class DoctorAppointmentController : Controller
    {
        #region "Database Object created"
        private PatientManagementEntities db = new PatientManagementEntities();
        #endregion

        #region "Index Action Method"
        // GET: DoctorAppointment
        /// <summary>Indexes the specified page.</summary>
        /// <param name="page">The page.</param>
        /// <returns> </returns>
        public ActionResult Index(int? page)
        {
            if (Session["UserEmail"] != null && Session["UserRole"].ToString() == "Doctor")
            {
                var appointmentDetails = db.AppointmentDetails.Include(a => a.PatientDetail).Where(a => a.AppointmentDate >= DateTime.Today);
                return View(appointmentDetails.ToList().ToPagedList(page ?? 1, 11));
            }
            Response.Write("<script>alert('Please Login')</script>");
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("SignIn", "Auth");
        }
        #endregion

        #region "Create Action Method"
        // GET: DoctorAppointment/Create
        /// <summary>Creates this instance.</summary>
        /// <returns>create view</returns>
        public ActionResult Create()
        {
            if (Session["UserEmail"] != null && Session["UserRole"].ToString() == "Doctor")
            {
                ViewBag.PatientId = new SelectList(db.PatientDetails, "Patient_Id", "Name");
                return View();
            }
            Response.Write("<script>alert('Please Login')</script>");
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("SignIn", "Auth");
        }

        // POST: DoctorAppointment/Create
        /// <summary>Creates the specified appointment detail.</summary>
        /// <param name="appointmentDetail">The appointment detail.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AppointmentDetail appointmentDetail)
        {
            if (Session["UserEmail"] != null && Session["UserRole"].ToString() == "Doctor")
            {
                if (ModelState.IsValid)
                {
                    appointmentDetail.SheduleUpdated = false;
                    appointmentDetail.SheduleDate = DateTime.Now;
                    db.AppointmentDetails.Add(appointmentDetail);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.PatientId = new SelectList(db.PatientDetails, "Patient_Id", "Name", appointmentDetail.PatientId);
                return View(appointmentDetail);
            }
            Response.Write("<script>alert('Please Login')</script>");
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("SignIn", "Auth");
        }
        #endregion

        #region "Edit Action Method"
        // GET: DoctorAppointment/Edit/5
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
                AppointmentDetail appointmentDetail = db.AppointmentDetails.Find(id);
                if (appointmentDetail == null)
                {
                    return HttpNotFound();
                }
                ViewBag.PatientId = new SelectList(db.PatientDetails, "Patient_Id", "Name", appointmentDetail.PatientId);
                return View(appointmentDetail);
            }
            Response.Write("<script>alert('Please Login')</script>");
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("SignIn", "Auth");

        }

        // POST: DoctorAppointment/Edit/5
        /// <summary>Edits the specified appointment detail.</summary>
        /// <param name="appointmentDetail">The appointment detail.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AppointmentDetail appointmentDetail)
        {
            if (Session["UserEmail"] != null && Session["UserRole"].ToString() == "Doctor")
            {
                if (ModelState.IsValid)
                {
                    AppointmentDetail objAppointmentDetails = db.AppointmentDetails.Where(x => x.AppointmentId == appointmentDetail.AppointmentId).FirstOrDefault();
                    objAppointmentDetails.PatientId = appointmentDetail.PatientId;
                    objAppointmentDetails.Age = appointmentDetail.Age;
                    objAppointmentDetails.DiseaseInfo = appointmentDetail.DiseaseInfo;
                    objAppointmentDetails.AppointmentDate = appointmentDetail.AppointmentDate;
                    objAppointmentDetails.AppointmentTime = appointmentDetail.AppointmentTime;
                    objAppointmentDetails.SheduleUpdated = true;
                    objAppointmentDetails.SheduleDate = DateTime.Now;

                    //db.Entry(appointmentDetail).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                //ViewBag.PatientId = new SelectList(db.PatientDetails, "Patient_Id", "Name", appointmentDetail.PatientId);
                return View(appointmentDetail);
            }
            Response.Write("<script>alert('Please Login')</script>");
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("SignIn", "Auth");
        }
        #endregion

        #region "Dispose Database"
        // GET: DoctorAppointment/Delete/5      
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
