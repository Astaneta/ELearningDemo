namespace ElearningDemo.Models.ViewModels
{
    public interface IPaginationInfo
    {
        int CurrentPage { get; }
        int TotalResult { get; }
        int ResultPerPage { get; }
        string Search { get; }
        string OrderBy { get; }
        bool Ascending { get; }
    }
}