using dotNet_backend.CustomActionFilters;

namespace dotNet_backend.Helpers.PaginationFilter;

[ValidatePozitiveNumber]
public class PaginationFilter
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}