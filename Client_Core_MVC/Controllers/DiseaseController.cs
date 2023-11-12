using Client_Core_MVC.Models.JsonViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace Client_Core_MVC.Controllers
{
    public class DiseaseController : Controller
    {
        Uri baseAdress = new Uri("http://localhost:60443/api");
        private readonly HttpClient Client;

        public DiseaseController()
        {
            Client = new HttpClient();
            Client.BaseAddress = baseAdress;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                List<DiseaseInformation> diseases = new List<DiseaseInformation>();
                HttpResponseMessage response = await Client.GetAsync(Client.BaseAddress + "/Disease");

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    diseases = JsonConvert.DeserializeObject<List<DiseaseInformation>>(data);
                }

                if (!response.IsSuccessStatusCode)
                {
                    string errorData = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error response from API: {errorData}");
                }

                return View(diseases);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex);
                throw;
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DiseaseInformation disease)
        {
            try
            {
                HttpResponseMessage response = await Client.PostAsJsonAsync(Client.BaseAddress + "/Disease", disease);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    string errorData = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error response from API: {errorData}");
                    return View(disease); // Return to the Create view with the model if there's an error
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
                HttpResponseMessage response = await Client.GetAsync(Client.BaseAddress + $"/Disease/{id}");

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    DiseaseInformation disease = JsonConvert.DeserializeObject<DiseaseInformation>(data);

                    return View(disease);
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
        public async Task<IActionResult> Edit(int? id, DiseaseInformation disease)
        {
            try
            {
                string apiUrl = $"{Client.BaseAddress}/Disease/{id}";
                Console.WriteLine($"API URL: {apiUrl}");
                HttpResponseMessage response = await Client.PutAsJsonAsync(apiUrl, disease);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    string errorData = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error response from API: {errorData}");
                    return View(disease); // Return to the Edit view with the model if there's an error
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
                HttpResponseMessage response = await Client.GetAsync(Client.BaseAddress + $"/Disease/{id}");

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    DiseaseInformation disease = JsonConvert.DeserializeObject<DiseaseInformation>(data);

                    return View(disease);
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
                HttpResponseMessage response = await Client.DeleteAsync(Client.BaseAddress + $"/Disease/{id}");

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
    }

    //public class DiseaseController : Controller
    //{
    //    Uri baseAdress = new Uri("http://localhost:60443/api");
    //    private readonly HttpClient Client;
    //    public DiseaseController()
    //    {
    //        Client = new HttpClient();
    //        Client.BaseAddress = baseAdress;
    //    }
    //    public IActionResult Index()
    //    {
    //        return View();
    //    }
    //}
}
