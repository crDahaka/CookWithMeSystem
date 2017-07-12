namespace CookWithMe.API.Controllers
{
    using CookWithMe.API.Models;
    using CookWithMeSystem.Common.Constants;
    using CookWithMeSystem.Data;
    using CookWithMeSystem.Models;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Web;
    using System.Web.Hosting;
    using System.Web.Http;

    [RoutePrefix("api/images")]
    public class PictureController : BaseController
    {
        public PictureController(ICookWithMeSystemData data)
            :base(data)
        {
        }
        
        [Route("upload/{recipeID:int}")]
        public HttpResponseMessage UploadImageToRecipe(int recipeID)
        {
            var dbRecipe = this.Data.Recipes.GetById(recipeID);

            if (dbRecipe == null)
            {
                return this.CreateSerializedJsonResponse(HttpStatusCode.NotFound, new ErrorViewModel { Message = GlobalConstants.RecipeNotFoundErrorMessage });
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
                    
                    var image = new Picture
                    {
                        Path = GlobalConstants.DefaultUploadPath + uploadedImage.FileName,
                        FileExtension = uploadedImage.FileName.Split(new [] { '.' }).Last(),
                        RecipeID = dbRecipe.ID
                    };

                    dbRecipe.Picture = image;
                    this.Data.Pictures.Add(image);
                    this.Data.Pictures.SaveChanges();
                    
                }
                return this.CreateSerializedJsonResponse(HttpStatusCode.OK, GlobalConstants.SuccessCreateMessage);
            }
            else
            {
                return this.CreateSerializedJsonResponse(HttpStatusCode.NotAcceptable, GlobalConstants.InvalidRequestFormat);
            }
            
        }

        [Route("{id:int}")]
        public HttpResponseMessage Get(int id)
        {
            var dbPicture = this.Data.Pictures.GetById(id);

            if (dbPicture == null)
            {
                return this.CreateSerializedJsonResponse(HttpStatusCode.NotFound, new ErrorViewModel { Message = GlobalConstants.PictureNotFoundErrorMessage });
            }

            var result = new HttpResponseMessage();
            string filePath = HostingEnvironment.MapPath(dbPicture.Path);

            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                var image = Image.FromStream(fileStream);
                MemoryStream memoryStream = new MemoryStream();

                image.Save(memoryStream, ImageFormat.Jpeg);
                result.Content = new ByteArrayContent(memoryStream.ToArray());
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            }
            return result;
        }
    }
}
