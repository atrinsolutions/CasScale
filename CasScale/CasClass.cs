//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;


namespace CasScale
{
    public class CasClass
    {
        private byte[] ReciveBuffer = new byte[1024];
        private Product[] Plus { get; }
        public byte[] RequestPluInfo = new byte[]
        {
            0x52,0x30,0x32,0x46,0x30,0x31,0x30,0x30,0x30,0x30,0x30,0x31,0x2c,0x30,0x30,0x0a,
        };

        public Product ReadPlu(string server, int port, int plu_no)
        {
            return Plus[0];
        }
        public int ReadAllPlus(string server, int port)
        {
            string Tempstring;
            int PluCounter;
            bool DataAvailabel = true;

            for (PluCounter = 0; DataAvailabel == true; PluCounter++)
            {
                try
                {
                    TcpClient client = new TcpClient(server, port);
                    if (client == null)
                        return -1; // client is null !!!
                    else
                    {
                        NetworkStream stream = client.GetStream();
                        Tempstring = PluCounter.ToString("X6");
                        ASCIIEncoding.ASCII.GetBytes(Tempstring, 0, 6, RequestPluInfo, 6);
                        stream.Write(RequestPluInfo, 0, RequestPluInfo.Length);
                        String responseData = String.Empty;
                        stream.Read(ReciveBuffer, 0, ReciveBuffer.Length);
                        if (ProcessRecivedPluData(Plus[PluCounter], ReciveBuffer) == -1)
                            DataAvailabel = false;
                        stream.Close();
                        client.Close();
                    }
                }
                catch (ArgumentNullException e)
                {
                    return -2; // ArgumentNullException !!!
                }
                catch (SocketException e)
                {
                    return -3; // SocketException !!!
                }
            }
            if (DataAvailabel == false)
            {
                if (PluCounter == 0)
                    return 2; // succeed : scale has no data
            }
            return 1; // succeed : scale has data 
        }

