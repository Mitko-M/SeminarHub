using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeminarHub.Data;
using SeminarHub.Data.Models;
using SeminarHub.Models.Category;
using SeminarHub.Models.Seminar;
using System.Security.Claims;
using static SeminarHub.Models.DateFormat;

namespace SeminarHub.Controllers
{
    [Authorize]
    public class SeminarController : Controller
    {
        private readonly SeminarHubDbContext _context;
        public SeminarController(SeminarHubDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var seminar = await _context.Seminars.FindAsync(id);
            var currenUserId = GetUserId();

            if (seminar == null)
            {
                return BadRequest();
            }

            if (currenUserId != seminar.OrganizerId)
            {
                return Unauthorized();
            }

            var model = new SeminarDeleteViewModel()
            {
                Id = seminar.Id,
                Topic = seminar.Topic,
                DateAndTime = seminar.DateAndTime
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(SeminarDeleteViewModel model)
        {
            var seminar = await _context.Seminars.FindAsync(model.Id);
            var currenUserId = GetUserId();

            if (seminar == null)
            {
                return BadRequest();
            }

            if (currenUserId != seminar.OrganizerId)
            {
                return Unauthorized();
            }

            _context.Seminars.Remove(seminar);
            await _context.SaveChangesAsync();

            return RedirectToAction("All", "Seminar");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var seminar = await _context.Seminars
                .Where(s => s.Id == id)
                .Select(s => new SeminarDetailsViewModel()
                {
                    Id = s.Id,
                    Topic = s.Topic,
                    DateAndTime = s.DateAndTime.ToString(Format),
                    Duration = s.Duration,
                    Lecturer = s.Lecturer,
                    Organizer = s.Organizer.UserName,
                    Category = s.Category.Name,
                    Details = s.Details
                })
                .FirstOrDefaultAsync();

            if (seminar == null)
            {
                return BadRequest();
            }

            return View(seminar);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var seminarToEdit = await _context.Seminars.FindAsync(id);
            string currentUserId = GetUserId();

            if (seminarToEdit == null)
            {
                return BadRequest();
            }

            if (currentUserId != seminarToEdit.OrganizerId)
            {
                return Unauthorized();
            }

            var model = new SeminarFormModel()
            {
                Topic = seminarToEdit.Topic,
                Lecturer = seminarToEdit.Lecturer,
                Details = seminarToEdit.Details,
                DateAndTime = seminarToEdit.DateAndTime,
                Duration = seminarToEdit.Duration,
                CategoryId = seminarToEdit.CategoryId,
                Categories = GetCategories()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, SeminarFormModel model)
        {
            var seminarToEdit = await _context.Seminars.FindAsync(id);
            string currentUserId = GetUserId();

            if (seminarToEdit == null)
            {
                return BadRequest();
            }

            if (currentUserId != seminarToEdit.OrganizerId)
            {
                return Unauthorized();
            }

            if (!GetCategories().Any(e => e.Id == model.CategoryId))
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Category does not exist!");
            }

            seminarToEdit.Topic = model.Topic;
            seminarToEdit.Lecturer = model.Lecturer;
            seminarToEdit.Details = model.Details;
            seminarToEdit.DateAndTime = model.DateAndTime;
            seminarToEdit.Duration = model.Duration;
            seminarToEdit.CategoryId = model.CategoryId;

            await _context.SaveChangesAsync();

            return RedirectToAction("All", "Seminar");
        }

        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            var seminarToAdd = await _context.Seminars.FindAsync(id);
            string currentUserId = GetUserId();

            if (seminarToAdd == null)
            {
                return BadRequest();
            }

            var entry = new SeminarParticipant()
            {
                ParticipantId = currentUserId,
                SeminarId = seminarToAdd.Id
            };

            if (await _context.SeminarsParticipants.ContainsAsync(entry))
            {
                return RedirectToAction("Joined", "Seminar");
            }

            await _context.SeminarsParticipants.AddAsync(entry);
            await _context.SaveChangesAsync();

            return RedirectToAction("Joined", "Seminar");
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            var seminarToRemove = await _context.Seminars.FindAsync(id);
            string currentUserId = GetUserId();

            if (seminarToRemove == null)
            {
                return BadRequest();
            }

            var entry = await _context.SeminarsParticipants.FirstOrDefaultAsync(p => p.ParticipantId == currentUserId && p.SeminarId == id);

            _context.SeminarsParticipants.Remove(entry);
            await _context.SaveChangesAsync();

            return RedirectToAction("Joined", "Seminar");
        }

        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            string currentUserId = GetUserId();

            var model = await _context.SeminarsParticipants
                .Where(p => p.ParticipantId == currentUserId)
                .Select(sp => new SeminarViewModel()
                {
                    Id = sp.Seminar.Id,
                    Topic = sp.Seminar.Topic,
                    Lecturer = sp.Seminar.Lecturer,
                    Category = sp.Seminar.Category.Name,
                    DateAndTime = sp.Seminar.DateAndTime.ToString(Format),
                    Organizer = sp.Seminar.Organizer.UserName
                })
                .ToListAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await _context.Seminars
                .Select(s => new SeminarViewModel()
                {
                    Id = s.Id,
                    Topic = s.Topic,
                    Lecturer = s.Lecturer,
                    Category = s.Category.Name,
                    DateAndTime = s.DateAndTime.ToString(Format),
                    Organizer = s.Organizer.UserName
                })
                .ToListAsync();

            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new SeminarFormModel()
            {
                Categories = GetCategories()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(SeminarFormModel model)
        {
            if (!GetCategories().Any(e => e.Id == model.CategoryId))
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Category does not exist!");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string currentUserId = GetUserId();

            var seminar = new Seminar()
            {
                Topic = model.Topic,
                Details = model.Details,
                Lecturer = model.Lecturer,
                DateAndTime = model.DateAndTime,
                Duration = model.Duration,
                CategoryId = model.CategoryId,
                OrganizerId = currentUserId
            };

            await _context.Seminars.AddAsync(seminar);
            await _context.SaveChangesAsync();

            return RedirectToAction("All", "Seminar");
        }

        private IEnumerable<CategoryViewModel> GetCategories()
        {
            return _context
                .Categories
                .Select(e => new CategoryViewModel()
                {
                    Id = e.Id,
                    Name = e.Name
                })
                .ToList();
        }
        private string GetUserId()
        {
           return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
