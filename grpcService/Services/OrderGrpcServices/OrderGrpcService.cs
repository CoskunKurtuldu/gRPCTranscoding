using AutoMapper;
using Grpc.Core;
using grpcService.Models;
using grpcService.Services.OrderServices;
using OrderServiceGrpc;

namespace grpcService.Services.OrderServiceGrpc
{
    public class OrderGrpcService: Orders.OrdersBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderGrpcService> _logger;
        private readonly IMapper _mapper;
        public OrderGrpcService(IOrderService orderService, ILogger<OrderGrpcService> logger, IMapper mapper)
        {
            _orderService = orderService;
            _logger = logger;
            _mapper = mapper;
        }

        // Create Order
        public override async Task<CreateOrderResponse> CreateOrder(CreateOrderRequest request, ServerCallContext context)
        {
            var mapRequestToOrder = _mapper.Map<Order>(request);
            var res = await _orderService.AddOrder(mapRequestToOrder);
            if (res is not null)
            {
                var mapRes = _mapper.Map<CreateOrderResponse>(res);
                return await Task.FromResult(mapRes);
            }
            throw new RpcException(new Status(StatusCode.Cancelled, "Create Order Failed!"));
        }

        // Edit Order
        public override async Task<EditOrderResponse> EditeOrder(EditOrderRequest request, ServerCallContext context)
        {
            var mapRequestToOrder = _mapper.Map<Order>(request);
            var res = await _orderService.EditOrder(mapRequestToOrder);
            if (res is not null)
            {
                var mapRes = _mapper.Map<EditOrderResponse>(res);
                return await Task.FromResult(mapRes);
            }
            throw new RpcException(new Status(StatusCode.Cancelled, "Edit Order Failed!"));
        }

        // Delete Order
        public override async Task<DeleteOrderResponse> DeleteOrder(DeleteOrderRequest request, ServerCallContext context)
        {
            var Order = _orderService.GetOrderById(request.Id);
            if (Order is null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "There is no Order"));
            }
            var res = _orderService.DeleteOrderById(request.Id);
            if (res == true)
            {
                var mapOrder = _mapper.Map<DeleteOrderResponse>(Order);
                return await Task.FromResult(mapOrder);
            }
            throw new RpcException(new Status(StatusCode.Cancelled, "Deleting failed!"));
        }

        // Get Order By Id
        public override async Task<GetOrderResponse> GetOrderById(GetOrderRequest request, ServerCallContext context)
        {
            var Order = _orderService.GetOrderById(request.Id);
            if (Order is not null)
            {
                var mapOrder = _mapper.Map<GetOrderResponse>(Order);
                return await Task.FromResult(mapOrder);
            }
            throw new RpcException(new Status(StatusCode.NotFound, "Order not found"));
        }

        // List Of All Orders
        public override async Task<GetAllOrdersResponse> ListOrders(GetAllOrdersRequest request, ServerCallContext context)
        {
            var response = new GetAllOrdersResponse();

            var list = _orderService.AllOrders();
            foreach (var item in list)
            {
                var mapModel = _mapper.Map<GetOrderResponse>(item);
                response.Order.Add(mapModel);
            }

            return await Task.FromResult(response);
        }
    }
}