using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//Using SQLite.Net.Attributes;

namespace observer
{
    public class userauth
    {
        //[NotNull]
        //[PrimaryKey]
        public string UserID
        { get; set; }
        //[NotNull]
        public string password
        { get; set; }
        public string profilePath
        { get; set; }
    }

    public class profile {
        //[NotNull]
        //[PrimaryKey]
        public string UserID
        { get; set; }
        //[NotNull]
        public string language
        { get; set; }
        public string agreement
        { get; set; }
        public string profilepic
        { get; set; }
        public string fname
        { get; set; }
        public string lname
        { get; set; }
        public string badge
        { get; set; }
        public string updt
        { get; set; }
        public string addt
        { get; set; }        
    }
    public class observations {
        //[NotNull]
        //[PrimaryKey]
        public string obsId
        { get; set; }
        //[NotNull]
        public string userid
        { get; set; }
        public string assettype
        { get; set; }
        public string assetsubtype
        { get; set; }
        public string title
        { get; set; }
        public string summary
        { get; set; }
        public string location
        { get; set; }
        //[NotNull]
        public string status
        { get; set; }
        //[NotNull]
        public string updt
        { get; set; }
        public string addt
        { get; set; }
    }
    public class images {
        //[NotNull]
        //[PrimaryKey]
        public string imgId { get; set; }
        //[NotNull]
        public string obsid { get; set; }
        //[NotNull]
        public string image { get; set; }

    }
}
