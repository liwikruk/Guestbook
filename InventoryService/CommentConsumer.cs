using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model;
using System;
using System.Threading.Tasks;

namespace InventoryService
{
    internal class CommentConsumer : IConsumer<Comment> 
    {
        
        private readonly ILogger<CommentConsumer> logger;

        public CommentConsumer(ILogger<CommentConsumer> logger)
        {
            this.logger = logger;
        }
        public async Task Consume(ConsumeContext<Comment> context)
        {
            
            //await Console.Out.WriteLineAsync(context.Message.Name);
            //logger.LogInformation($"Got new message {context.Message.Name}");
            Comment comment = new Comment();          
            comment.Name = context.Message.Name;
            comment.Email = context.Message.Email;
            comment.Message = context.Message.Message;
            await using(GuestBookDBContext db = new GuestBookDBContext())
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                
            }
            
        }

    }
}