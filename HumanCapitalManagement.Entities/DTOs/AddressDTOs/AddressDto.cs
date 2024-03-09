namespace HumanCapitalManagement.Entities.DTOs.AddressDTOs;
public class AddressDto
{
    public string StreetName { get; set; } = string.Empty;
    public string StreetNumber { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string? Building{ get; set; }
    public string? Flat { get; set; } 
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string? OtherDetails { get; set; }
}
