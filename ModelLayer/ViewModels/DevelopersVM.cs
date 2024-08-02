using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.ViewModels
{
    public class DevelopersVM
    {
        public Guid? AccountId { get; set; }
        public string ProfessionalStatus { get; set; }

        public string Name { get; set; }

        public string Imagepath { get; set; }
        public string Address { get; set; }
        
        public string Skills { get; set; }
        public string Company { get; set; }
    }
}
