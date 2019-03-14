using System;
using Microsoft.AspNetCore.Mvc;


namespace AspNetCore.Api
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    } 
    [Route("/api/news/{id}")]
    public class NewsController:Controller
    {
        [HttpGet]
        public ObjectResult Get(int id)
        {
            //var news = GetNews(id);
            var news = new News()
            {
                Id = id,
                Title = "Get news",
                Text = "News by id " + id
            };
            return Ok(news);
        }
        [HttpDelete] 
        public NoContentResult DeleteNews(int id) 
        {
            // Do something here to delete the news // ...
            return NoContent(); 
        }
        [HttpPost] 
        public CreatedResult AddNews([FromBody] News news) 
        {
            // Do something here to save the news
            var newsId = news.Id; //SaveNewsInSomeWay(news); 
            // Returns HTTP 201 and sets the URI to the Location header
            var relativePath = string.Format("/api/news/{0}", newsId); 
            return Created(relativePath, news); 
        } 
        [HttpPut] 
        public AcceptedResult UpdateNews([FromBody] News news) 
        { 
            // Do something here to update the news
            var news1 = new News() {Id = news.Id,Title = news.Text,Text = news.Title}; //UpdateNewsInSomeWay( id, title, content);
            var relativePath = string.Format("/api/news/{0}", news1.Id); 
            return Accepted(relativePath); 
        }
    }
}