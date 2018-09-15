using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSample.Param
{
    public class MyPostModel
    {
        public long id { get; set; }
        public string name { get; set; }
        public DateTime time { get; set; }
        public MyPostDetailModel detail { get; set; }
    }

    public class MyPostDetailModel
    {
        public string other { get; set; }
    }

    public class MyReturnModel
    {
        public decimal score{ get; set; }
        public double doubleScore { get; set; }
        public int intScore { get; set; }
    }
}
