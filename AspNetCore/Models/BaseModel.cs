using System;

namespace AspNetCore.Models
{
    public interface ILayoutModel
    {
        string CopyRight { get; }
        string Title { get; }
        string ErrorMessage { get; }
        string SuccessMessage { get; }
        bool IsError();
    }
    public class BaseModel: ILayoutModel
    {
        public string Title { get; }
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
        bool ILayoutModel.IsError()
        {
            return ErrorMessage != String.Empty;
        }
        string ILayoutModel.Title => this.Title;
        string ILayoutModel.ErrorMessage => this.ErrorMessage;
        string ILayoutModel.SuccessMessage => this.SuccessMessage;
        string ILayoutModel.CopyRight => $"&copy; {DateTime.Today.Year} - AspNetCore";

        public BaseModel(string title)
        {
            Title = title;
            ErrorMessage = string.Empty;
        }
    }
}