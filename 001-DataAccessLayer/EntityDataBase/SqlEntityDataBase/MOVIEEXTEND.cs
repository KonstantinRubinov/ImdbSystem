//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ImdbSystem.EntityDataBase.SqlEntityDataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class MOVIEEXTEND
    {
        public string movieImdbID { get; set; }
        public string moviePlot { get; set; }
        public string movieUrl { get; set; }
        public string movieRated { get; set; }
        public float movieImdbRating { get; set; }
        public bool movieSeen { get; set; }
        public string userID { get; set; }
    
        public virtual MOVy MOVy { get; set; }
    }
}
