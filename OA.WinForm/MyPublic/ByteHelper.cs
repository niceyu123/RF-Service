using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSMT.MEDIA.WinForm.MyPublic
{
    public class ByteHelper
    {
        #region byte[]转换成其他类型
        /// <summary>
        /// byte[]转string
        /// </summary>
        /// <param name="byteArr">byte数组</param>
        /// <returns>string</returns>
        public static string ByteToString(byte[] byteArr, int arrLen)
        {
            return Encoding.GetEncoding("GB2312").GetString(byteArr, 0, arrLen);
        }
        /// <summary>
        /// byte[]转int
        /// </summary>
        /// <param name="byteArr">byte数组</param>
        /// <param name="arrLen">byte数组长度</param>
        /// <returns>int</returns>
        public static int ByteToInt(byte[] byteArr, int arrLen)
        {
            int retNum = 0;
            for (int i = 0; i < arrLen; i++)
            {
                retNum += byteArr[i] * (int)Math.Pow(256, i);
            }
            return retNum;
        }
        /// <summary>
        /// byte[]转uint
        /// </summary>
        /// <param name="byteArr">byte数组</param>
        /// <param name="arrLen">byte数组长度</param>
        /// <returns>uint</returns>
        public static uint ByteToUInt(byte[] byteArr, int arrLen)
        {
            uint retNum = 0;
            for (int i = 0; i < arrLen; i++)
            {
                retNum += byteArr[i] * (uint)Math.Pow(256, arrLen - i - 1);
            }
            return retNum;
        }
        /// <summary>
        /// byte[]转DateTime
        /// </summary>
        /// <param name="byteArr">byte数组</param>
        /// <returns>DateTime</returns>
        public static DateTime ByteToDateTime(byte[] byteArr)
        {
            int year = byteArr[0] + byteArr[1] * 100 + 1900;
            int month = byteArr[2];
            int day = byteArr[3];
            int hour = byteArr[4];
            int minute = byteArr[5];
            int second = byteArr[6];
            return new DateTime(year, month, day, hour, minute, second);
        }
        /// <summary>
        /// byte[]复制到byte[]
        /// </summary>
        /// <param name="toArr">目标数组</param>
        /// <param name="arrOffset">目标组偏移位置</param>
        /// <param name="fromArr">原数组</param>
        /// <param name="arrSize">原数组长度</param>
        public static void ByteArrToByteArr(byte[] toArr, int arrOffset, byte[] fromArr, int arrSize)
        {
            for (int i = 0; i < arrSize; i++)
            {
                toArr[arrOffset + i] = fromArr[i];
            }
        }
        #endregion byte[]转换成其他类型

        #region 其他类型转换成byte[]
        /// <summary>
        /// DateTime转byte[]
        /// </summary>
        /// <param name="dateTime">待转换时间</param>
        /// <returns>byte[]</returns>
        public static byte[] DateTimeToByte(DateTime dateTime)
        {
            byte[] byteDateTime = new byte[7];
            byte[] year0 = BitConverter.GetBytes((dateTime.Year - 1900) % 100);
            byte[] year1 = BitConverter.GetBytes((dateTime.Year - 1900) / 100);
            byte[] month = BitConverter.GetBytes(dateTime.Month);
            byte[] day = BitConverter.GetBytes(dateTime.Day);
            byte[] hour = BitConverter.GetBytes(dateTime.Hour);
            byte[] minute = BitConverter.GetBytes(dateTime.Minute);
            byte[] second = BitConverter.GetBytes(dateTime.Second);
            ByteArrToByteArr(byteDateTime, 0, year0, 1);
            ByteArrToByteArr(byteDateTime, 1, year1, 1);
            ByteArrToByteArr(byteDateTime, 2, month, 1);
            ByteArrToByteArr(byteDateTime, 3, day, 1);
            ByteArrToByteArr(byteDateTime, 4, hour, 1);
            ByteArrToByteArr(byteDateTime, 5, minute, 1);
            ByteArrToByteArr(byteDateTime, 6, second, 1);
            return byteDateTime;
        }
        /// <summary>
        /// 错误信息转byte[]
        /// </summary>
        /// <param name="outResult">错误信息</param>
        /// <returns>byte[]</returns>
        public static byte[] ErrorToByte(string outResult, int middleLength, int dataLength = 2)
        {
            string[] errorArr = outResult.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            byte[] byteErrorCode = BitConverter.GetBytes(Convert.ToInt32(errorArr[1]));
            byte[] byteErrorName = Encoding.GetEncoding("GB2312").GetBytes(errorArr[2]);
            byte[] byteErrorNameLen = BitConverter.GetBytes(byteErrorName.Length);
            byte[] byteArr = new byte[2 + middleLength + dataLength + byteErrorName.Length];
            ByteHelper.ByteArrToByteArr(byteArr, 0, byteErrorCode, 2);
            ByteHelper.ByteArrToByteArr(byteArr, 2 + middleLength, byteErrorNameLen, dataLength);
            ByteHelper.ByteArrToByteArr(byteArr, 2 + middleLength + dataLength, byteErrorName, byteErrorName.Length);
            return byteArr;
        }
        #endregion 其他类型转换成byte[]
    }
}
