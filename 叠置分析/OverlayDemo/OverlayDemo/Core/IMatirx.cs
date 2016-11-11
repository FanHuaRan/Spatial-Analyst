using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlayDemo.Core
{
    /// <summary>
    /// 矩阵操作接口
    /// </summary>
    interface IMatirx
    {
        /// <summary>
        /// 求行列式值
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        float GetRowColumnValue(List<List<float>> matrix);

        /// <summary>
        /// 矩阵求逆
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        List<List<float>> GetInverseMatrix(List<List<float>> matrix);

        /// <summary>
        /// 矩阵数乘运算
        /// </summary>
        /// <param name="matrixA"></param>
        /// <param name="matrixB"></param>
        /// <returns></returns>
        List<List<float>> GetMultipMatrix(List<List<float>> matrixA, List<List<float>> matrixB);

        /// <summary>
        /// 矩阵加减运算
        /// </summary>
        /// <param name="matrixA"></param>
        /// <param name="matrixB"></param>
        /// <param name="opreation"></param>
        /// <returns></returns>
        List<List<float>> GetAddRemoveMatrix(List<List<float>> matrixA, List<List<float>> matrixB, float opreation);

        /// <summary>
        /// 矩阵倍法运算
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        List<List<float>> GetKMatrix(List<List<float>> matrix, float k);
    }
}
