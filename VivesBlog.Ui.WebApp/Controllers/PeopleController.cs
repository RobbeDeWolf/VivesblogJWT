
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VivesBlog.Model;
using VivesBlog.Sdk;

namespace VivesBlog.Ui.WebApp.Controllers
{
	public class PeopleController : Controller
	{
		private readonly PersonSdk _personSdk;

		public PeopleController(PersonSdk personSdk)
		{
            _personSdk = personSdk;
		}

		public async Task<IActionResult> Index()
        {
            var people = await _personSdk.FindAsync();
			return View(people);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Person person)
		{
			if (!ModelState.IsValid)
			{
				return View(person);
			}

            await _personSdk.CreateAsync(person);

			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
        {
            var person = await _personSdk.GetAsync(id);

			if (person is null)
			{
				return RedirectToAction("Index");
			}

			return View(person);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, Person person)
		{
			if (!ModelState.IsValid)
			{
				return View(person);
			}

            var dbPerson = await _personSdk.GetAsync(id);

			if (dbPerson is null)
			{
				return RedirectToAction("Index");
			}

            await _personSdk.UpdateAsync(id, person);

			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
        {
            var person = await _personSdk.GetAsync(id);

			if (person is null)
			{
				return RedirectToAction("Index");
			}

			return View(person);
		}

		[HttpPost]
		[Route("People/Delete/{id}")]
		public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dbPerson = await _personSdk.GetAsync(id);

			if (dbPerson is null)
			{
				return RedirectToAction("Index");
			}

			await _personSdk.DeleteAsync(id);

            return RedirectToAction("Index");
		}
	}
}
