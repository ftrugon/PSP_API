using BookStoreApi.Models;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers;

[ApiController]
[Route("/puntuacion")]
public class PuntuacionController : ControllerBase
{
    private readonly PuntuacionService _PuntuacionService;

    public PuntuacionController(PuntuacionService puntuacionService) =>
        _PuntuacionService = puntuacionService;

    [HttpGet]
    public async Task<List<Puntuacion>> Get() =>
        await _PuntuacionService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Puntuacion>> Get(string id)
    {
        var puntuacion = await _PuntuacionService.GetAsync(id);

        if (puntuacion is null)
        {
            return NotFound();
        }

        return puntuacion;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Puntuacion newPuntuacion)
    {
        await _PuntuacionService.CreateAsync(newPuntuacion);

        return CreatedAtAction(nameof(Get), new { id = newPuntuacion.Id }, newPuntuacion);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Puntuacion updatedPuntuacion)
    {
        var puntuacion = await _PuntuacionService.GetAsync(id);

        if (puntuacion is null)
        {
            return NotFound();
        }

        updatedPuntuacion.Id = puntuacion.Id;

        await _PuntuacionService.UpdateAsync(id, updatedPuntuacion);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var book = await _PuntuacionService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        await _PuntuacionService.RemoveAsync(id);

        return NoContent();
    }
}