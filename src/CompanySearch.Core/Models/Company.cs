using CompanySearch.Core.Enums;

namespace CompanySearch.Core.Models;

public record Company(
    string CompanyNumber,
    CompanyStatusEnum Status
);
