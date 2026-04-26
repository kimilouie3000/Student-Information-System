using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Information_System.Data;
using Student_Information_System.Models;

namespace Student_Information_System.Controllers;

public class DepartmentsController(ApplicationDbContext context) : Controller
{
    public async Task<IActionResult> Index() => View(await context.Departments.AsNoTracking().ToListAsync());

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound();
        var department = await context.Departments
            .Include(d => d.Courses)
            .Include(d => d.Instructors)
            .FirstOrDefaultAsync(m => m.Id == id);
        return department is null ? NotFound() : View(department);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Description")] Department department)
    {
        if (!ModelState.IsValid) return View(department);
        context.Add(department);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();
        var department = await context.Departments.FindAsync(id);
        return department is null ? NotFound() : View(department);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Department department)
    {
        if (id != department.Id) return NotFound();
        if (!ModelState.IsValid) return View(department);
        context.Update(department);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();
        var department = await context.Departments.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
        return department is null ? NotFound() : View(department);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var department = await context.Departments.FindAsync(id);
        if (department is not null)
        {
            context.Departments.Remove(department);
            await context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
