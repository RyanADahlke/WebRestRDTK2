using Microsoft.AspNetCore.Mvc;
using WebRestEF.EF.Models;

namespace WebRest.Interfaces
{
    public interface iController<T1, T2>
    {
        Task<IActionResult> Delete(string id);
        Task<ActionResult<T1>> Get(string id);
        Task<ActionResult<IEnumerable<T1>>> Get();
        Task<ActionResult<T1>> Post(T2 _item);
        Task<IActionResult> Put(string id, T2 _item);
    }
}