using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Newtonsoft.Json;
using Parcial_SaraMelissaMarroquinVega.DAL.Entities;
using ServiceStack.Host;
using System.Diagnostics;
using System.Net.Http;
using WebPages.Models;
using static ServiceStack.Diagnostics.Events;

namespace WebPages.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpclient;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpclient)
        {
            _logger = logger;
            _httpclient = httpclient;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost, ActionName("Search")]
        public async Task<IActionResult> Search(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out _))
                {
                    Ticket _ticket = new Ticket { Message = "Boleta no válida" };
                    return View(_ticket);
                }

                var url = string.Format("https://localhost:7064/api/Tickets/Get/{0}", id);
                var json = await _httpclient.CreateClient().GetStringAsync(url);
                Ticket ticket = JsonConvert.DeserializeObject<Ticket>(json);


                if (ticket != null)
                {
                    if (!ticket.IsUsed == true)
                    {
                        Ticket _ticket = new Ticket
                        {
                            Id = ticket.Id,
                            Message = "Boleta válida, puede ingresar al concierto",
                            UseDate = DateTime.Now,
                            IsUsed = true,
                            EntranceGate = "Portería Occidental"
                        };
                        Edit(_ticket.Id, _ticket);
                        return View(_ticket);
                    }

                    /*If the ticket has already been used*/
                    Ticket __ticket = new Ticket {
                        Message = "Boleta ya usada",
                        UseDate = ticket.UseDate,
                        IsUsed = ticket.IsUsed,
                        EntranceGate = ticket.EntranceGate
                    };
                    return View(__ticket);
                }
                else
                {
                    return View(null);
                }
            }
            catch (Exception ex)
            {
                Ticket _ticket = new Ticket { Message = "Boleta no válida" };
                return View(_ticket);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, Ticket ticket)
        {
            try
            {
                var url = String.Format("https://localhost:7064/api/Tickets/Edit/{0}", id);
                await _httpclient.CreateClient().PutAsJsonAsync(url, ticket);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Details()
        {
            var url = "https://localhost:7064/api/Tickets/Get";
            var json = await _httpclient.CreateClient().GetStringAsync(url);
            List<Ticket> tickets = JsonConvert.DeserializeObject<List<Ticket>>(json);
            return View(tickets);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}