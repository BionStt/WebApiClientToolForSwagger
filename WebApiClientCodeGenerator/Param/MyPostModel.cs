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

    /// <summary>
    /// 返回值
    /// </summary>
    public class MyReturnModel
    {
        /// <summary>
        /// 分数
        /// </summary>
        public decimal score{ get; set; }
        /// <summary>
        /// double分数
        /// </summary>
        public double doubleScore { get; set; }
        public float floatScore { get; set; }
        public int intScore { get; set; }
    }
}
