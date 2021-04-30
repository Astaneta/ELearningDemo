using System.Collections.Generic;
using ELearningDemo.Models.InputModels;

namespace ElearningDemo.Models.ViewModels
{
    public class CourseListViewModel : IPaginationInfo
    {
        public ListViewModel<CoursesViewModel> Course { get; set; }
        public CoursesListInputModel Input { get; set; }


        #region Implementazione IPaginationInfo
        int IPaginationInfo.CurrentPage => Input.Page;

        int IPaginationInfo.TotalResult => Course.TotalCount;

        int IPaginationInfo.ResultPerPage => Input.Limit;

        string IPaginationInfo.Search => Input.Search;

        string IPaginationInfo.OrderBy => Input.OrderBy;

        bool IPaginationInfo.Ascending => Input.Ascending;
        #endregion
    }
}