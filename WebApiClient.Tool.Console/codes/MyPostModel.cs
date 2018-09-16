using System;
using System.Collections.Generic;
using System.Text;

namespace Test.NameSpace
{
    /// <summary>
    /// MyPostModel
    /// </summary>
    public class MyPostModel
    {
       
		/// <summary>
		/// id
		/// <summary>
		public long id { get; set; }
		/// <summary>
		/// name
		/// <summary>
		public string name { get; set; }
		/// <summary>
		/// time
		/// <summary>
		public DateTime? time { get; set; }
		/// <summary>
		/// detail
		/// <summary>
		public MyPostDetailModel detail { get; set; }
    }
}