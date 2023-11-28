using ESTA.Models;

namespace ESTA.ViewModels
{
    public class HomeSearchViewModel
    {



        public List<SearchResult> Results { get; set; } = new List<SearchResult>();


    }

}



namespace ESTA.Models
{
   public  class SearchResult
    {
        public int Type { get; set; }
        public string Description { get; set; }
        public string Header { get; set; }
        public int Id { get; set; }   // id of item found weather it is event or course


    }

}