using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Student_MVC.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string StudenttName { get; set; }
        public Nullable<decimal> Fee { get; set; }
        public string City { get; set; }
        public string Gender { get; set; }
        public string Hobby { get; set; }  // For checkboxes
        public List<string> SelectedHobbies { get; set; }
        public int TeacherId { get; set; }  // Selected teacher
        public IEnumerable<SelectListItem> Teachers { get; set; }  // Dropdown
    }
}