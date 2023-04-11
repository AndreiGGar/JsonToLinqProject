using JsonToLinqProject.Helpers;
using JsonToLinqProject.Models;
using Newtonsoft.Json.Linq;

namespace JsonToLinqProject.Repositories
{
    public class RepositoryJSON
    {
        private HelperPathProvider helper;
        private string pathPelisyseries;

        public RepositoryJSON(HelperPathProvider helper)
        {
            this.helper = helper;
            this.pathPelisyseries = this.helper.MapPath("pelisyseries.json", Folders.Documents);
        }

        public List<Pelicula> GetPeliculas()
        {
            List<Pelicula> peliculas = new List<Pelicula>();
            string json = File.ReadAllText(this.pathPelisyseries);
            JObject jObject = JObject.Parse(json);
            JArray jArray = (JArray)jObject["peliculas"];
            foreach (JObject jPelicula in jArray)
            {
                Pelicula pelicula = new Pelicula();
                pelicula.Id = (int)jPelicula["id"];
                pelicula.Titulo = (string)jPelicula["titulo"];
                pelicula.Director = (string)jPelicula["director"];
                pelicula.Fecha = (int)jPelicula["fecha"];
                pelicula.Duracion = (int)jPelicula["duracion"];
                pelicula.Genero = (string)jPelicula["genero"];
                pelicula.Descripcion = (string)jPelicula["descripcion"];
                pelicula.Poster = (string)jPelicula["poster"];
                pelicula.Fondo = (string)jPelicula["fondo"];
                peliculas.Add(pelicula);
            }
            return peliculas;
        }

        public Pelicula FindPelicula(int idpelicula)
        {
            string json = File.ReadAllText(this.pathPelisyseries);
            JObject jObject = JObject.Parse(json);
            JArray jArray = (JArray)jObject["peliculas"];
            JObject jPelicula = (JObject)jArray.FirstOrDefault(p => (int)p["id"] == idpelicula);
            if (jPelicula != null)
            {
                Pelicula pelicula = new Pelicula();
                pelicula.Id = (int)jPelicula["id"];
                pelicula.Titulo = (string)jPelicula["titulo"];
                pelicula.Director = (string)jPelicula["director"];
                pelicula.Fecha = (int)jPelicula["fecha"];
                pelicula.Duracion = (int)jPelicula["duracion"];
                pelicula.Genero = (string)jPelicula["genero"];
                pelicula.Descripcion = (string)jPelicula["descripcion"];
                pelicula.Poster = (string)jPelicula["poster"];
                pelicula.Fondo = (string)jPelicula["fondo"];
                return pelicula;
            }
            else
            {
                return null;
            }
        }

        public void CreatePelicula(string titulo, string director, int fecha, int duracion, string genero, string descripcion, string poster, string fondo)
        {
            string json = File.ReadAllText(this.pathPelisyseries);
            JObject jObject = JObject.Parse(json);
            JArray jArray = (JArray)jObject["peliculas"];

            int maxId = jArray.Max(p => (int)p["id"]);
            int idpelicula = maxId + 1;

            JObject newJPelicula = new JObject(
                new JProperty("id", idpelicula),
                new JProperty("titulo", titulo),
                new JProperty("director", director),
                new JProperty("fecha", fecha),
                new JProperty("duracion", duracion),
                new JProperty("genero", genero),
                new JProperty("descripcion", descripcion),
                new JProperty("poster", poster),
                new JProperty("fondo", fondo)
            );

            jArray.Add(newJPelicula);
            jObject["peliculas"] = jArray;
            File.WriteAllText(this.pathPelisyseries, jObject.ToString());
        }

        public void UpdatePelicula(int idpelicula, string titulo, string director, int fecha, int duracion, string genero, string descripcion, string poster, string fondo)
        {
            string json = File.ReadAllText(this.pathPelisyseries);
            JObject jObject = JObject.Parse(json);
            JArray jArray = (JArray)jObject["peliculas"];

            int index = jArray.IndexOf(jArray.FirstOrDefault(p => (int)p["id"] == idpelicula));

            jArray[index]["titulo"] = titulo;
            jArray[index]["director"] = director;
            jArray[index]["fecha"] = fecha;
            jArray[index]["duracion"] = duracion;
            jArray[index]["genero"] = genero;
            jArray[index]["descripcion"] = descripcion;
            jArray[index]["poster"] = poster;
            jArray[index]["fondo"] = fondo;

            jObject["peliculas"] = jArray;
            File.WriteAllText(this.pathPelisyseries, jObject.ToString());
        }

        public void DeletePelicula(int id)
        {
            string json = File.ReadAllText(this.pathPelisyseries);
            JObject jObject = JObject.Parse(json);
            JArray jArray = (JArray)jObject["peliculas"];

            int index = jArray.IndexOf(jArray.FirstOrDefault(p => (int)p["id"] == id));

            jArray.RemoveAt(index);
            jObject["peliculas"] = jArray;
            File.WriteAllText(this.pathPelisyseries, jObject.ToString());
        }
    }
}
