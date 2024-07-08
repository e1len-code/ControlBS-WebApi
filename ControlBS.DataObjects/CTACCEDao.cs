
using System.Data;
using System.Data.Common;
using ControlBS.BusinessObjects;
using ControlBS.BusinessObjects.Models;
using ControlBS.BusinessObjects.Security;

namespace ControlBS.DataObjects
{
    public partial class CTACCEDao : DataAccessBase
    {

        // public virtual bool Save(CTATTN oCTATTN)
        // {
        //     using (DbCommand dbCmd = Db.GetStoredProcCommand("dbo.spu_CTATTN_Save"))
        //     {
        //         Db.AddInParameter(dbCmd, "ATTNIDEN", DbType.Int32, oCTATTN.ATTNIDEN);
        //         Db.AddInParameter(dbCmd, "PERSIDEN", DbType.Int32, oCTATTN.PERSIDEN);
        //         Db.AddInParameter(dbCmd, "ATTNUBIC", DbType.String, oCTATTN.ATTNUBIC);
        //         Db.AddInParameter(dbCmd, "ATTNDATE", DbType.DateTime, oCTATTN.ATTNDATE);
        //         Db.AddInParameter(dbCmd, "ATTNOBSE", DbType.String, oCTATTN.ATTNOBSE);
        //         Db.AddInParameter(dbCmd, "ATTNLINE", DbType.Int32, oCTATTN.ATTNLINE);
        //         return Db.ExecuteNonQuery(dbCmd) > 0;
        //     }
        // }
        // public virtual bool Delete(int ATTNIDEN)
        // {
        //     return Db.ExecuteNonQuery("dbo.spu_CTATTN_delete", ATTNIDEN) == 1;
        // }
        // public virtual CTATTN? Get(int ATTNIDEN)
        // {
        //     CTATTN? gotCTATTN = new CTATTN();
        //     DataTable dtDatos = Db.ExecuteDataSet("dbo.spu_CTATTN_get", ATTNIDEN).Tables[0];
        //     gotCTATTN = dtDatos.Rows.Count > 0 ? Util.ToObject<CTATTN>(dtDatos.Rows[0]) : null;
        //     return gotCTATTN;
        // }
        // public virtual bool Exist(int ATTNIDEN)
        // {
        //     DataTable dtDatos = Db.ExecuteDataSet("dbo.spu_CTATTN_get", ATTNIDEN).Tables[0];
        //     return dtDatos.Rows.Count > 0;
        // }
        // public virtual List<CTATTN> List()
        // {
        //     List<CTATTN> list = new List<CTATTN>();
        //     using (IDataReader dr = Db.ExecuteReader(CommandType.StoredProcedure, "dbo.spu_CTATTN_list"))
        //     {
        //         DataTable dt = new DataTable();
        //         dt.Load(dr);

        //         foreach (DataRow dataRow in dt.Rows)
        //         {
        //             list.Add(Util.ToObject<CTATTN>(dataRow));
        //         }
        //     }
        //     return list;
        // }
        public virtual List<CTACCE> ListAccess(int oPERSIDEN)
        {
            List<CTACCE> list = new List<CTACCE>();
            using (IDataReader dr = Db.ExecuteReader("Security.spu_CTUSPE_ListAccess", oPERSIDEN))
            {
                DataTable dt = new DataTable();
                dt.Load(dr);

                foreach (DataRow dataRow in dt.Rows)
                {
                    list.Add(Util.ToObject<CTACCE>(dataRow));
                }
            }
            return list;
        }
    }
}