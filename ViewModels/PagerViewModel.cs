namespace ESTA.ViewModels
{
    public class PagerViewModel<T>
    {
        
        public void Update(List<T> ListTobePaginated/*, int currentPage = 1, int pageSize = 5*/)
        {
            double totalpages = (double) ListTobePaginated.Count() / (double)PageSize;
            TotalPages = (int)Math.Ceiling(totalpages);
            if (CurrentPage>TotalPages||CurrentPage<1)
            {
                CurrentPage = 1;
            }
            //CurrentPage = currentPage;
            //PageSize = pageSize;
            this.PaginatedList = ListTobePaginated
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToList<T>();
          //  FullList = PaginatedList;
        }

        public int TotalPages { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } =8;
        public int lengthOfFullList { get; set; }
        public List<T> PaginatedList { get; set; }

 //       public List<T> FullList { get; set; }

    }
}
