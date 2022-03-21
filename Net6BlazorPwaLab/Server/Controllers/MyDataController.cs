using Microsoft.AspNetCore.Mvc;
using Net6BlazorPwaLab.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Net6BlazorPwaLab.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MyDataController : ControllerBase
{
  [HttpPost("[action]")]
  public List<MyFormData> QryDataList()
  {
    return simsMyDataTable;
  }

  [HttpPost]
  public string GetFormData([FromBody] string value)
  {
    return "value";
  }

  [HttpPost]
  public void AddFormData([FromBody] string value)
  {
  }

  [HttpPost]
  public void UpdFormData([FromBody] string value)
  {
  }

  [HttpPost]
  public void DelFormData([FromBody] string value)
  {
  }

  #region Sims DB Access

  static List<MyFormData> simsMyDataTable = new List<MyFormData>() {
    new(){ Name="AAA", Gender = "M", Nid = "N0001" },
    new(){ Name="BBB", Gender = "F", Nid = "N0002" },
    new(){ Name="CCC", Gender = "F", Nid = "N0003" },
  };

  #endregion
}
