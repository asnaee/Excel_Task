using Client_Core_MVC.Models.JsonViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace Client_Core_MVC.Controllers
{
    public class NCDController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:60443/api");
        private readonly HttpClient Client;

        public NCDController()
        {
            Client = new HttpClient();
            Client.BaseAddress = baseAddress;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                List<NCD> ncds = new List<NCD>();
                HttpResponseMessage response = await Client.GetAsync(Client.BaseAddress + "/NDC");

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    ncds = JsonConvert.DeserializeObject<List<NCD>>(data);
                }

                if (!response.IsSuccessStatusCode)
                {
                    string errorData = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error response from API: {errorData}");
                }

                return View(ncds);
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
        public async Task<IActionResult> Create(NCD ncd)
        {
            try
            {
                HttpResponseMessage response = await Client.PostAsJsonAsync(Client.BaseAddress + "/NDC", ncd);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    string errorData = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error response from API: {errorData}");
                    return View(ncd); // Return to the Create view with the model if there's an error
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
                HttpResponseMessage response = await Client.GetAsync(Client.BaseAddress + $"/NDC/{id}");

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    NCD ncd = JsonConvert.DeserializeObject<NCD>(data);

                    return View(ncd);
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
        public async Task<IActionResult> Edit(int? id, NCD ncd)
        {
            try
            {
                string apiUrl = $"{Client.BaseAddress}/NDC/{id}";
                Console.WriteLine($"API URL: {apiUrl}");
                HttpResponseMessage response = await Client.PutAsJsonAsync(apiUrl, ncd);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    string errorData = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error response from API: {errorData}");
                    return View(ncd); // Return to the Edit view with the model if there's an error
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
                HttpResponseMessage response = await Client.GetAsync(Client.BaseAddress + $"/NDC/{id}");

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    NCD ncd = JsonConvert.DeserializeObject<NCD>(data);

                    return View(ncd);
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
                HttpResponseMessage response = await Client.DeleteAsync(Client.BaseAddress + $"/NDC/{id}");

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

}
