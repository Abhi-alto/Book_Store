using CommonLayer.WishList;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IWishListRL
    {
        public void AddToWishList(WishListModel wishListModel);
        public bool DeleteWishList(int WishListId);
        public List<GetWishListModel> GetWishList();

    }
}
