//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DiplomFreelance.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Service
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ID_Subcategory { get; set; }
        public string TIme_work { get; set; }
        public string Place { get; set; }
        public string Expirience { get; set; }
        public int ID_Measure { get; set; }
        public decimal Price { get; set; }
        public string Notation { get; set; }
        public int ID_Executor { get; set; }
    
        public virtual Executor Executor { get; set; }
        public virtual Measure Measure { get; set; }
        public virtual Subcategory Subcategory { get; set; }
    }
}
