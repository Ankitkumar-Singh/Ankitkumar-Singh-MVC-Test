using PatientManagement.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace PatientManagement.Controllers
{
    public class AuthController : Controller
    {
        #region "Database Object Creation"
        private PatientManagementEntities db = new PatientManagementEntities();
        #endregion

        #region "Signin authentication"
        /// <summary>Signs the in.</summary>
        /// <returns></returns
        public ActionResult SignIn()
        {
            return View();
        }

        /// <summary>Signs the in.</summary>
        /// <param name="userDetail">The user detail.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SignIn(PatientDetail patientDetail)
        {
            using (var context = new PatientManagementEntities())
            {
                var email = patientDetail.Email;
                var passsword = patientDetail.Password;

                if (context.PatientDetails.Any(x => x.Email.Equals(patientDetail.Email, StringComparison.Ordinal) && x.Password.Equals(patientDetail.Password, StringComparison.Ordinal)))
                {
                    PatientDetail user = context.PatientDetails.Single(x => x.Email == patientDetail.Email);

                    Session["UserEmail"] = user.Email;
                    FormsAuthentication.SetAuthCookie(user.Email, false);

                    if (user.UserType == true)
                    {
                        Session["UserRole"] = "Doctor";
                        Session["UserName"] = user.Name;
                        return RedirectToAction("Index", "DoctorAppointment");
                    }
                    else if (user.UserType == false)
                    {
                        Session["UserRole"] = "Patient";
                        Session["UserName"] = user.Name;
                        Session["UserId"] = user.Patient_Id;
                        return RedirectToAction("Index", "PatientAppointment");
                    }
                    else
                    {
                        return View();
                    }
                }
            }
            ModelState.AddModelError("", "Invalid email and password");
            return View();
        }
        #endregion

        #region "Signup authentication"
        /// <summary>Signs up.</summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        /// <summary>Signs up.</summary>
        /// <param name="userDetail">The user detail.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(PatientDetail patientDetail)
        {
            if (ModelState.IsValid)
            {
                patientDetail.UserType = false;
                patientDetail.RegistrationDate = DateTime.Now;
                using (var context = new PatientManagementEntities())
                {
                    context.PatientDetails.Add(patientDetail);
                    context.SaveChanges();
                    return RedirectToAction("SignIn");
                }
            }
            return View();
        }
        #endregion

        #region "Sign out"
        /// <summary>Represents an event that is raised when the sign-out operation is complete.</summary>
        /// <returns></returns>
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            HttpContext.Session.Abandon();
            HttpContext.Session.Clear();
            return RedirectToAction("SignIn");
        }
        #endregion

        #region "Error"
        /// <summary>Errors this instance.</summary>
        /// <returns></returns>
        public ActionResult Error()
        {
            return View();
        }
        #endregion
    }
}