        private int ConvertEncoding1252ToDecimal(string EncodeData)
        {
            byte[] EncodeArray = Encoding.GetEncoding(1252).GetBytes(EncodeData);
            Array.Reverse(EncodeArray, 0, EncodeArray.Length);
            string HexStr = BitConverter.ToString(EncodeArray).Replace("-", "");
            return Int32.Parse(HexStr, System.Globalization.NumberStyles.HexNumber);
        }
        public short ProcessRecivedPluData(Product _productInfo, byte[] data)
        {
            int ChecksumIndex = 18;
            byte CalculatedChecksum = 0;
            if (Encoding.GetEncoding(1252).GetString(data, 0, 13) != "W02AF4240,00L")
                return -1;
            if (Encoding.GetEncoding(1252).GetString(data, 0, 4) != "W02A")
                return -2;
            if (data.Length < ChecksumIndex)
                return -3;
            CalculatedChecksum = data[18];
            for (int PackCounter = ChecksumIndex+1; PackCounter < data.Length - 1; PackCounter++)
                CalculatedChecksum ^= data[PackCounter];
            if (CalculatedChecksum != data[data.Length - 1])
                return -4;
            else
            {
                string[] CountDelimiter = {
                            "F=",
                          };
                string[] ConfigDelimiters = {
                            "W02A",
                            "L",
                            ",",
                            ".",
                            "=",
                          };
                string[] DataDelimiters = {
                            "F=",
                            ":",
                          };
                string BasePack = Encoding.GetEncoding(1252).GetString(data, 0, data.Length - 1);
                var DataCount = BasePack.Split(CountDelimiter, StringSplitOptions.RemoveEmptyEntries).Count() - 1;
                var SplitedConfigs = BasePack.Split(ConfigDelimiters, StringSplitOptions.RemoveEmptyEntries);
                var SplitedData = BasePack.Split(DataDelimiters, StringSplitOptions.RemoveEmptyEntries);

                _productInfo.PLU_No = Int32.Parse(SplitedConfigs[0], System.Globalization.NumberStyles.HexNumber);
                _productInfo.DepartmentNo = Int32.Parse(SplitedConfigs[1], System.Globalization.NumberStyles.HexNumber);
                _productInfo.ScaleID = Int32.Parse(SplitedConfigs[3], System.Globalization.NumberStyles.HexNumber);
                _productInfo.LockInfo = (Int32.Parse(SplitedConfigs[7], System.Globalization.NumberStyles.HexNumber)) > 0 ? true : false;
                _productInfo.PackIP = String.Format("{0}.{1}.{2}.{3}",
                    int.Parse(SplitedConfigs[9].Substring(0, 2), System.Globalization.NumberStyles.HexNumber),
                    int.Parse(SplitedConfigs[9].Substring(2, 2), System.Globalization.NumberStyles.HexNumber),
                    int.Parse(SplitedConfigs[9].Substring(4, 2), System.Globalization.NumberStyles.HexNumber),
                    int.Parse(SplitedConfigs[9].Substring(6, 2), System.Globalization.NumberStyles.HexNumber));
                _productInfo.PackPort = Int32.Parse(SplitedConfigs[11], System.Globalization.NumberStyles.HexNumber);
                _productInfo.ScaleServiceType = (byte)Int32.Parse(SplitedConfigs[13], System.Globalization.NumberStyles.HexNumber);
                _productInfo.TableRow = Int32.Parse(SplitedConfigs[15], System.Globalization.NumberStyles.HexNumber);
                string SpFunc;
                string[] SplitedFunc;
                for (int LoopCnt = 0, DataCounter = 3; LoopCnt < DataCount; LoopCnt++, DataCounter += 2)
                {
                    SpFunc = SplitedData[DataCounter - 1];
                    SplitedFunc = SpFunc.Split(',');
                    switch (SplitedFunc[0].ToUpper())
                    {
                        case "01.57":
                            _productInfo.DepartmentNo = ConvertEncoding1252ToDecimal(SplitedData[DataCounter]);
                            break;
                        case "0A.53":
                            _productInfo.Name1 = SplitedData[DataCounter];
                            break;
                        case "0B.4C":
                            _productInfo.Itemcode = ConvertEncoding1252ToDecimal(SplitedData[DataCounter]).ToString();
                            break;
                        case "06.4C":
                            _productInfo.Unit_Price = ConvertEncoding1252ToDecimal(SplitedData[DataCounter]);
                            break;
                        case "04.4D":
                            _productInfo.PLU_Type = ConvertEncoding1252ToDecimal(SplitedData[DataCounter]);
                            break;
                        case "02.4C":
                            _productInfo.PLU_No = ConvertEncoding1252ToDecimal(SplitedData[DataCounter]);
                            break;
                        case "05.42":
                            _productInfo.Unit_Weight = ConvertEncoding1252ToDecimal(SplitedData[DataCounter]);
                            break;
                        case "09.57":
                            _productInfo.Group_No = ConvertEncoding1252ToDecimal(SplitedData[DataCounter]);
                            break;
                        case "0D.4C":
                            _productInfo.TareValue = ConvertEncoding1252ToDecimal(SplitedData[DataCounter]);
                            break;
                        case "0C.42":
                            _productInfo.Tare_No = ConvertEncoding1252ToDecimal(SplitedData[DataCounter]);
                            break;
                        case "08.42":
                            _productInfo.TaxCode = ConvertEncoding1252ToDecimal(SplitedData[DataCounter]);
                            break;
                        case "51.57":
                            _productInfo.Ax_Label_No = ConvertEncoding1252ToDecimal(SplitedData[DataCounter]);
                            break;
                        case "56.57":
                            _productInfo.Barcode2_No = ConvertEncoding1252ToDecimal(SplitedData[DataCounter]);
                            break;
                        case "5A.42":
                            _productInfo.SaleMessage_No = ConvertEncoding1252ToDecimal(SplitedData[DataCounter]);
                            break;
                        case "5B.42":
                            _productInfo.Special_Price = ConvertEncoding1252ToDecimal(SplitedData[DataCounter]);
                            break;
                        case "55.57":
                            _productInfo.Barcode_No = ConvertEncoding1252ToDecimal(SplitedData[DataCounter]);
                            break;
                        case "50.57":
                            _productInfo.Label_No = ConvertEncoding1252ToDecimal(SplitedData[DataCounter]);
                            break;
                    }
                }

            }
            return 1;
        }
    }
}
