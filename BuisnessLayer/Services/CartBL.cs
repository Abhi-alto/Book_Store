using BuisnessLayer.Interface;
using CommonLayer.BookModel;
using CommonLayer.CartModel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLayer.Services
{
    public class CartBL : ICartBL
    {
        ICartRL cartRL;
        public CartBL(ICartRL cartRL)
        {
            this.cartRL = cartRL;
        }
        public void AddCart(CartModel cartModel)
        {
            try
            {
                this.cartRL.AddCart(cartModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteCart(int CartId)
        {
            try
            {
                return this.cartRL.DeleteCart(CartId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GetCart> GetCart()
        {
            try
            {
                return this.cartRL.GetCart();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateCart(int CartId,int BookId, UpdateCart updateCart)
        {
            try
            {
                return this.cartRL.UpdateCart(CartId,BookId, updateCart);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
