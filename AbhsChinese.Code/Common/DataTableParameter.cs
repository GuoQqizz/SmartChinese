using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Code.Common
{
    /// <summary>
    ///分页类处理

    /// </summary>
    public class DataTableParameter
    {
        /// <summary>
        /// DataTable用来生成的信息
        /// </summary>       
        public string sEcho { get; set; }

        /// <summary>
        /// 分页起始索引
        /// </summary>
        public int iDisplayStart { get; set; }

        /// <summary>
        /// 每页显示的数量
        /// </summary>
        public int iDisplayLength { get; set; }

        /// <summary>
        /// 搜索字段
        /// </summary>
        public string sSearch { get; set; }

        /// <summary>
        /// 列数
        /// </summary>
        public int iColumns { get; set; }

        /// <summary>
        /// 排序列的数量
        /// </summary>
        public int iSortingCols { get; set; }

        /// <summary>
        /// 逗号分割所有的列
        /// </summary>
        public string sColumns { get; set; }
    }
}
