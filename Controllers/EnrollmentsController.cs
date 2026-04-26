using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Student_Information_System.Data;
using Student_Information_System.Models;

namespace Student_Information_System.Controllers;

public class EnrollmentsController(ApplicationDbContext context) : Controller
{
    public async Task<IActionResult> Index() => View(await context.Enrollments.Include(e => e.Student).Include(e => e.Subject).AsNoTracking().ToListAsync());

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound();
        var enrollment = await context.Enrollments.Include(e => e.Student).Include(e => e.Subject).FirstOrDefaultAsync(e => e.Id == id);
        return enrollment is null ? NotFound() : View(enrollment);
    }

    public IActionResult Create()
    {
        ViewData["StudentId"] = new SelectList(context.Students, "Id", "FullName");
        ViewData["SubjectId"] = new SelectList(context.Subjects, "Id", "Name");
        return View(new Enrollment { EnrollmentDate = DateTime.UtcNow.Date });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,StudentId,SubjectId,EnrollmentDate,Grade")] Enrollment enrollment)
    {
        if (!ModelState.IsValid)
        {
            ViewData["StudentId"] = new SelectList(context.Students, "Id", "FullName", enrollment.StudentId);
            ViewData["SubjectId"] = new SelectList(context.Subjects, "Id", "Name", enrollment.SubjectId);
            return View(enrollment);
        }
        context.Add(enrollment);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();
        var enrollment = await context.Enrollments.FindAsync(id);
        if (enrollment is null) return NotFound();
        ViewData["StudentId"] = new SelectList(context.Students, "Id", "FullName", enrollment.StudentId);
        ViewData["SubjectId"] = new SelectList(context.Subjects, "Id", "Name", enrollment.SubjectId);
        return View(enrollment);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,SubjectId,EnrollmentDate,Grade")] Enrollment enrollment)
    {
        if (id != enrollment.Id) return NotFound();
        if (!ModelState.IsValid)
        {
            ViewData["StudentId"] = new SelectList(context.Students, "Id", "FullName", enrollment.StudentId);
            ViewData["SubjectId"] = new SelectList(context.Subjects, "Id", "Name", enrollment.SubjectId);
            return View(enrollment);
        }
        context.Update(enrollment);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();
        var enrollment = await context.Enrollments.Include(e => e.Student).Include(e => e.Subject).FirstOrDefaultAsync(m => m.Id == id);
        return enrollment is null ? NotFound() : View(enrollment);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var enrollment = await context.Enrollments.FindAsync(id);
        if (enrollment is not null)
        {
            context.Enrollments.Remove(enrollment);
            await context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
