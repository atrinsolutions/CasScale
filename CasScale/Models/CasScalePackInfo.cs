using System;
using System.Collections.Generic;
using System.Linq;


namespace CL_WorkApp.Models.Entities
{
    public class CasScalePackInfo
    {

        public int id { get; set; }
        public string CasProtocolIdentifier { get; set; }
        public byte FunctionCode { get; set; }
        public uint PackLenght { get; set; }
        public byte ScaleLocked { get; set; }
        public string ScaleIP { get; set; }
        public uint ScalePort { get; set; }
        public byte ScaleServiceType { get; set; }
        public uint ScaleTail { get; set; }
        public byte ScaleID { get; set; }
        public byte PluType { get; set; }
        public byte DeparmentNumber { get; set; }
        public uint PLUNumber { get; set; }
        public uint Itemcode { get; set; }
        public uint Weight { get; set; }
        public short Qty { get; set; }
        public short Pcs { get; set; }
        public uint UnitPrice { get; set; }
        public uint TotalPrice { get; set; }
        public uint DiscountPrice { get; set; }
        public uint ScaleTransactioncounter { get; set; }
        public short TicketNumber { get; set; }
        public uint CurrentDate_year { get; set; }
        public uint CurrentDate_month { get; set; }
        public uint CurrentDate_day { get; set; }
        public uint CurrentTime_hour { get; set; }
        public uint CurrentTime_min { get; set; }
        public uint CurrentTime_second { get; set; }
        public uint SaleDate_year { get; set; }
        public uint SaleDate_month { get; set; }
        public uint SaleDate_day { get; set; }
        public string Barcode { get; set; }
        public string TraceCode { get; set; }
        public byte CurrentTicketSaleorder { get; set; }
        public byte reserved { get; set; }
        public byte[] PluName { get; set; }

        public bool NegativeSale { get; set; }
        public bool Return { get; set; }
        public bool Void { get; set; }
        public bool Prepack { get; set; }
        public bool Label { get; set; }
        public bool Override { get; set; }
        public bool Add { get; set; }
        public bool NoVoid { get; set; }
        public bool Normal { get; set; }
        public bool PrepackData { get; set; }
        public bool SelfService { get; set; }
        public bool PluData { get; set; }
        public bool TicketData { get; set; }

        public CasScalePackInfo()
        {
            PluName = new byte[55];
        }
    }
}
