﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public record OrderDTO(DateTime OrderDate, double OrderSum, int UrerId, List<OrderItemDTO> Products);

}
