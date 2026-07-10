using AegeanLogs.Application.Common.Exceptions;
using AegeanLogs.Application.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AegeanLogs.Application.PortCalls.GetById;
public sealed class GetPortCallByIdService
{
    private readonly IAegeanLogsDbContext _dbContext;

    public GetPortCallByIdService(
        IAegeanLogsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetPortCallByIdResponse> GetByIdAsync(int id,CancellationToken cancellationToken = default)
    {
        var response = await _dbContext.PortCalls
            .AsNoTracking().Where(portCall => portCall.Id == id).Select(portCall => new GetPortCallByIdResponse
            {
                Id = portCall.Id,

                VesselId = portCall.VesselId,
                VesselName = portCall.Vessel.Name,
                VesselImoNumber = portCall.Vessel.ImoNumber,

                PortId = portCall.PortId,
                PortName = portCall.Port.Name,
                PortUnLocode = portCall.Port.UnLocode,

                AssignedAgentUserId =portCall.AssignedAgentUserId,
                AssignedAgentDisplayName =portCall.AssignedAgent.DisplayName,
                AssignedAgentEmail =portCall.AssignedAgent.Email,

                Purpose = portCall.Purpose,
                Eta = portCall.Eta,
                Etd = portCall.Etd,

                ActualArrivalTime =portCall.ActualArrivalTime,
                ActualDepartureTime =portCall.ActualDepartureTime,

                Status = portCall.Status,
                Notes = portCall.Notes,
                CreatedAt = portCall.CreatedAt,
                ClosedAt = portCall.ClosedAt
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (response is null)
        {
            throw new NotFoundException($"Port call with ID {id} was not found.");
        }

        return response;
    }
}
