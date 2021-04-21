using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuestBook.Controllers
{
    public class CommentController:Controller
    {
        private readonly GuestBookDBContext _db;
        private readonly IPublishEndpoint publishEndpoint;
        public CommentController(IPublishEndpoint publishEndpoint, GuestBookDBContext db)
        {
            this.publishEndpoint = publishEndpoint;
            _db = db;
        }
        [HttpGet]
        [Route("/comment")]
        public IActionResult GET()
        {
            var model = _db.Comments;
            return Ok(model);
        }
        
        [Route("/comment")]
        [HttpPost]
        public async Task<IActionResult> POST([FromBody]  Comment model)//add comments rabbitmq
        {
            await publishEndpoint.Publish<Comment>(model);
            return Ok(model);
        }
        [HttpPut]
        [Route("/comment")]
        public IActionResult PUT(Comment model)
        {
            var comment = _db.Comments.Find(model.Id);
            comment.Name = model.Name;
            comment.Email = model.Email;
            comment.Message = model.Message;
            _db.SaveChanges();
            return Ok(model);
        }
        [HttpDelete]
        [Route("/comment")]
        public IActionResult DELETE(Comment model)
        {
            var comment = _db.Comments.Find(model.Id);
            _db.Comments.Remove(comment);
            _db.SaveChanges();
            return Ok(model);
        }
    }
}
