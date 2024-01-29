using SPW.Admin.Api.Features.SpecialDate.DataAccess;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.SpecialDate.Delete;

internal sealed class DeleteHandler : IRequestHandler<DeleteCommand, Result<Guid>>
{
    private readonly ISpecialDateData _specialDateData;
    private readonly IValidator<DeleteCommand> _validator;

    public DeleteHandler(ISpecialDateData specialDateData, IValidator<DeleteCommand> validator)
    {
        _specialDateData = specialDateData;
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

        var userEntity = await _specialDateData.GetByIdAsync(request.Id, cancellationToken);

        if (userEntity is null)
        {
            return new Result<Guid>(Guid.Empty, Errors.ReturnSpecialDateNotFoundError());
        }

        await _specialDateData.DeleteAsync(userEntity, cancellationToken);

        return new Result<Guid>(request.Id);
    }
}