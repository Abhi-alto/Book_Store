using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public  interface ICartRL
    {
        public void AddCart();
        public bool UpdateCart();
        public bool DeleteCart();
        public CartModel GetCart();
    }
}
