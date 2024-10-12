namespace ToutLeMondeVeutPrendreSaPlace.API.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<string> Options { get; set; }
        public string CorrectAnswer { get; set; }
        public int Difficulty { get; set; }
    }
}