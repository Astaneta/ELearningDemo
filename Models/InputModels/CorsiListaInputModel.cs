using System;
using System.Linq;
using ElearningDemo.Models.Options;
using ELearningDemo.Customization.ModelBinders;
using Microsoft.AspNetCore.Mvc;

namespace ELearningDemo.Models.InputModels
{
    [ModelBinder(BinderType = typeof(CorsiListaInputModelBinder))]
    public class CorsiListaInputModel
    {
        public CorsiListaInputModel(string search, int page, string orderBy, bool ascending, int limit, CoursesOrderOptions orderOption)   
        {

            if (!orderOption.Allow.Contains(orderBy))
            {
                orderBy = orderOption.By;
                ascending = orderOption.Ascending;
            }

            Search = search ?? "";
            Page = Math.Max(1, page);
            Limit = Math.Max(1, limit);
            OrderBy = orderBy;
            Ascending = ascending;
            Offset = (Page - 1) * limit;
        }
         public string Search { get; }
         public int Page { get; }
         public string OrderBy { get; }
         public bool Ascending { get; }
         public int Limit { get; }
         public int Offset { get; }
    }
}