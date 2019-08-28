using PatientManagement.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace PatientManagement.Controllers
{
    public class PatientAppointmentController : Controller
    {
        #region "database object Creation"
        private PatientManagementEntities db = new PatientManagementEntities();
        #endregion

        #region "Index Action Method"
        /// <summary>Indexes this instance.</summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (Session["UserEmail"] != null && Session["UserRole"].ToString() == "Patient")
            {
                var id = Convert.ToInt32(Session["UserId"]);
                var appointmentDetail = db.AppointmentDetails.Include(a => a.PatientDetail).Where(a => a.PatientId == id && a.AppointmentDate >= DateTime.Today);
                return View(appointmentDetail.ToList());

            }
            Response.Write("<script>alert('Please Login')</script>");
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("SignIn", "Auth");

        }
        #endregion

        #region "Create Action Method"
        // GET: Patient/Create
        /// <summary>Creates this instance.</summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            if (Session["UserEmail"] != null && Session["UserRole"].ToString() == "Patient")
            {
                return View();
            }
            Response.Write("<script>alert('Please Login')</script>");
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("SignIn", "Auth");
        }

        // POST: Patient/Create
        /// <summary>Creates the specified appoinment detail.</summary>
        /// <param name="appoinmentDetail">The appoinment detail.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AppointmentDetail appoinmentDetail)
        {
            if (Session["UserEmail"] != null && Session["UserRole"].ToString() == "Patient")
            {
                if (ModelState.IsValid)
                {
                    //Remove this comment and static valure of user id
                    appoinmentDetail.PatientId = Convert.ToInt32(Session["UserId"]);
                    //appoinmentDetail.PatientId = 2;
                    appoinmentDetail.SheduleDate = DateTime.Now;

                    db.AppointmentDetails.Add(appoinmentDetail);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.PatientId = new SelectList(db.PatientDetails, "Patient_Id", "Name", appoinmentDetail.PatientId);
                return View(appoinmentDetail);
            }
            Response.Write("<script>alert('Please Login')</script>");
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("SignIn", "Auth");
        }
        #endregion

        #region "Patient Details Action Method"
        //Treatment Deatils viewed by user 
        /// <summary>Patients the details.</summary>
        /// <returns></returns>
        public ActionResult PatientDetails()
        {
            if (Session["UserEmail"] != null && Session["UserRole"].ToString() == "Patient")
            {
                var id = Convert.ToInt32(Session["UserId"]);
                var patientDetails = db.TreatmentDetails.Include(p => p.PatientDetail).Where(x => x.PatientId == id).ToList();
                return View(patientDetails);
            }
            Response.Write("<script>alert('Please Login')</script>");
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("SignIn", "Auth");
        }
        #endregion

        #region "Dispose Action Method"
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
