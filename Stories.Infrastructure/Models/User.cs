﻿namespace Stories.Infrastructure.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Vote> Votes { get; set; } = new List<Vote>();
        public int PositiveVotesCount { get; set; }
        public int NegativeVotesCount { get; set; }
    }
}

#region
//namespace Stories.Infrastructure.Models
//{
//    public class User
//    {
//        public int Id { get; set; }
//        public string Name { get; set; }
//        public List<Vote> Votes { get; set; } = new List<Vote>();

//    }
//}
#endregion