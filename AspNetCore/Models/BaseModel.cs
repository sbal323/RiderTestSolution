using System;

namespace AspNetCore.Models
{
    public interface IBaseModel
    {
        string CopyRight { get; }
        string Title { get; }
    }
    public class BaseModel: IBaseModel
    {
        public string Title { get; }

        string IBaseModel.Title => this.Title;

        string IBaseModel.CopyRight => $"&copy; {DateTime.Today.Year} - AspNetCore";

        public BaseModel(string title)
        {
            Title = title;
        }
    }
}