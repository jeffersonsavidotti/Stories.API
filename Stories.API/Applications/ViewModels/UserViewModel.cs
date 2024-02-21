namespace Stories.API.Applications.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PositiveVotesCount { get; set; }
        public int NegativeVotesCount { get; set; }
    }
}
