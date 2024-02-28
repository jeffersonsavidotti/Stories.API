namespace Stories.Infrastructure.Models
{
    public class Story
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Department { get; set; }
        public List<Vote> Votes { get; set; } = new List<Vote>();
        public int PositiveVotesCount { get; set; }
        public int NegativeVotesCount { get; set; }
    }
}


#region 
//namespace Stories.Infrastructure.Models
//{
//    public class Story
//    {
//        public int Id { get; set; }
//        public string Title { get; set; }
//        public string Description { get; set; }
//        public string Department { get; set; }
//        public List<Vote> Votes { get; set; } = new List<Vote>();
//    }
//}
#endregion