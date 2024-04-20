using MarketHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.Domain.ViewModels
{
    public class SizeEditorViewModel
    {
        public List<SizeEntity> Sizes { get; set; } = new();
        public ProductEntity? Product { get; set; }
        public int ProductId {  get; set; }

        //Поля для экземпляров
        public int SizeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public uint Amount { get; set; }

    }
}
