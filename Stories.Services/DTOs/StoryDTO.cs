﻿using Stories.Infrastructure.Models;

namespace Stories.Services.DTOs
{
    public class StoryDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Department { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
    }

}
