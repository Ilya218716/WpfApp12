using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApplication2.Context;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Microsoft.AspNetCore.Components.Route("api/[controller")]
    [ApiController]
    public class StudentsController
    {
        private readonly CrudContext _CrudContext;
        public StudentsController(CrudContext crudContext )
        {
            _CrudContext = crudContext;
        }
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            return _CrudContext.Students;
        }
        [HttpGet("{id}", Name ="Get")]
        public Student Get(int id)
        {
            return _CrudContext.Students.SingleOrDefault(x => x.StudentId == id);
        }
        [HttpPost]
        public void Post([FromBody] Student student)
        {
            _CrudContext.Students.Add(student);
            _CrudContext.SaveChanges();
        }
        [HttpPut("{id}")]
        public void Put([FromBody] Student student)
        {
            _CrudContext.Students.Update(student);
            _CrudContext.SaveChanges();
        }
        

        [Authorize(Roles = "admin")]
        public void Delete(int id)
        {
            var item = _CrudContext.Students.FirstOrDefault(x => x.StudentId == id);
            if (item != null)
            {
                _CrudContext.Students.Remove(item);
                _CrudContext.SaveChanges();
            }
        }
    }
}
