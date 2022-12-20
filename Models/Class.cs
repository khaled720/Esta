namespace ESTA.Models
{
    public interface Irep
    {

        Icourse Course { get; set; }
    }
    public interface Icourse { }

    class SqlRep : Irep
    {
        public Icourse Course { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    class courseRep : Icourse { public int MyProperty { get; set; } }
}
