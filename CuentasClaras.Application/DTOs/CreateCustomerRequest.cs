namespace CuentasClaras.Application.DTOs
{
    public class CreateCustomerRequest
    {
        public string DNI { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address1 { get; set; } = string.Empty;
        public string Address2 { get; set; } = string.Empty;
        public string Phone1 { get; set; } = string.Empty;
        public string Phone2 { get; set; } = string.Empty;
        public string Phone3 { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int LocalityId { get; set; }
        public int CollectorId { get; set; }
        public int SalesmanId { get; set; }
    }
}
