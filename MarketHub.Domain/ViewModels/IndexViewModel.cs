using MarketHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Domain.ViewModels
{
    public class IndexViewModel
    {
        public List<CategoryEntity> Categories { get; set; } = new();
    }
}
