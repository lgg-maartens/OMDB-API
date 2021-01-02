using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieDB_example.Models;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.Json;

namespace MovieDB_example.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
      _logger = logger;
    }

    public IActionResult Index()
    {
      // https://www.omdbapi.com/
      string apiKey = "";
      string url = "http://www.omdbapi.com/?i=tt3896198&apikey=" + apiKey;
      
      // Created movie for our model object
      Movie m;

      // Create a request for the URL. 		
      WebRequest request = WebRequest.Create(url);

      // Get the response.
      HttpWebResponse response = (HttpWebResponse)request.GetResponse();

      // Get the stream containing content returned by the server.
      using (Stream dataStream = response.GetResponseStream())
      {
        // Open the stream using a StreamReader for easy access.
        using (StreamReader reader = new StreamReader(dataStream))
        {
          // Read the content.
          string responseFromServer = reader.ReadToEnd();

          // deserialize json response to movie object
          m = JsonSerializer.Deserialize<Movie>(responseFromServer);
        }
      }

      return View(m);
    }

    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
