using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAppContatos.Models;

namespace WebAppContatos.Controllers
{
    //URL: http://localhost:25378

    public class HomeController : Controller
    {
        string BaseUrl = "http://localhost:25378";

        public IActionResult Index()
        {
            //Criando uma lista de contatos
            List<Contato> contatoLista = new List<Contato>();

            //Criando uma instância de HttpClient
            using (HttpClient cliente = new HttpClient())
            {
                //Definindo o endereço base
                cliente.BaseAddress = new Uri(BaseUrl);

                //Definindo o formato dos dados no request
                MediaTypeWithQualityHeaderValue contentType = new
                    MediaTypeWithQualityHeaderValue("application/json");

                cliente.DefaultRequestHeaders.Accept.Add(contentType);

                HttpResponseMessage response = cliente.GetAsync("/api/contatos").Result;
                if (response.IsSuccessStatusCode)
                {
                    var contatoResponse = response.Content.ReadAsStringAsync().Result;
                    contatoLista = JsonConvert.DeserializeObject<List<Contato>>(contatoResponse);
                }
                return View(contatoLista);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            //Apresenta o formulário(view) dos contatos
            return View();
        }

        [HttpPost]
        public IActionResult Create(Contato contato)
        {
            Contato contatoInfo = new Contato();

            using (HttpClient cliente = new HttpClient())
            {
                //Define a URL Base
                cliente.BaseAddress = new Uri(BaseUrl);
                //Serializa os dados do objeto Contato
                string contatoSerializado = JsonConvert.SerializeObject(contato);
                //Define o formato dos dados do requiest
                var conteudoDados = new StringContent(contatoSerializado, System.Text.Encoding.UTF8, "application/json");
                //Envia o request para encontrar o recurso de serviço REST exposto pela Web API
                HttpResponseMessage response = cliente.PostAsync("/api/contatos", conteudoDados).Result;
                //Verifica se a resposta foi obtida com sucesso
                if (response.IsSuccessStatusCode)
                {
                    //Armazena os detalhes da resposta recebidos pela Web API
                    var contatoResponse = response.Content.ReadAsStringAsync().Result;
                    //Deserializa a resposta recebida e armazena no objeto Contato
                    contatoInfo = JsonConvert.DeserializeObject<Contato>(contatoResponse);
                    //Redireciona para a Action "Index"
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(contatoInfo);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Contato contato = new Contato();
            using (HttpClient cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(BaseUrl);
                HttpResponseMessage response = cliente.GetAsync("/api/contatos/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    var contatoResposta = response.Content.ReadAsStringAsync().Result;
                    contato = JsonConvert.DeserializeObject<Contato>(contatoResposta);
                }
                return View(contato);
            }
        }

        [HttpPost]
        public IActionResult Edit(Contato contato)
        {
            using (HttpClient cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(BaseUrl);
                string stringData = JsonConvert.SerializeObject(contato);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = cliente.PutAsync("/api/contatos/" + contato.Id, contentData).Result;
                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Message = response.Content.ReadAsStringAsync().Result;
                    var contatoResultado = response.Content.ReadAsStringAsync().Result;
                    return RedirectToAction(nameof(Index));
                }
                return View(contato);
            }
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Contato contato = new Contato();
            using (HttpClient cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(BaseUrl);
                HttpResponseMessage response = cliente.GetAsync("/api/contatos/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    var contatoResposta = response.Content.ReadAsStringAsync().Result;
                    contato = JsonConvert.DeserializeObject<Contato>(contatoResposta);
                }
                return View(contato);
            }
        }

        public IActionResult Delete(int id)
        {
            using (HttpClient cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(BaseUrl);
                HttpResponseMessage response = cliente.GetAsync("/api/contatos/" + id).Result;
                var contatoResposta = response.Content.ReadAsStringAsync().Result;
                Contato contatoDados = JsonConvert.DeserializeObject<Contato>(contatoResposta);
                return View(contatoDados);
            }
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            using (HttpClient cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(BaseUrl);
                HttpResponseMessage response = cliente.DeleteAsync("/api/contatos/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    var contatoResposta = response.Content.ReadAsStringAsync().Result;
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Minha descrição.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Meu contato.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
