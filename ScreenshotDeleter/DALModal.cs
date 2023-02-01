using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using System.Collections.Specialized;
using System.Net;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Data;
using System.Xml.Serialization;

namespace ScreenshotDeleter
{
    class DALModal
    {
        public static void insertLog(DataTable dtData)
        {
            try
            {
                DataSet tDataSet = new DataSet();
                //tDataSet
                DataTable dtcopy;
                dtcopy = dtData.Copy();

                tDataSet.Tables.Add(dtcopy);

                //Convert datatable into XML  format
                XmlSerializer tXmlSerializer = new XmlSerializer(tDataSet.GetType());
                System.IO.MemoryStream tStream = new System.IO.MemoryStream();
                tDataSet.DataSetName = "Logs";
                tDataSet.Tables[0].TableName = "dt_log";
                tDataSet.WriteXml(tStream);
                String LogXml = System.Text.Encoding.UTF8.GetString(tStream.ToArray());
                SqlParameter[] param = new SqlParameter[1];
                
                param[0] = new SqlParameter("@xmlDatatable", LogXml);
               

                
            }
            catch
            {

            }

        }
    }
}
