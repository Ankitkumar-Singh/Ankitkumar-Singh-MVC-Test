//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PatientManagement.Models
{
    using System.ComponentModel.DataAnnotations;

    public partial class AdviceDetail
    {
        public int PatientId { get; set; }
        [Required(ErrorMessage = "Please enter your message")]
        public string PatientMessage { get; set; }
        public string DoctorMessage { get; set; }
        public int AdviceId { get; set; }
        public System.DateTime AdviceTime { get; set; }

        public virtual PatientDetail PatientDetail { get; set; }
    }
}