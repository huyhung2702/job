//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gioithieuvieclam.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CongViec
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CongViec()
        {
            this.UngViens = new HashSet<UngVien>();
        }
    
        public int ID { get; set; }
        public Nullable<int> NhaTuyenDungID { get; set; }
        public string MoTaCongViec { get; set; }
        public string YeuCau { get; set; }
        public string ViTri { get; set; }
        public Nullable<decimal> MucLuong { get; set; }
        public string ThongTinLienHe { get; set; }
    
        public virtual NhaTuyenDung NhaTuyenDung { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UngVien> UngViens { get; set; }
    }
}
