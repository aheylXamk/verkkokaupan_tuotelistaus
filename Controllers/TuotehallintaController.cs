using System;
using Microsoft.AspNetCore.Mvc;
using ot8.Models;

namespace ot8.Controllers
{
    public class TuotehallintaController : Controller
    {
        public IActionResult Index()
        {

            Tuotehallinta tuotehallinta = new Tuotehallinta();

            return View("Index", tuotehallinta.haeKaikki());
        }

        public IActionResult Uusi(string uusiVari, string uusiKoko, decimal uusiHinta, int uusiSaldo) {

            try {

                Tuotehallinta tuotehallinta = new Tuotehallinta();

                tuotehallinta.LisaaUusi(uusiVari, uusiKoko, uusiHinta, uusiSaldo);

                return RedirectToAction("Index", "Tuotehallinta");

            } catch (Exception e) {

                return View("Virhe", e.Message);
            }
        }
    }
}
