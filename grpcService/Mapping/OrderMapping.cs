using AutoMapper;
using grpcService.Models;
using OrderServiceGrpc;

namespace grpcService.Mapping
{
    public class OrderMapping : Profile
    {
        public OrderMapping()
        {
            // Create Request
            CreateMap<CreateOrderRequest, Order>();
            CreateMap<Order, CreateOrderResponse>();

            // Edit Request
            CreateMap<EditOrderRequest, Order>();
            CreateMap<Order, EditOrderResponse>();

            // Delete
            CreateMap<Order, DeleteOrderResponse>();

            // Get By Id
            CreateMap<Order, GetOrderResponse>();

        }
    }
}