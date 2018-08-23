
using FineUIMvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CCSIM.Web.Models
{
    public class DropDownListModel
    {
        [Required]
        [Display(Name ="下拉框")]
        public string DropDownList { get; set; }

        public List<ListItem> DropDownListItem { get; set; }
    }
}