using System.ComponentModel.DataAnnotations.Schema;

namespace ESTA.Models
{
    public class Course
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public string PaymentLink  { get; set; }

        public virtual Level level { get; set; }
        public int Price { get; set; }

        //[ForeignKey("TypeIdFornKey")]
        //public int TypeId { get; set; }

        public DateTime StartDate { get; set; }

        public int FinalGrade { get; set; }
    }

}
