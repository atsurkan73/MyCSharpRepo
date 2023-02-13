using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Net;
using System.Numerics;

namespace WebApplicationProject.Data;

public class ServiceProfile
{
    [Key]
    public int ProfileId { get; set; }
    public int UserId { get; set; }
    public int? CostPerMonth { get; set; }
    public string? TariffPlan { get; set; }
    public string IPaddress { get; set; }
    public string? RealIPaddress { get; set; }
    public string? AddInfo { get; set; }


    public Customer Customer { get; set; }
    public TariffPlan Tariff_Plan { get; set; }

    public ServiceProfile() { }
    public ServiceProfile(int profileId, int userId, int costPerMonth, string tariffPlan, string ipAddress, string realIpAddress, string addInfo)
    {
        profileId = ProfileId;
        UserId = userId;
        CostPerMonth = costPerMonth;
        TariffPlan = tariffPlan;
        IPaddress = ipAddress;
        RealIPaddress = realIpAddress;
        AddInfo = AddInfo;
    }
}