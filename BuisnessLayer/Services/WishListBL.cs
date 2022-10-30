using BuisnessLayer.Interface;
using CommonLayer.WishList;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLayer.Services
{
    public class WishListBL : IWishListBL
    {
        IWishListRL wishListRL;
        public WishListBL(IWishListRL wishListRL)
        {
            this.wishListRL = wishListRL;
        }
        public void AddToWishList(WishListModel wishListModel)
        {
            try
            {
                this.wishListRL.AddToWishList(wishListModel);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteWishList(int WishListId)
        {
            try
            {
                return this.wishListRL.DeleteWishList(WishListId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GetWishListModel> GetWishList()
        {
            try
            {
                return this.wishListRL.GetWishList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
