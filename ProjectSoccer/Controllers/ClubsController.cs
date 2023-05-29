using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectSoccer.DataAccessLayer.Repositories;
using ProjectSoccer.Models;
using ProjectSoccer.Models.ViewModels;

namespace ProjectSoccer.Controllers
{
    public class ClubsController : Controller
    {
        private readonly ClubRepository _clubRepo;

        public ClubsController(ClubRepository clubRepo)
        {
            _clubRepo = clubRepo;
        }

        public async Task<IActionResult> Index()
        {
            var clubsList = await _clubRepo.GetAll();
            return View(clubsList);
        }
        public async Task<IActionResult> RefreshList()
        {
            var clubsListJson = JsonConvert.SerializeObject(await _clubRepo.GetAll());
            return Json(clubsListJson);
        }
        public async Task<IActionResult> Details(int id)
        {
            var club = await _clubRepo.GetById(id);
            if (club == null)
                return View("Error!");
            return View(club);
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _clubRepo.Delete(id);

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClubViewModel clubvm)
        {
            var club = new Club { Name = clubvm.Name, ShortName = clubvm.ShortName, Logo = clubvm.Logo };
            await _clubRepo.Create(club);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var club = await _clubRepo.GetById(id);
            if (club == null)
                return View("Error!");
            return View(club);
        }
        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] Club club)
        {
            var existingClub = await _clubRepo.GetById(id);
            if (existingClub == null)
            {
                return NotFound();
            }
            if(club.Name is not "")
                existingClub.Name = club.Name;
            if (club.ShortName is not "")
                existingClub.ShortName = club.ShortName;
            if (club.Logo is not "")
                existingClub.Logo = club.Logo;

            await _clubRepo.Update(existingClub);
            return Ok();
        }

    }
}
