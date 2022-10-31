using CommonLayer.AddressModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IAddressRL
    {
        public AddAddressModel AddAddress(AddAddressModel addAddress);
        public UpdateAddressModel updateAddress(UpdateAddressModel updateAddress, int AddessId);
        public bool DeleteAddress(int AddressId);
    }
}
