namespace ToutLeMondeVeutPrendreSaPlace.API.Models
{
    public class Game
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public GameStatus Status { get; set; }
        public List<GamePlayer> Players { get; set; }
    }

    public enum GameStatus
    {
        Waiting,
        InProgress,
        Finished
    }
}