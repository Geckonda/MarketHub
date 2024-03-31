using MarketHub.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Domain.Abstractions.Responses
{
    public interface IBaseResponse<T>
    {
        T? Data { get; set; }
        public StatusCode StatusCode { get; set; }

        public string? Description { get; set; }
        public string? ErrorForUser { get; set; }
    }
}
