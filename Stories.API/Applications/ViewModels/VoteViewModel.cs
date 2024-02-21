namespace Stories.API.Applications.ViewModels
{
    public class VoteViewModel
    {
        public int Id { get; set; }
        public int IdStory { get; set; }
        public int IdUser { get; set; }
        public bool VoteValue { get; set; } // true para voto positivo, false para negativo
    }
}
