//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLNHANVIEN.DTO
{
    using System;
    using System.Collections.Generic;
    
    public partial class PHANCONG
    {
        public short MANV { get; set; }
        public short MADA { get; set; }
        public Nullable<System.DateTime> THOIGIAN { get; set; }
    
        public virtual DEAN DEAN { get; set; }
        public virtual NHANVIEN NHANVIEN { get; set; }
    }
}
