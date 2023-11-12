using Client_Core_MVC.Models.JsonViewModel;

namespace Client_Core_MVC.Models
{
    public class PatientViewModel
    {
        public PatientViewModel()
        {
            this.NCDID = new List<int>();
            this.allergiesID = new List<int>();
        }
        public int PatientId { get; set; }
        // public int PatientID { get; set; }
        public string PatientName { get; set; }
        public Epilepsy Epilepsy { get; set; }
        public string? DiseaseName { get; set; }
        public List<string>? NCDNames { get; set; }
        public List<string>? AllergiesNames { get; set; }
       
       

       // public string PatientName { get; set; } = null!;

        //public Epilepsy Epilepsy { get; set; }

        public int DiseaseId { get; set; }
        //public List<NCD> NCD { get; set; }

        // public List<Allergie> allergies { get; set; }

        public List<int> NCDID { get; set; }

        public List<int> allergiesID { get; set; }
    }
}
