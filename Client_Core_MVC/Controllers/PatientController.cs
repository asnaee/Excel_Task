using Client_Core_MVC.Models;
using Client_Core_MVC.Models.JsonViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net;

namespace Client_Core_MVC.Controllers
{
    public class PatientController : Controller
    {
        Uri baseAdress = new Uri("http://localhost:60443/api");
        private readonly HttpClient Client;

        public PatientController()
        {
            Client = new HttpClient();
            Client.BaseAddress = baseAdress;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                List<PatientViewModel> patients = new List<PatientViewModel>();
                HttpResponseMessage response = await Client.GetAsync(Client.BaseAddress + "/Patient/All");

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    patients = JsonConvert.DeserializeObject<List<PatientViewModel>>(data);

                  
                }

                if (!response.IsSuccessStatusCode)
                {
                    string errorData = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error response from API: {errorData}");
                }

                return View(patients);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex);
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            //DiseaseInformation
            List<DiseaseInformation> diseases = new List<DiseaseInformation>();
            HttpResponseMessage responsed = await Client.GetAsync(Client.BaseAddress + "/Disease");

            if (responsed.IsSuccessStatusCode)
            {
                string data = await responsed.Content.ReadAsStringAsync();
                diseases = JsonConvert.DeserializeObject<List<DiseaseInformation>>(data);
            }

            if (!responsed.IsSuccessStatusCode)
            {
                string errorData = await responsed.Content.ReadAsStringAsync();
                Console.WriteLine($"Error response from API: {errorData}");
            }
            //NCD
            List<NCD> ncds = new List<NCD>();
            HttpResponseMessage responsen = await Client.GetAsync(Client.BaseAddress + "/NDC");

            if (responsen.IsSuccessStatusCode)
            {
                string data = await responsen.Content.ReadAsStringAsync();
                ncds = JsonConvert.DeserializeObject<List<NCD>>(data);
            }

            if (!responsen.IsSuccessStatusCode)
            {
                string errorData = await responsen.Content.ReadAsStringAsync();
                Console.WriteLine($"Error response from API: {errorData}");
            }

