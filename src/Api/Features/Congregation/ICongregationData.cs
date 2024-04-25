namespace SPW.Admin.Api.Features.Congregation;

internal interface ICongregationData
{
    Task<int> CreateCongregationAsync(CongregationEntity congregation, CancellationToken cancellationToken);

    Task<int> DeleteCongregationAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<CongregationEntity>> GetAllCongregationsAsync(CancellationToken cancellationToken);

    Task<CongregationEntity> GetCongregationByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<int> UpdateCongregationAsync(CongregationEntity congregation, CancellationToken cancellationToken);
}