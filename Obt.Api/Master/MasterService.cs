using Obt.Api.Booking;
using Obt.Api.Master.Dto;
using Obt.Api.Security;
using Obt.Api.Store;

namespace Obt.Api.Master;

public class MasterService
{
    private readonly InMemoryStore _store;

    public MasterService(InMemoryStore store)
    {
        _store = store;
    }

    public IReadOnlyList<CustomerDto> ListCustomers() =>
        _store.Customers.Select(c => new CustomerDto(c.SapId, c.Name)).ToList();

    public IReadOnlyList<ContractDto> ListContracts(AuthUserPrincipal user) =>
        _store.Contracts
            .Where(c => !SalesScope.IsSalesScoped(user) || user.Id == c.SalesUserId)
            .Select(c => new ContractDto(
                c.ContractRef,
                c.SoldTo,
                c.SalesUserId,
                c.SapRarRef,
                BookingRules.HasSapRar(c.SapRarRef)))
            .ToList();
}
