//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace dapperPath.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Shoes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Shoes()
        {
            this.Cart = new HashSet<Cart>();
            this.Reviews = new HashSet<Reviews>();
            this.Wishlist = new HashSet<Wishlist>();
            this.Orders = new HashSet<Orders>();
        }
    
        public int ProductID { get; set; }
        public string Title { get; set; }
        public string Brand { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string AvailableSizes { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Sex { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public string UnavailableSizes { get; set; }
        public Nullable<decimal> Sale { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cart> Cart { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reviews> Reviews { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Wishlist> Wishlist { get; set; }
        public virtual ShoeCategory ShoeCategory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
