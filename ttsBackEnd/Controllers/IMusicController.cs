using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using test.Models;

namespace test.Controllers
{
    public interface IMusicController
    {
        Task<IActionResult> connectionTest();
        Task<IActionResult> GetSongs(string name);

    }
}