using System.Text.Json;
using AegeanLogs.Application.Common.Exceptions;
using AegeanLogs.Application.Common.Persistence;
using AegeanLogs.Domain.Entities;
using AegeanLogs.Domain.Enums;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ApplicationValidationException =AegeanLogs.Application.Common.Exceptions.ValidationException;

//creation of portcall,validtion of the supplied info & verifying referenced db records are suitable
namespace AegeanLogs.Application.PortCalls.Create;
public sealed class CreatePortCallService
{
    private readonly IAegeanLogsDbContext _dbContext;
    private readonly ITransactionManager _transactionManager;
    private readonly IValidator<CreatePortCallRequest> _validator;

    public CreatePortCallService(
        IAegeanLogsDbContext dbContext,
        ITransactionManager transactionManager,
        IValidator<CreatePortCallRequest> validator)
    {
        _dbContext = dbContext;
        _transactionManager = transactionManager;
        _validator = validator;
    }

    public async Task<CreatePortCallResponse> CreateAsync(CreatePortCallRequest request,CancellationToken cancellationToken = default)
    {
        var validationResult =await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            var errorMessage = string.Join(" ",validationResult.Errors.Select(error => error.ErrorMessage));
            throw new ApplicationValidationException(errorMessage);
        }

        return await _transactionManager.ExecuteAsync(async transactionCancellationToken =>
            {
                var vessel = await _dbContext.Vessels.AsNoTracking().SingleOrDefaultAsync(vessel => vessel.Id == request.VesselId,transactionCancellationToken);
                if (vessel is null)
                {
                    throw new NotFoundException($"Vessel with ID {request.VesselId} was not found.");
                }
                if (!vessel.IsActive)
                {
                    throw new ConflictException($"Vessel with ID {request.VesselId} is inactive and cannot be used for a new port call.");
                }

                var port = await _dbContext.Ports.AsNoTracking().SingleOrDefaultAsync(port => port.Id == request.PortId,transactionCancellationToken);
                if (port is null)
                {
                    throw new NotFoundException($"Port with ID {request.PortId} was not found.");
                }
                if (!port.IsActive)
                {
                    throw new ConflictException($"Port with ID {request.PortId} is inactive and cannot be used for a new port call.");
                }

                var assignedAgent = await _dbContext.ApplicationUsers.AsNoTracking().SingleOrDefaultAsync(user =>user.Id == request.AssignedAgentUserId,transactionCancellationToken);
                if (assignedAgent is null)
                {
                    throw new NotFoundException($"User with ID {request.AssignedAgentUserId} was not found.");
                }
                if (!assignedAgent.IsActive)
                {
                    throw new ConflictException($"User with ID {request.AssignedAgentUserId} is inactive and cannot be assigned to a port call.");
                }
                if (assignedAgent.Role != UserRole.PortAgent)
                {
                    throw new ConflictException($"User with ID {request.AssignedAgentUserId} must have the PortAgent role.");
                }

                var createdAt = DateTimeOffset.UtcNow;
                var portCall = new PortCall
                {
                    VesselId = request.VesselId,
                    PortId = request.PortId,
                    AssignedAgentUserId =request.AssignedAgentUserId,
                    Purpose = request.Purpose,
                    Eta = request.Eta,
                    Etd = request.Etd,
                    Status = PortCallStatus.Expected,
                    Notes = request.Notes,
                    CreatedAt = createdAt
                };

                _dbContext.PortCalls.Add(portCall);
                await _dbContext.SaveChangesAsync(transactionCancellationToken);

                var auditLogEntry = new AuditLogEntry
                {
                    PortCallId = portCall.Id,
                    UserId = null,
                    ActionType =AuditActionType.PortCallCreated,
                    EntityName = nameof(PortCall),
                    EntityId = portCall.Id,
                    OldValue = null,
                    NewValue = JsonSerializer.Serialize(
                        new
                        {
                            portCall.VesselId,
                            portCall.PortId,
                            portCall.AssignedAgentUserId,
                            portCall.Purpose,
                            portCall.Eta,
                            portCall.Etd,
                            portCall.Status,
                            portCall.Notes
                        }),
                    Summary =$"Port call {portCall.Id} was created for vessel '{vessel.Name}' at port '{port.Name}'.",
                    CreatedAt = createdAt
                };

                _dbContext.AuditLogEntries.Add(auditLogEntry);
                await _dbContext.SaveChangesAsync(transactionCancellationToken);
                return new CreatePortCallResponse
                {
                    Id = portCall.Id,
                    VesselId = portCall.VesselId,
                    PortId = portCall.PortId,
                    Purpose = portCall.Purpose,
                    Eta = portCall.Eta,
                    Etd = portCall.Etd,
                    AssignedAgentUserId =portCall.AssignedAgentUserId,
                    Status = portCall.Status,
                    Notes = portCall.Notes,
                    CreatedAt = portCall.CreatedAt
                };
            },
            cancellationToken);
    }
}
