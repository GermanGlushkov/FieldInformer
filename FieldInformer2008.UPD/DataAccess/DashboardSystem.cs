using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

using FI.Common;
using FI.Common.Data;
using FI.Common.DataAccess;

namespace FI.DataAccess
{


    public class DashboardSystem : MarshalByRefObject, IDashboardSystemDA
	{
        public DashboardSystem()
		{
		}

        public FIDataTable GetUserGauges(decimal userId)
		{
            FIDataTable dataTable = new FIDataTable();

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@user_id", userId);
            string sql = @"select id, user_id, name, type, x, y, width, height, visible, refresh from dbo.t_gauges where user_id=@user_id order by timestamp asc";
            DataBase.Instance.ExecuteCommand(sql, CommandType.Text, parameters, dataTable);
            return dataTable;
		}

        public FIDataTable GetUserGaugeConfig(decimal userId, Guid gaugeId)
        {
            FIDataTable dataTable = new FIDataTable();

            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@id", gaugeId);
            parameters[1] = new SqlParameter("@user_id", userId);

            string sql = @"select id, user_id, name, type, x, y, width, height, visible, refresh, config from dbo.t_gauges where id=@id and user_id=@user_id";
            DataBase.Instance.ExecuteCommand(sql, CommandType.Text, parameters, dataTable);
            return dataTable;
        }

        public void DeleteUserGaugeLinks(Guid gaugeId)
        {
            string sql = string.Format("delete from dbo.t_gauges_reports where gauge_id='{0}'", gaugeId.ToString());
            DataBase.Instance.ExecuteScalar(sql, null);
        }

        public void DeleteUserGaugeConfig(
           Guid gaugeId,
           decimal userId)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@id", gaugeId);
            parameters[1] = new SqlParameter("@user_id", userId);

            string sql ="delete from dbo.t_gauges where id=@id and user_id=@user_id";
            int count = DataBase.Instance.ExecuteCommand(sql, CommandType.Text, parameters, null);

            DeleteUserGaugeLinks(gaugeId);
        }

        public void SaveUserGaugeConfig(
            Guid gaugeId,
            decimal userId,
            string name, 
            string type, 
            int x, 
            int y, 
            int width, 
            int height, 
            int visible,
            int refresh,
            string config)
        {            
            SqlParameter[] parameters = new SqlParameter[11];
            parameters[0] = new SqlParameter("@id", gaugeId);
            parameters[1] = new SqlParameter("@user_id", userId);
            parameters[2] = new SqlParameter("@name", (name==null ? DBNull.Value : (object)name));
            parameters[3] = new SqlParameter("@type", (type == null ? DBNull.Value : (object)type));
            parameters[4] = new SqlParameter("@x", (x < 0 ? DBNull.Value : (object)x));
            parameters[5] = new SqlParameter("@y", (y < 0 ? DBNull.Value : (object)y));
            parameters[6] = new SqlParameter("@width", (width < 0 ? DBNull.Value : (object)width));
            parameters[7] = new SqlParameter("@height", (height < 0 ? DBNull.Value : (object)height));
            parameters[8] = new SqlParameter("@visible", (visible < 0 ? DBNull.Value : (object)(visible > 0)));
            parameters[9] = new SqlParameter("@refresh", (refresh < 0 ? DBNull.Value : (object)refresh));
            parameters[10] = new SqlParameter("@config", (config == null ? DBNull.Value : (object)config));

            string sql =
@"update dbo.t_gauges set 
user_id=@user_id,
name=(case when @name is null then name else @name end),
type=(case when @type is null then type else @type end),
x=(case when @x is null then x else @x end),
y=(case when @y is null then y else @y end),
width=(case when @width is null then width else @width end),
height=(case when @height is null then height else @height end),
visible=(case when @visible is null then visible else @visible end),
refresh=(case when @refresh is null then refresh else @refresh end),
config=(case when @config is null then config else @config end) 
where id=@id";
            int count=DataBase.Instance.ExecuteCommand(sql, CommandType.Text, parameters, null);

            if (count <= 0)
            {

                sql = @"insert into dbo.t_gauges (id, user_id, name, type, x, y, width, height, visible, refresh, config)
                        values(@id, @user_id, @name, @type, @x, @y, @width, @height, @visible, @refresh, @config)";
                DataBase.Instance.ExecuteCommand(sql, CommandType.Text, parameters, null);
            }

            // t_gauges_reports links
            try
            {
                if (config != null)
                {
                    config = "<root>" + config + "</root>"; // shit i'm so lazy
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(config);

                    // delete
                    DeleteUserGaugeLinks(gaugeId);

                    // insert
                    foreach(XmlElement el in doc.SelectNodes("//QUERY"))
                    {
                        string rptType = el.GetAttribute("DATASOURCE");
                        string rptId = el.GetAttribute("DATASOURCEID");
                        if (string.Compare(rptType, "OLAP", true)==0)
                        {
                            sql=string.Format(
                                @"insert into dbo.t_gauges_reports(gauge_id, rpt_id, rpt_type) 
                                    select '{0}', {1}, 0
                                    where not exists(select top 1 1 from dbo.t_gauges_reports where gauge_id='{0}' and rpt_id={1} and rpt_type=0)",
                                    gaugeId.ToString(),
                                    rptId);
                            DataBase.Instance.ExecuteScalar(sql, null);
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }



    }
}
