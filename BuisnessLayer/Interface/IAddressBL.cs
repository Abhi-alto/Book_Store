using CommonLayer.AddressModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLayer.Interface
{
    public interface IAddressBL
    {
        public AddAddressModel AddAddress(AddAddressModel addAddress);
        public UpdateAddressModel updateAddress(UpdateAddressModel updateAddress, int AddessId);
        public bool DeleteAddress(int AddressId);
    }
}
