using CommonLayer.OrderModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLayer.Interface
{
    public interface IOrderBL
    {
        AddOrderModel AddOrder(AddOrderModel addOrderModel);
        bool DeleteOrder(int Orderid);
        List<GetAllOrderModel> GetAllOrder(int userId);
    }
}
