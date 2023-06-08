
using Webshop.Payment.Api.Models;
using Webshop.Domain.Common;

namespace Webshop.Payment.Api.Repository
{
    public interface IMemoryRepository
    {
        Result AddTransaction(Transaction transaction);
    }
}
