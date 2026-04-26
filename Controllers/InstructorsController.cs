using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Student_Information_System.Data;
using Student_Information_System.Models;

namespace Student_Information_System.Controllers;

public class InstructorsController(ApplicationDbContext context) : Controller
{
    public async Task<IActionResult> Index() => View(await context.Instructors.Include(i => i.Department).AsNoTracking().ToListAsync());

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound();
        var instructor = await context.Instructors.Include(i => i.Department).Include(i => i.Subjects).FirstOrDefaultAsync(i => i.Id == id);
        return instructor is null ? NotFound() : View(instructor);
    }

    public IActionResult Create()
    {
        ViewData["DepartmentId"] = new SelectList(context.Departments, "Id", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,FullName,Email,DepartmentId")] Instructor instructor)
    {
        if (!ModelState.IsValid)
        {
            ViewData["DepartmentId"] = new SelectList(context.Departments, "Id", "Name", instructor.DepartmentId);
            return View(instructor);
        }
        context.Add(instructor);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();
        var instructor = await context.Instructors.FindAsync(id);
        if (instructor is null) return NotFound();
        ViewData["DepartmentId"] = new SelectList(context.Departments, "Id", "Name", instructor.DepartmentId);
        return View(instructor);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Email,DepartmentId")] Instructor instructor)
    {
        if (id != instructor.Id) return NotFound();
        if (!ModelState.IsValid)
        {
            ViewData["DepartmentId"] = new SelectList(context.Departments, "Id", "Name", instructor.DepartmentId);
            return View(instructor);
        }
        context.Update(instructor);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();
        var instructor = await context.Instructors.Include(i => i.Department).FirstOrDefaultAsync(i => i.Id == id);
        return instructor is null ? NotFound() : View(instructor);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var instructor = await context.Instructors.FindAsync(id);
        if (instructor is not null)
        {
            context.Instructors.Remove(instructor);
            await context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
