namespace MediTechBackendAPI.Models
{
    public class Dependent
    {
        public System.Guid? Dependent_ID { get; set; }
        public System.Guid PID { get; set; }
    public string? PatientID { get; set; }
    public string? First_Name { get; set; }
    public string? Middle_Name { get; set; }
    public string? Last_Name { get; set; }
        public int Age { get; set; }
        public int RelationshipID { get; set; }
    public string? Created_by { get; set; }
        public System.DateTime? D_Created_At { get; set; }
    public string? Modified_by { get; set; }
    public System.DateTime? D_Modified_At { get; set; }
    public string? ImageUrl { get; set; }
    }
}
