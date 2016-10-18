using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlayDemo.Entity;
namespace OverlayDemo.Core
{
    class MatrixClass :IMatirx
    {
        public float GetRowColumnValue(List<List<float>> matrix)
        {
            if (!IsMatrix(matrix))
            {
                throw new Exception("不是矩阵，无法进行矩阵运算");
            }
            float result = 0;
            int n = matrix.Count;
            if (n == 1)
            {
                return matrix[0][0];
            }
            List<List<float>> temp;
            InitalMatix(out temp, n - 1, n - 1);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n - 1; j++)
                {
                    for (int k = 0; k < n - 1; k++)
                    {
                        int flag;
                        if (j < i) flag = 0;
                        else flag = 1;
                        temp[j][k] = matrix[j + flag][k + 1];
                    }
                }
                int flag2 = -1;
                if (i % 2 == 0) flag2 = 1;
                result += flag2 * matrix[i][0] * GetRowColumnValue(temp);
            }
            return result;
        }

        public List<List<float>> GetInverseMatrix(List<List<float>> matrix)
        {
            if(!IsSquareMatrix(matrix))
            {
                throw new Exception("不是方阵，无法进行求逆运算");
            }
            int n=matrix.Count;
            List<List<float>> resultMarin;
            InitalMatix(out resultMarin, n, n);
            float resultSum = GetRowColumnValue(matrix);
            List<List<float>> temp;
            InitalMatix(out temp, n-1, n-1);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    for (int k = 0; k < n - 1; k++)
                    {
                        for (int m = 0; m < n - 1; m++)
                        {
                            int flag1 = 0;
                            int flag2 = 0;
                            if (k < i) flag1 = 0;
                            else flag1 = 1;
                            if (m < j) flag2 = 0;
                            else flag2 = 1;
                            temp[k][m] = matrix[k + flag1][m + flag2];
                        }
                    }
                    int flag3 = -1;
                    if ((i + j) % 2 == 0) flag3 = 1;
                    resultMarin[j][i] = (float)flag3 * GetRowColumnValue(temp) / resultSum;
                }
            }
            return resultMarin;
        }

        public List<List<float>> GetMultipMatrix(List<List<float>> matrixA, List<List<float>> matrixB)
        {
            if(!IsMatrix(matrixA)||!IsMatrix(matrixB))
            {
                throw new Exception("不是矩阵，无法进行矩阵运算");
            }
            int rowCount1 = matrixA.Count;
            int rowCount2 = matrixB.Count;
            int columCount1 = matrixA[0].Count;
            int columCount2 = matrixB[0].Count;
            if (columCount1 != rowCount2)
            {
                throw new Exception("两个矩阵的行列无法对应");
            }
            List<List<float>> result;
            InitalMatix(out result, rowCount1, columCount2);
            //数乘运算
            for (int i = 0; i < rowCount1; i++)
            {
                for (int j = 0; j < columCount2; j++)
                {
                    float temp = 0;
                    for (int k = 0; k < columCount1; k++)
                    {
                        temp += matrixA[i][k] * matrixB[k][j];
                    }
                    result[i][j] = temp;
                }
            }
            return result;
        }

        public List<List<float>> GetAddRemoveMatrix(List<List<float>> matrixA, List<List<float>> matrixB, float opreation)
        {
            if (!IsMatrix(matrixA) || !IsMatrix(matrixB))
            {
                throw new Exception("不是矩阵，无法进行矩阵运算");
            }
            int rowCount1 = matrixA.Count;
            int rowCount2 = matrixB.Count;
            int columCount1 = matrixA[0].Count;
            int columCount2 = matrixB[0].Count;
            if (columCount1 != columCount2 || rowCount1 != rowCount2)
            {
                throw new Exception("两个矩阵的行列无法对应");
            }
            List<List<float>> result;
            InitalMatix(out result, rowCount1, columCount1);
            for (int i = 0; i < rowCount1; i++)
            {
                for (int j = 0; j < columCount1; j++)
                {
                    result[i][j] = matrixA[i][j] + opreation* matrixA[i][j];
                }
            }
            return result;
        }

        public List<List<float>> GetKMatrix(List<List<float>> matrix, float k)
        {
            if (!IsMatrix(matrix))
            {
                throw new Exception("不是矩阵，无法进行矩阵运算");
            }
            int rowCount = matrix.Count;
            int columCount = matrix[0].Count;
            List<List<float>> result;
            InitalMatix(out result, rowCount, columCount);
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columCount; j++)
                {
                    result[i][j] = k * (matrix[i][j]);
                }
            }
            return result;
        }
        //是否方阵
        private bool IsSquareMatrix(List<List<float>> matrix)
        {
            bool flag = true;
            int n = matrix.Count;
            for(int i=0;i<n;i++)
            {
                if(matrix[i].Count!=n)
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }
        //是否矩阵
        private bool IsMatrix(List<List<float>> matrix)
        {
            bool flag = true;
            int n = matrix[0].Count;
            for (int i = 1; i < n; i++)
            {
                if (matrix[i].Count != n)
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }
        //初始化矩阵
        private void InitalMatix(out List<List<float>> matrix,int rowCount,int columCount)
        {
            matrix = new List<List<float>>();
            for(int i=0;i<rowCount;i++)
            {
                List<float> element=new List<float>();
                for(int j=0;j<columCount;j++)
                {
                    element.Add(0);
                }
                matrix.Add(element);
            }
        }
    }
}
