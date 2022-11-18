using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DiNezioCamiletti.Data;
using System.Collections.Generic;
using DiNezioCamiletti.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DiNezioCamiletti.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoController : ControllerBase
    {
        public  DBAutoContext context { get; set; }
        public AutoController(DBAutoContext context) 
        {
            this.context = context;
        }

        //GET-api/Auto
        [HttpGet]
        public List<Auto> Get() => (from a in context.Autos select a).ToList();

        //GET-api/Auto/modelo
        [HttpGet("modelo={modelo}")]
        public ActionResult<Auto> Get(string modelo)
        {
           var auto = (from a in context.Autos where a.Modelo.ToUpper() == modelo.ToUpper() select a).SingleOrDefault();
            if (auto == null) return NotFound();
            return auto;
        }

        //GET-api/Auto/id
        [HttpGet("{id}")]
        public ActionResult<Auto> Get(int id)
        {
            var auto = (from a in context.Autos where a.AutoId == id select a).SingleOrDefault();
            if (auto == null) return NotFound();
            return auto;
        }

        //GET-api/Auto/marca/modelo
        [HttpGet("{marca}/{modelo}")]
        public List<Auto> Get(string marca,string modelo)
        {
            var auto = (from a in context.Autos where a.Marca.ToUpper() == marca && a.Modelo.ToUpper() == modelo select a).ToList();
            return auto;
        }

        //GET-api/Auto/color
        [HttpGet("color={color}")]
        public List<Auto> GetXColor(string color) => (from a in context.Autos where a.Color.ToUpper() == color.ToUpper() select a).ToList();


        //POST-api/Auto
        [HttpPost]
        public ActionResult Post([FromBody]Auto auto)
        {
            context.Autos.Add(auto);
            context.SaveChanges();
            return Ok();
        }

        //PUT-api/Auto
        [HttpPut("{id}")]
        public ActionResult<Auto> Put(int id, [FromBody] Auto auto)
        {
            if (id != auto.AutoId) return BadRequest();
            context.Entry(auto).State=EntityState.Modified;
            context.SaveChanges();
            return NoContent();
        }

        //DELETE-api/Auto
        [HttpDelete("{id}")]
        public ActionResult<Auto> Delete(int id)
        {
            var aEliminar = context.Autos.Find(id);
            if(aEliminar == null) return NotFound();
            context.Remove(aEliminar);
            context.SaveChanges();
            return aEliminar;
        }

    }
}
