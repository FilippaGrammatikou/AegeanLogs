using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace AegeanLogs.Application.PortCalls.Create;
public sealed class CreatePortCallValidator : AbstractValidator<CreatePortCallRequest>
{
    public CreatePortCallValidator()
    {
        RuleFor(request => request.VesselId).GreaterThan(0).WithMessage("VesselId must be greater than zero.");
        RuleFor(request => request.PortId).GreaterThan(0).WithMessage("PortId must be greater than zero.");
        RuleFor(request => request.AssignedAgentUserId).GreaterThan(0).WithMessage("AssignedAgentUserId must be greater than zero.");
        RuleFor(request => request.Purpose).IsInEnum().WithMessage("Purpose must be a valid port-call purpose.");
        RuleFor(request => request.Eta).NotEmpty().WithMessage("ETA is required.");
        RuleFor(request => request.Etd).NotEmpty().WithMessage("ETD is required.");
        RuleFor(request=>request.Etd).GreaterThan(request=>request.Eta).WithMessage("ETD must be later than ETA.");
        RuleFor(request => request.Notes).MaximumLength(1000).WithMessage("Notes cannot exceed 1000 characters.");
    }
}
