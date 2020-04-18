using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

/// <summary>
/// Summary description for Car
/// </summary>
/// 
[DataContract]
public class Car
{
    [DataMember]
   public int Id { get; set; }

    [DataMember]
    public string Car_Type { get; set; }

    [DataMember]
    public string Car_Name { get; set; }

    [DataMember]
    public string Driver { get; set; }

    [DataMember]
    public string Car_Number { get; set; } 
}