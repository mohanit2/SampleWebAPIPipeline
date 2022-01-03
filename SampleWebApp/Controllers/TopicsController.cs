using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNetCore.Mvc.Formatters;
using SampleWebApp;

namespace SampleWebApp.Controllers
{
    public class TopicsController : ApiController
    {
        private SampleDatabaseEntities _context;
        public TopicsController(SampleDatabaseEntities context)
        {
            _context = context;
        }

        // GET: api/Topics
        public IQueryable<Topic> GetTopics()
        {
            return _context.Topics;
        }

        // GET: api/Topics/5
        [ResponseType(typeof(Topic))]
        public IHttpActionResult GetTopic(long id)
        {
            Topic topic = _context.Topics.Find(id);
            if (topic == null)
            {
                return NotFound();
            }

            return Ok(topic);
        }


        [HttpGet]
        [ResponseType(typeof(Question))]
        public IHttpActionResult Search(string name)
        {
            var question = _context.Questions.Where(a => a.QuestionDescription.Contains(name)).ToList();
            if (question == null)
            {
                return NotFound();
            }

            return Ok(question);
        }
        // PUT: api/Topics/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTopic(long id, Topic topic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != topic.TopicID)
            {
                return BadRequest();
            }

            _context.Entry(topic).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TopicExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Topics
        [ResponseType(typeof(Topic))]
        public IHttpActionResult PostTopic(Topic topic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Topics.Add(topic);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TopicExists(topic.TopicID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = topic.TopicID }, topic);
        }

        // DELETE: api/Topics/5
        [ResponseType(typeof(Topic))]
        public IHttpActionResult DeleteTopic(long id)
        {
            Topic topic = _context.Topics.Find(id);
            if (topic == null)
            {
                return NotFound();
            }

            _context.Topics.Remove(topic);
            _context.SaveChanges();

            return Ok(topic);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TopicExists(long id)
        {
            return _context.Topics.Count(e => e.TopicID == id) > 0;
        }
    }
}