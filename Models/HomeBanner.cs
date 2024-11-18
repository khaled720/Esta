namespace ESTA.Models
{
    public class HomeBanner
    {
        public int Id { get; set; }
        public int Type { get; set; }//1->banner 1, 2->banner 2, 3-> banner 3, 4-> banner 4, 5-> banner 5,6->banner 6
        public string FilePath { get; set; }
        public string SloganAr { get; set; }
        public string SloganEn { get; set; }
        public string DetailsAr { get; set; }
        public string DetailsEn { get; set; }
    }
}
