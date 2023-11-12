using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Client_Core_MVC.Models.JsonViewModel
{
    public class Patient
    {
        [JsonProperty("patientID")]
        public int PatientID { get; set; }

        [JsonProperty("diseaseID")]
        public int DiseaseID { get; set; }

        [JsonProperty("patientName")]
        public string PatientName { get; set; } = default!;

        [JsonProperty("epilepsy")]
        public Epilepsy Epilepsy { get; set; }

        // Navigation properties for relationships
        [JsonProperty("ncdDetails")]
        public ICollection<NCD_Detail> NCDDetails { get; set; } = new List<NCD_Detail>();

        [JsonProperty("allergiesDetails")]
        public ICollection<Allergies_Detail> AllergiesDetails { get; set; } = new List<Allergies_Detail>();

        [JsonProperty("diseaseInformation")]
        public DiseaseInformation DiseaseInformation { get; set; }
    }
    public class DiseaseInformation
    {
        [JsonProperty("diseaseID")]
        public int DiseaseID { get; set; }

        [JsonProperty("diseaseName")]
        public string DiseaseName { get; set; } = default!;

        [JsonProperty("patients")]
        public ICollection<Patient> Patients { get; set; } = new List<Patient>();
    }
    public enum Epilepsy
    {
        Yes = 1,
        No,
        NotVerified
    }
    public class NCD
    {
        [JsonProperty("ncdid")]
        public int NCDID { get; set; }

        [JsonProperty("ncdName")]
        public string NCDName { get; set; } = default!;
    }
    public class Allergie
    {
        [JsonProperty("allergiesID")]
        public int allergiesID { get; set; }

        [JsonProperty("allergiesName")]
        public string AllergiesName { get; set; } = default!;
    }
    public class NCD_Detail
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Patient")]
        public int PatientID { get; set; }

        [ForeignKey("NCD")]
        public int NCDID { get; set; }

        // Navigation properties for relationships
        public Patient Patient { get; set; }
        public NCD NCD { get; set; }
    }
    public class Allergies_Detail
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Patient")]
        public int PatientID { get; set; }

        [ForeignKey("Allergie")]
        public int? AllergiesID { get; set; }

        // Other Allergies details properties

        // Navigation properties for relationships
        public Patient Patient { get; set; }
        public Allergie Allergie { get; set; }
    }
}
