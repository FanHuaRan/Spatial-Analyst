using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlayDemo.Core
{
    interface IMatirx
    {
        //求行列式值
        float GetRowColumnValue(List<List<float>> matrix);

        //矩阵求逆
        List<List<float>> GetInverseMatrix(List<List<float>> matrix);

        //矩阵数乘运算
        List<List<float>> GetMultipMatrix(List<List<float>> matrixA, List<List<float>> matrixB);

        //矩阵加减运算
        List<List<float>> GetAddRemoveMatrix(List<List<float>> matrixA, List<List<float>> matrixB, float opreation);

        //矩阵倍数运算
        List<List<float>> GetKMatrix(List<List<float>> matrix, float k);
    }
}
