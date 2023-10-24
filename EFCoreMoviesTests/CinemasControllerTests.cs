using EFCoreMovies.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreMoviesTests
{
     [TestClass]
    public class CinemasControllerTests
    {
        [TestMethod]
        public async Task Get_Cinemas2KilometersOrCloser()
        {
            var latitude = 18.482009;
            var longitude = -69.939464;

            using (var context = LocalDbInitializer.GetDbContextLocalDb(false))
            {
                var controller = new CinemasController(context, mapper: null);
                var response = await controller.Get(latitude, longitude);
                var objectResult = response as ObjectResult;
                var cinemas = (IEnumerable<object>)objectResult.Value;
                Assert.AreEqual(2, cinemas.Count());
            }
        }
    }
}