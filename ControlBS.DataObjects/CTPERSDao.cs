
using System.Data;
using System.Data.Common;
using ControlBS.BusinessObjects;

namespace ControlBS.DataObjects
{
    public partial class CTPERSDao : DataAccessBase
    {

        public virtual bool Save(CTPERS oCTPERS)
        {
            using (DbCommand dbCmd = Db.GetStoredProcCommand("dbo.spu_CTPERS_Save"))
            {
                Db.AddInParameter(dbCmd, "PERSIDEN", DbType.Int32, oCTPERS.PERSIDEN);
                Db.AddInParameter(dbCmd, "PERSNAME", DbType.String, oCTPERS.PERSNAME);
                Db.AddInParameter(dbCmd, "PERSNMUS", DbType.String, oCTPERS.PERSNMUS);
                Db.AddInParameter(dbCmd, "PERSPASS", DbType.String, oCTPERS.PERSPASS);
                Db.AddInParameter(dbCmd, "PERSSTAT", DbType.Int32, oCTPERS.PERSSTAT);
                return Db.ExecuteNonQuery(dbCmd) > 0;
            }
        }
        public virtual bool Delete(int PERSIDEN)
        {
            return Db.ExecuteNonQuery("dbo.spu_CTPERS_delete", PERSIDEN) == 1;
        }
        public virtual CTPERS? Get(int PERSIDEN)
        {
            CTPERS? gotCTPERS = new CTPERS();
            DataTable dtDatos = Db.ExecuteDataSet("dbo.spu_CTPERS_get", PERSIDEN).Tables[0];
            gotCTPERS = dtDatos.Rows.Count > 0 ? Util.ToObject<CTPERS>(dtDatos.Rows[0]) : null;
            return gotCTPERS;
        }
        public virtual bool Exist(int PERSIDEN)
        {
            DataTable dtDatos = Db.ExecuteDataSet("dbo.spu_CTPERS_get", PERSIDEN).Tables[0];
            return dtDatos.Rows.Count > 0;
        }
        public virtual List<CTPERS> List()
        {
            List<CTPERS> list = new List<CTPERS>();
            using (IDataReader dr = Db.ExecuteReader(CommandType.StoredProcedure, "dbo.spu_CTPERS_list"))
            {
                DataTable dt = new DataTable();
                dt.Load(dr);

                foreach (DataRow dataRow in dt.Rows)
                {
                    list.Add(Util.ToObject<CTPERS>(dataRow));
                }
            }
            return list;
        }
    }
}