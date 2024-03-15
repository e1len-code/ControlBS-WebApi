
using System.Data;
using System.Data.Common;
using ControlBS.BusinessObjects;

namespace ControlBS.DataObjects
{
    public partial class CTATTNDao : DataAccessBase
    {

        public virtual bool Save(CTATTN oCTATTN)
        {
            using (DbCommand dbCmd = Db.GetStoredProcCommand("dbo.spu_CTPERS_Save"))
            {
                Db.AddInParameter(dbCmd, "ATTNIDEN", DbType.Int32, oCTATTN.ATTNIDEN);
                Db.AddInParameter(dbCmd, "PERSIDEN", DbType.Int32, oCTATTN.PERSIDEN);
                Db.AddInParameter(dbCmd, "ATTNUBIC", DbType.String, oCTATTN.ATTNIDEN);
                Db.AddInParameter(dbCmd, "ATTNDATE", DbType.DateTime, oCTATTN.ATTNDATE);
                Db.AddInParameter(dbCmd, "ATTNOBSE", DbType.String, oCTATTN.ATTNOBSE);
                return Db.ExecuteNonQuery(dbCmd) > 0;
            }
        }
        public virtual bool Delete(int ATTNIDEN)
        {
            return Db.ExecuteNonQuery("dbo.spu_CTATTN_delete", ATTNIDEN) == 1;
        }
        public virtual CTATTN Get(int ATTNIDEN)
        {
            CTATTN gotCTATTN = new CTATTN();
            DataTable dtDatos = Db.ExecuteDataSet("dbo.spu_CTATTN_get", ATTNIDEN).Tables[0];
            gotCTATTN = dtDatos.Rows.Count > 0 ? Util.ToObject<CTATTN>(dtDatos.Rows[0]) : gotCTATTN;
            return gotCTATTN;
        }
        public virtual bool Exist(int ATTNIDEN)
        {
            DataTable dtDatos = Db.ExecuteDataSet("dbo.spu_CTATTN_get", ATTNIDEN).Tables[0];
            return dtDatos.Rows.Count > 0;
        }
        public virtual List<CTATTN> List()
        {
            List<CTATTN> list = new List<CTATTN>();
            using (IDataReader dr = Db.ExecuteReader(CommandType.StoredProcedure, "dbo.spu_CTATTN_list"))
            {
                DataTable dt = new DataTable();
                dt.Load(dr);

                foreach (DataRow dataRow in dt.Rows)
                {
                    list.Add(Util.ToObject<CTATTN>(dataRow));
                }
            }
            return list;
        }
    }
}