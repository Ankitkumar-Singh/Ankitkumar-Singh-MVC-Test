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
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class PatientDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PatientDetail()
        {
            this.AdviceDetails = new HashSet<AdviceDetail>();
            this.AppointmentDetails = new HashSet<AppointmentDetail>();
            this.TreatmentDetails = new HashSet<TreatmentDetail>();
        }

        public int Patient_Id { get; set; }
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "First name cannot be empty")]
        [RegularExpression("^([a-zA-Z ]{2,})$", ErrorMessage = "Name contains only alphabets")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please select a gender")]
        public string Gender { get; set; }

        [Display(Name = "Phone")]
        [Required(ErrorMessage = "Phone cannot be empty")]
        [RegularExpression(@"^\(?([0-9]{3})\)?([0-9]{3})?([0-9]{4})$", ErrorMessage = "Phone contains only numbers and 10 digits")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email cannot be empty")]
        [RegularExpression(@"^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$", ErrorMessage = "Email address should be in the format abc@abc.com")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password cannot be empty")]
        [DataType(DataType.Password, ErrorMessage = "Invalid password")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).{8,16}$", ErrorMessage = "Password contains one special character, one uppercase, one lowercase, minimum 8 and maximum 16 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Address cannot be empty")]
        public string Address { get; set; }

        public bool UserType { get; set; }
        public System.DateTime RegistrationDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdviceDetail> AdviceDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AppointmentDetail> AppointmentDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TreatmentDetail> TreatmentDetails { get; set; }
    }
}