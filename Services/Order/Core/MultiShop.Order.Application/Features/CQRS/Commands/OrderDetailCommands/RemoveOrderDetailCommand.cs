﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands
{
    public class RemoveOrderDetailCommand
    {
        public int OrderDetailID { get; set; } //Sipariş Detay ID'yi aldık.

        public RemoveOrderDetailCommand(int orderDetailID) //
        {
            OrderDetailID = orderDetailID; //
        }
    }
}
