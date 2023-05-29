using Microsoft.AspNetCore.Mvc;
using ProjectSoccer.DataAccessLayer.Repositories;
using ProjectSoccer.Models;
using ProjectSoccer.Models.ViewModels;

namespace ProjectSoccer.Controllers
{
    public class PlayersController : Controller
    {
        private readonly PlayerRepository _playerRepo;
        private readonly ClubRepository _clubRepo;

        public PlayersController(PlayerRepository playerRepo, ClubRepository clubRepo)
        {
            _playerRepo = playerRepo;
            _clubRepo = clubRepo;
        }

        public async Task<IActionResult> Index()
        {
            var players = await _playerRepo.GetAll();
            return View(players);
        }
        public async Task<IActionResult> Details(int id)
        {
            var player = await _playerRepo.GetById(id);
            if (player == null) { return View("Error"); }
            return View(player);
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _playerRepo.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Create([FromBody] PlayerViewModel playervm)
        {
            var clubs = await _clubRepo.GetAll();
            var clubId = clubs.SingleOrDefault(x => x.ShortName == playervm.CurrentClub.ToUpper()).Id;
            if (clubId == null) { return View("Error"); }
            var player = new Player { FirstName = playervm.FirstName, LastName = playervm.LastName, DateOfBirth = DateTime.Now.AddYears(-playervm.Age), ClubId = clubId };
            await _playerRepo.Create(player);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var player = await _playerRepo.GetById(id);
            if (player == null)
                return View("Error!");
            return View(player);
        }
        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromHeader]Player player)
        {
            var existingPlayer = await _playerRepo.GetById(player.Id);
            if(existingPlayer is null)
                return NotFound();
            if (player.FirstName is not "")
                existingPlayer.FirstName = player.FirstName;
            if (player.LastName is not "")
                existingPlayer.LastName = player.LastName;
            if (player.ClubId is not 0)
                existingPlayer.ClubId = player.ClubId;
            if (player.DateOfBirth.Year is not 0 || player.DateOfBirth.Year is not 1)
                existingPlayer.DateOfBirth = player.DateOfBirth;

            await _playerRepo.Update(existingPlayer);
            return RedirectToAction(nameof(Index));
        }
    }
}
