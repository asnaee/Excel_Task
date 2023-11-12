using System.ComponentModel.DataAnnotations;

namespace work_01.Model.ViewModel
{
    public class PatientVM
    {
        public PatientVM()
        {
            this.NCDID = new List<int>();
            this.AllergiesID = new List<int>();
        }
        public int PatientId { get; set; }

        public string PatientName { get; set; } = null!;

        public Epilepsy Epilepsy { get; set; }

        public int DiseaseId { get; set; }

        public List<int> NCDID { get; set; }

        public List<int> AllergiesID { get; set; }
    }
}
