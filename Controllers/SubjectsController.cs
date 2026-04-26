using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Student_Information_System.Data;
using Student_Information_System.Models;

namespace Student_Information_System.Controllers;

public class SubjectsController(ApplicationDbContext context) : Controller
{
    public async Task<IActionResult> Index() => View(await context.Subjects.Include(s => s.Instructor).AsNoTracking().ToListAsync());

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound();
        var subject = await context.Subjects.Include(s => s.Instructor).Include(s => s.Enrollments).ThenInclude(e => e.Student).FirstOrDefaultAsync(s => s.Id == id);
        return subject is null ? NotFound() : View(subject);
    }

    public IActionResult Create()
    {
        ViewData["InstructorId"] = new SelectList(context.Instructors, "Id", "FullName");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,InstructorId")] Subject subject)
    {
        if (!ModelState.IsValid)
        {
            ViewData["InstructorId"] = new SelectList(context.Instructors, "Id", "FullName", subject.InstructorId);
            return View(subject);
        }
        context.Add(subject);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();
        var subject = await context.Subjects.FindAsync(id);
        if (subject is null) return NotFound();
        ViewData["InstructorId"] = new SelectList(context.Instructors, "Id", "FullName", subject.InstructorId);
        return View(subject);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,InstructorId")] Subject subject)
    {
        if (id != subject.Id) return NotFound();
        if (!ModelState.IsValid)
        {
            ViewData["InstructorId"] = new SelectList(context.Instructors, "Id", "FullName", subject.InstructorId);
            return View(subject);
        }
        context.Update(subject);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();
        var subject = await context.Subjects.Include(s => s.Instructor).FirstOrDefaultAsync(m => m.Id == id);
        return subject is null ? NotFound() : View(subject);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var subject = await context.Subjects.FindAsync(id);
        if (subject is not null)
        {
            context.Subjects.Remove(subject);
            await context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
