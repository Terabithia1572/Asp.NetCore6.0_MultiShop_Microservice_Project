using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands
{
    public class RemoveAddressCommand
    {
        public int AddressID { get; set; } // Adresin benzersiz kimliği, silinecek adresi tanımlar

        public RemoveAddressCommand(int addressID) // Adres silme komutu için gerekli parametreleri alır
        {
            AddressID = addressID; // Adresin benzersiz kimliği
        }
    }
}
