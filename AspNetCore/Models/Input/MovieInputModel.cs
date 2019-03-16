using System;

namespace AspNetCore.Models.Input
{
    public enum FormOptions
    {
        None = 0,
        Add = 1,
        Save = 2,
        Delete = 3
    }
    public class MovieInputModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public FormOptions Action { get; set; }
    }
}