using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Point.Delete;

internal sealed class DeleteHandler : IRequestHandler<DeleteCommand, Result<Guid>>
{
    private readonly IPointData _pointData;
    private readonly IValidator<DeleteCommand> _validator;

    public DeleteHandler(IPointData pointData, IValidator<DeleteCommand> validator)
    {
        _pointData = pointData;
        _validator = validator;
    }

    public async Task<Result<Guid>> Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return new Result<Guid>(Guid.Empty,
                Errors.ReturnInvalidEntriesError(validationResult.ToString()));
        }

        var pointEntity = await _pointData.GetByIdAsync(request.Id, cancellationToken);

        if (pointEntity is null)
        {
            return new Result<Guid>(Guid.Empty, Errors.ReturnPointNotFoundError());
        }

        await _pointData.DeleteAsync(pointEntity, cancellationToken);

        return new Result<Guid>(request.Id);
    }
}