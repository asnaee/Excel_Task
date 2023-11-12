using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace work_01.Model
{
    public class Patient
    {
        [Key]
        public int PatientID { get; set; }
        [ForeignKey ("DiseaseInformation")]
        public int DiseaseID { get; set; }
        public string PatientName { get; set; } = default!;
        public Epilepsy Epilepsy { get; set; }

        // Navigation properties for relationships
        [JsonIgnore]
        public ICollection<NCD_Detail> NCDDetails { get; set; } = new List<NCD_Detail>();
        [JsonIgnore]
        public ICollection<Allergies_Detail> AllergiesDetails { get; set; } = new List<Allergies_Detail>();
        public  DiseaseInformation DiseaseInformation { get; set; }

    }

    public class DiseaseInformation
    {
        [Key]
        public int DiseaseID { get; set; }
        public string DiseaseName { get; set; } = default!;

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
        [Key]
        public int NCDID { get; set; }
        public string NCDName { get; set; } = default!;
    }

    public class Allergie
    {
        [Key]
        public int AllergiesID { get; set; }
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

    public class AsigmentContext : DbContext
    {
        public AsigmentContext(DbContextOptions<AsigmentContext> options) : base(options) { }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<DiseaseInformation> DiseaseInformations { get; set; }
        public DbSet<NCD> NCDs { get; set; }
        public DbSet<Allergie> Allergies { get; set; }
        public DbSet<NCD_Detail> NCDDetails { get; set; }
        public DbSet<Allergies_Detail>? AllergiesDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed data
            modelBuilder.Entity<DiseaseInformation>().HasData(
                new DiseaseInformation { DiseaseID = 1, DiseaseName = "Diabetes" },
                new DiseaseInformation { DiseaseID = 2, DiseaseName = "Hypertension" },
                new DiseaseInformation { DiseaseID = 3, DiseaseName = "Arthritis" },
                new DiseaseInformation { DiseaseID = 4, DiseaseName = "Heart Disease" },
                new DiseaseInformation { DiseaseID = 5, DiseaseName = "Respiratory Infections" }
            );

            modelBuilder.Entity<NCD>().HasData(
                new NCD { NCDID = 1, NCDName = "Asthma" },
                new NCD { NCDID = 2, NCDName = "Cancer" },
                new NCD { NCDID = 3, NCDName = "Disorders of ear" },
                new NCD { NCDID = 4, NCDName = "Disorder of eye" },
                new NCD { NCDID = 5, NCDName = "Mental illness" }
            );

            modelBuilder.Entity<Allergie>().HasData(
                new Allergie { AllergiesID = 1, AllergiesName = "Drugs - Penicillin" },
                new Allergie { AllergiesID = 2, AllergiesName = "Drugs - Others" },
                new Allergie { AllergiesID = 3, AllergiesName = "Animals" },
                new Allergie { AllergiesID = 4, AllergiesName = "Food" },
                new Allergie { AllergiesID = 5, AllergiesName = "Ointments" },
                new Allergie { AllergiesID = 6, AllergiesName = "Plant" },
                new Allergie { AllergiesID = 7, AllergiesName = "Sprays" },
                new Allergie { AllergiesID = 8, AllergiesName = "Others" },
                new Allergie { AllergiesID = 9, AllergiesName = "No Allergies" }
            );
        }
    }
}
          