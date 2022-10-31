using BuisnessLayer.Interface;
using CommonLayer.OrderModel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLayer.Services
{
    public class OrderBL : IOrderBL
    {
        IOrderRL _orderRL;
        public OrderBL(IOrderRL orderRL)
        {
            _orderRL = orderRL;
        }

        public AddOrderModel AddOrder(AddOrderModel addOrderModel)
        {
            try
            {
                return this._orderRL.AddOrder(addOrderModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteOrder(int Orderid)
        {
            try
            {
                return this._orderRL.DeleteOrder(Orderid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GetAllOrderModel> GetAllOrder(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
