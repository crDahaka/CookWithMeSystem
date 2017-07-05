namespace CookWithMe.API.Controllers
{
    using CookWithMeSystem.Common.Constants;
    using CookWithMeSystem.Data;
    using CookWithMeSystem.Models;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web;
    using System.Web.Hosting;
    using System.Web.Http;

    [RoutePrefix("api/images")]
    public class ImageController : ApiController
    {
        private IRepository<Recipe> recipes;
        private IRepository<Image> images;

        public ImageController(IRepository<Recipe> recipes, IRepository<Image> images)
        {
            this.recipes = recipes;
            this.images = images;
        }

        [Route("upload/{recipeID:int}")]
        public HttpResponseMessage UploadImageToRecipe(int recipeID)
        {
            var dbRecipe = this.recipes.GetById(recipeID);

            if (dbRecipe == null)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, ValidationConstants.RecipeNotFoundErrorMessage));
            }

            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var uploadedImage = HttpContext.Current.Request.Files[GlobalConstants.UploadImageKey];
                if (uploadedImage != null && uploadedImage.ContentLength > 0)
                {
                    try
                    {
                        uploadedImage.SaveAs(HostingEnvironment.MapPath(GlobalConstants.DefaultUploadPath) + uploadedImage.FileName);
                    }
                    catch (DirectoryNotFoundException dirEx)
                    {

                        throw dirEx;
                    }
                    
                    var image = new Image
                    {
                        Path = GlobalConstants.DefaultUploadPath + uploadedImage.FileName,
                        RecipeID = dbRecipe.ID
                    };

                    dbRecipe.Image = image;
                    this.images.Add(image);
                    this.images.SaveChanges();
                    
                }
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted."));
            }
            
        }
    }
}
