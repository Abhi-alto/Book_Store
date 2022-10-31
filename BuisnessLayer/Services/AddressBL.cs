using BuisnessLayer.Interface;
using CommonLayer.AddressModel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLayer.Services
{
    public class AddressBL : IAddressBL
    {
        IAddressRL addressRL;

        public AddressBL(IAddressRL addressRL)
        {
            this.addressRL = addressRL;
        }

        public AddAddressModel AddAddress(AddAddressModel addAddress)
        {
            try
            {
                return this.addressRL.AddAddress(addAddress);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UpdateAddressModel updateAddress(UpdateAddressModel updateAddress, int AddressId)
        {
            try
            {
                return this.addressRL.updateAddress(updateAddress, AddressId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteAddress(int AddressId)
        {
            try
            {
                return this.addressRL.DeleteAddress(AddressId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
