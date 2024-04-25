using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Circuit.GetAll;

[ExcludeFromCodeCoverage]
internal sealed class GetAllHandler : IRequestHandler<GetAllQuery, Result<IEnumerable<CircuitEntity>>>
{
    private readonly ICircuitData _circuitData;

    public GetAllHandler(ICircuitData circuitData)
    {
        _circuitData = circuitData;
    }

    public async Task<Result<IEnumerable<CircuitEntity>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        return new Result<IEnumerable<CircuitEntity>>(await _circuitData.GetAllCircuitsAsync(cancellationToken));
    }
}