            //Allergie
            List<Allergie> AllergieVm = new List<Allergie>();
            HttpResponseMessage response = await Client.GetAsync(Client.BaseAddress + "/Allergie");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                AllergieVm = JsonConvert.DeserializeObject<List<Allergie>>(data);
            }

            if (!response.IsSuccessStatusCode)
            {
                string errorData = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error response from API: {errorData}");
            }

            // Set ViewBag properties
            ViewBag.diseaseList = new SelectList(diseases, "DiseaseID", "DiseaseName");
            ViewBag.NCDList = new SelectList(ncds, "NCDID", "NCDName");
            ViewBag.AllergiesList = new SelectList(AllergieVm, "allergiesID", "AllergiesName");

            return View();
        }

       
        [HttpPost]
        public async Task<IActionResult> Create(PatientVM patientVM)
        {
            try
            {
                HttpResponseMessage response = await Client.PostAsJsonAsync(Client.BaseAddress + "/Patient", patientVM);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    string errorData = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error response from API: {errorData}");
                    return View(patientVM); // Return to the Create view with the model if there's an error
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex);
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                var model = new PatientVM();
                //return View(model);
                //DiseaseInformation
                List<DiseaseInformation> diseases = new List<DiseaseInformation>();
                HttpResponseMessage responsed = await Client.GetAsync(Client.BaseAddress + "/Disease");

                if (responsed.IsSuccessStatusCode)
                {
                    string data = await responsed.Content.ReadAsStringAsync();
                    diseases = JsonConvert.DeserializeObject<List<DiseaseInformation>>(data);
                }

                if (!responsed.IsSuccessStatusCode)
                {
                    string errorData = await responsed.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error response from API: {errorData}");
                }
                //NCD
                List<NCD> ncds = new List<NCD>();
                HttpResponseMessage responsen = await Client.GetAsync(Client.BaseAddress + "/NDC");

                if (responsen.IsSuccessStatusCode)
                {
                    string data = await responsen.Content.ReadAsStringAsync();
                    ncds = JsonConvert.DeserializeObject<List<NCD>>(data);
                }

                if (!responsen.IsSuccessStatusCode)
                {
                    string errorData = await responsen.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error response from API: {errorData}");
                }

                //Allergie
                List<Allergie> AllergieVm = new List<Allergie>();
                HttpResponseMessage responsea = await Client.GetAsync(Client.BaseAddress + "/Allergie");

                if (responsea.IsSuccessStatusCode)
                {
                    string data = await responsea.Content.ReadAsStringAsync();
                    AllergieVm = JsonConvert.DeserializeObject<List<Allergie>>(data);
                }

                if (!responsea.IsSuccessStatusCode)
                {
                    string errorData = await responsea.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error response from API: {errorData}");
                }

                // Set ViewBag properties
                ViewBag.diseaseList = new SelectList(diseases, "DiseaseID", "DiseaseName");
                ViewBag.NCDList = new SelectList(ncds, "NCDID", "NCDName");
                ViewBag.AllergiesList = new SelectList(AllergieVm, "allergiesID", "AllergiesName");
                //-------------------------------

                HttpResponseMessage response = await Client.GetAsync(Client.BaseAddress + $"/Patient/{id}");

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    PatientViewModel patient = JsonConvert.DeserializeObject<PatientViewModel>(data);

                    return View(patient);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    Console.WriteLine("Resource not found. Returning to Index.");
                    return RedirectToAction("Index");
                }
                else
                {
                    string errorData = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error response from API. Status Code: {response.StatusCode}. Error Data: {errorData}");
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, PatientViewModel patient)
        {
            try
            {
                string apiUrl = $"{Client.BaseAddress}/Patient/{id}";
                Console.WriteLine($"API URL: {apiUrl}");
                HttpResponseMessage response = await Client.PutAsJsonAsync(apiUrl, patient);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    string errorData = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error response from API: {errorData}");
                    return View(patient); // Return to the Edit view with the model if there's an error
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex);
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                HttpResponseMessage response = await Client.GetAsync(Client.BaseAddress + $"/Patient/{id}");

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    Patient patient = JsonConvert.DeserializeObject<Patient>(data);

                    return View(patient);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    Console.WriteLine("Resource not found. Returning to Index.");
                    return RedirectToAction("Index");
                }
                else
                {
                    string errorData = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error response from API. Status Code: {response.StatusCode}. Error Data: {errorData}");
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                HttpResponseMessage response = await Client.DeleteAsync(Client.BaseAddress + $"/Patient/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    string errorData = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error response from API: {errorData}");
                    return RedirectToAction("Index"); // Redirect to the Index view even if there's an error during deletion
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex);
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Dynamic()
        {
            //DiseaseInformation
            List<DiseaseInformation> diseases = new List<DiseaseInformation>();
            HttpResponseMessage responsed = await Client.GetAsync(Client.BaseAddress + "/Disease");

            if (responsed.IsSuccessStatusCode)
            {
                string data = await responsed.Content.ReadAsStringAsync();
                diseases = JsonConvert.DeserializeObject<List<DiseaseInformation>>(data);
            }

            if (!responsed.IsSuccessStatusCode)
            {
                string errorData = await responsed.Content.ReadAsStringAsync();
                Console.WriteLine($"Error response from API: {errorData}");
            }
            //NCD
            List<NCD> ncds = new List<NCD>();
            HttpResponseMessage responsen = await Client.GetAsync(Client.BaseAddress + "/NDC");

            if (responsen.IsSuccessStatusCode)
            {
                string data = await responsen.Content.ReadAsStringAsync();
                ncds = JsonConvert.DeserializeObject<List<NCD>>(data);
            }

            if (!responsen.IsSuccessStatusCode)
            {
                string errorData = await responsen.Content.ReadAsStringAsync();
                Console.WriteLine($"Error response from API: {errorData}");
            }

            //Allergie
            List<Allergie> AllergieVm = new List<Allergie>();
            HttpResponseMessage response = await Client.GetAsync(Client.BaseAddress + "/Allergie");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                AllergieVm = JsonConvert.DeserializeObject<List<Allergie>>(data);
            }

            if (!response.IsSuccessStatusCode)
            {
                string errorData = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error response from API: {errorData}");
            }

            // Set ViewBag properties
            ViewBag.diseaseList = new SelectList(diseases, "DiseaseID", "DiseaseName");
            ViewBag.NCDList = new SelectList(ncds, "NCDID", "NCDName");
            ViewBag.AllergiesList = new SelectList(AllergieVm, "allergiesID", "AllergiesName");

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Dynamic(PatientVM patientVM)
        {
            try
            {
                HttpResponseMessage response = await Client.PostAsJsonAsync(Client.BaseAddress + "/Patient", patientVM);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    string errorData = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error response from API: {errorData}");
                    return View(patientVM); // Return to the Create view with the model if there's an error
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex);
                throw;
            }
        }
    }

}
