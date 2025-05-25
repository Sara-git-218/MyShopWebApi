using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DTO
{
 
        public record OrderDTO(DateOnly OrderDate,int OrderSum,int UserId,List<OrderItemDTO> Orderitems);
   
}
