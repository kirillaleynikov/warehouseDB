using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse
{
    /// <summary>
    /// Сущность товара
    /// </summary>
    public class Tovar
    {
        public int Id { get; set; }
        /// <summary>
        /// Название товара
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Размер
        /// </summary>
        public decimal Razmer { get; set; }
        /// <summary>
        /// Материал
        /// </summary>
        public material Material { get; set; }
        /// <summary>
        /// Кол-во на складе
        /// </summary>
        public int kolvo { get; set; }
        /// <summary>
        /// Минимальный предел кол-ва
        /// </summary>
        public int minpr { get; set; }
        /// <summary>
        /// Цена (без НДС)
        /// </summary>
        public decimal price { get; set; }
    }
}
