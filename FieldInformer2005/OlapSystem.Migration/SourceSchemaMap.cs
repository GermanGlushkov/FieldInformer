using System;
using Microsoft.AnalysisServices.AdomdClient;

namespace OlapSystem.Migration {


    public partial class SourceSchemaMap
    {
        public static string GetStringFromSchemaObjectType(SchemaObjectType type)
        {
            if (type == SchemaObjectType.ObjectTypeDimension)
                return "Dimension";
            else if (type == SchemaObjectType.ObjectTypeHierarchy)
                return "Hierarchy";
            else if (type == SchemaObjectType.ObjectTypeLevel)
                return "Level";
            else if (type == SchemaObjectType.ObjectTypeMeasure)
                return "Measure";
            else if (type == SchemaObjectType.ObjectTypeMember)
                return "Member";

            throw new Exception("Invalid type: " + type.ToString());
        }

        public static SchemaObjectType GetSchemaObjectTypeFromString(string type)
        {
            if (type == "Dimension")
                return SchemaObjectType.ObjectTypeDimension;
            else if (type == "Hierarchy")
                return SchemaObjectType.ObjectTypeHierarchy;
            else if (type == "Level")
                return SchemaObjectType.ObjectTypeLevel;
            else if (type == "Measure")
                return SchemaObjectType.ObjectTypeMeasure;
            else if (type == "Member")
                return SchemaObjectType.ObjectTypeMember;

            throw new Exception("Invalid type: " + type);
        }

        public SourceSchemaMap.MapRow GetMapRow(SchemaObjectType type, string uniqueName)
        {            
            return GetMapRow(GetStringFromSchemaObjectType(type), uniqueName);
        }

        public SourceSchemaMap.MapRow GetMapRow(string type, string uniqueName)
        {
            string lookupUN = type + ":" + uniqueName;
            return this.Map.FindBySourceUniqueName(lookupUN);
        }

        public string ConvertSchemaObjectUN(SchemaObjectType type, string uniqueName)
        {
            SourceSchemaMap.MapRow row=null;
            string toReplace = uniqueName;

            // if its' member, extract parent object
            if (type == SchemaObjectType.ObjectTypeMember)
            {
                // lookup by key
                int delimiterIndex = uniqueName.IndexOf("].&[");
                if (delimiterIndex >= 0)
                {
                    toReplace = uniqueName.Substring(0, delimiterIndex + 1);

                    // try to lookup as level
                    row = this.GetMapRow(SchemaObjectType.ObjectTypeLevel, toReplace);

                    // if not found, try as hierarchy
                    if (row == null)
                        row = this.GetMapRow(SchemaObjectType.ObjectTypeHierarchy, toReplace);
                }
                else
                {
                    // if measure
                    if(uniqueName.StartsWith("[Measures]"))
                        row = this.GetMapRow(SchemaObjectType.ObjectTypeMeasure, toReplace);
                    else // lookup as member (all member)                                            
                        row = this.GetMapRow(SchemaObjectType.ObjectTypeMember, toReplace);

                    //if (row == null)
                    //    throw new Exception("Unable to find parent object for member " + uniqueName);
                }


            }
            else // straight full-name lookup
                row = this.GetMapRow(type, toReplace);

            // return 
            if (row == null || row.IsDestUniqueNameNull() || row.DestUniqueName == "")
                return null;                            
            else
            {
                string replaceWith = row.DestUniqueName.Substring(row.DestUniqueName.IndexOf(':', 0) + 1);
                return uniqueName.Replace(toReplace, replaceWith);
            }
        }

        public static object LookupAdomdSchemaObject(CubeDef cube, SourceSchemaMap.MapRow row)
        {
            string typeStr = row.SourceUniqueName.Substring(0, row.SourceUniqueName.IndexOf(':', 0));
            string uniqueName = row.SourceUniqueName.Substring(row.SourceUniqueName.IndexOf(':', 0) + 1);

            return LookupAdomdSchemaObject(cube, typeStr, uniqueName);
        }

        public static object LookupAdomdSchemaObject(CubeDef cube, string objectType, string uniqueName)
        {
            SchemaObjectType type = GetSchemaObjectTypeFromString(objectType);
            return LookupAdomdSchemaObject(cube, type, uniqueName);
        }

        public static object LookupAdomdSchemaObject(CubeDef cube, SchemaObjectType type, string uniqueName)
        {                      
            try
            {
                return cube.GetSchemaObject(type, uniqueName, true);
            }
            catch(Exception exc)
            {
                // object not found from old analysis services connection
                if(exc.Message.StartsWith("Invalid syntax for schema"))
                    return null;

                // object not found from new analysis services connection
                if (exc.Message.Contains("object was not found"))
                    return null;
                

                throw exc;
            }
        }

    }
}
