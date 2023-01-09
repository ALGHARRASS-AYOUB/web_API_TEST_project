using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ExerciceAPITest2.Models;
using Newtonsoft.Json;

namespace ExerciceAPITest2.Controllers
{
    public class EtudiantController : Controller
    {

        //private string URI = "https://localhost:44371/"; this uri is for the https of ssl but the certificate does not accept two instance run for the solution
            String BASEURI = "http://localhost:51880/api/"; // an alternative trick for it is to run the first project in http and the consuming project in https 
        // GET: Etudiant
        public   ActionResult Index()
        {
            IEnumerable<Etudiant> students = new List<Etudiant>();
            using (var client = new HttpClient())
            {
                // the question here is the uri is for ? //
                client.BaseAddress = new Uri(BASEURI);
                //var responseTask = client.GetAsync("Etudiant/Getetudiants").Result;
                var responseTask = client.GetAsync("Etudiant/Getetudiants");   
                 responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                //if(responseTask.IsSuccessStatusCode)
                {
                    //var readTask = result.Content.ReadAsAsync<IList<Etudiant>>();
                    var readTask = result.Content.ReadAsAsync<IList<Etudiant>>();


                    readTask.Wait();
                    //var responseString = responseTask.Content.ReadAsStringAsync().Result;
                    //students = JsonConvert.DeserializeObject<IList<Etudiant>>(responseString);
                    students=readTask.Result;
                    
                }

/*
                HttpResponseMessage response = await client.GetAsync("Etudiant/Geretudiants");
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a string
                    IEnumerable<Etudiant> result = await response.Content.ReadAsAsync<List<Etudiant>>();
                    students = result;

                    // Do something with the result
                }
*/

                else //web api sent error response
                {//log response status here.
                    students = Enumerable.Empty<Etudiant>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return  View(students);
        }

        public ActionResult Details(int id)
        {
            Etudiant e=new Etudiant();

            using(var client = new HttpClient())
            {
                client.BaseAddress=new Uri(BASEURI);
                var responseTask = client.GetAsync("Etudiant/Getetudiant/"+id);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Etudiant>();
                    readTask.Wait();
                    e = readTask.Result;
                }
                else //web api sent error response
                {//log response status here.
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            } 

            return View(e);
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(Etudiant Etudiant)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASEURI);
                //HTTP POST
                var postTask = client.PostAsJsonAsync<Etudiant>("Etudiant/Postetudiant", Etudiant);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                { return RedirectToAction("Index"); }
            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            return View(Etudiant);
        }


        
        /*        public ActionResult Edit(int id)
                {
                    Etudiant Etudiant = null;
              *//*      using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(BASEURI);
                        //HTTP GET
                        var responseTask = client.GetAsync("Etudiant/PutEtudiant?id=" + id.ToString());
                        responseTask.Wait();
                        var result = responseTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            var readTask = result.Content.ReadAsAsync<Etudiant>();
                            readTask.Wait();
                            Etudiant = readTask.Result;
                        }
                    }*//*


                    return View(Etudiant);
                }*/

        public ActionResult Edit(int id)
        {
            Etudiant e = new Etudiant();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASEURI);
                var responseTask = client.GetAsync("Etudiant/Getetudiant/" + id);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Etudiant>();
                    readTask.Wait();
                    e = readTask.Result;
                }
                else //web api sent error response
                {//log response status here.
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            return View(e);
        }

        [HttpPost]
        public ActionResult Edit(Etudiant etudiant)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASEURI);
                var putTask = client.PutAsJsonAsync<Etudiant>("Etudiant/PutEtudiant", etudiant);
                putTask.Wait();
                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                    RedirectToAction("Index");
                return View(etudiant);
            }
        }


        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BASEURI);
                //HTTP DELETE
                var deleteTask = client.DeleteAsync("Etudiant/Deleteetudiant?id=" + id.ToString());
                deleteTask.Wait();
                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }



    }


}