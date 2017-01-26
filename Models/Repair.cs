using System;
using System.ComponentModel.DataAnnotations;

namespace MvcGarage.Models {
    public class Repair {
        public int ID { get; set; }
        [RequiredAttribute]
        [StringLengthAttribute(25)]
        [MinLengthAttribute(5)]
        public string Vehicle {get; set; }
        [RequiredAttribute]
        [Display(Name = "Repair Date")]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime RepairDate { get; set; }
        [RequiredAttribute]
        [Display(Name = "Repair Type")]
        [MinLengthAttribute(5)]
        public string RepairType { get; set; }
        [RequiredAttribute]
        public int Miles { get; set; }
        [RequiredAttribute]
        [StringLengthAttribute(50)]
        [MinLengthAttribute(4)]
        public string Workshop {get; set; }
        [RequiredAttribute]
        [MinLengthAttribute(5)]
        [Display(Name = "Work Details")]
        public string Workdone { get; set; }
    }
}