using CommonLayer.OrderModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IOrderRL
    {
        AddOrderModel AddOrder(AddOrderModel addOrderModel);
        bool DeleteOrder(int Orderid);
        List<GetAllOrderModel> GetAllOrder(int userId);
    }
}
