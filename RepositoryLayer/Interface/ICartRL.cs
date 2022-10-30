using CommonLayer.CartModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public  interface ICartRL
    {
        public void AddCart(CartModel cartModel);
        public int UpdateCart(int CartId,int BookId,UpdateCart updateCart);
        public bool DeleteCart(int CartId);
        public List<GetCart> GetCart();
    }
}
