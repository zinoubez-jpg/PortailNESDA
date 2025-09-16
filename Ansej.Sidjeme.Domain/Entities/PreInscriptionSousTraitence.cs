using Ansej.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ansej.Sidjeme.Domain.Entities
{
    public class PreInscriptionSousTraitence : IEntity<int>
    {
        [Display(Name = "Id")]
        [Required(ErrorMessage = "يجب ملء هذا الحقل")]
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}")]
        public DateTime DateInscription { get; set; } = DateTime.Now;
        [Display(Name = "RaisonSocial")]
        [Required(ErrorMessage = "يجب ملء هذا الحقل"), StringLength(128, ErrorMessage = "StringLengthMessage")]
        public string RaisonSocial { get; set; }
        [Required(ErrorMessage = "يرجى اختيار ولاية المقر الاجتماعي")]
        [Display(Name = "Wilaya")]
        public int WilayaId { get; set; }
        [Required(ErrorMessage = "يرجى اختيار محيط النشاط")]
        public int PerimetreActiviteId { get; set; }
        [Required(ErrorMessage = "يرجى اختيار النشاط")]
        public int ActiviteId { get; set; }
        [Display(Name = "NIN")]
        [Required(ErrorMessage = "يجب ملء هذا الحقل"), StringLength(18, ErrorMessage = "StringLengthMessage")]
        [RegularExpression(@"^\d{18}$", ErrorMessage = "يجب أن يتكون الرقم التعريفي الوطني الوحيد من 18 رقم.")]
        public string NIN { get; set; }

        [Display(Name = "NomAr")]
        [Required(ErrorMessage = "يجب ملء هذا الحقل"), StringLength(128, ErrorMessage = "StringLengthMessage")]
        public string NomAr { get; set; }
        [Display(Name = "PrenomAr")]
        [Required(ErrorMessage = "يجب ملء هذا الحقل"), StringLength(128, ErrorMessage = "StringLengthMessage")]
        public string PrenomAr { get; set; }
        [Display(Name = "NomFr")]
        [Required(ErrorMessage = "يجب ملء هذا الحقل"), StringLength(128, ErrorMessage = "StringLengthMessage")]
        public string NomFr { get; set; }
        [Display(Name = "PrenomFr")]
        [Required(ErrorMessage = "يجب ملء هذا الحقل"), StringLength(128, ErrorMessage = "StringLengthMessage")]
        public string PrenomFr { get; set; }

        [Required(ErrorMessage = "يجب إدخال رقم شهادة الميلاد"), StringLength(5, ErrorMessage = "StringLengthMessage")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "يجب أن يتكون رقم شهادة الميلاد من 05 أرقام.")]
        public string NumActNais { get; set; }
        [Display(Name = "DateNaissance"), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "يجب ملء هذا الحقل")]
        public DateTime DateNaissance { get; set; }

        [Display(Name = "WilayaNaissanceId")]
        [Required(ErrorMessage = "يجب ملء هذا الحقل")]
        public int WilayaNaissanceId { get; set; }
        [Display(Name = "CommuneNaissanceId")]
        [Required(ErrorMessage = "يجب ملء هذا الحقل")]
        public int CommuneNaissanceId { get; set; }


        [Display(Name = "NumTelephone")]
        [RegularExpression(@"^0[5-7]\d{8}$", ErrorMessage = "يرجى إدخال رقم هاتف صحيح مكون من 10 أرقام يبدأ بـ 05 أو 06 أو 07.")]
        [Required(ErrorMessage = "يجب ملء هذا الحقل")]
        public string NumTelephone { get; set; }
        [Display(Name = "Email")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "الرجاء إدخال عنوان بريد إلكتروني صحيح.")]
        [Required(ErrorMessage = "يجب ملء هذا الحقل")]
        public string Email { get; set; }

        [Required(ErrorMessage = "يرجى التحقق من الكابتشا")]
        [Display(Name = "ReCaptcha")]
        public string RecaptchaResponse { get; set; }
        public bool IsTraited { get; set; } = false;
        public bool IsNesda { get; set; } = false;
        public string Observation { get; set; }
        public Wilaya Wilaya { get; set; }
        public Activite Activite { get; set; }
        public PerimetreActivite PerimetreActivite { get; set; }
        public virtual Commune CommuneNaissance { get; set; }

    }
}
