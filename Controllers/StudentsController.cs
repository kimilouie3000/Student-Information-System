using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Student_Information_System.Data;
using Student_Information_System.Models;

namespace Student_Information_System.Controllers;

public class StudentsController(ApplicationDbContext context) : Controller
{
    public async Task<IActionResult> Index()
    {
        var students = context.Students
            .Include(s => s.Course)
            .Include(s => s.Enrollments)
            .ThenInclude(e => e.Subject);
        return View(await students.AsNoTracking().ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound();
        var student = await context.Students
            .Include(s => s.Course)
            .Include(s => s.Enrollments)
            .ThenInclude(e => e.Subject)
            .FirstOrDefaultAsync(m => m.Id == id);

        return student is null ? NotFound() : View(student);
    }

    public IActionResult Create()
    {
        ViewData["CourseId"] = new SelectList(context.Courses, "Id", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,FullName,Email,DateOfBirth,CourseId")] Student student)
    {
        if (!ModelState.IsValid)
        {
            ViewData["CourseId"] = new SelectList(context.Courses, "Id", "Name", student.CourseId);
            return View(student);
        }
        context.Add(student);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();
        var student = await context.Students.FindAsync(id);
        if (student is null) return NotFound();
        ViewData["CourseId"] = new SelectList(context.Courses, "Id", "Name", student.CourseId);
        return View(student);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Email,DateOfBirth,CourseId")] Student student)
    {
        if (id != student.Id) return NotFound();
        if (!ModelState.IsValid)
        {
            ViewData["CourseId"] = new SelectList(context.Courses, "Id", "Name", student.CourseId);
            return View(student);
        }
        context.Update(student);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();
        var student = await context.Students.Include(s => s.Course).FirstOrDefaultAsync(m => m.Id == id);
        return student is null ? NotFound() : View(student);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var student = await context.Students.FindAsync(id);
        if (student is not null)
        {
            context.Students.Remove(student);
            await context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
