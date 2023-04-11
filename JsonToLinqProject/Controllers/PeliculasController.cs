using JsonToLinqProject.Models;
using JsonToLinqProject.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace JsonToLinqProject.Controllers
{
    public class PeliculasController : Controller
    {
        private RepositoryJSON repo;

        public PeliculasController(RepositoryJSON repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            List<Pelicula> peliculas = this.repo.GetPeliculas();
            return View(peliculas);
        }

        public IActionResult Details(int idpelicula)
        {
            Pelicula pelicula = this.repo.FindPelicula(idpelicula);
            return View(pelicula);
        }

        public IActionResult Delete(int idpelicula)
        {
            this.repo.DeletePelicula(idpelicula);
            return RedirectToAction("Index");
        }

        public IActionResult Update(int idpelicula)
        {
            Pelicula pelicula = this.repo.FindPelicula(idpelicula);
            return View(pelicula);
        }

        [HttpPost]
        public IActionResult Update(Pelicula pelicula)
        {
            this.repo.UpdatePelicula(pelicula.Id, pelicula.Titulo, pelicula.Director, pelicula.Fecha, pelicula.Duracion, pelicula.Genero, pelicula.Descripcion, pelicula.Poster, pelicula.Fondo);
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Pelicula pelicula)
        {
            this.repo.CreatePelicula(pelicula.Titulo, pelicula.Director, pelicula.Fecha, pelicula.Duracion, pelicula.Genero, pelicula.Descripcion, pelicula.Poster, pelicula.Fondo);
            return RedirectToAction("Index");
        }
    }
}
