namespace NetCoreApiScaffolding.Application.Common.Request
{
    public interface IPaginatedRequest
    {
        int Skip { get; set; }
        int Take { get; set; }
    }
}
