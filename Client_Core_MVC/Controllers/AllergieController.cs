using Client_Core_MVC.Models.JsonViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace Client_Core_MVC.Controllers
{

    public class AllergieController : Controller
    {
        Uri baseAdress = new Uri("http://localhost:60443/api");
        private readonly HttpClient Client;
        public AllergieController()
        {
            Client = new HttpClient();
            Client.BaseAddress = baseAdress;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
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

                return View(AllergieVm);
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
        public async Task<IActionResult> Create(Allergie allergie)
        {
            try
            {
                HttpResponseMessage response = await Client.PostAsJsonAsync(Client.BaseAddress + "/Allergie", allergie);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    string errorData = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error response from API: {errorData}");
                    return View(allergie); // Return to the Create view with the model if there's an error
                }
            }
            catch (Exception ex)
            {
                //Log the exception
                Console.WriteLine(ex);
                throw;
            }
        }

       // Edit method to update an existing Allergie
       [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                HttpResponseMessage response = await Client.GetAsync(Client.BaseAddress + $"/Allergie/{id}");

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    Allergie allergie = JsonConvert.DeserializeObject<Allergie>(data);

                    return View(allergie);
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
        public async Task<IActionResult> Edit(int? id, Allergie allergie)
        {
            try
            {
                string apiUrl = $"{Client.BaseAddress}/Allergie/{id}";
                Console.WriteLine($"API URL: {apiUrl}");
                HttpResponseMessage response = await Client.PutAsJsonAsync(apiUrl, allergie);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    string errorData = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error response from API: {errorData}");
                    return View(allergie); // Return to the Edit view with the model if there's an error
                }
            }
            catch (Exception ex)
            {
               // Log the exception
                Console.WriteLine(ex);
                throw;
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> Edit(Allergie allergie)
        //{
        //    try
        //    {
        //        HttpResponseMessage response = await Client.PutAsJsonAsync(Client.BaseAddress + $"/Allergie", allergie);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            string errorData = await response.Content.ReadAsStringAsync();
        //            Console.WriteLine($"Error response from API: {errorData}");
        //            return View(allergie); // Return to the Edit view with the model if there's an error
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception
        //        Console.WriteLine(ex);
        //        throw;
        //    }
        //}
        //public async Task<IActionResult> Edit(int allergieId, Allergie allergie)
        //{
        //    try
        //    {
        //        string apiUrl = $"{Client.BaseAddress}/Allergie/{allergieId}";
        //        Console.WriteLine($"API URL: {apiUrl}");

        //        HttpResponseMessage response = await Client.PutAsJsonAsync(apiUrl, allergie);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            string errorData = await response.Content.ReadAsStringAsync();
        //            Console.WriteLine($"Error response from API: {errorData}");
        //            return View(allergie); // Return to the Edit view with the model if there's an error
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception
        //        Console.WriteLine(ex);
        //        throw;
        //    }
        //}

        //public async Task<IActionResult> Edit(int allergieId, Allergie allergie)
        //{
        //    try
        //    {
        //        string apiUrl = $"{Client.BaseAddress}/Allergie/{allergieId}";
        //        Console.WriteLine($"API URL: {apiUrl}");
        //        HttpResponseMessage response = await Client.PutAsJsonAsync(apiUrl, allergie);
        //        // HttpResponseMessage response = await Client.PutAsJsonAsync(Client.BaseAddress + $"/Allergie/{allergieId}", allergie);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            string errorData = await response.Content.ReadAsStringAsync();
        //            Console.WriteLine($"Error response from API: {errorData}");
        //            return View(allergie); // Return to the Edit view with the model if there's an error
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception
        //        Console.WriteLine(ex);
        //        throw;
        //    }
        //}

        //Delete method to remove an Allergie

       [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                HttpResponseMessage response = await Client.GetAsync(Client.BaseAddress + $"/Allergie/{id}");

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    Allergie allergie = JsonConvert.DeserializeObject<Allergie>(data);

                    return View(allergie);
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
                //Log the exception
                Console.WriteLine(ex);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                HttpResponseMessage response = await Client.DeleteAsync(Client.BaseAddress + $"/Allergie/{id}");

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
