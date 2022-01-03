using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using System.Threading.Tasks;

namespace SampleWebApp.Controllers
{
    public class QuestionsController : ApiController
    {
        private SampleDatabaseEntities _context;
        public QuestionsController(SampleDatabaseEntities context)
        {
            _context = context;
        }
        // GET: api/Questions
        public IQueryable<Question> GetQuestions()
        {
           // SampleDatabaseEntities db = new SampleDatabaseEntities();
            return _context.Questions;
        }

        // GET: api/Questions/5
        [ResponseType(typeof(Question))]
        public async Task<IHttpActionResult> GetQuestion(long id)
        {
            SampleDatabaseEntities db = new SampleDatabaseEntities();
            Question question = await db.Questions.FirstOrDefaultAsync(a => a.QuestionID == id);
            if (question == null)
            {
                return NotFound();
            }

            return Ok(question);
        }

        [HttpGet]
        [ResponseType(typeof(Question))]
        public IHttpActionResult Search(string name)
        {
            SampleDatabaseEntities db = new SampleDatabaseEntities();
            var question = db.Questions.Where(a => a.QuestionDescription.Contains(name)).ToList();
            if (question == null)
            {
                return NotFound();
            }

            return Ok(question);
        }

        // PUT: api/Questions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutQuestion(long id, Question question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != question.QuestionID)
            {
                return BadRequest();
            }
            SampleDatabaseEntities db = new SampleDatabaseEntities();
            db.Entry(question).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(id))
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

        // POST: api/Questions
        [ResponseType(typeof(Question))]
        public IHttpActionResult PostQuestion(Question question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            SampleDatabaseEntities db = new SampleDatabaseEntities();
            db.Questions.Add(question);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (QuestionExists(question.QuestionID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = question.QuestionID }, question);
        }

        // DELETE: api/Questions/5
        [ResponseType(typeof(Question))]
        public IHttpActionResult DeleteQuestion(long id)
        {
            SampleDatabaseEntities db = new SampleDatabaseEntities();
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return NotFound();
            }

            db.Questions.Remove(question);
            db.SaveChanges();

            return Ok(question);
        }

        protected override void Dispose(bool disposing)
        {
            SampleDatabaseEntities db = new SampleDatabaseEntities();
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool QuestionExists(long id)
        {
            SampleDatabaseEntities db = new SampleDatabaseEntities();
            return db.Questions.Count(e => e.QuestionID == id) > 0;
        }

        public async Task<List<Question>> GetAllQuestionsAsync()
        {
            var query = from b in _context.Questions
                        orderby b.QuestionID
                        select b;

            return await query.ToListAsync();
        }
    }
}