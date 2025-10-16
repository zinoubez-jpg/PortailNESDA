using Ansej.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ansej.Sidjeme.Domain.Entities
{
    public class DeclarationHonneurMI : IEntity<int>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "الحقل مطلوب")]      
        [StringLength(11, MinimumLength = 11, ErrorMessage = "يجب أن يكون الحجم في11  موضع ")]
        [RegularExpression(@"^\d+$", ErrorMessage = "يجب أن يحتوي الحقل على أرقام فقط")]
        public string ProjectNumber { get; set; }

        [Required(ErrorMessage = "الحقل مطلوب")]
        [RegularExpression(@"^[\u0600-\u06FF\s]+$", ErrorMessage = "يرجى إدخال أحرف عربية فقط")]
        [StringLength(100)]
        public string FirstlName { get; set; }

        [Required(ErrorMessage = "الحقل مطلوب")]
        [RegularExpression(@"^[\u0600-\u06FF\s]+$", ErrorMessage = "يرجى إدخال أحرف عربية فقط")]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "الحقل مطلوب")]
        [StringLength(18, MinimumLength = 18, ErrorMessage = "يجب أن يكون الحجم في18  موضع ")]
        [RegularExpression(@"^\d+$", ErrorMessage = "يجب أن يحتوي الحقل على أرقام فقط")]       
        public string NIN { get; set; }

        [Required(ErrorMessage = "الحقل مطلوب")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }



        [Required(ErrorMessage = "الحقل مطلوب")]
        public int GenderId { get; set; }
        public Genre Gender { get; set; }



        [Required(ErrorMessage = "الحقل مطلوب")]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; }



        [Required(ErrorMessage = "الحقل مطلوب")]
        [Phone]
        [StringLength(10, ErrorMessage = "يجب أن يكون الحجم في10  موضع ")]
        [RegularExpression(@"^\d{9,10}$", ErrorMessage = "يجب أن يكون الحجم في10  موضع ")]
        public string Phone { get; set; }



        [Required(ErrorMessage = "الحقل مطلوب")]
        [RegularExpression(@"^[\u0600-\u06FF\s]+$", ErrorMessage = "يرجى إدخال أحرف عربية فقط")]
        [StringLength(255)]
        public string CompanyName { get; set; }

        
        
        
        [Required(ErrorMessage = "الحقل مطلوب")]
        public int ActivityId { get; set; }
        public Activite Activity { get; set; }


		[Required(ErrorMessage = "الحقل مطلوب")]
		public int TypeOfNeedId { get; set; }
        public TypeOfNeed TypeOfNeed { get; set; }



        [Required(ErrorMessage = "الحقل مطلوب")]
        [StringLength(15)]
        public string NIF { get; set; }




        [Required(ErrorMessage = "الحقل مطلوب")]
        public int LegalStatusId { get; set; }
        public FormeJuridique LegalStatus { get; set; }




        [Required(ErrorMessage = "الحقل مطلوب")]
        public string BusinessRegisterNumber { get; set; }



        [Required(ErrorMessage = "الحقل مطلوب")]
        public int WilayaId { get; set; }
        public virtual Wilaya Wilaya { get; set; }



        [Required(ErrorMessage = "الحقل مطلوب")]
        public string Adresse { get; set; }


        public DateTime Processingdate { get; set; }
        public bool RequestProcessed { get; set; } = false;

        public int? MotifTraitementId { get; set; }

        public MotifTraitement MotifTraitement { get; set; }

        public bool granted { get; set; }

        [Required]
        public DateTime Requestdate { get; set; }
       
        public string Observation { get; set; }

        public int StructureManagerId { get; set; }
        public StructureManager StructureManager { get; set; }


        public bool Notified { get; set; }
        public DateTime? NotificationDate { get; set; }


        public bool GarantedMC { get; set; }
        public DateTime? GarantedMCDate { get; set; }
        public string ObservationMC { get; set; }


        public virtual ICollection<DownloadFile> DownloadFiles { get; set; }
        
    }
}
