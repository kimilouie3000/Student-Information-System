using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Student_Information_System.Data;
using Student_Information_System.Models;

namespace Student_Information_System.Controllers;

public class CoursesController(ApplicationDbContext context) : Controller
{
    public async Task<IActionResult> Index()
    {
        var courses = context.Courses.Include(c => c.Department);
        return View(await courses.AsNoTracking().ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound();
        var course = await context.Courses.Include(c => c.Department).Include(c => c.Students).FirstOrDefaultAsync(m => m.Id == id);
        return course is null ? NotFound() : View(course);
    }

    public IActionResult Create()
    {
        ViewData["DepartmentId"] = new SelectList(context.Departments, "Id", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,DurationInMonths,DepartmentId")] Course course)
    {
        if (!ModelState.IsValid)
        {
            ViewData["DepartmentId"] = new SelectList(context.Departments, "Id", "Name", course.DepartmentId);
            return View(course);
        }
        context.Add(course);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();
        var course = await context.Courses.FindAsync(id);
        if (course is null) return NotFound();
        ViewData["DepartmentId"] = new SelectList(context.Departments, "Id", "Name", course.DepartmentId);
        return View(course);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DurationInMonths,DepartmentId")] Course course)
    {
        if (id != course.Id) return NotFound();
        if (!ModelState.IsValid)
        {
            ViewData["DepartmentId"] = new SelectList(context.Departments, "Id", "Name", course.DepartmentId);
            return View(course);
        }
        context.Update(course);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();
        var course = await context.Courses.Include(c => c.Department).FirstOrDefaultAsync(m => m.Id == id);
        return course is null ? NotFound() : View(course);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var course = await context.Courses.FindAsync(id);
        if (course is not null)
        {
            context.Courses.Remove(course);
            await context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
