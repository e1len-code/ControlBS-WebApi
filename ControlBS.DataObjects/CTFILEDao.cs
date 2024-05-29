
using System.Data;
using System.Data.Common;
using ControlBS.BusinessObjects;
using ControlBS.BusinessObjects.Auth;

namespace ControlBS.DataObjects
{
    public partial class CTFILEDao : DataAccessBase
    {

        public virtual bool Save(CTFILE oCTFILE)
        {
            using (DbCommand dbCmd = Db.GetStoredProcCommand("dbo.spu_CTFILE_Save"))
            {
                Db.AddInParameter(dbCmd, "FILEIDEN", DbType.Int32, oCTFILE.FILEIDEN);
                Db.AddInParameter(dbCmd, "FILENAME", DbType.String, oCTFILE.FILENAME);
                Db.AddInParameter(dbCmd, "FILETYPE", DbType.String, oCTFILE.FILETYPE);
                Db.AddInParameter(dbCmd, "FILEPATH", DbType.String, oCTFILE.FILEPATH);
                return Db.ExecuteNonQuery(dbCmd) > 0;
            }
        }
        // public virtual bool Delete(int PERSIDEN)
        // {
        //     return Db.ExecuteNonQuery("dbo.spu_CTPERS_delete", PERSIDEN) == 1;
        // }
        // public virtual CTPERS? Get(int PERSIDEN)
        // {
        //     CTPERS? gotCTPERS = new CTPERS();
        //     DataTable dtDatos = Db.ExecuteDataSet("dbo.spu_CTPERS_get", PERSIDEN).Tables[0];
        //     gotCTPERS = dtDatos.Rows.Count > 0 ? Util.ToObject<CTPERS>(dtDatos.Rows[0]) : null;
        //     return gotCTPERS;
        // }
        // public virtual CTPERS? Auth(AuthRequest oAuthRequest)
        // {
        //     CTPERS? gotCTPERS = new CTPERS();
        //     DataTable dtDatos = Db.ExecuteDataSet("dbo.spu_CTPERS_Auth", oAuthRequest.userName, oAuthRequest.password).Tables[0];
        //     gotCTPERS = dtDatos.Rows.Count > 0 ? Util.ToObject<CTPERS>(dtDatos.Rows[0]) : null;
        //     return gotCTPERS;
        // }
        // public virtual bool Exist(int PERSIDEN)
        // {
        //     DataTable dtDatos = Db.ExecuteDataSet("dbo.spu_CTPERS_get", PERSIDEN).Tables[0];
        //     return dtDatos.Rows.Count > 0;
        // }
        // public virtual List<CTPERS> List()
        // {
        //     List<CTPERS> list = new List<CTPERS>();
        //     using (IDataReader dr = Db.ExecuteReader(CommandType.StoredProcedure, "dbo.spu_CTPERS_list"))
        //     {
        //         DataTable dt = new DataTable();
        //         dt.Load(dr);

        //         foreach (DataRow dataRow in dt.Rows)
        //         {
        //             list.Add(Util.ToObject<CTPERS>(dataRow));
        //         }
        //     }
        //     return list;
        // }
    }
}