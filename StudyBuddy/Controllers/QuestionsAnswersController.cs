using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyBuddy.Models;

namespace StudyBuddy.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class QuestionsAnswersController : ControllerBase
    {
        StudyBuddyDBContext dBContext = new StudyBuddyDBContext();

        // GET: QuestionsAnswers/user/{userId}
        [HttpGet("user/{userId}")]
        public List<QuestionsAndAnswer> GetQuestionsByUser(string userId)
        {
            return dBContext.QuestionsAndAnswers.Where(q => q.UserId == userId).ToList();
        }

        // GET: QuestionsAnswers/examples
        [HttpGet("examples")]
        public List<QuestionsAndAnswer> GetExampleQuestons()
        {
            return dBContext.QuestionsAndAnswers.Where(q => q.UserId == null).ToList();
        }

        // GET: QuestionsAnswers/5
        [HttpGet("{id}")]
        public QuestionsAndAnswer GetById(int id)
        {
            return dBContext.QuestionsAndAnswers.Find(id);
        }

        // POST: QuestionsAnswers
        [HttpPost]
        public IActionResult PostQuestion([FromBody] QuestionsAndAnswer question)
        {
            // if (string.IsNullOrEmpty(question.UserId))
            // {
            //     return BadRequest("User ID is required");
            // }

            dBContext.QuestionsAndAnswers.Add(question);
            if (question.UserId != null)
            {
                dBContext.SaveChanges();
            }
            
            return Ok(question);
        }

        // PUT: QuestionsAnswers/5
        [HttpPut("{id}")]
        public IActionResult PutQuestion(int id, [FromBody] QuestionsAndAnswer question)
        {
            QuestionsAndAnswer q = dBContext.QuestionsAndAnswers.Find(id);
            if (q == null)
            {
                return NotFound();
            }

            q.Questions = question.Questions;
            q.Answers = question.Answers;
            q.UserId = question.UserId;
            dBContext.QuestionsAndAnswers.Update(q);
            dBContext.SaveChanges();
            return Ok(q);
        }

        // DELETE: QuestionsAnswers/5
        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            QuestionsAndAnswer deleted = dBContext.QuestionsAndAnswers.Find(id);
            // Delete related favorites in the Favorites table
            if(deleted != null)
            {
                IEnumerable<Favorite> relatedFavorites = dBContext.Favorites.Where(f => f.QuestionId == id);
                dBContext.Favorites.RemoveRange(relatedFavorites);
            }
            dBContext.QuestionsAndAnswers.Remove(deleted);
            if (deleted != null && deleted.UserId != null)
            {
                dBContext.SaveChanges();
            }
            return Ok(deleted);
        }
    }
}
