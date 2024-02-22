using CoreLayer.Core.Entities;
using CoreLayer.Entities;
using MediatR;

namespace StockService.Command
{
    public record StockEventOccuredCommand(Stock stock) : IRequest<Stock>;
}
