﻿namespace BrainStormerBackend.Models.Entities
{
    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ProjectDescription { get; set; }

        public bool Visibility { get; set; }

        public List<Issue> Issues { get; set; }

    }
}
