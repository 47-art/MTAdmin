using System;
using System.Collections.Generic;

namespace MTAdmin.Shared.Models
{
    public abstract class PagedBase
    {
        public int CurrentPage { get; protected set; }
        public int TotalPages { get; protected set; }
        public int PageSize { get; protected set; }
        public int TotalCount { get; protected set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
    }
    public class PagedList<T> : PagedBase
    {
        public IReadOnlyList<T> Items { get; set; }
        public PagedList(IReadOnlyList<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Items = items;
        }
    }
}
