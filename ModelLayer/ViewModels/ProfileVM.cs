using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ModelLayer.ViewModels
{
    public class ProfileVM
    {
       public Guid? ProfessionalStatusId { get; set; }
       
        public HttpPostedFileBase Image { get; set; }

        public string Imagepath { get; set; }

        [DataType(DataType.Url)]
        public string Website { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Skills { get; set; }
        public string GitHubUsername { get; set; }
        [Required(ErrorMessage ="Add atleast 30 words for bio"), MinLength(length:50)]
        public string Bio { get; set; }


        public List<string> SocialLinks { get; set; }
        

    }
}
