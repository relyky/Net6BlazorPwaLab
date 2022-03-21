using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net6BlazorPwaLab.DTO;

public class MyFormData
{
  [DisplayName("姓名")]
  public string Name { get; set; } = string.Empty;

  [DisplayName("姓別")]
  public string Gender { get; set; } = string.Empty;

  [DisplayName("身份證號")]
  public string Nid { get; set; } = string.Empty;
}

public class MyFormDataQryArgs { }

