using System.Collections.Generic;
using ELearningDemo.Models.InputModels;

namespace ElearningDemo.Models.ViewModels
{
    public class CourseListViewModel : IPaginationInfo
    {
        public ListViewModel<CorsiViewModel> Corsi { get; set; }
        public CorsiListaInputModel Input { get; set; }


        #region Implementazione IPaginationInfo
        int IPaginationInfo.CurrentPage => Input.Page;

        int IPaginationInfo.TotalResult => Corsi.TotalCount;

        int IPaginationInfo.ResultPerPage => Input.Limit;

        string IPaginationInfo.Search => Input.Search;

        string IPaginationInfo.OrderBy => Input.OrderBy;

        bool IPaginationInfo.Ascending => Input.Ascending;
        #endregion
    }
}